<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BeeperWebApp._Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvwDirectory" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField HeaderText="Server Path" DataField="DirectoryName" />
            <asp:BoundField HeaderText="File Name" DataField="Name" />
            <asp:HyperLinkField HeaderText="Link" DataNavigateUrlFields="Name" DataTextField="Name" />
        </Columns>
    </asp:GridView>
</asp:Content>

        