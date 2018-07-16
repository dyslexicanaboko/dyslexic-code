<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcApplication1.Models.AddExample>" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	AddExample
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>AddExample</h2>
    <% using (Html.BeginForm("AddExample", "AddExample")) { %>
        <div>
            <fieldset>
                <div class="editor-label">
                    Enter Two Numbers
                </div>
                <div class="editor-label">
                    X: <%: Html.TextBoxFor(m => m.X) %>
                </div>
                <div class="editor-label">
                    Y: <%: Html.TextBoxFor(m => m.Y) %>
                </div>
                <div class="editor-label">
                    <%: Html.DisplayTextFor(m => m.Result) %>
                </div>
                <p>
                    <input type="submit" value="Add Numbers" />
                </p>
            </fieldset>
        </div>
    <% } %>
</asp:Content>
