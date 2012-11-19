using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServerScan
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            LoadProps();

            if (Program.config.StartMinimized)
                WindowState = FormWindowState.Minimized;

            if (Program.config.AutoConnect)
                ConnectButton();
        }

        public void LoadProps()
        {
            if (Program.config.ScannerID != "")
                lb_scanner.Text = Program.config.ScannerName;
            if (Program.config.ButtonVID != 0)
                box_vid.Text = Program.config.ButtonVID.ToString("X");
            if (Program.config.ButtonPID != 0)
                box_pid.Text = Program.config.ButtonPID.ToString("X");
            if (Program.config.ButtonReadSize != 0)
                box_read.Text = Program.config.ButtonReadSize.ToString();
            if (Program.config.SavePath != "")
                lb_path.Text = Program.config.SavePath;

            check_autostart.Checked = Program.config.AutoConnect;
            check_minimized.Checked = Program.config.StartMinimized;
            check_useAdf.Checked = Program.config.ScanADF;
            check_tryFlatbed.Checked = Program.config.ScanTryFlatbed;
            combo_dpi.SelectedItem = Program.config.ScanDpi.ToString();
            check_tryFlatbed.Enabled = check_useAdf.Checked;

            switch (Program.config.ScanColor)
            {
                case 4:
                    combo_color.SelectedItem = "Black & White";
                    break;
                case 2:
                    combo_color.SelectedItem = "Greyscale";
                    break;
                case 1:
                    combo_color.SelectedItem = "Color";
                    break;
            }
        }

        private void ConnectButton()
        {
            bt_connect.Enabled = false;
            bt_connect.Text = "Working...";

            if (!USBRead.IsReading())
            {
                if (USBRead.StartReading(Program.config.ButtonVID, Program.config.ButtonPID))
                {
                    box_vid.Enabled = false;
                    box_pid.Enabled = false;
                    bt_connect.Text = "Disconnect";
                    mainNotify.Text = "ServerScan - Connected";
                }
                else
                {
                    bt_connect.Text = "Retry";
                }
            }
            else
            {
                USBRead.StopReading();
                box_vid.Enabled = true;
                box_pid.Enabled = true;
                bt_connect.Text = "Connect";
                mainNotify.Text = "ServerScan - Disconnected";
            }
            
            bt_connect.Enabled = true;
        }

        private void bt_selectScanner_Click(object sender, EventArgs e)
        {
            Form select = new SelectScanner();
            select.Show();
        }

        private void bt_testScanner_Click(object sender, EventArgs e)
        {
            Scan.StartScan();
        }

        private void bt_path_Click(object sender, EventArgs e)
        {
            if (pathBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                Program.config.SavePath = pathBrowserDialog.SelectedPath;
                Program.config.Serialize();
                LoadProps();
            }
        }

        private void box_vid_Leave(object sender, EventArgs e)
        {
            try
            {
                Program.config.ButtonVID = Convert.ToInt32(box_vid.Text, 16);
            }
            catch
            {
                Program.config.ButtonVID = 0;
            }

            Program.config.Serialize();
        }

        private void box_pid_Leave(object sender, EventArgs e)
        {
            try
            {
                Program.config.ButtonPID = Convert.ToInt32(box_pid.Text, 16);
            }
            catch
            {
                Program.config.ButtonPID = 0;
            }

            Program.config.Serialize();
        }

        private void bt_connect_Click(object sender, EventArgs e)
        {
            ConnectButton();
        }

        private void check_autostart_Click(object sender, EventArgs e)
        {
            Program.config.AutoConnect = check_autostart.Checked;
            Program.config.Serialize();
        }

        private void check_useAdf_Click(object sender, EventArgs e)
        {
            Program.config.ScanADF = check_useAdf.Checked;
            Program.config.Serialize();
        }

        private void check_tryFlatbed_Click(object sender, EventArgs e)
        {
            Program.config.ScanTryFlatbed = check_tryFlatbed.Checked;
            Program.config.Serialize();
        }

        private void combo_color_Leave(object sender, EventArgs e)
        {
            switch (combo_color.SelectedItem.ToString())
            {
                case "Black & White":
                    Program.config.ScanColor = 4;
                    break;
                case "Greyscale":
                    Program.config.ScanColor = 2;
                    break;
                case "Color":
                    Program.config.ScanColor = 1;
                    break;
            }

            Program.config.Serialize();
        }

        private void combo_dpi_Leave(object sender, EventArgs e)
        {
            Program.config.ScanDpi = Convert.ToInt32(combo_dpi.SelectedItem);
            Program.config.Serialize();
        }

        private void check_useAdf_CheckStateChanged(object sender, EventArgs e)
        {
            check_tryFlatbed.Enabled = check_useAdf.Checked;
        }

        private void Main_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                Hide();
                mainNotify.Visible = true;
            }
        }

        private void mainNotify_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            mainNotify.Visible = false;
        }

        private void check_minimized_Click(object sender, EventArgs e)
        {
            Program.config.StartMinimized = check_minimized.Checked;
            Program.config.Serialize();
        }

        private void box_read_Leave(object sender, EventArgs e)
        {
            try
            {
                Program.config.ButtonReadSize = Convert.ToInt32(box_read.Text);
            }
            catch
            {
                Program.config.ButtonReadSize = 0;
            }

            Program.config.Serialize();
        }
    }
}
