<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMP.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BeeperWebApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <br />&nbsp;<br />
    </div>
    <div style="border:solid 3px black; font-weight:bold; font-size:x-large; font-family:Courier New;">
        <table border="0" style="text-align:center; vertical-align:middle; padding:5% 5% 5% 35%;">
            <tr><td>Username:</td><td><asp:TextBox ID="txtUserName" runat="server" style="width:150px;" /></td></tr>
            <tr><td>Password:</td><td><asp:TextBox ID="txtPassWord" TextMode="Password" runat="server" style="width:150px;"/></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr><td colspan="2"><asp:Button ID="btnLogin" Text="Obnoxiously Large Login Button" OnClick="btnLogin_Click" CssClass="obnoxiousTestBtn" runat="server" /></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
        </table>
        <p>&nbsp;</p>
        <div id="divError" class="Error" visible="false" runat="server"><asp:Label ID="lblError" Visible="false" runat="server" /></div>
    </div>
</asp:Content>

