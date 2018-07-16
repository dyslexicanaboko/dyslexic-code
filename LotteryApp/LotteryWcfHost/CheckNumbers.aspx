<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckNumbers.aspx.cs" Inherits="LotteryWcfHost.CheckNumbers" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Check Lottery Numbers</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblTimeStamp" runat="server" Text="" />
        <br />
        <br />
        <asp:TextBox ID="txtNumbers" runat="server" 
            TextMode="MultiLine" 
            Height="200px" 
            Width="150px" />
        <br />
        <br />
        <div>
            <flc:NumberPairGridControl ID="ctrlNumbers" runat="server" />
        </div>
        <asp:Button ID="btnCheckNumbers" runat="server" Text="Did I Win!?" onclick="btnCheckNumbers_Click" />
        <asp:CheckBox ID="chkBxSaveNumbers" runat="server" /> Save Numbers
    </form>
</body>
</html>
