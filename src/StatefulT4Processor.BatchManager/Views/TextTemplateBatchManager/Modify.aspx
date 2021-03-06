﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.TextTemplateBatchManager.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>
	#InputModel_InstructionXml
	{
		width:100%;
		height:300px;
		font-size:12px;
	}
</style>

<div style="padding-top:50px;">
	<form Id="form" action="/<%=RouteData.Values["Controller"].ToString() %>/<%=RouteData.Values["Action"].ToString() %>" method="post" enctype="multipart/form-data">
	<%--<% using (Html.BeginForm(RouteData.Values["Action"].ToString(), RouteData.Values["Controller"].ToString(), FormMethod.Post, new { enctype = "multipart/form-data " })) { %>--%>

			<%=Html.EditorFor(a => a.ModifyInputModel) %>
            
            <p>
                <input type="submit" value="Save" class="button" />
				<% if (ViewContext.RouteData.Values["Action"].ToString() == "Modify") { %>
				<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "TextTemplateBatchManager", new { id = Model.ModifyInputModel.Id }) %>'; }" />
				&nbsp;
				&nbsp;
				<%--<input type="button" class="button important" value="Execute" onclick="if (confirm('Are you sure?')) { window.location='<%=Url.Action("Execute", "TextTemplateBatchManager", new { id = Model.ModifyInputModel.Id }) %>'; }" />--%>
				<% } %>
            </p>
    </form>
    <%--<% } %>--%>

</div>
</asp:Content>