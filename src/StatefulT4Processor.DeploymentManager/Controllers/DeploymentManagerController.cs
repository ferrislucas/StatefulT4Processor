using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using StatefulT4Processor.DeploymentManager.Helpers;
using StatefulT4Processor.DeploymentManager.Mappers;
using StatefulT4Processor.DeploymentManager.Models;
using StatefulT4Processor.DeploymentManager.Repositories;
using StatefulT4Processor.DeploymentManager.Services;
using StatefulT4Processor.DeploymentManager.ViewModelBuilders;

namespace StatefulT4Processor.DeploymentManager.Controllers
{
    public class DeploymentManagerController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IModifyViewModelBuilder modifyViewModelBuilder;
    	private readonly IProcessInputModelService processInputModelService;
    	private readonly IWidgetRepository widgetRepository;
    	private readonly IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper;
    	private readonly IGuidGetter guidGetter;

    	public DeploymentManagerController(IIndexViewModelBuilder indexViewModelBuilder,
								IModifyViewModelBuilder modifyViewModelBuilder,
								IProcessInputModelService processInputModelService,
								IWidgetRepository widgetRepository,
								IInstanceToWidgetInputModelMapper instanceToWidgetInputModelMapper,
								IGuidGetter guidGetter)
    	{
    		this.guidGetter = guidGetter;
    		this.instanceToWidgetInputModelMapper = instanceToWidgetInputModelMapper;
    		this.widgetRepository = widgetRepository;
    		this.processInputModelService = processInputModelService;
    		this.modifyViewModelBuilder = modifyViewModelBuilder;
    		this.indexViewModelBuilder = indexViewModelBuilder;
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
		public ActionResult Create(InputModel inputModel)
		{
			return Modify(inputModel);
		}

		public ActionResult Modify(string id)
		{
			var user = widgetRepository.GetAll().Where(a => a.Id == id).FirstOrDefault();

			return View("Modify", modifyViewModelBuilder.BuildViewModel(instanceToWidgetInputModelMapper.CreateInstance(user)));
		}

		[HttpPost]
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
			
			widgetRepository.Delete(id);

			return new RedirectToRouteResult(routeValues);
		}

		private string GetTheNameOfThisController()
		{
			return GetType().Name.Replace("Controller", string.Empty);
		}
    }
}
