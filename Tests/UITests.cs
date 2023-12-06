using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.Core.Input;
using FlaUI.Core.Tools;
using FlaUI.Core.WindowsAPI;
using FlaUI.UIA3;

namespace Tests
{
    [TestFixture]
    public class UITests
    {
        public string _mainExePath;
        public string _testTrackPath;

        [SetUp]
        public void Setup()
        {
            var thisProjExeDir = AppDomain.CurrentDomain.BaseDirectory;
            var mainProjExeDir = thisProjExeDir.Replace("Tests", "SmplPlyr");
            var samplesDir = Path.Combine(mainProjExeDir, "Samples");
            if (!Directory.Exists(samplesDir))
            {
                Directory.CreateDirectory(samplesDir);
            }
            var firstBigPlayButtonDir = Path.Combine(mainProjExeDir, "Samples", "PlayButton_0");
            if (!Directory.Exists(firstBigPlayButtonDir))
            {
                Directory.CreateDirectory(firstBigPlayButtonDir);
            }
            var thisProjDir = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            _testTrackPath = Path.Combine(thisProjDir, "TestTrack");
            if (Directory.GetFiles(firstBigPlayButtonDir, "*.mp3").Length == 0)
            {
                var sourceFile = Path.Combine(_testTrackPath, "1kHz.mp3");
                var destFile = Path.Combine(firstBigPlayButtonDir, "1kHz.mp3");
                File.Copy(sourceFile, destFile, true);
            }
            _mainExePath = Path.Combine(mainProjExeDir, "SmplPlyr.exe");
        }

