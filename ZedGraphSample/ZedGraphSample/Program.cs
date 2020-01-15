using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ZedGraphSample
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}


/*
 Open Visual C# 2005 Express Edition (you can download it free here) 
From the File menu, select New Project 
Select Windows Application, and name it ZedGraphSample 
In the Solution Explorer, right-click on References and select "Add Reference..." 
Pick the browse tab, and navigate to the zedGraph.dll (downloadable from here), and click OK 
From View menu, select Toolbox, scroll down to the bottom of the toolbox window to see the "General" bar 
If ZedGraphControl is not already available as an option, rightclick on the "General" bar, and select "Choose Items..." 
Under ".Net Framework Items" tab, click browse 
Navigate to the zedgraph.dll file, and click Open, Click OK 

In the toolbox, left-click on the ZedGraphControl, go to your Form and click inside it to place a ZedGraphControl item. 
With the ZedGraphControl selected in the form, under the View menu select "Properties Window" 
Change the "(Name)" field for the ZedGraphControl to zg1 (it would typically be 'zedGraphControl1'). 

Double click the Windows Form (not the ZedGraphControl), this will cause the window to switch to CodeView, with a template for the Form1_Load() method 
Go to the top of the file and add using ZedGraph; 
Go to any sample graph from the ZedGraphWiki Sample section, and copy the entire contents of the CreateGraph() method into this new Code File (inside the Form1 class definition, but outside any existing method definition) 
Here's an example CreateGraph() method:
 */
