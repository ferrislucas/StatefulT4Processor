<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.DeploymentManager.Models.ExecuteViewModel>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% foreach (var error in Model.Errors) { %>

		<p><%=error %></p>

	<% } %>

</asp:Content>
