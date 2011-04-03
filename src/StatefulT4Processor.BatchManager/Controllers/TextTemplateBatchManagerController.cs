using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StatefulT4Processor.TextTemplateBatchManager.ViewModelBuilders;

namespace StatefulT4Processor.TextTemplateBatchManager.Controllers
{
    public class TextTemplateBatchManagerController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;

    	public TextTemplateBatchManagerController(IIndexViewModelBuilder indexViewModelBuilder)
    	{
    		this.indexViewModelBuilder = indexViewModelBuilder;
    	}

    	public ActionResult Index()
        {
            return View("Index", indexViewModelBuilder.BuildViewModel());
        }
    }
}
