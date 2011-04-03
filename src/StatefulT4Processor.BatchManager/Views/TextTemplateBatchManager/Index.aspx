<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.TextTemplateBatchManager.Models.IndexViewModel>" %>
<%@ Import Namespace="MvcContrib.UI.Grid" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript" charset="utf-8">
	$(document).ready(function () {
		$('#gridView').dataTable();
	});
</script>

<style>
#gridView { width: 100%; clear:both; margin-top:10px; }
#gridView th { text-align: left; }
#gridView_filter { float:left; clear:none; }
#gridView_length { clear:none; float:right; }
#container { width: 600px; }
</style>


<div id="gridContainer" style="padding-top:40px;">

	<% if (Model.TextTemplateBatches.Count() > 0) { %>

		<% Html.Grid(Model.TextTemplateBatches)
		   .Columns(column =>
		   {
			   column.For(item => Html.ActionLink(item.Name, "Modify", new { id = item.Id }, null)).Named("Name");
			   column.For(item => item.LastModifyDate).Named("Last Modify Date");
			   //column.For(item => Html.ActionLink("Execute", "Execute", new { id = item.Id }, null)).Named("Execute");
		   }).Attributes(id => "gridView").Render();
		%>

	<% } %>

	<% if (Model.TextTemplateBatches.Count() == 0) { %>
	No items found.
	<% } %>

	<p style="padding-top:20px;">
		<input type="button" onclick="window.location='<%=Url.Action("Create") %>';" value="Create" class="button">
	</p>

</div>
</asp:Content>