        [Test, Order(1)]
        public void AppShouldLaunch()
        {
            using (var app = Application.Launch(_mainExePath))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);
                    Assert.That(window, Is.Not.Null);
                    Assert.That(window.Title, Is.Not.Null);
                }
                app.Close();
            }
        }

        [Test, Order(2)]
        public void PlusButtonShouldAddBigPlayButtonAndMinusButtonShouldRemoveLastBigPlayButton()
        {
            using (var app = Application.Launch(_mainExePath))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);
                    var layoutPnl = window.FindFirstChild(cf => cf.ByAutomationId("flowLayoutPanel1"));
                    Assert.IsNotNull(layoutPnl);

                    var pnlChildren = layoutPnl.FindAllChildren();
                    var playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var origNumPlayButtons = playButtons.Count();

                    var plusButton = window.FindFirstChild(cf => cf.ByAutomationId("plusButton"));
                    Assert.IsNotNull(plusButton);

                    plusButton.Click();
                    pnlChildren = layoutPnl.FindAllChildren();
                    playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var numPlayButtons = playButtons.Count();
                    Assert.AreEqual(origNumPlayButtons + 1, numPlayButtons);

                    var minusButton = window.FindFirstChild(cf => cf.ByAutomationId("minusButton"));
                    Assert.IsNotNull(minusButton);

                    minusButton.Click();
                    pnlChildren = layoutPnl.FindAllChildren();
                    playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    numPlayButtons = playButtons.Count();
                    Assert.AreEqual(origNumPlayButtons, numPlayButtons);
                }
                app.Close();
            }
        }

        [Test, Order(3)]
        public void NewlyAddedBigPlayButtonShouldLaunchFileUploaderWhenBlankAndFillInTextOfUploadedFile()
        {
            using (var app = Application.Launch(_mainExePath))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);

                    var plusButton = window.FindFirstChild(cf => cf.ByAutomationId("plusButton"));
                    plusButton.Click();

                    var layoutPnl = window.FindFirstChild(cf => cf.ByAutomationId("flowLayoutPanel1"));
                    var pnlChildren = layoutPnl.FindAllChildren();
                    var playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var lastPlayButton = playButtons.LastOrDefault();
                    Assert.IsNotNull(lastPlayButton);
                    
                    var lastPlayButtonChk = lastPlayButton.FindFirstChild(cf => cf.ByAutomationId("checkBox1"));
                    Assert.IsEmpty(lastPlayButtonChk.AsCheckBox().Text);
                    lastPlayButtonChk.Click();

                    var waitForUploadWindow = Retry.WhileNull(() => window.FindFirstDescendant(cf => cf.ByControlType(ControlType.Window)));
                    var uploadWindow = waitForUploadWindow.Result;

                    var uploadWindowProgBar = uploadWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.ProgressBar));
                    var uploadWindowToolBars = uploadWindowProgBar.FindAllDescendants(cf => cf.ByControlType(ControlType.ToolBar));
                    var uploadWindowAddressToolBar = uploadWindowToolBars.FirstOrDefault(t => t.Name.StartsWith("Address"));
                    Assert.IsNotNull(uploadWindowAddressToolBar);

                    var uploadWindowAddressToolBarAllLocationsButton = uploadWindowAddressToolBar.FindFirstChild(cf => cf.ByName("All locations"));
                    uploadWindowAddressToolBarAllLocationsButton.Click();

                    var waitForComboBox = Retry.WhileNull(() => uploadWindowProgBar.FindFirstDescendant(cf => cf.ByControlType(ControlType.ComboBox)));
                    var uploadWindowAddressComboBox = waitForComboBox.Result;

                    var uploadWindowAddressEditBox = uploadWindowAddressComboBox.FindFirstChild(cf => cf.ByControlType(ControlType.Edit));
                    uploadWindowAddressEditBox.AsTextBox().Text = _testTrackPath;
                    Keyboard.Type(VirtualKeyShort.RETURN);

                    var fileList = uploadWindow.FindFirstDescendant(cf => cf.ByControlType(ControlType.List));
                    var fileListItems = fileList.FindAllChildren(cf => cf.ByControlType(ControlType.ListItem));
                    var mp3FileItem = fileListItems.FirstOrDefault(f => f.Name.ToLower().Equals("440hz.mp3"));
                    Assert.IsNotNull(mp3FileItem);
                    var fileName = mp3FileItem.Name;

                    mp3FileItem.DoubleClick();
                    Assert.IsNotEmpty(Retry.WhileEmpty(() => lastPlayButtonChk.AsCheckBox().Text).Result);
                    Assert.AreEqual(fileName, lastPlayButtonChk.AsCheckBox().Text);
                }
                app.Close();
            }
        }

        [Test, Order(4)]
        public void SearchFieldCanFilterOutButtonsByText()
        {
            using (var app = Application.Launch(_mainExePath))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);

                    var layoutPnl = window.FindFirstChild(cf => cf.ByAutomationId("flowLayoutPanel1"));
                    var pnlChildren = layoutPnl.FindAllChildren();
                    var playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var origNumPlayButtons = playButtons.Count();

                    var searchBox = window.FindFirstDescendant(cf => cf.ByAutomationId("searchBox"));
                    Assert.IsNotNull(searchBox);
                    searchBox.Click();

                    Keyboard.Type(VirtualKeyShort.KEY_4);
                    Keyboard.Type(VirtualKeyShort.KEY_4);
                    Keyboard.Type(VirtualKeyShort.KEY_0);

                    pnlChildren = layoutPnl.FindAllChildren();
                    playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var numPlayButtons = playButtons.Count();

                    Assert.AreEqual(1, numPlayButtons);

                    searchBox.AsTextBox().Text = "";

                    pnlChildren = layoutPnl.FindAllChildren();
                    playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    numPlayButtons = playButtons.Count();

                    Assert.AreEqual(origNumPlayButtons, numPlayButtons);
                }
                app.Close();
            }
        }

        [Test, Order(5)]
        public void ButtonCanBeResetAndBeRemovedFromLayout()
        {
            using (var app = Application.Launch(_mainExePath))
            {
                using (var automation = new UIA3Automation())
                {
                    var window = app.GetMainWindow(automation);

                    var layoutPnl = window.FindFirstChild(cf => cf.ByAutomationId("flowLayoutPanel1"));
                    var pnlChildren = layoutPnl.FindAllChildren();
                    var playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var origNumPlayButtons = playButtons.Count();
                    var lastPlayButton = playButtons.LastOrDefault();
                    Assert.IsNotNull(lastPlayButton);

                    var lastPlayButtonChk = lastPlayButton.FindFirstChild(cf => cf.ByAutomationId("checkBox1"));
                    lastPlayButtonChk.RightClick();

                    var resetMenuItem = window.FindFirstDescendant(cf => cf.ByName("Reset"));
                    resetMenuItem.Click();
                    Assert.IsEmpty(Retry.WhileNull(() => lastPlayButtonChk.AsCheckBox().Text).Result);

                    var minusButton = window.FindFirstChild(cf => cf.ByAutomationId("minusButton"));
                    minusButton.Click();

                    pnlChildren = layoutPnl.FindAllChildren();
                    playButtons = pnlChildren.Where(p => p.AutomationId.StartsWith("PlayButton_"));
                    var numPlayButtons = playButtons.Count();

                    Assert.AreEqual(origNumPlayButtons -1, numPlayButtons);
                }
                app.Close();
            }
        }
    }
}