<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNumbersTest.aspx.cs" Inherits="TestWebApp.AddNumbersTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        X: <asp:TextBox ID="txtX" runat="server" Text="1" /> + Y: <asp:TextBox ID="txtY" runat="server" Text="2" /> = Z: <asp:Label ID="lblZ" runat="server" Text="0" />
        <br />        
        <asp:Button ID="btnSubmit" runat="server" Text="Do It!" 
            onclick="btnSubmit_Click"  />
    </div>
    </form>
</body>
</html>
