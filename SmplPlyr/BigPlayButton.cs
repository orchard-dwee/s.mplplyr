using WMPLib;

namespace SmplPlyr
{
    public partial class BigPlayButton : UserControl
    {
        public WindowsMediaPlayer? Mp3Player { get; set; }
        public double LoopBegPos { get; set; }
        public double LoopEndPos { get; set; }
        private System.Windows.Forms.Timer TmrWmpPlayerPosition { get; set; }

        readonly int playing = 3;
        readonly int rewind = 5;

        public BigPlayButton(string name, string text, string path)
        {
            InitializeComponent();
            TmrWmpPlayerPosition = new System.Windows.Forms.Timer { Interval = 1 };
            TmrWmpPlayerPosition.Tick += new EventHandler(TmrWmpPlayerPosition_Tick);
            Name = name;
            if (!string.IsNullOrEmpty(text))
            {
                var fileToPlay = string.Format("{0}Samples\\{2}\\{1}", path, text, Name);
                AssignClipToPlayButton(text, fileToPlay);
            }
        }

        private void TmrWmpPlayerPosition_Tick(object sender, EventArgs e)
        {
            if (Mp3Player == null)
            {
                return;
            }
            if (Mp3Player.controls.currentPosition < LoopEndPos) {
                return;
            };
            // if this is reached, then trackbar has moved and the position needs to be set
            Mp3Player.controls.currentPosition = LoopBegPos;
            TmrWmpPlayerPosition.Stop();
            TmrWmpPlayerPosition.Start();
        }

        private void StartWmpPlayerTimer()
        {
            TmrWmpPlayerPosition.Enabled = true;
            TmrWmpPlayerPosition.Start();
        }

        internal void ChangeSpeed(SpeedValue speedVal)
        {
            if (Mp3Player == null)
            {
                return;
            }
            if ((int) Mp3Player.playState >= playing && (int)Mp3Player.playState <= rewind)
            {
                if (speedVal == SpeedValue.Faster)
                {
                    ++Mp3Player.settings.rate;
                }
                else if (speedVal == SpeedValue.Reset)
                {
                    Mp3Player.settings.rate = 1;
                }
                else
                {
                    if (Mp3Player.settings.rate > 1)
                    {
                        --Mp3Player.settings.rate;
                    }
                }
            }
        }

        internal void Stutter(double pos)
        {
            if (Mp3Player == null)
            {
                return;
            }
            if ((int)Mp3Player.playState >= playing && (int)Mp3Player.playState <= rewind)
            {
                //reset track so it play normally when trackbar is all the way to the left
                if (pos == 0)
                {
                    LoopBegPos = 0;
                    LoopEndPos = 0;
                    TmrWmpPlayerPosition.Stop();
                }
                else
                {
                    // set loop based on position of trackbar
                    var divPos = (100 - pos) / 100;
                    if (LoopEndPos == 0)
                    {
                        LoopEndPos = Mp3Player.controls.currentPosition;
                        Mp3Player.controls.currentPosition -= divPos;
                        LoopBegPos = Mp3Player.controls.currentPosition;
                    }
                    else
                    {
                        LoopEndPos = LoopBegPos + divPos;
                    }
                    StartWmpPlayerTimer();
                }
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
            Mp3Player.PlayStateChange += new _WMPOCXEvents_PlayStateChangeEventHandler(
                (state) => Wplayer_PlayStateChange(state, checkBox1));
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