<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.Webroot.Models.ModifyViewModel>" %>

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
	<% using (Html.BeginForm()) { %>

        <%= Html.ValidationSummary(false) %>
        
			<%=Html.EditorFor(a => a.InputModel) %>
            
            <p>
                <input type="submit" value="Save" class="button" />
				<% if (ViewContext.RouteData.Values["Action"].ToString() == "Modify") { %>
				<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", "T4StateManagement", new { id = Model.InputModel.Id }) %>'; }" />
				&nbsp;
				&nbsp;
				<input type="button" class="button important" value="Execute" onclick="if (confirm('Are you sure?')) { window.location='<%=Url.Action("Execute", "T4StateManagement", new { id = Model.InputModel.Id }) %>'; }" />
				<% } %>
            </p>
        
    <% } %>

</div>
</asp:Content>