This is a Microsoft .NET Gadgeteer module project created using the Module Template included in .NET Gadgeteer Core.

This template provides a simple way to build software for a custom Microsoft .NET Gadgeteer hardware module 
that is compliant with the Microsoft .NET Gadgeteer Module Builder's Guide specifications found at http://gadgeteer.codeplex.com/ 
Using this template auto-generates an installer (MSI) that can easily be distributed to end users.  

Some of the functionality referred to in this template is targeted toward Visual Studio Add-ins that are not yet available,
but, following this guide will make your module forward-compatible with forthcoming add-ins.

We recommend that you read the Module Builder's Guide at http://gadgeteer.codeplex.com/ for full details.

==============================================

SYSTEM REQUIREMENT

To build the installer MSI automatically, this template requires WiX 3.5 to be installed:
http://wix.codeplex.com/releases/view/60102 (Wix35.msi)

==============================================

BUILD NOTES

Building with the Release configuration generates an MSI installer, which includes your code, in the output directory 
of the project (bin/Release/Installer).  This takes a litttle time, and  Visual Studio/C# Express can appear to hang briefly.  
To avoid this delay, build with the Debug configuration. 

Visual C# Express always builds in Release configuration. In order to turn off the installer build to speed up the build process,
you can go to Menu->Project-><ModuleName> Properties->Build tab and tick the "Define DEBUG constant" box.

If you see the error "The system cannot find the file..." try "Rebuild" rather than "Build"

==============================================

MODULE TEMPLATE USE INSTRUCTIONS 

0) IMPORTANT. If you are using Visual C# 2010 Express, you must first 
	i) Save this project to a permanent location by hitting File->Save All. 
    ii) Then, open Menu->Project->Properties, and change the Assembly name field to GTM.<ManufacturerName>.<ModuleName>.
	Otherwise, when you save the project, the Assembly name is automatically modified and the installer will pick up the old assemblies.

1) In Menu->Project->Properties, change "ManufacturerName" to the short name of your institution, with no spaces or punctuation,
   in the two places it appears.  This should match what you print on the module PCB.

2) Edit the <ModuleName>.cs file to implement software for your module, in particular, change "ManufacturerName" in the namespace
   as above.  There are comments and examples in this file to assist you with this process.

3) Test your module. Modules cannot be run directly on Gadgeteer hardware, since they are class libraries (dlls) not executables (exe).   
   Testing is most easily accomplished by adding a new Gadgeteer project to the same Visual Studio/Visual C# Express solution. 
   With the new Program.cs file open, use the menu item Project->Add Reference, and, in the Projects tab, choose your new module. 
   Then you should be able to instantiate the module using GTM.<ManufacturerName>.<ModuleName> as usual.
   From any other existing project, you can also reference the module by using the same Add Reference process described above and browsing to the
   output location of this module bin\Release\NETMF\

4) Edit the GadgeteerHardware.xml file to specify information about your module, in particular, change the "ManufacturerName" as described above.
   Also fill in the other fields as specified in the GadgeteerHardware.xml file.

5) Optionally, change Resources\Image.jpg to a good quality top-down image of the module with the socket side facing up,
   cropped tight (no margin), in the same orientation as the width and height specified in GadgeteerHardware.xml (not rotated).   

6) Edit Setup\common.wxi and Setup\en-us.wxl to specify parameters for the installer, as specified in those files.

7) Build in Release configuration to build the module installer!

==============================================

RELEASING THE MODULE SOFTWARE, INDIVIDUALLY OR IN A KIT

The MSI installer generated in the bin\Release\Installer directory can be distributed to end users.
The MSM merge module in the bin\Release\Installer directory can be used to build other installers such as "kit" installers that incorporate multiple
module(s)/mainboard(s).  This will install/remove correctly - e.g. if two kits including a Foo Module are both installed, there will be one copy
of the Foo module (the most up-to-date version) and if either kit is removed, the Foo module will remain installed, because the other kit requires it.

==============================================

MAKING CHANGES

If you make want to release a new version of your module, make sure to change the version number in Setup\common.wxi. 
Otherwise, the auto-generated installer will not be able to upgrade the older version correctly (an error message will result).
It is also advisable to change the versions in Properties/AssemblyInfo.cs and keep these synchronized with your Setup\common.wxi version.  

If you want to change the name of your module, be sure to search all the files for instances of the name.
As per the Module Builder's Guide, the software module name should match the name printed on the module itself.
The ManufacturerName should match the manufacturer name printed on the module (remove any spaces/punctuation).

==============================================

MODULE TEMPLATE FILE DETAILS

1) <ModuleName>.cs - software implementation of the module's "device driver".
2) GadgeteerHardware.xml - defines some parameters about your module.
3) Resources\Image.jpg - placeholder for an image representing the module.
4) Setup\common.wxi - WiX (installer) configuration file that specifies parameters for the installer, including the version number. 
5) Setup\en-us.wxl - WiX (installer) localization file that specifies text strings that are displayed during installation.
6) Setup\msm.wxs - WiX (installer) script that generates an installation merge module for this module.
7) Setup\msi.wxs - WiX (installer) script that generates an installer (msi file) using the merge module.
8) Setup\G.ico - G graphic used by the installer.

==============================================
