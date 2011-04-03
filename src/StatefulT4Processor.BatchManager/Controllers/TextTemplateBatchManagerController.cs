using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Helpers;
using StatefulT4Processor.TextTemplateBatchManager.Mappers;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.Repositories;
using StatefulT4Processor.TextTemplateBatchManager.Services;
using StatefulT4Processor.TextTemplateBatchManager.ViewModelBuilders;

namespace StatefulT4Processor.TextTemplateBatchManager.Controllers
{
    public class TextTemplateBatchManagerController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IGuidGetter guidGetter;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly ITextTemplateBatchToTextTemplateBatchModifyInputModelMapper textTemplateBatchToTextTemplateBatchModifyInputModelMapper;
    	private readonly ITextTemplateBatchRepository textTemplateBatchRepository;
    	private readonly IProcessTextTemplateBatchModifyInputModelService processTextTemplateBatchModifyInputModelService;
    	private readonly IGetWorkingFolderPath getWorkingFolderPath;
    	private readonly IFileSystem fileSystem;

    	public TextTemplateBatchManagerController(IIndexViewModelBuilder indexViewModelBuilder, 
													IGuidGetter guidGetter,
													IModifyViewModelBuilder modifyViewModelBuilder,
													ITextTemplateBatchToTextTemplateBatchModifyInputModelMapper textTemplateBatchToTextTemplateBatchModifyInputModelMapper,
													ITextTemplateBatchRepository textTemplateBatchRepository,
													IProcessTextTemplateBatchModifyInputModelService processTextTemplateBatchModifyInputModelService,
													IGetWorkingFolderPath getWorkingFolderPath,
													IFileSystem fileSystem)
    	{
    		this.fileSystem = fileSystem;
    		this.getWorkingFolderPath = getWorkingFolderPath;
    		this.processTextTemplateBatchModifyInputModelService = processTextTemplateBatchModifyInputModelService;
    		this.textTemplateBatchRepository = textTemplateBatchRepository;
    		this.textTemplateBatchToTextTemplateBatchModifyInputModelMapper = textTemplateBatchToTextTemplateBatchModifyInputModelMapper;
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    		this.guidGetter = guidGetter;
    		this.indexViewModelBuilder = indexViewModelBuilder;
    	}

    	public ActionResult Index()
        {
            return View("Index", indexViewModelBuilder.BuildViewModel());
        }

		public ActionResult Delete(string id)
		{
			textTemplateBatchRepository.Delete(id);
			return RedirectToAction("Index");
		}

		public ActionResult Create()
		{
			return View("Modify", new ModifyViewModel()
			                      	{
			                      		ModifyInputModel = new TextTemplateModifyInputModel()
			                      		                   	{
																Id = guidGetter.GetGuid().ToString()
			                      		                   	}
			                      	});
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Create(TextTemplateModifyInputModel modifyInputModel)
		{
			if (ModelState.IsValid)
				return Modify(modifyInputModel);

			return View("Modify", modifyViewModelBuilder.BuildViewModel(modifyInputModel));
		}

		public ActionResult Modify(string id)
		{
			var batch = textTemplateBatchRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();
			if (batch == null) return RedirectToAction("Index");

			return View("Modify", modifyViewModelBuilder.BuildViewModel(textTemplateBatchToTextTemplateBatchModifyInputModelMapper.CreateInstance(batch)));
		}

		[HttpPost]
		[ValidateInput(false)]
		public ActionResult Modify(TextTemplateModifyInputModel modifyInputModel)
		{
			if (ModelState.IsValid)
			{
				var batchId = processTextTemplateBatchModifyInputModelService.ProcessAndReturnId(modifyInputModel);

				if ((HttpContext.Request.Files.AllKeys.Where(a => a == "ModifyInputModel_ZipFile").Any()) && (!string.IsNullOrEmpty(HttpContext.Request.Files["ModifyInputModel_ZipFile"].FileName)))
				{
					var path = getWorkingFolderPath.GetPathToWorkingFolder() +
					           TextTemplateBatchManagerSettings.TextTemplateBatchFileUploadFolderName + Path.DirectorySeparatorChar +
					           Path.DirectorySeparatorChar + batchId + Path.DirectorySeparatorChar;
					fileSystem.CreateFolder(path);
					HttpContext.Request.Files["ModifyInputModel_ZipFile"].SaveAs(path + HttpContext.Request.Files["ModifyInputModel_ZipFile"].FileName);
				}

				return RedirectToAction("Modify", new { id = batchId });
			}
			return View("Modify", modifyViewModelBuilder.BuildViewModel(modifyInputModel));
		}
    }
}
