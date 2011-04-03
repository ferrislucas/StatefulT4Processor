using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EasyObjectStore;
using StatefulT4Processor.TextTemplateBatchManager.Models;

namespace StatefulT4Processor.TextTemplateBatchManager.Repositories
{
	public interface ITextTemplateBatchRepository
	{
		IEnumerable<TextTemplateBatch> GetAll();
	}

	public class TextTemplateBatchRepository : ITextTemplateBatchRepository
	{
		private readonly IEasyObjectStore<TextTemplateBatch> easyObjectStore;

		public TextTemplateBatchRepository(EasyObjectStore<TextTemplateBatch> easyObjectStore)
		{
			this.easyObjectStore = easyObjectStore;
		}

		public IEnumerable<TextTemplateBatch> GetAll()
		{
			return easyObjectStore.GetAll();
		}
	}
}