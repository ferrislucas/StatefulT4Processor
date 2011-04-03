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
		string SaveAndReturnId(TextTemplateBatch textTemplateBatch);
		void Delete(string id);
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

		public string SaveAndReturnId(TextTemplateBatch textTemplateBatch)
		{
			return easyObjectStore.SaveAndReturnId(textTemplateBatch);
		}

		public void Delete(string id)
		{
			easyObjectStore.Delete(id);
		}
	}
}