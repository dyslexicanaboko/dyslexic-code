<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jQueryTest.aspx.cs" Inherits="WebApplication1.jQueryTest" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" href="CSS/ui.all.css" rel="stylesheet" />
	<script type="text/javascript" src="Scripts/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="Scripts/ui.core.js"></script>
	<script type="text/javascript" src="Scripts/ui.progressbar.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready
        (
            function ()
            {
                $("#<%=txtEntity.ClientID%>").change
                (
                    function ()
                    {
                        $.ajax
                        ({
                            type: "POST",
                            url: "jQueryTest.aspx/GetBuildings",
                            data: "{}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            success: function (msg)
                            {
                                alert("Okay!");
                            },
                            error: function() 
                            {
                                alert("No good.");
                            }
                        });

                        return false;
                    }
                );
            }
        );
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ARSelector">
            <table border="0">
                <tr>
                    <td>Entity</td>
                    <td>
                        <asp:TextBox ID="txtEntity" runat="server"></asp:TextBox>
                    </td>
                    <td>Building</td>
                    <td>
                        <asp:DropDownList ID="ddlBuildings" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Unit</td>
                    <td>
                        <asp:DropDownList ID="ddlUnits" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>Resident</td>
                    <td>
                        <asp:DropDownList ID="ddlResidents" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
