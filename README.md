( see http://www.benf.org/other/olewoo for old site )

# OleWoo - Yet another TLB viewer.

Oleview's a very handy tool, but it's got some really annoying niggles (only 1 typelibrary open at once, no search, slow on large TLBs, yadda yadda) - OleWoo is my attempt at building something which drags oleview kicking and screaming into at least the late 1990s!

![Olewoo screen](http://www.benf.org/other/olewoo/olewoo1.png)

This might look familiar... (now hyperlinks take you to interface/type definitions...)

![Olewoo screen](http://www.benf.org/other/olewoo/olewoo2.png)

Using the 'find symbol' search box opens a list on the right, with all matching nodes of the TypeLibrary tree - click on one to navigate to it, and view the relevant IDL.
Click on the [>>>] to dismiss the list.

Of course, you can CTRL+F on the IDL viewer to search (It's infuriating that that's missing from OLEVIEW!)

![Olewoo screen](http://www.benf.org/other/olewoo/olewoo3.png)

Click 'Add new Tab' to open up another view on the typelibrary - useful when you need to flick back and forth between interfaces.

![Olewoo screen](http://www.benf.org/other/olewoo/olewoo4.png)

And, of course, you can open as many typelibraries as you want!

![Olewoo screen](http://www.benf.org/other/olewoo/oledump1.png)

OleDump allows you to dump the IDL for a TLB directly from the command line - this is something I've occasionally wished for from OleView!

* I consider this pretty much stable (and have moved on to other pet projects), which probably means it's got a tonne of annoying features. ;), but feel free to drop me a note (or a pull request!) if you've got a tlb that it doesn't play well with. 

* License file in repo
