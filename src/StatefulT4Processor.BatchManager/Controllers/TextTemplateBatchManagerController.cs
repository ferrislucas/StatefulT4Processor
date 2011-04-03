using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StatefulT4Processor.Shared;
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

    	public TextTemplateBatchManagerController(IIndexViewModelBuilder indexViewModelBuilder, 
													IGuidGetter guidGetter,
													IModifyViewModelBuilder modifyViewModelBuilder,
													ITextTemplateBatchToTextTemplateBatchModifyInputModelMapper textTemplateBatchToTextTemplateBatchModifyInputModelMapper,
													ITextTemplateBatchRepository textTemplateBatchRepository,
													IProcessTextTemplateBatchModifyInputModelService processTextTemplateBatchModifyInputModelService)
    	{
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
		public ActionResult Modify(TextTemplateModifyInputModel modifyInputModel)
		{
			if (ModelState.IsValid)
			{
				var batchId = processTextTemplateBatchModifyInputModelService.ProcessAndReturnId(modifyInputModel);
				return RedirectToAction("Modify", new { id = batchId });
			}
			return View("Modify", modifyViewModelBuilder.BuildViewModel(modifyInputModel));
		}
    }
}
