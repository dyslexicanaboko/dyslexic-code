<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NumberPair.ascx.cs" Inherits="LotteryWcfHost.Controls.NumberPair" %>
<table id="tblNumbers" runat="server" style="border:1px solid black;">
    <tr>
        <td><asp:TextBox ID="txtLotto1" Width="20px" ReadOnly="true" runat="server" /></td>
        <td><asp:TextBox ID="txtLotto2" Width="20px" ReadOnly="true" runat="server" /></td>
        <td><asp:TextBox ID="txtLotto3" Width="20px" ReadOnly="true" runat="server" /></td>
        <td><asp:TextBox ID="txtLotto4" Width="20px" ReadOnly="true" runat="server" /></td>
        <td><asp:TextBox ID="txtLotto5" Width="20px" ReadOnly="true" runat="server" /></td>
        <td><asp:TextBox ID="txtLotto6" Width="20px" ReadOnly="true" runat="server" /></td>
    </tr>
    <tr>
        <td><asp:TextBox ID="txtPlayer1" Width="20px" runat="server" /></td>
        <td><asp:TextBox ID="txtPlayer2" Width="20px" runat="server" /></td>
        <td><asp:TextBox ID="txtPlayer3" Width="20px" runat="server" /></td>
        <td><asp:TextBox ID="txtPlayer4" Width="20px" runat="server" /></td>
        <td><asp:TextBox ID="txtPlayer5" Width="20px" runat="server" /></td>
        <td><asp:TextBox ID="txtPlayer6" Width="20px" runat="server" /></td>
    </tr>
</table>