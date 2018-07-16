<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="JQProgressBar.ascx.cs" Inherits="WebApplication1.JQProgressBar" %>
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
    
<div id="progressbar"></div>
<div id="result"></div><br />    