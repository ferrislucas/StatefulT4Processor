<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<StatefulT4Processor.Webroot.Models.ExecuteViewModel>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<h1>Results:</h1>

<% foreach (var item in Model.Errors) { %>
	<%=item %><br /><br />
<% } %>

</asp:Content>