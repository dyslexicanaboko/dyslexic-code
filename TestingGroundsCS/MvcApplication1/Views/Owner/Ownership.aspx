<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<MvcApplication1.Models.Owner>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ownership
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Ownership</h2>

    <table>
        <tr>
            <th>
                ID
            </th>
            <th>
                Name
            </th>
            <th></th>
        </tr>

    <% foreach (var item in Model) { %>
    
        <tr>
            <td>
                <%: item.ID %>
            </td>
            <td>
                <%: item.Name %>
            </td>
            <td>
                <%: Html.ActionLink("Claim Owner", "Link", new { id = item.ID })%> |
                <%: Html.ActionLink("Edit", "Edit", new { id = item.ID }) %> |
                <%: Html.ActionLink("Details", "Details", new { id = item.ID })%> |
                <%: Html.ActionLink("Delete", "Delete", new { id = item.ID })%>
            </td>
        </tr>
    
    <% } %>

    </table>

    <p>
        <%: Html.ActionLink("Create New", "Create") %>
    </p>

</asp:Content>

