# TestResultBar

TestResultBar provides a nifty summary of the last test run, right in the status bar.

## Appearance

TestResultBar is inserted in the right end of the status bar and shows the number of passed and failed tests respectively.

![Screenshot of passing tests](/Resources/ScreenshotPassingTests.png "Screenshot of passing tests")

If any test fails the background will turn red and the failing test(s) will be displayed in a popup. Clicking the popup will open up the Test explorer.

![Screenshot of failing tests](/Resources/ScreenshotFailingTests.png "Screenshot of failing tests")

You can at any time click on TestResultBar to run all tests.

## Compatibility

TestResultBar is tested on the following versions of Visual Studio. It may of course work with other versions as well, please update the list if you have successfully installed it on other Visual Studio versions.

* Visual Studio 2017 Community

## Acknowledgement

Based on [StatusInfo][https://github.com/lkytal/StatusInfo]