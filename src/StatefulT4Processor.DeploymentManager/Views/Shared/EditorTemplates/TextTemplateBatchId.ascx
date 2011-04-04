<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<string>" %>
<%@ Import Namespace="MvcTurbine.ComponentModel" %>
<%@ Import Namespace="StatefulT4Processor.TextTemplateBatchManager.Shared" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Web.Mvc" %>

<%= Html.DropDownListFor(x => x, 
	(new SelectListItem[] {
			new SelectListItem()
                    {
                        Value = string.Empty,
						Text = "- please select -",
					},
	}).Union(
				ServiceLocatorManager.Current.Resolve<ITextTemplateBatchContext>().GetAll()
				.Select(a => new SelectListItem()
				             	{
				             		Selected = (Model == a.Id), Text = a.Name, Value = a.Id
				             	})
	)) %>
