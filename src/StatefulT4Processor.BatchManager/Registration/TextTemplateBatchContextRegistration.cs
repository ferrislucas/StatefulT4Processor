using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcTurbine.ComponentModel;
using StatefulT4Processor.TextTemplateBatchManager.Contexts;
using StatefulT4Processor.TextTemplateBatchManager.Shared;

namespace StatefulT4Processor.TextTemplateBatchManager.Registration
{
	public class TextTemplateBatchContextRegistration : IServiceRegistration
	{
		public void Register(IServiceLocator locator)
		{
			locator.Register<ITextTemplateBatchContext, TextTemplateBatchContext>();
		}
	}
}