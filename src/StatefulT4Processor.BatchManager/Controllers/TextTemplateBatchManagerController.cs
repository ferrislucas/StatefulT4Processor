using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StatefulT4Processor.Shared;
using StatefulT4Processor.TextTemplateBatchManager.Models;
using StatefulT4Processor.TextTemplateBatchManager.ViewModelBuilders;

namespace StatefulT4Processor.TextTemplateBatchManager.Controllers
{
    public class TextTemplateBatchManagerController : Controller
    {
    	private readonly IIndexViewModelBuilder indexViewModelBuilder;
    	private readonly IGuidGetter guidGetter;

    	public TextTemplateBatchManagerController(IIndexViewModelBuilder indexViewModelBuilder, IGuidGetter guidGetter)
    	{
    		this.guidGetter = guidGetter;
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
			                      		ModifyInputModel = new ModifyInputModel()
			                      		                   	{
																Id = guidGetter.GetGuid().ToString()
			                      		                   	}
			                      	});
		}
    }
}
