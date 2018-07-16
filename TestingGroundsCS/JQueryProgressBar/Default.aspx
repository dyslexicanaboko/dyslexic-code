<%@ Page Language="C#" AutoEventWireup="true" EnableSessionState="False" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>jQuery ProgressBar Example</title>    
    <link type="text/css" href="CSS/ui.all.css" rel="stylesheet" />
	<script type="text/javascript" src="Scripts/jquery-1.3.2.js"></script>
	<script type="text/javascript" src="Scripts/ui.core.js"></script>
	<script type="text/javascript" src="Scripts/ui.progressbar.js"></script>
    <script type="text/javascript" language="javascript">
        $(document).ready
        (
            function() 
            {
                $("#progressbar").progressbar({ value: 0 });
                
                $("#btnGetData").click
                (
                    function() 
                    {
                        var intervalID = setInterval(updateProgress, 250);

                        $.ajax
                        ({
                            type: "POST",
                            url: "Default.aspx/GetText",
                            data: "{}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            success: function(msg) 
                            {
                                $("#progressbar").progressbar("value", 100);
                                $("#result").text(msg.d);                        
                                clearInterval(intervalID);
                            }
                        });
                    
                        return false;
                    }
                );
            }
        );

        function updateProgress() 
        {            
            var value = $("#progressbar").progressbar("option", "value");
            
            if (value < 100)
                $("#progressbar").progressbar("value", value + 1);                
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="progressbar"></div>
    <div id="result"></div><br />
    <asp:Button ID="btnGetData" runat="server" Text="Get Data" />
    </form>    
</body>
</html>
