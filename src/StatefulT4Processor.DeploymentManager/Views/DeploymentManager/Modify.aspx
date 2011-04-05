<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.DeploymentManager.Models.ModifyViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<style>
	#InputModel_StateXml 
	{
		width:100%;
		height:300px;
	}
</style>

<div style="padding-top:50px;">
    
	<% using (Html.BeginForm()) {%>

        <%= Html.ValidationSummary(true) %>
        
			<%=Html.EditorFor(a => a.InputModel) %>
            
            <p>
                <input type="submit" value="Save" class="button" />
				<% if (ViewContext.RouteData.Values["Action"].ToString() == "Modify") { %>
				<input type="button" class="button important" value="Delete" onclick="if (confirm('Are you sure you want to delete this item?')) { window.location='<%=Url.Action("Delete", ViewContext.RouteData.Values["Controller"].ToString(), new { id = Model.InputModel.Id }) %>'; }" />
				<% } %>
            </p>
        
    <% } %>

</div>

<div style="padding-top:20px;">
	Example State:<br />
	<textarea style="width:100%;height:200px;">
	<%: @"<?xml version=""1.0""?>
<ArrayOfArrayOfString xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
  <ArrayOfString>
    <string>KEY</string>
    <string>VALUE</string>
  </ArrayOfString>
</ArrayOfArrayOfString>" %>
	</textarea>
</div>

</asp:Content>

