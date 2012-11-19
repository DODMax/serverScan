ServerScan
==========

A simple one button scanning software that turns your WIA compatible scanner into a networked scanner.
WARNING: This is only a prototype that still need some work and will propably need some fixing with some scanners.

In a small office like mine, when you have one innexpensive scanner connected to single computer, the usual scanning procedure goes as follow:
* Walk to the scanner
* Put your documents in the scanner
* Go back to your computer
* Connect to the scanner using remote desktop or another scanner sharing software (couldn't find any that's free)
* Scan your documents using the device's software
* If remote desktop then save your document on a network folder then copy them on your computer
* Walk to the scanner and take back your documents
* Walk to your computer again

With this small program you can use any USB device (say a numpad or those emergency buttons you can find on the web) to launch the scanning procedure and have your documents saved on a network location.
The procedure is now:
* Walk to the scanner
* Put your documents in the scanner
* Push the button
* Take back your documents and walk to your computer

REQUIREMENTS
------------
* A WIA scanner USB connected to a Windows XP / 7 computer
* The .Net framework (probably at least in v4)
* A LibUsbDotNet installation to create a driver and configure your button the first time
  http://sourceforge.net/projects/libusbdotnet/

USAGE
-----
### Configure your triggering device
* Install LibUsbDotNet
* Use InfWizard.exe to create a driver for your device
* Go to Windows' device manager and replace your device's driver by the newly generated
* Run Test_Bulk.exe, select your device, try an endpoint (usually 1) and click 'open'
* Click 'read', push a button on your device and look what's read

### Configure ServerScan
* Run the app
* Select your scanner in the list (if it fails make sure wiaaut.dll is present and registered on your system)
* Test the scanner
* Enter VID and PID as shown next to your device's name in Test_Bulk.exe
* Enter the size of the message to read as shown on Test_Bulk.exe after you pushed a single button
* Try to click 'Connect' to test your triggering device
* Push da button!

Have a look at the log.txt file if something seems to go wrong.
The configuration is saved in a simple (config.xml) file in the same directory to allow you to start the program as a scheduled task witout having to bother with which user is running it.


TODO
----
* Find contributors
* Improve compatibility with most scanner brands (currently working well with brother)
* Implement image compression
* Save images on disk on by one instead of at the end (can be a problem when scanning more than 20 pages on old computers)
* Implement a better way of selecting the triggering device than providing VID and PID
* Implement an actually reading of the triggering device's signal to allow different action depending on which button is pressed
* Refactor some code here and there