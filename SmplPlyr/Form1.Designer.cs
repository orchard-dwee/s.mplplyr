namespace SmplPlyr
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            flowLayoutPanel1 = new FlowLayoutPanel();
            plusButton = new Button();
            minusButton = new Button();
            toolTip1 = new ToolTip(components);
            label1 = new Label();
            searchBox = new TextBox();
            trackBar1 = new TrackBar();
            fasterButton = new Button();
            slowerButton = new Button();
            resetSpeedButton = new Button();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.ControlDark;
            flowLayoutPanel1.BorderStyle = BorderStyle.FixedSingle;
            flowLayoutPanel1.Location = new Point(11, 75);
            flowLayoutPanel1.Margin = new Padding(4, 2, 4, 2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1820, 902);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // plusButton
            // 
            plusButton.Location = new Point(82, 13);
            plusButton.Margin = new Padding(4, 2, 4, 2);
            plusButton.Name = "plusButton";
            plusButton.Size = new Size(45, 45);
            plusButton.TabIndex = 1;
            plusButton.Text = "+";
            toolTip1.SetToolTip(plusButton, "Add Play Button");
            plusButton.UseVisualStyleBackColor = true;
            plusButton.Click += PlusButton_Click;
            // 
            // minusButton
            // 
            minusButton.Location = new Point(11, 13);
            minusButton.Margin = new Padding(4, 2, 4, 2);
            minusButton.Name = "minusButton";
            minusButton.Size = new Size(45, 45);
            minusButton.TabIndex = 0;
            minusButton.Text = "-";
            toolTip1.SetToolTip(minusButton, "Remove Play Button");
            minusButton.UseVisualStyleBackColor = true;
            minusButton.Click += MinusButton_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(807, 17);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(90, 32);
            label1.TabIndex = 3;
            label1.Text = "Search:";
            toolTip1.SetToolTip(label1, "Filter Play Buttons");
            // 
            // searchBox
            // 
            searchBox.BorderStyle = BorderStyle.None;
            searchBox.Location = new Point(5, 0);
            searchBox.Margin = new Padding(4, 2, 4, 2);
            searchBox.Name = "searchBox";
            searchBox.Size = new Size(919, 32);
            searchBox.TabIndex = 4;
            toolTip1.SetToolTip(searchBox, "Filter Play Buttons");
            searchBox.TextChanged += SearchBox_TextChanged;
            // 
            // trackBar1
            // 
            trackBar1.AutoSize = false;
            trackBar1.LargeChange = 1;
            trackBar1.Location = new Point(178, 13);
            trackBar1.Maximum = 100;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(424, 50);
            trackBar1.TabIndex = 7;
            toolTip1.SetToolTip(trackBar1, "Stutter Interval");
            trackBar1.Scroll += TrackBar1_Scroll;
            // 
            // fasterButton
            // 
            fasterButton.Location = new Point(631, 13);
            fasterButton.Margin = new Padding(4, 2, 4, 2);
            fasterButton.Name = "fasterButton";
            fasterButton.Size = new Size(45, 45);
            fasterButton.TabIndex = 8;
            fasterButton.Text = "▲";
            toolTip1.SetToolTip(fasterButton, "Increase Speed");
            fasterButton.UseVisualStyleBackColor = true;
            fasterButton.Click += FasterButton_Click;
            // 
            // slowerButton
            // 
            slowerButton.Location = new Point(695, 13);
            slowerButton.Margin = new Padding(4, 2, 4, 2);
            slowerButton.Name = "slowerButton";
            slowerButton.Size = new Size(45, 45);
            slowerButton.TabIndex = 9;
            slowerButton.Text = "▼";
            toolTip1.SetToolTip(slowerButton, "Decrease Speed");
            slowerButton.UseVisualStyleBackColor = true;
            slowerButton.Click += SlowerButton_Click;
            // 
            // resetSpeedButton
            // 
            resetSpeedButton.Location = new Point(755, 13);
            resetSpeedButton.Margin = new Padding(4, 2, 4, 2);
            resetSpeedButton.Name = "resetSpeedButton";
            resetSpeedButton.Size = new Size(45, 45);
            resetSpeedButton.TabIndex = 10;
            resetSpeedButton.Text = "X";
            toolTip1.SetToolTip(resetSpeedButton, "Reset Speed");
            resetSpeedButton.UseVisualStyleBackColor = true;
            resetSpeedButton.Click += ResetSpeedButton_Click;
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Window;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(pictureBox1);
            panel1.Controls.Add(searchBox);
            panel1.Location = new Point(904, 15);
            panel1.Margin = new Padding(4, 2, 4, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(927, 40);
            panel1.TabIndex = 5;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(900, 10);
            pictureBox1.Margin = new Padding(4, 2, 4, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(39, 38);
            pictureBox1.TabIndex = 5;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1844, 990);
            Controls.Add(resetSpeedButton);
            Controls.Add(slowerButton);
            Controls.Add(fasterButton);
            Controls.Add(trackBar1);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(minusButton);
            Controls.Add(plusButton);
            Controls.Add(flowLayoutPanel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 2, 4, 2);
            Name = "Form1";
            Text = "S.mple Player";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private Button plusButton;
        private Button minusButton;
        private ToolTip toolTip1;
        private Label label1;
        private TextBox searchBox;
        private Panel panel1;
        private PictureBox pictureBox1;
        private TrackBar trackBar1;
        private Button fasterButton;
        private Button slowerButton;
        private Button resetSpeedButton;
    }
}