using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace ServerScan
{
    class ScanSettings
    {
        public int dpi = 150;
        public int color = 2; //4 is black-white, gray is 2, color is 1
        public bool adf = false;
        public bool tryFlatbed = false; //try flatbed if adf fails
    }

    class Scan
    {
        public static void StartScan()
        {
            if (Program.config.SavePath == "")
            {
                Program.ShowError("Cannot start scanning, no save path defined");
                return;
            }

            if (!PathWritable(Program.config.SavePath))
            {
                Program.ShowError("Provided path is not writable");
                return;
            }

            if (Program.config.ScannerID == "")
            {
                Program.ShowError("Cannot start scanning, no scanner device selected.");
                return;
            }

            //Load settings
            ScanSettings settings = new ScanSettings();
            settings.color = Program.config.ScanColor;
            settings.dpi = Program.config.ScanDpi;
            settings.adf = Program.config.ScanADF;
            settings.tryFlatbed = Program.config.ScanTryFlatbed;

            try
            {
                SaveImages(WIAScanner.Scan(Program.config.ScannerID, settings));

                //Call garbage collector
                GC.Collect();
            }
            catch (Exception ex)
            {
                Program.ShowError(ex);
            }
        }

        private static Boolean PathWritable(String path)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(path);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
            catch (Exception)
            {
                Program.ShowError("Cannot access designated save path.");
                return false;
            }
        }

        private static int SaveImages(List<Image> list)
        {
            //Check if path still ok
            if (!PathWritable(Program.config.SavePath))
            {
                Program.ShowError("Provided path is not writable");
                return 0;
            }

            Logger.Log("Finished scanning " + list.Count + " files");
            DateTime now = DateTime.Now;
            String filename = Program.config.SavePath + "\\" + now.ToString("yy_MM_dd-H_mm_ss");

            int index = 1;
            foreach (Image img in list)
            {
                img.Save(filename + "-" + index++ + ".jpg",  System.Drawing.Imaging.ImageFormat.Jpeg);
                img.Dispose();
            }

            if (list.Count > 0)
                Logger.Log("Images saved in " + Program.config.SavePath);

            return list.Count;
        }
    }
}
