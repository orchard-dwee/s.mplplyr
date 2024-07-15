# S.mplPlyr

A very no-frills mp3 trigger that I use to play samples when DJ'ing.[^1]
---
I wanted a very easy-to-use application where I can play samples without any distracting controls to get in my way, similar to a drum pad.  I just decided to roll my own for the fun of it.  Naming something is always a challenge so I went with S.mplPlyr, because it seems to be a popular thing to leave out vowels.  I noticed that "Smpl" could be read as "Simple" or "Sample", and since both words do describe this, I went with the "." as a wildcard.

Feel free to fork this and play around with it.  However, if you just want the finished release, you can download it from [here](https://dwee.org/about/SmplPlyr.zip).

I also have a little test suite that makes use of the FlaUI project, so if you're a developer who's struggling with getting that working for your project, please feel free to poke around for some tips.[^2]

---
# Usage

This will be quick.  When you start up the application, you're presented with a blank slate.  The application will then create a sub-directory called "Samples" in the same directory where the executable is located.  This directory is used to keep track of the files you've assigned to the player buttons.

![Example One](https://dwee.org/img/smplplyr1.jpg?)

Click on the + button up in the upper-left corner to add a player button.  You can add as many as you like.  The - button removes buttons.

![Example Two](https://dwee.org/img/smplplyr2.jpg?)

When the blank button is clicked, it will present you with an "upload file" dialog.

![Example Three](https://dwee.org/img/smplplyr3.jpg?)

When the file is uploaded, the name of the file will show in the button.

![Example Four](https://dwee.org/img/smplplyr4.jpg?)

When you click the button now, the file will play.  The button will turn green while it's playing.  When the sound is done playing, the color changes back.

![Example Five](https://dwee.org/img/smplplyr5.jpg?)

The "Search" text field up top filters the buttons by their text for your convenience.

![Example Six](https://dwee.org/img/smplplyr6.jpg?)

I wanted the UI to be as simple as possible but I knew I had to have a way to clear a button, so I added a right-click event.  I thought this was a good way to add this functionality without it getting accidentally triggered by a clumsy hand gesture.  To clear the button, right click on the button and select Reset.

![Example Seven](https://dwee.org/img/smplplyr7.jpg?)

And that's it!  Nice and simple.  I hope you enjoy.

Update - July 15th 2024:  I've added some more functionality regarding playback between the add/subtract buttons and the search box.  From left to right they are a slider which, when moved, will loop a portion of the track.  The more you slide it to the right, the shorter the loop.  Next is an arrow-up button, which will speed up the playback.  Likewise, the arrow-down button will slow the playback.  As of now, the slowest speed is the natural playback.  Lastly, the X button will reset the speed back to it's natural rate with one click.  Basically why I added these were to make some glitchy effects, because I'm a sucker for IDM.

![Example Eight](https://dwee.org/img/smplplyr8.jpg)

[^1]: This has been tested on Windows 10 only.  I know that covers most people who use Windows since no one wants to upgrade to 11, but I'm in a pendantic mood.
[^2]: If you're messing about with FlaUI you should grab the Inspector.  You'll be glad you did.

