using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ServerScan
{
    public partial class SelectScanner : Form
    {
        private Hashtable devices = null;

        public SelectScanner()
        {
            InitializeComponent();
            this.populateList();
        }

        private void populateList()
        {
            listScan.DisplayMember = "Value";
            listScan.ValueMember = "Key";
            try
            {
                devices = WIAScanner.GetDevices();
            }
            catch (Exception ex)
            {
                Program.ShowError(ex);
                return;
            }

            //listScan.Items.Add(new DictionaryEntry("0", "Fake scanner"));

            foreach (DictionaryEntry e in devices)
                listScan.Items.Add(e);
        }

        private void bt_cancell_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            if (listScan.SelectedItem != null)
            {
                DictionaryEntry device = (DictionaryEntry)listScan.SelectedItem;
                Program.config.ScannerID = device.Key.ToString();
                Program.config.ScannerName = device.Value.ToString();
                Program.config.Serialize();
                Program.GetInstance().LoadProps();
            }

            this.Close();
        }
    }
}
