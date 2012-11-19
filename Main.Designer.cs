namespace ServerScan
{
    partial class Main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                USBRead.StopReading();

                if (components != null)
                    components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.check_tryFlatbed = new System.Windows.Forms.CheckBox();
            this.check_useAdf = new System.Windows.Forms.CheckBox();
            this.bt_testScanner = new System.Windows.Forms.Button();
            this.lb_path = new System.Windows.Forms.Label();
            this.lb_scanner = new System.Windows.Forms.Label();
            this.bt_path = new System.Windows.Forms.Button();
            this.bt_select = new System.Windows.Forms.Button();
            this.combo_color = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.combo_dpi = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.check_minimized = new System.Windows.Forms.CheckBox();
            this.check_autostart = new System.Windows.Forms.CheckBox();
            this.box_pid = new System.Windows.Forms.TextBox();
            this.box_vid = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.bt_connect = new System.Windows.Forms.Button();
            this.pathBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.mainNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.box_read = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.check_tryFlatbed);
            this.groupBox1.Controls.Add(this.check_useAdf);
            this.groupBox1.Controls.Add(this.bt_testScanner);
            this.groupBox1.Controls.Add(this.lb_path);
            this.groupBox1.Controls.Add(this.lb_scanner);
            this.groupBox1.Controls.Add(this.bt_path);
            this.groupBox1.Controls.Add(this.bt_select);
            this.groupBox1.Controls.Add(this.combo_color);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.combo_dpi);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Scanner Settings";
            // 
            // check_tryFlatbed
            // 
            this.check_tryFlatbed.AutoSize = true;
            this.check_tryFlatbed.Location = new System.Drawing.Point(114, 145);
            this.check_tryFlatbed.Name = "check_tryFlatbed";
            this.check_tryFlatbed.Size = new System.Drawing.Size(177, 17);
            this.check_tryFlatbed.TabIndex = 51;
            this.check_tryFlatbed.Text = "Switch to flatbed if feeder empty";
            this.check_tryFlatbed.UseVisualStyleBackColor = true;
            this.check_tryFlatbed.Click += new System.EventHandler(this.check_tryFlatbed_Click);
            // 
            // check_useAdf
            // 
            this.check_useAdf.AutoSize = true;
            this.check_useAdf.Location = new System.Drawing.Point(114, 122);
            this.check_useAdf.Name = "check_useAdf";
            this.check_useAdf.Size = new System.Drawing.Size(152, 17);
            this.check_useAdf.TabIndex = 51;
            this.check_useAdf.Text = "Use auto document feeder";
            this.check_useAdf.UseVisualStyleBackColor = true;
            this.check_useAdf.Click += new System.EventHandler(this.check_useAdf_Click);
            // 
            // bt_testScanner
            // 
            this.bt_testScanner.Location = new System.Drawing.Point(123, 178);
            this.bt_testScanner.Name = "bt_testScanner";
            this.bt_testScanner.Size = new System.Drawing.Size(162, 23);
            this.bt_testScanner.TabIndex = 50;
            this.bt_testScanner.Text = "Test Scanner";
            this.bt_testScanner.UseVisualStyleBackColor = true;
            this.bt_testScanner.Click += new System.EventHandler(this.bt_testScanner_Click);
            // 
            // lb_path
            // 
            this.lb_path.AutoEllipsis = true;
            this.lb_path.Location = new System.Drawing.Point(111, 44);
            this.lb_path.Name = "lb_path";
            this.lb_path.Size = new System.Drawing.Size(135, 13);
            this.lb_path.TabIndex = 7;
            this.lb_path.Text = "Please select...";
            // 
            // lb_scanner
            // 
            this.lb_scanner.AutoEllipsis = true;
            this.lb_scanner.Location = new System.Drawing.Point(111, 20);
            this.lb_scanner.Name = "lb_scanner";
            this.lb_scanner.Size = new System.Drawing.Size(135, 13);
            this.lb_scanner.TabIndex = 7;
            this.lb_scanner.Text = "Please select...";
            // 
            // bt_path
            // 
            this.bt_path.Location = new System.Drawing.Point(260, 39);
            this.bt_path.Name = "bt_path";
            this.bt_path.Size = new System.Drawing.Size(25, 23);
            this.bt_path.TabIndex = 20;
            this.bt_path.Text = "...";
            this.bt_path.UseVisualStyleBackColor = true;
            this.bt_path.Click += new System.EventHandler(this.bt_path_Click);
            // 
            // bt_select
            // 
            this.bt_select.Location = new System.Drawing.Point(260, 15);
            this.bt_select.Name = "bt_select";
            this.bt_select.Size = new System.Drawing.Size(25, 23);
            this.bt_select.TabIndex = 10;
            this.bt_select.Text = "...";
            this.bt_select.UseVisualStyleBackColor = true;
            this.bt_select.Click += new System.EventHandler(this.bt_selectScanner_Click);
            // 
            // combo_color
            // 
            this.combo_color.FormattingEnabled = true;
            this.combo_color.Items.AddRange(new object[] {
            "Color",
            "Greyscale",
            "Color"});
            this.combo_color.Location = new System.Drawing.Point(114, 90);
            this.combo_color.Name = "combo_color";
            this.combo_color.Size = new System.Drawing.Size(171, 21);
            this.combo_color.TabIndex = 40;
            this.combo_color.Leave += new System.EventHandler(this.combo_color_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Save path:";
            // 
            // combo_dpi
            // 
            this.combo_dpi.FormattingEnabled = true;
            this.combo_dpi.Items.AddRange(new object[] {
            "100",
            "150",
            "200",
            "300",
            "400",
            "500"});
            this.combo_dpi.Location = new System.Drawing.Point(114, 67);
            this.combo_dpi.Name = "combo_dpi";
            this.combo_dpi.Size = new System.Drawing.Size(171, 21);
            this.combo_dpi.TabIndex = 30;
            this.combo_dpi.Leave += new System.EventHandler(this.combo_dpi_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(75, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "Feeder option:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Color:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 70);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Resolution:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Source:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.box_read);
            this.groupBox2.Controls.Add(this.check_minimized);
            this.groupBox2.Controls.Add(this.check_autostart);
            this.groupBox2.Controls.Add(this.box_pid);
            this.groupBox2.Controls.Add(this.box_vid);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.bt_connect);
            this.groupBox2.Location = new System.Drawing.Point(12, 231);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(300, 133);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Button Settings";
            // 
            // check_minimized
            // 
            this.check_minimized.AutoSize = true;
            this.check_minimized.Location = new System.Drawing.Point(10, 42);
            this.check_minimized.Name = "check_minimized";
            this.check_minimized.Size = new System.Drawing.Size(96, 17);
            this.check_minimized.TabIndex = 81;
            this.check_minimized.Text = "Start minimized";
            this.check_minimized.UseVisualStyleBackColor = true;
            this.check_minimized.Click += new System.EventHandler(this.check_minimized_Click);
            // 
            // check_autostart
            // 
            this.check_autostart.AutoSize = true;
            this.check_autostart.Location = new System.Drawing.Point(10, 19);
            this.check_autostart.Name = "check_autostart";
            this.check_autostart.Size = new System.Drawing.Size(180, 17);
            this.check_autostart.TabIndex = 81;
            this.check_autostart.Text = "Automatically connect on startup";
            this.check_autostart.UseVisualStyleBackColor = true;
            this.check_autostart.Click += new System.EventHandler(this.check_autostart_Click);
            // 
            // box_pid
            // 
            this.box_pid.Location = new System.Drawing.Point(239, 42);
            this.box_pid.Name = "box_pid";
            this.box_pid.Size = new System.Drawing.Size(46, 20);
            this.box_pid.TabIndex = 70;
            this.box_pid.Leave += new System.EventHandler(this.box_pid_Leave);
            // 
            // box_vid
            // 
            this.box_vid.Location = new System.Drawing.Point(239, 16);
            this.box_vid.Name = "box_vid";
            this.box_vid.Size = new System.Drawing.Size(46, 20);
            this.box_vid.TabIndex = 60;
            this.box_vid.Leave += new System.EventHandler(this.box_vid_Leave);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 1;
            this.label6.Text = "PID:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(205, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "VID:";
            // 
            // bt_connect
            // 
            this.bt_connect.Location = new System.Drawing.Point(202, 104);
            this.bt_connect.Name = "bt_connect";
            this.bt_connect.Size = new System.Drawing.Size(83, 23);
            this.bt_connect.TabIndex = 80;
            this.bt_connect.Text = "Connect";
            this.bt_connect.UseVisualStyleBackColor = true;
            this.bt_connect.Click += new System.EventHandler(this.bt_connect_Click);
            // 
            // mainNotify
            // 
            this.mainNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("mainNotify.Icon")));
            this.mainNotify.Text = "ServerScan - Disconnected";
            this.mainNotify.DoubleClick += new System.EventHandler(this.mainNotify_DoubleClick);
            // 
            // box_read
            // 
            this.box_read.Location = new System.Drawing.Point(217, 68);
            this.box_read.Name = "box_read";
            this.box_read.Size = new System.Drawing.Size(68, 20);
            this.box_read.TabIndex = 82;
            this.box_read.Leave += new System.EventHandler(this.box_read_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(64, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(147, 13);
            this.label8.TabIndex = 83;
            this.label8.Text = "Read size (use libusb toolkit) :";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 376);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "ServerScan";
            this.Resize += new System.EventHandler(this.Main_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox combo_color;
        private System.Windows.Forms.ComboBox combo_dpi;
        private System.Windows.Forms.Label lb_scanner;
        private System.Windows.Forms.Button bt_select;
        private System.Windows.Forms.Button bt_testScanner;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button bt_connect;
        private System.Windows.Forms.Label lb_path;
        private System.Windows.Forms.Button bt_path;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.FolderBrowserDialog pathBrowserDialog;
        private System.Windows.Forms.TextBox box_pid;
        private System.Windows.Forms.TextBox box_vid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox check_autostart;
        private System.Windows.Forms.CheckBox check_tryFlatbed;
        private System.Windows.Forms.CheckBox check_useAdf;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NotifyIcon mainNotify;
        private System.Windows.Forms.CheckBox check_minimized;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox box_read;
    }
}

