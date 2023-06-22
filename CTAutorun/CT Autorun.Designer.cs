namespace CTAutorun
{
    partial class CTAutorun
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTAutorun));
            this.ConnectApp = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.initBullet = new System.Windows.Forms.RadioButton();
            this.load_trayBullet = new System.Windows.Forms.RadioButton();
            this.select_taskBullet = new System.Windows.Forms.RadioButton();
            this.scanBullet = new System.Windows.Forms.RadioButton();
            this.query_filesBullet = new System.Windows.Forms.RadioButton();
            this.query_robotBullet = new System.Windows.Forms.RadioButton();
            this.exitBullet = new System.Windows.Forms.RadioButton();
            this.connect_robotBullet = new System.Windows.Forms.RadioButton();
            this.disconnect_robotBullet = new System.Windows.Forms.RadioButton();
            this.stopBullet = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inactiveBullet = new System.Windows.Forms.RadioButton();
            this.startButton = new System.Windows.Forms.Button();
            this.comm_test_checkBox = new System.Windows.Forms.CheckBox();
            this.pause_button = new System.Windows.Forms.Button();
            this.stop_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ConnectApp
            // 
            this.ConnectApp.Location = new System.Drawing.Point(179, 181);
            this.ConnectApp.Name = "ConnectApp";
            this.ConnectApp.Size = new System.Drawing.Size(178, 56);
            this.ConnectApp.TabIndex = 0;
            this.ConnectApp.Text = "Connect App";
            this.ConnectApp.UseVisualStyleBackColor = true;
            this.ConnectApp.Click += new System.EventHandler(this.ConnectApp_click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(11, 23);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(320, 119);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // initBullet
            // 
            this.initBullet.AutoSize = true;
            this.initBullet.Enabled = false;
            this.initBullet.Location = new System.Drawing.Point(11, 158);
            this.initBullet.Margin = new System.Windows.Forms.Padding(2);
            this.initBullet.Name = "initBullet";
            this.initBullet.Size = new System.Drawing.Size(39, 17);
            this.initBullet.TabIndex = 2;
            this.initBullet.TabStop = true;
            this.initBullet.Text = "Init";
            this.initBullet.UseVisualStyleBackColor = true;
            this.initBullet.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // load_trayBullet
            // 
            this.load_trayBullet.AutoSize = true;
            this.load_trayBullet.Enabled = false;
            this.load_trayBullet.Location = new System.Drawing.Point(11, 178);
            this.load_trayBullet.Margin = new System.Windows.Forms.Padding(2);
            this.load_trayBullet.Name = "load_trayBullet";
            this.load_trayBullet.Size = new System.Drawing.Size(72, 17);
            this.load_trayBullet.TabIndex = 3;
            this.load_trayBullet.TabStop = true;
            this.load_trayBullet.Text = "Load_tray";
            this.load_trayBullet.UseVisualStyleBackColor = true;
            this.load_trayBullet.CheckedChanged += new System.EventHandler(this.load_trayBullet_CheckedChanged);
            // 
            // select_taskBullet
            // 
            this.select_taskBullet.AutoSize = true;
            this.select_taskBullet.Enabled = false;
            this.select_taskBullet.Location = new System.Drawing.Point(11, 197);
            this.select_taskBullet.Margin = new System.Windows.Forms.Padding(2);
            this.select_taskBullet.Name = "select_taskBullet";
            this.select_taskBullet.Size = new System.Drawing.Size(81, 17);
            this.select_taskBullet.TabIndex = 4;
            this.select_taskBullet.TabStop = true;
            this.select_taskBullet.Text = "Select_task";
            this.select_taskBullet.UseVisualStyleBackColor = true;
            this.select_taskBullet.CheckedChanged += new System.EventHandler(this.select_taskBullet_CheckedChanged);
            // 
            // scanBullet
            // 
            this.scanBullet.AutoSize = true;
            this.scanBullet.Enabled = false;
            this.scanBullet.Location = new System.Drawing.Point(11, 217);
            this.scanBullet.Margin = new System.Windows.Forms.Padding(2);
            this.scanBullet.Name = "scanBullet";
            this.scanBullet.Size = new System.Drawing.Size(50, 17);
            this.scanBullet.TabIndex = 5;
            this.scanBullet.TabStop = true;
            this.scanBullet.Text = "Scan";
            this.scanBullet.UseVisualStyleBackColor = true;
            this.scanBullet.CheckedChanged += new System.EventHandler(this.scanBullet_CheckedChanged);
            // 
            // query_filesBullet
            // 
            this.query_filesBullet.AutoSize = true;
            this.query_filesBullet.Enabled = false;
            this.query_filesBullet.Location = new System.Drawing.Point(11, 236);
            this.query_filesBullet.Margin = new System.Windows.Forms.Padding(2);
            this.query_filesBullet.Name = "query_filesBullet";
            this.query_filesBullet.Size = new System.Drawing.Size(77, 17);
            this.query_filesBullet.TabIndex = 6;
            this.query_filesBullet.TabStop = true;
            this.query_filesBullet.Text = "Query_files";
            this.query_filesBullet.UseVisualStyleBackColor = true;
            this.query_filesBullet.CheckedChanged += new System.EventHandler(this.query_filesBullet_CheckedChanged);
            // 
            // query_robotBullet
            // 
            this.query_robotBullet.AutoSize = true;
            this.query_robotBullet.Enabled = false;
            this.query_robotBullet.Location = new System.Drawing.Point(11, 256);
            this.query_robotBullet.Margin = new System.Windows.Forms.Padding(2);
            this.query_robotBullet.Name = "query_robotBullet";
            this.query_robotBullet.Size = new System.Drawing.Size(83, 17);
            this.query_robotBullet.TabIndex = 7;
            this.query_robotBullet.TabStop = true;
            this.query_robotBullet.Text = "Query_robot";
            this.query_robotBullet.UseVisualStyleBackColor = true;
            this.query_robotBullet.CheckedChanged += new System.EventHandler(this.query_robotBullet_CheckedChanged);
            // 
            // exitBullet
            // 
            this.exitBullet.AutoSize = true;
            this.exitBullet.Enabled = false;
            this.exitBullet.Location = new System.Drawing.Point(11, 275);
            this.exitBullet.Margin = new System.Windows.Forms.Padding(2);
            this.exitBullet.Name = "exitBullet";
            this.exitBullet.Size = new System.Drawing.Size(42, 17);
            this.exitBullet.TabIndex = 8;
            this.exitBullet.TabStop = true;
            this.exitBullet.Text = "Exit";
            this.exitBullet.UseVisualStyleBackColor = true;
            this.exitBullet.CheckedChanged += new System.EventHandler(this.exitBullet_CheckedChanged);
            // 
            // connect_robotBullet
            // 
            this.connect_robotBullet.AutoSize = true;
            this.connect_robotBullet.Enabled = false;
            this.connect_robotBullet.Location = new System.Drawing.Point(11, 295);
            this.connect_robotBullet.Margin = new System.Windows.Forms.Padding(2);
            this.connect_robotBullet.Name = "connect_robotBullet";
            this.connect_robotBullet.Size = new System.Drawing.Size(95, 17);
            this.connect_robotBullet.TabIndex = 9;
            this.connect_robotBullet.TabStop = true;
            this.connect_robotBullet.Text = "Connect_robot";
            this.connect_robotBullet.UseVisualStyleBackColor = true;
            this.connect_robotBullet.CheckedChanged += new System.EventHandler(this.connect_robotBullet_CheckedChanged);
            // 
            // disconnect_robotBullet
            // 
            this.disconnect_robotBullet.AutoSize = true;
            this.disconnect_robotBullet.Enabled = false;
            this.disconnect_robotBullet.Location = new System.Drawing.Point(11, 314);
            this.disconnect_robotBullet.Margin = new System.Windows.Forms.Padding(2);
            this.disconnect_robotBullet.Name = "disconnect_robotBullet";
            this.disconnect_robotBullet.Size = new System.Drawing.Size(109, 17);
            this.disconnect_robotBullet.TabIndex = 10;
            this.disconnect_robotBullet.TabStop = true;
            this.disconnect_robotBullet.Text = "Disconnect_robot";
            this.disconnect_robotBullet.UseVisualStyleBackColor = true;
            this.disconnect_robotBullet.CheckedChanged += new System.EventHandler(this.disconnect_robotBullet_CheckedChanged);
            // 
            // stopBullet
            // 
            this.stopBullet.AutoSize = true;
            this.stopBullet.Enabled = false;
            this.stopBullet.Location = new System.Drawing.Point(11, 334);
            this.stopBullet.Margin = new System.Windows.Forms.Padding(2);
            this.stopBullet.Name = "stopBullet";
            this.stopBullet.Size = new System.Drawing.Size(47, 17);
            this.stopBullet.TabIndex = 11;
            this.stopBullet.TabStop = true;
            this.stopBullet.Text = "Stop";
            this.stopBullet.UseVisualStyleBackColor = true;
            this.stopBullet.CheckedChanged += new System.EventHandler(this.stopBullet_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(11, 143);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Current state";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(13, 7);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Debug Log";
            // 
            // inactiveBullet
            // 
            this.inactiveBullet.AutoSize = true;
            this.inactiveBullet.Enabled = false;
            this.inactiveBullet.Location = new System.Drawing.Point(11, 353);
            this.inactiveBullet.Margin = new System.Windows.Forms.Padding(2);
            this.inactiveBullet.Name = "inactiveBullet";
            this.inactiveBullet.Size = new System.Drawing.Size(63, 17);
            this.inactiveBullet.TabIndex = 14;
            this.inactiveBullet.TabStop = true;
            this.inactiveBullet.Text = "Inactive";
            this.inactiveBullet.UseVisualStyleBackColor = true;
            this.inactiveBullet.CheckedChanged += new System.EventHandler(this.inactiveBullet_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(230, 253);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(87, 20);
            this.startButton.TabIndex = 15;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // comm_test_checkBox
            // 
            this.comm_test_checkBox.AutoSize = true;
            this.comm_test_checkBox.Location = new System.Drawing.Point(191, 158);
            this.comm_test_checkBox.Name = "comm_test_checkBox";
            this.comm_test_checkBox.Size = new System.Drawing.Size(140, 17);
            this.comm_test_checkBox.TabIndex = 16;
            this.comm_test_checkBox.Text = "Run communication test";
            this.comm_test_checkBox.UseVisualStyleBackColor = true;
            this.comm_test_checkBox.CheckedChanged += new System.EventHandler(this.comm_test_checkBox_CheckedChanged);
            // 
            // pause_button
            // 
            this.pause_button.Enabled = false;
            this.pause_button.Location = new System.Drawing.Point(230, 287);
            this.pause_button.Name = "pause_button";
            this.pause_button.Size = new System.Drawing.Size(87, 32);
            this.pause_button.TabIndex = 17;
            this.pause_button.Text = "Pause";
            this.pause_button.UseVisualStyleBackColor = true;
            this.pause_button.Click += new System.EventHandler(this.pause_button_Click);
            // 
            // stop_button
            // 
            this.stop_button.Enabled = false;
            this.stop_button.Location = new System.Drawing.Point(230, 334);
            this.stop_button.Name = "stop_button";
            this.stop_button.Size = new System.Drawing.Size(87, 23);
            this.stop_button.TabIndex = 18;
            this.stop_button.Text = "Stop";
            this.stop_button.UseVisualStyleBackColor = true;
            this.stop_button.Click += new System.EventHandler(this.stop_button_Click);
            // 
            // CTAutorun
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 385);
            this.Controls.Add(this.stop_button);
            this.Controls.Add(this.pause_button);
            this.Controls.Add(this.comm_test_checkBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.inactiveBullet);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopBullet);
            this.Controls.Add(this.disconnect_robotBullet);
            this.Controls.Add(this.connect_robotBullet);
            this.Controls.Add(this.exitBullet);
            this.Controls.Add(this.query_robotBullet);
            this.Controls.Add(this.query_filesBullet);
            this.Controls.Add(this.scanBullet);
            this.Controls.Add(this.select_taskBullet);
            this.Controls.Add(this.load_trayBullet);
            this.Controls.Add(this.initBullet);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ConnectApp);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CTAutorun";
            this.Text = "CT Autorun";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ConnectApp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.RadioButton initBullet;
        private System.Windows.Forms.RadioButton load_trayBullet;
        private System.Windows.Forms.RadioButton select_taskBullet;
        private System.Windows.Forms.RadioButton scanBullet;
        private System.Windows.Forms.RadioButton query_filesBullet;
        private System.Windows.Forms.RadioButton query_robotBullet;
        private System.Windows.Forms.RadioButton exitBullet;
        private System.Windows.Forms.RadioButton connect_robotBullet;
        private System.Windows.Forms.RadioButton disconnect_robotBullet;
        private System.Windows.Forms.RadioButton stopBullet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton inactiveBullet;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.CheckBox comm_test_checkBox;
        private System.Windows.Forms.Button pause_button;
        private System.Windows.Forms.Button stop_button;
    }
}

