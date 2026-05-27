namespace EarthQ
{
    partial class Main
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
            button1 = new Button();
            MainInfo = new TextBox();
            lastupdate = new Label();
            magnitude = new Label();
            subinfo = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // button1
            // 
            button1.Dock = DockStyle.Bottom;
            button1.Location = new Point(0, 408);
            button1.Name = "button1";
            button1.RightToLeft = RightToLeft.No;
            button1.Size = new Size(800, 42);
            button1.TabIndex = 0;
            button1.Text = "最新の情報に更新";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MainInfo
            // 
            MainInfo.BackColor = Color.Silver;
            MainInfo.Dock = DockStyle.Top;
            MainInfo.Location = new Point(0, 0);
            MainInfo.Multiline = true;
            MainInfo.Name = "MainInfo";
            MainInfo.ReadOnly = true;
            MainInfo.Size = new Size(800, 137);
            MainInfo.TabIndex = 1;
            MainInfo.Text = "地震は発生していません";
            // 
            // lastupdate
            // 
            lastupdate.AutoSize = true;
            lastupdate.Dock = DockStyle.Bottom;
            lastupdate.Location = new Point(0, 393);
            lastupdate.Name = "lastupdate";
            lastupdate.Size = new Size(100, 15);
            lastupdate.TabIndex = 3;
            lastupdate.Text = "最終更新:00:00:00";
            lastupdate.Click += lastupdate_Click;
            // 
            // magnitude
            // 
            magnitude.AutoSize = true;
            magnitude.Dock = DockStyle.Bottom;
            magnitude.Location = new Point(0, 378);
            magnitude.Name = "magnitude";
            magnitude.Size = new Size(93, 15);
            magnitude.TabIndex = 5;
            magnitude.Text = "マグニチュード:不明";
            magnitude.Click += magnitude_Click;
            // 
            // subinfo
            // 
            subinfo.AutoSize = true;
            subinfo.Dock = DockStyle.Bottom;
            subinfo.Location = new Point(0, 363);
            subinfo.Name = "subinfo";
            subinfo.Size = new Size(90, 15);
            subinfo.TabIndex = 6;
            subinfo.Text = "テスト用のテキスト";
            subinfo.Click += subinfo_Click_1;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(800, 450);
            Controls.Add(subinfo);
            Controls.Add(magnitude);
            Controls.Add(lastupdate);
            Controls.Add(MainInfo);
            Controls.Add(button1);
            Name = "Main";
            Text = "MainWindow";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox MainInfo;
        private Label lastupdate;
        private Label magnitude;
        private Label subinfo;
        private System.Windows.Forms.Timer timer1;
    }
}
