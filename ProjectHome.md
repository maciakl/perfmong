# What is it? #

PerfMonG is a minimalistic performance monitor tool for Windows written in C#. It will sit on your desktop and display information such as CPU speed, free RAM, uptime and etc. The positioning of the tool, opacity and colors are configurable.

This project aims to provide a small and simple to use application with functionality similar to [Samurize](http://www.samurize.com/modules/news/) or Linux/Unix based [GKrellM](http://members.dslextreme.com/users/billw/gkrellm/gkrellm.html). PerfMonG is much simpler, requires no configuration and is ready to use in seconds.

# Requirements #

You will need .NET Framework 1.1 or higher to use PerfMonG. You can download the redistributable package from [Microsoft Website](http://www.microsoft.com/downloads/details.aspx?FamilyID=0856EACB-4362-4B0D-8EDD-AAB15C5E04F5&displaylang=en).

The product was tested with Windows XP.

Note: PerfMonG 0.2.5 and lower will not work on Windows 2000 and below. See [Issue 9](https://code.google.com/p/perfmong/issues/detail?id=9). These versions require Windows XP. Versions 0.2.6 and above will work on Windows 2000 with limited functionality.

# Features #

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

# Installation #

  * Download the installer from the Downloads section.
  * The installer will:
    1. Install PerfMonG in the _Program Files_ folder on your computer.
    1. Create shortcuts in your _Start_ menu.
    1. Put a shortcut in your Startup folder so that PerfMonG it starts when you log into windows.
    1. Create un-installation script available from the Start Menu
  * To configure PerfMonG right click in the program area and choose Properties from the context menu.

# Screen Shots #

PerfMonG:

![http://i11.photobucket.com/albums/a165/maciakl/PerfMonGFull.png](http://i11.photobucket.com/albums/a165/maciakl/PerfMonGFull.png)

PerfMonG running in the background on the desktop:

![http://i11.photobucket.com/albums/a165/maciakl/PerfMonG.png](http://i11.photobucket.com/albums/a165/maciakl/PerfMonG.png)

The setup panel:

![http://i11.photobucket.com/albums/a165/maciakl/setup.jpg](http://i11.photobucket.com/albums/a165/maciakl/setup.jpg)

Screenshots by Softpedia:

![http://www.softpedia.com/screenshots/PerfMonG_1.png](http://www.softpedia.com/screenshots/PerfMonG_1.png)

![http://www.softpedia.com/screenshots/PerfMonG_2.png](http://www.softpedia.com/screenshots/PerfMonG_2.png)

# Privacy and Security #

PerfMonG does not include any spyware, adware or mallware. It is not bundled with any 3'rd party software. PerfMonG does not keep any persistent logs, beyond a simple configuration file and it does not record the information about your hardware and software configuration. This software does not, and never will "call home" or send out any information over the internet.

PerfMonG was tested by Softpedia and was [certified](http://www.softpedia.com/progClean/PerfMonG-Clean-105124.html) to be clean of spyware, adware and viruses.

![http://www.softpedia.com/images/spyward/softpedia_free_award_f.gif](http://www.softpedia.com/images/spyward/softpedia_free_award_f.gif)

# Other Questions #

Please see the [FAQ](http://code.google.com/p/perfmong/wiki/FAQ) in the Wiki section for more details.

# Primary Contributors #

  * Łukasz Grzegorz Maciak http://terminally-incoherent.com
  * mcmcom http://www.mustcodemore.com