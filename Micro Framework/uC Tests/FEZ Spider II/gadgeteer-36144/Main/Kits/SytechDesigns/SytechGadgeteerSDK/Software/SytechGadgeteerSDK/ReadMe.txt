This is a Microsoft .NET Gadgeteer kit solution created using the Kit Template (2011-09-07).

This template provides a simple way to build a custom kit installer for Microsoft .NET Gadgeteer incorporating
mainboard(s) and module(s) as well as custom Getting Started Guide documentation for this kit. 

Using this template auto-generates an installer (MSI) that can easily be distributed to end users.

We recommend that you read the Mainboard and Module Builder's Guides at http://gadgeteer.codeplex.com/ for full details.

==============================================

SYSTEM REQUIREMENT

To build the installer MSI automatically, this template requires WiX 3.5 to be installed:
http://wix.codeplex.com/releases/view/60102 (Wix35.msi)

==============================================

KIT TEMPLATE USE INSTRUCTIONS 

For the Getting Started Guide (optional)
	1) Write an HTML getting started guide and place it at [Solution]\Getting Started Guide\GettingStarted.htm, with images in the same directory.
	2) For your convenience, you may want to add the Getting Started Guide as a Solution Folder to the solution, and add all the files underneath that folder.

For the kit's installer (the project that this readme.txt is in)
	1) Edit en-us.wxl to specify the DistributorFull company name.  You can also change the kit name in this file.
	2) Edit msi.wxs, the installer WiX file:
		- Under "<!-- List all images used in the getting started guide here -->" list the getting started guide image files
		  If you don't have a getting started guide, remove the WiX elements relating to the Getting Started Guide

		- Under "<!-- List merge modules for mainboard(s) and module(s) here" follow the instructions to reference merge modules (msm files)
		  for the mainboard(s) and module(s) you are including in this kit. 
		
		- Under "<!-- List all merge modules above here" follow the instructions to reference merge modules as well

==============================================

BUILDING THE KIT

The msi.wxs file relies on merge module (MSM) files being present in the right locations for it to pick up. 
These are built by the respective mainboard and module projects, created using the respective .NET Gadgeteer templates.
These templates only build the MSM file in Release configuration since it is a time-consuming operation.

So, one way to build the Kit Installer is to open up each of the individual mainboard/module solutions 
and build under Release configuration to make the MSMs, before building the kit template.

Another more convenient way to rebuild the kit is to add the mainboard/module projects (csprojs) to the Kit solution (sln).
To do this, make a new solution folder e.g. "MSM Sources" in Solution Explorer and under that folder choose "Add->Existing Project", and thereby add each mainboard/module.
Then, when in Release configuration, you can right-click on the "MSM Sources" folder and use "Rebuild" to rebuild all the MSMs, or rebuild them individually.

The kit installer will build in Debug or Release configuration, so one way to ensure that you don't accidentally rebuild all your MSMs is to stay in Debug configuration.

==============================================

MAKING CHANGES

If you make want to release a new version of your kit, make sure to change the version number in common.wxi. 
Otherwise, the auto-generated installer will not be able to upgrade the older version correctly (an error message will result).

==============================================
