using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using StatefulT4Processor.DeploymentManager.Mappers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;
using StatefulT4Processor.DeploymentManager.Services;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;
using StatefulT4Processor.Shared;
using StatefulT4Processor.T4StateManager;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Shared;
using StatefulT4Processor.TextTemplateZipProcessor.StatefulT4Processor.BatchProcessor;
using IGuidGetter = StatefulT4Processor.DeploymentManager.Helpers.IGuidGetter;

namespace StatefulT4Processor.DeploymentManager.Controllers
{
    public class DeploymentManagerController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly IProcessInputModelService processInputModelService;
    	private readonly IDeploymentRepository deploymentRepository;
    	private readonly IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper;
    	private readonly IGuidGetter guidGetter;
    	private readonly ITextTemplateZipProcessor textTemplateZipProcessor;
    	private readonly ITextTemplateBatchContext textTemplateBatchContext;
    	private readonly IGetWorkingFolderPath getWorkingFolderPath;
    	private readonly IFileSystem fileSystem;
    	private readonly IT4StateContext t4StateContext;

    	public DeploymentManagerController(IIndexViewModelBuilder indexViewModelBuilder,
								IModifyViewModelBuilder modifyViewModelBuilder,
								IProcessInputModelService processInputModelService,
								IDeploymentRepository deploymentRepository,
								IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper,
								IGuidGetter guidGetter,
								ITextTemplateZipProcessor textTemplateZipProcessor,
								ITextTemplateBatchContext textTemplateBatchContext,
								IGetWorkingFolderPath getWorkingFolderPath,
								IFileSystem fileSystem,
								IT4StateContext t4StateContext)
    	{
    		this.t4StateContext = t4StateContext;
    		this.fileSystem = fileSystem;
    		this.getWorkingFolderPath = getWorkingFolderPath;
    		this.textTemplateBatchContext = textTemplateBatchContext;
    		this.textTemplateZipProcessor = textTemplateZipProcessor;
    		this.guidGetter = guidGetter;
    		this.instanceToWidgetInputModelMapper = instanceToWidgetInputModelMapper;
    		this.deploymentRepository = deploymentRepository;
    		this.processInputModelService = processInputModelService;
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    		this.indexViewModelBuilder = indexViewModelBuilder;
    	}

		public ActionResult Execute(string id)
		{
			var deployment = deploymentRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

			var batch = textTemplateBatchContext.GetAll().Where(a => a.Id == deployment.TextTemplateBatchId).FirstOrDefault();

			t4StateContext.SetState(deployment.StateXml);

			var zipFilePath = getWorkingFolderPath.GetPathToWorkingFolder() + "TextTemplateBatchFileUploads" + Path.DirectorySeparatorChar + batch.Id + Path.DirectorySeparatorChar + batch.ZipFilename;
			var outputPath = getWorkingFolderPath.GetPathToWorkingFolder() + "T4Output" + Path.DirectorySeparatorChar + deployment.Id + Path.DirectorySeparatorChar;

			if (fileSystem.DirectoryExists(outputPath))
				fileSystem.DeleteDirectory(outputPath);

			var errors = textTemplateZipProcessor.ProcessZip(zipFilePath, outputPath);
			//if (errors.Count() == 0)
			//{
			//    return File(outputPath, "application/zip", "pack.zip");
			//}

			return View("Execute", new ExecuteViewModel()
			                       	{
										OutputPath = outputPath,
			                       		Errors = errors,
			                       	});
		}

    	public ActionResult Index()
        {
			return View("Index", indexViewModelBuilder.BuildViewModel());
        }

		public ActionResult Create()
		{
			return View("Modify", new ModifyViewModel()
										{
											InputModel = new InputModel()
											{
												Id = guidGetter.GetGuid().ToString()
											}
										});
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(InputModel inputModel)
		{
			return Modify(inputModel);
		}

		public ActionResult Modify(string id)
		{
			var user = deploymentRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

			return View("Modify", modifyViewModelBuilder.BuildViewModel(instanceToWidgetInputModelMapper.CreateInstance(user)));
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Modify(InputModel inputModel)
		{
			if (ModelState.IsValid)
			{
				processInputModelService.ProcessAndReturnId(inputModel);
				var routeValues = new RouteValueDictionary();
				routeValues.Add("Controller", GetTheNameOfThisController());
				routeValues.Add("Action", "Modify");
				routeValues.Add("id", inputModel.Id);

				return new RedirectToRouteResult(routeValues);
			}

			return View("Modify", modifyViewModelBuilder.BuildViewModel(inputModel));
		}

    	public ActionResult Delete(string id)
		{
			var routeValues = new RouteValueDictionary();
			routeValues.Add("Controller", GetTheNameOfThisController());
			routeValues.Add("Action", "Index");
			
			deploymentRepository.Delete(id);

			return new RedirectToRouteResult(routeValues);
		}

		private string GetTheNameOfThisController()
		{
			return GetType().Name.Replace("Controller", string.Empty);
		}
    }
}
