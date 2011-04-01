using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using EasyObjectStore.Helpers;
using StatefulT4Processor.Webroot.Mappers;
using StatefulT4Processor.Webroot.Models;
using StatefulT4Processor.Webroot.Repositories;
using StatefulT4Processor.Webroot.Services;
using StatefulT4Processor.Webroot.ViewModelBuilders;
using IGuidGetter = StatefulT4Processor.Shared.IGuidGetter;

namespace StatefulT4Processor.Webroot.Controllers
{
    public class T4StateManagementController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly IProcessInputModelService processInputModelService;
    	private readonly IStateRepository stateRepository;
    	private readonly IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper;
    	private readonly IGuidGetter guidGetter;
    	private readonly IXmlFileSerializationHelper xmlFileSerializationHelper;

    	public T4StateManagementController(IIndexViewModelBuilder indexViewModelBuilder,
								IModifyViewModelBuilder modifyViewModelBuilder,
								IProcessInputModelService processInputModelService,
								IStateRepository stateRepository,
								IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper,
								IGuidGetter guidGetter,
								IXmlFileSerializationHelper xmlFileSerializationHelper)
    	{
    		this.xmlFileSerializationHelper = xmlFileSerializationHelper;
    		this.guidGetter = guidGetter;
    		this.instanceToWidgetInputModelMapper = instanceToWidgetInputModelMapper;
    		this.stateRepository = stateRepository;
    		this.processInputModelService = processInputModelService;
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    		this.indexViewModelBuilder = indexViewModelBuilder;
    	}

    	public ActionResult Index()
        {
			return View("Index", indexViewModelBuilder.BuildViewModel());
        }

		public ActionResult Execute(string id)
		{
			var state = stateRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();
			var pathToState = @"C:\_Application\StatefulT4Processor\localWorkingFolder\T4ProcessState.xml";

			xmlFileSerializationHelper.SerializeToPath(state, pathToState);
			var outputs = new List<string>();
			var p = new Process
			        	{
			        		StartInfo =
			        			{
			        				FileName =
			        					@"C:\_Application\StatefulT4Processor\src\ConsoleApplication1\bin\Debug\ConsoleApplication1.exe",
			        				Arguments = pathToState,
			        				UseShellExecute = false,
			        				RedirectStandardOutput = true
			        			}
			        	};
			p.Start();
			outputs.Add(p.StandardOutput.ReadToEnd().Replace("\n", "<br />"));

			if (state.PushToRepository)
			{
				var p2 = new Process
				{
					StartInfo =
					{
						FileName =
							@"""C:\Program Files (x86)\Git\bin\sh.exe"" ""C:\_Application\StatefulT4Processor\git_push.sh""",
						Arguments = state.BranchName + " " + state.RemoteName,
						UseShellExecute = false,
						RedirectStandardOutput = true
					}
				};
				p2.Start();
				outputs.Add(p.StandardOutput.ReadToEnd().Replace("\n", "<br />"));
			}


			return View("Execute", new ExecuteViewModel()
									{
										Errors = outputs,
									});
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
			var user = stateRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

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
				routeValues.Add("Controller", this.GetType().Name.Replace("Controller", string.Empty));
				routeValues.Add("Action", "Modify");
				routeValues.Add("id", inputModel.Id);

				return new RedirectToRouteResult(routeValues);
			}

			return View("Modify", modifyViewModelBuilder.BuildViewModel(inputModel));
		}

		public ActionResult Delete(string id)
		{
			var routeValues = new RouteValueDictionary();
			routeValues.Add("Controller", "T4StateManagement");
			routeValues.Add("Action", "Index");
			
			stateRepository.Delete(id);

			return new RedirectToRouteResult(routeValues);
		}
    }
}
