# S.mplPlyr

A very no-frills mp3 trigger that I use to play samples when DJ'ing.
---
I wanted a very easy-to-use application where I can play samples without any distracting controls to get in my way, similar to a drum pad.  I just decided to roll my own for the fun of it.  Naming something is always a challenge so I went with S.mplPlyr, because it seems to be a popular thing to leave out vowels.  I noticed that "Smpl" could be read as "Simple" or "Sample", and since both words do describe this, I went with the "." as a wildcard.

Feel free to fork this and play around with it.  However, if you just want the finished release, you can download it from [here](https://dwee.org/about/SmplPlyr.zip).

I also have a little test suite that makes use of the FlaUI project, so if you're a developer who's struggling with getting that working for your project, please feel free to poke around for some tips.[^1]

---
# Usage

This will be quick.  When you start up the application, you're presented with a blank slate.  The application will then create a sub-directory called "Samples" in the same directory where the executable is located.  This directory is used to keep track of the files you've assigned to the player buttons.

![Example One](https://dwee.org/img/smplplyr1.jpg)

Click on the + button up in the upper-left corner to add a player button.  You can add as many as you like.  The - button removes buttons.

![Example Two](https://dwee.org/img/smplplyr2.jpg)

When the blank button is clicked, it will present you with an "upload file" dialog.

![Example Three](https://dwee.org/img/smplplyr3.jpg)

When the file is uploaded, the name of the file will show in the button.

![Example Four](https://dwee.org/img/smplplyr4.jpg)

When you click the button now, the file will play.  The button will turn green while it's playing.  When the sound is done playing, the color changes back.

![Example Five](https://dwee.org/img/smplplyr5.jpg)

The "Search" text field up top filters the buttons by their text for your convenience.

![Example Six](https://dwee.org/img/smplplyr6.jpg)

To reset a button, just right-click on it.

![Example Seven](https://dwee.org/img/smplplyr7.jpg)

And that's it!  Nice and simple.  I hope you enjoy.

[^1]: Grab the FlaUI Inspector.  You'll be glad you did.
