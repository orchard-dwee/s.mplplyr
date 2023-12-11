using WMPLib;

namespace SmplPlyr
{
    public partial class BigPlayButton : UserControl
    {
        public WindowsMediaPlayer? Mp3Player { get; set; }

        public BigPlayButton(string name, string text)
        {
            InitializeComponent();
            Name = name;
            if (!string.IsNullOrEmpty(text))
            {
                var fileToPlay = string.Format("{0}\\Samples\\{2}\\{1}", Application.StartupPath, text, Name);
                AssignClipToPlayButton(text, fileToPlay);
            }
        }

        public CheckBox GetCheckBox()
        {
            return checkBox1;
        }

        private void AssignClipToPlayButton(string text, string fileToPlay)
        {
            checkBox1.Text = text;
            Mp3Player = new WindowsMediaPlayer();
            Mp3Player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler((s) => Wplayer_PlayStateChange(s, checkBox1));
            Mp3Player.URL = fileToPlay;
            Mp3Player.controls.stop();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var chk = (CheckBox) sender;
            if (chk == null)
            {
                return;
            }
            if (chk.CheckState == CheckState.Checked)
            {
                //if button is filled out, play it, otherwise open file upload dialog
                if (string.IsNullOrEmpty(chk.Text))
                {
                    openFileDialog1.Filter = "MP3 files (*.mp3)|*.mp3";
                    openFileDialog1.FileName = "";
                    var result = openFileDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        var appPath = Application.StartupPath;
                        if (!Directory.Exists(appPath + "\\Samples\\" + Name))
                        {
                            Directory.CreateDirectory(appPath + "\\Samples\\" + Name);
                        }
                        var fileNameWithPath = openFileDialog1.FileName;
                        var fileName = Path.GetFileName(fileNameWithPath);
                        var destFile = string.Format(
                            "{0}\\Samples\\{2}\\{1}", appPath, Path.GetFileName(fileName), Name);
                        File.Copy(fileNameWithPath, destFile, true);
                        AssignClipToPlayButton(fileName, destFile);
                    }
                    chk.Checked = false;
                }
                else
                {
                    if (chk.Text.ToLower().EndsWith("mp3") && Mp3Player != null)
                    {
                        Mp3Player.controls.play();
                    }
                }
            }
            else
            {
                if (Mp3Player != null)
                {
                    Mp3Player.controls.pause();
                    Mp3Player.controls.stop();
                }
            }
        }

        private static void Wplayer_PlayStateChange(int newState, CheckBox chk)
        {
            //when clip is done playing, uncheck the button to show the user it's done
            if (newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                chk.Checked = false;
            }
        }

        private void ResetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //right-click reset to clear the play button
            var menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                var owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    var bigButton = owner.SourceControl as CheckBox;
                    if (bigButton == null)
                    {
                        return;
                    }
                    if (Mp3Player != null)
                    {
                        Cursor.Current = Cursors.WaitCursor;
                        Mp3Player.controls.pause();
                        Mp3Player.controls.stop();
                        File.Delete(Mp3Player.URL);
                        Mp3Player.close();
                        Mp3Player = null;
                        bigButton.Text = "";
                        bigButton.Checked = false;
                        Cursor.Current = Cursors.Default;
                    }
                }
            }
        }
    }
}