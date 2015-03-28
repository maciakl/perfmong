## What is it?

PerfMonG is a minimalistic performance monitor tool for Windows written in C#. It will sit on your desktop and display information such as CPU speed, free RAM, uptime and etc. The positioning of the tool, opacity and colors are configurable.

This project aims to provide a small and simple to use application with functionality similar to [Samurize](http://www.samurize.com/modules/news/) or Linux/Unix based [GKrellM](http://members.dslextreme.com/users/billw/gkrellm/gkrellm.html GKrellM). PerfMonG is much simpler, requires no configuration and is ready to use in seconds.

## Requirements

You will need .NET Framework 1.1 or higher to use PerfMonG. You can download the redistributable package from [Microsoft Website](http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en). 

The product was tested with Windows XP.

Note: PerfMonG 0.2.5 and lower will not work on Windows 2000 and below. See Issue 9. These versions require Windows XP. Versions 0.2.6 and above will work on Windows 2000 with limited functionality.

## Features

At the moment PerfMonG tracks the following system information:

  * CPU Usage (in %)
  * Free RAM (in MB)
  * Page File usage (in %, here labeled as SWAP)
  * Number of running processes
  * Uptime
  * Free HD space (in MB)

Following items can be configured by the user:

  * Text Color
  * Background Color
  * Opacity (you can make PerfMonG transparent or solid)
  * Screen Position (by specifying X and Y values)

## Installation

* Download the installer from the Downloads section. 
* The installer will: 
  - Install PerfMonG in the _Program Files_ folder on your computer.
  - Create shortcuts in your _Start_ menu.
  - Put a shortcut in your Startup folder so that PerfMonG it starts when you log into windows. 
  - Create un-installation script available from the Start Menu
* To configure PerfMonG right click in the program area and choose Properties from the context menu.

## Screen Shots

PerfMonG:

![PerfMongG](http://i11.photobucket.com/albums/a165/maciakl/PerfMonGFull.png)

PerfMonG running in the background on the desktop:

![On Desktop](http://i11.photobucket.com/albums/a165/maciakl/PerfMonG.png)

The setup panel:

![Setup])http://i11.photobucket.com/albums/a165/maciakl/setup.jpg)

Screenshots by Softpedia:

![Softpedia 1](http://www.softpedia.com/screenshots/PerfMonG_1.png)

![Softpedia 1](http://www.softpedia.com/screenshots/PerfMonG_2.png)

## Privacy and Security

PerfMonG does not include any spyware, adware or mallware. It is not bundled with any 3'rd party software. PerfMonG does not keep any persistent logs, beyond a simple configuration file and it does not record the information about your hardware and software configuration. This software does not, and never will "call home" or send out any information over the internet.

PerfMonG was tested by Softpedia and was [certified](http://www.softpedia.com/progClean/PerfMonG-Clean-105124.html certified) to be clean of spyware, adware and viruses.

![Free Award](http://s1.softpedia-static.com/_img/sp100free.png?1)

## Other Questions

Please see the [FAQ](http://code.google.com/p/perfmong/wiki/FAQ) in the Wiki section for more details.

# Primary Contributors

  * Łukasz Grzegorz Maciak http://terminally-incoherent.com
  * mcmcom http://www.mustcodemore.com
