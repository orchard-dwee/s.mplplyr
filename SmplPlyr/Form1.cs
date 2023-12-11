namespace SmplPlyr
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var appPath = Application.StartupPath;
            if (!Directory.Exists(appPath + "\\Samples"))
            {
                Directory.CreateDirectory(appPath + "\\Samples");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var directories = Directory.GetDirectories(Application.StartupPath + "\\Samples");
            foreach (var directory in directories)
            {
                // if files found in Samples directory, add filled-out buttons to layout
                var directoryInfo = new DirectoryInfo(directory);
                var files = directoryInfo.EnumerateFiles();
                if (!files.Any())
                {
                    continue;
                }
                var fileName = files.Select(f => f.Name).First();
                var button = new BigPlayButton(directoryInfo.Name, fileName);
                flowLayoutPanel1.Controls.Add(button);
            }
            searchBox.Select();
        }

        private void PlusButton_Click(object sender, EventArgs e)
        {
            var numOfControls = flowLayoutPanel1.Controls.Count;
            var button = new BigPlayButton(string.Format("PlayButton_{0}", numOfControls), string.Empty);
            flowLayoutPanel1.Controls.Add(button);
        }

        private void MinusButton_Click(object sender, EventArgs e)
        {
            var numOfControls = flowLayoutPanel1.Controls.Count;
            if (numOfControls == 0) { 
                return; 
            }
            flowLayoutPanel1.Controls.RemoveAt(numOfControls - 1);
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            var txtBox = (TextBox)sender;
            var typedText = txtBox.Text;
            var controlCol = flowLayoutPanel1.Controls;
            foreach(var ctrl in controlCol)
            {
                var buttCtrl = (BigPlayButton)ctrl;
                var buttCtrlChk = buttCtrl.GetCheckBox();
                //if play button doesn't contain search text, hide it
                buttCtrl.Visible = buttCtrlChk.Text.ToLower().IndexOf(typedText) >= 0;
            }
        }
    }
}