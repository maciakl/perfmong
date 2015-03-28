### How do I configure PerfMonG? ###

Right click on the visible area and choose _Properites_

### PerfMonG doesn't start and I get an error message. WTF? ###

To solve the problem please upgrade to 0.2.3 or higher.

It's a known problem - see [Issue 1](http://code.google.com/p/perfmong/issues/detail?id=1&can=2&q=). You will need to create a PerfMonG folder in your _Application Data_ folder. The easiest way to do this is:

  1. Go to _Start_ menu and choose _Run_
  1. Type in: mkdir %APPDATA%\PerfMonG
  1. Run PerfMonG again. It should start normally this time.

### PerfMonG starts up, but I get an error message every few seconds. WTF? ###

If you are using Windows 2000 see [Issue 9](https://code.google.com/p/perfmong/issues/detail?id=9). All versions prior to 0.2.6 will have problems running on Windows 2000 and lower. Upgrade to a newer version. There is no workaround.

### Why does PerfMonG display my free Hard Drive space as N/A ###

You are most likely using Windows 2000 or lower. Because your version of windows doesn't expose the _LogicalDisk_ counter category, it is currently impossible to track this statistic. Thus, this feature is disabled when running on Windows 2000 and lower.

### I fucked up and and set the position of the screen! Help! ###

This is no longer a problem. [Issue 3](https://code.google.com/p/perfmong/issues/detail?id=3) and [Issue 4](https://code.google.com/p/perfmong/issues/detail?id=4) fixed the way the display can be moved. Since you now primarily drag it to move, it is hard to position the display of the screen.  [Issue 5](https://code.google.com/p/perfmong/issues/detail?id=5) and [Issue 6](https://code.google.com/p/perfmong/issues/detail?id=6) fixed the way this is handled in a dual screen configuration.

If you experience this problem you should upgrade immediately. If that's not possible here is what you do: --

  1. Kill PerfMonG process using Task Manager
  1. Go to _Start_ menu and choose _Run_
  1. Type in %APPDATA% and press enter
  1. This should open your _Application Data_ folder in explorer window
  1. Locate the PerfMonG folder
  1. Edit the pm.cfg file inside with Notepad
  1. Change the x and y values to something more manageable. Try x=330 y=680 for example.
  1. Save the file and run PerfMonG again.

This should solve your problem. --

### Can I see the code? ###

Sure you can. Just download a [Source Code Budnle](http://code.google.com/p/perfmong/downloads/list?can=1&q=label%3AType-Source&colspec=Filename+Summary+Uploaded+Size+DownloadCount) or grab it from the [Subversion Repository](http://code.google.com/p/perfmong/source).

Source code bundles are usually .rar files with a snapshot of the code from a given release. The same code can be checked out from appropriate tag in the subversion repository. For example the code in the [PerfMonG 0.2.4.rar](http://code.google.com/p/perfmong/downloads/detail?name=PerfMonG%200.2.4.rar&can=1&q=) is the same as the one as the [release\_0.2.4](http://perfmong.googlecode.com/svn/tags/release_0.2.4/).

### I found a bug! What do I do? ###

Create a bug report using the [Issues](http://code.google.com/p/perfmong/issues/list) tab. The more specific you are the better! Make sure you put "maciakl" in the CC field.

### I have a cool idea for a new feature, but I can't code ###

Submit it via the [Issues](http://code.google.com/p/perfmong/issues/list) tab. Make sure to change the first label to Type-Enhancement (you can choose it from a pull-down menu).

### I wrote a patch! How do I get it to you? ###

Use the [Issue Tab](http://code.google.com/p/perfmong/issues/list). Create a new issue, and attach the patch to it. You can also comment on an existing issue if that's what you wrote the patch for. If you create a new issue please change the first label to Type-Patch.

### What are the Labels for in the issue tracking system? ###

The labels help to identify and prioritize code. Please label your issues the best you can. You should usually add at least one label: type. All new issues default to Type-Defect. However if you are submitting a feature suggestion or a patch, you should change that to Type-Enhancement or Type-Patch respectfully.

Also if you can please specify the nature of the bug or feature request. You can choose an option from the pull-down menu. The most common choices are:

  * Security
  * Performance
  * Usability
  * Maintainability

We make it a priority to resolve all the Security related issues first. Performance issues are also important. Usability and Maintainability patches on the other hand can sometimes be pushed back and included in the next release.

### Why is this on Google Code and not SourceForge? ###

It's here for now. I might move it to SF at some point. I'm still waiting for SF to approve my other, more recent project so we'll see.

### What if I want the code for specific version? ###

I will try to upload code bundles in the download section for all the releases. However, you can always grab a specific release from the svn. Each release will get a snapshot in:

http://perfmong.googlecode.com/svn/tags/

The releases will be subdirectories of that folder named:

release\_x.x.x

For example:

http://perfmong.googlecode.com/svn/tags/release_0.2.1

This way you don't need to know which SVN revision was which version - each will have it's own snapshot.


### Your code sucks! ###

You suck! I wrote it 3 years ago. GTFO!

### Does it work on 64 Bit Vista? ###

Yes it does. Tested on Vista Home Premium (64 bit).