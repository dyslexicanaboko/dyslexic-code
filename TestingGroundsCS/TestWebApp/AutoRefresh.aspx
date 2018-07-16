<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoRefresh.aspx.cs" Inherits="TestWebApp._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript">

            //Refresh page script- By Brett Taylor (glutnix@yahoo.com.au)
            //Modified by Dynamic Drive for NS4, NS6+
            //Visit http://www.dynamicdrive.com for this script

            //configure refresh interval (in seconds)
            var countDownInterval = 5;
            //configure width of displayed text, in px (applicable only in NS4)
            var c_reloadwidth = 200

        </script>
        <ilayer id="c_reload" width=&{c_reloadwidth}; >
            <layer id="c_reload2" width=&{c_reloadwidth}; left=0 top=0>
            </layer>
        </ilayer>
        <script type="text/javascript">
            var countDownTime = countDownInterval + 1;
            
            function countDown()
            {
                countDownTime--;
                
                if (countDownTime <= 0)
                {
                    countDownTime = countDownInterval;
                    clearTimeout(counter);
                    window.location.reload();
                    return;
                }

                if (document.all) //if IE 4+
                    document.all.countDownText.innerText = countDownTime + " ";
                else if (document.getElementById) //else if NS6+
                    document.getElementById("countDownText").innerHTML = countDownTime + " ";
                else if (document.layers)
                { //CHANGE TEXT BELOW TO YOUR OWN
                    document.c_reload.document.c_reload2.document.write('Next <a href="javascript:window.location.reload()">refresh</a> in <b id="countDownText">' + countDownTime + ' </b> seconds');
                    document.c_reload.document.c_reload2.document.close();
                }
                
                counter = setTimeout("countDown()", 1000);
            }

            function startit()
            {
                if (document.all || document.getElementById) //CHANGE TEXT BELOW TO YOUR OWN
                    document.write('Next <a href="javascript:window.location.reload()">refresh</a> in <b id="countDownText">' + countDownTime + ' </b> seconds');
                
                countDown();
            }

            if (document.all || document.getElementById)
                startit();
            else
                window.onload = startit;

        </script>
    </form>
</body>
</html>