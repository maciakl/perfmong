### Which code should I check out? ###

In most cases you should be checking out from the `trunk` just how it says on the Source page. The trunk holds the most up to date code. This code should be good quality and unbroken most of the time.

### Where do I get the experimental bleeding edge code? ###

Experimental code that may or may not get included in the releases will usually get checked into `branches`. Usually a branch will be labeled as:

```
http://perfmong.googlecode.com/branches/release_x.x.x_whatever
```

where `whatever` is some descriptive label indicating the nature of this branch. Most experimental branches should also have it's own wiki page - but this is not a rule.

### Where do I get stable releases? ###

Each stable release is branched off into the `tags` directory and labeled as so:

```
http://perfmong.googlecode.com/tags/release_x.x.x
```

### Can I get SVN write access? ###

At this moment I want to be able to review changes to the code base. However I will seriously consider giving access to long time contributors at some later time.

### How do I check in code without SVN write? ###

Create a patch and submit it via the Issue Tracking system.

### I submitted a patch a month ago and got no response, wtf? ###

Please make sure you put _maciakl_ in the CC field when you submit an issue or patch. Otherwise I might not get an email notification when the issue gets submitted. If I don't get that email, then I will see your patch only after I visit the site - which might take a while.

### How do I create a patch? ###

Most SVN clients have this functionality. For example [Tortoise SVN](http://tortoisesvn.tigris.org/) has it as an option in the right-click context menu.