using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Drawing;
using WIA;

namespace ServerScan
{
    class WIAScanner
    {
        /*
         * Many thanks to Miljenko Barbir for his source code which this class is widely based on.
         * http://miljenkobarbir.com/using-a-scanner-without-dialogs-in-net/
         * And to iCopy project and their source code that helped me to debug most problems.
         * http://icopy.svn.sourceforge.net/viewvc/icopy/trunk/iCopy/Classes/Scanner.vb
         */

        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatJPG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";

        class WIA_DPS_DOCUMENT_HANDLING_SELECT
        {
            public const int FEEDER         = 0x001;
            public const int FLATBED        = 0x002;
            public const int DUPLEX         = 0x004;
            public const int FRONT_FIRST    = 0x008;
            public const int BACK_FIRST     = 0x010;
            public const int FRONT_ONLY     = 0x020;
            public const int BACK_ONLY      = 0x040;
            public const int NEXT_PAGE      = 0x080;
            public const int PREFEED        = 0x100;
            public const int AUTO_ADVANCE   = 0x200;
        }

        class WIA_DPS_DOCUMENT_HANDLING_STATUS
        {
            public const int FEED_READY     = 0x01;
            public const int FLAT_READY     = 0x02;
            public const int DUP_READY      = 0x04;
            public const int FLAT_COVER_UP  = 0x08;
            public const int PATH_COVER_UP  = 0x10;
            public const int PAPER_JAM      = 0x20;
        }

        class WIA_ERRORS
        {
            public const uint BASE_VAL_WIA_ERROR = 0x80210000;
            public const uint WIA_ERROR_GENERAL_ERROR = BASE_VAL_WIA_ERROR + 1;
            public const uint WIA_ERROR_PAPER_JAM = BASE_VAL_WIA_ERROR + 2;
            public const uint WIA_ERROR_PAPER_EMPTY = BASE_VAL_WIA_ERROR + 3;
            public const uint WIA_ERROR_BUSY = BASE_VAL_WIA_ERROR + 6;
        }

        class WIA_PROPERTIES
        {
            public const int WIA_RESERVED_FOR_NEW_PROPS = 1024;
            public const int WIA_DIP_FIRST = 2;
            public const int WIA_DPA_FIRST = WIA_DIP_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const int WIA_DPC_FIRST = WIA_DPA_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            // Scanner only device properties (DPS)
            public const int WIA_DPS_FIRST = WIA_DPC_FIRST + WIA_RESERVED_FOR_NEW_PROPS;
            public const int WIA_DPS_DOCUMENT_HANDLING_STATUS = WIA_DPS_FIRST + 13;
            public const int WIA_DPS_DOCUMENT_HANDLING_SELECT = WIA_DPS_FIRST + 14;
            public const int WIA_DPS_PAGES = WIA_DPS_FIRST + 22;
        }



        /// <summary>
        /// Use scanner to scan an image (with user selecting the scanner from a dialog).
        /// </summary>
        /// <returns>Scanned images.</returns>
        public static List<Image> Scan(ScanSettings settings)
        {
            ICommonDialog dialog = new CommonDialog();
            Device device = null;
            try
            {
                device = dialog.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, true, false);
            }
            catch (Exception)
            {
                throw new Exception("Cannot initialize scanner selection window. No WIA scanner installed?");
            }

            if (device != null)
            {
                return Acquire(device, settings);
            }
            else
            {
                throw new Exception("You must first select a WIA scanner.");
            }
        }

        /// <summary>
        /// Use scanner provided scanner id to scan an image.
        /// </summary>
        /// <returns>Scanned images.</returns>
        public static List<Image> Scan(String scannerId, ScanSettings settings)
        {
            // select the correct scanner using the provided scannerId parameter
            DeviceManager manager = new DeviceManager();
            Device device = null;
            foreach (DeviceInfo info in manager.DeviceInfos)
            {
                if (info.DeviceID == scannerId)
                {
                    try
                    {   //Check connection to scanner
                        device = info.Connect();
                    }
                    catch (Exception)
                    {
                        throw new Exception("Cannot connect to scanner, please check your device and try again.");
                    }

                    break;
                }
            }

            // device was not found
            if (device == null)
            {
                throw new Exception("The provided scanner device could not be found.");
            }

            return Acquire(device, settings);
        }

        /// <summary>
        /// Gets the list of available WIA devices.
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetDevices()
        {
            Hashtable devices = new Hashtable();
            DeviceManager manager = null;
            try
            {
                manager = new DeviceManager();
            }
            catch (Exception)
            {
                throw new Exception("Cannot initialize WIA device manager.\nMake sure wiaaut.dll is present in your system32 directory and that it is registered (run 'regsvr32 wiaaut.dll').");
            }

            foreach (DeviceInfo info in manager.DeviceInfos)
            {
                String name = info.Properties["Name"].get_Value().ToString();
                devices.Add(info.DeviceID, name);
            }

            return devices;
        }

        /*===================
         * Scanning routines 
         *===================*/

        private static void SetDeviceHandling(ref Device device, ScanSettings settings)
        {
            Logger.Log("Setup device handling");

            try
            {   //Setup ADF vs Flatbed
                if (settings.adf)
                    SetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER);
                else
                    SetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED);
            }
            catch (Exception ex)
            {
                Logger.Log("Cannot configure scanner mode properly\n    " + ex);
            }
        }

        private static void SetDeviceProperties(ref Device device, ScanSettings settings)
        {
            Logger.Log("Setup device properties");

            // configure scanning properties
            Item scan = device.Items[1] as Item;
            foreach (Property prop in scan.Properties)
            {
                switch (prop.PropertyID)
                {
                    case 6146: //4 is Black-white, gray is 2, color 1
                        SetProperty(prop, settings.color);
                        break;
                    case 6147: //dots per inch/horizontal 
                        SetProperty(prop, settings.dpi);
                        break;
                    case 6148: //dots per inch/vertical 
                        SetProperty(prop, settings.dpi);
                        break;
                    case 6149: //x point where to start scan 
                        SetProperty(prop, 0);
                        break;
                    case 6150: //y-point where to start scan 
                        SetProperty(prop, 0);
                        break;
                    case 6151: //horizontal exent 
                        SetProperty(prop, (int)(8.5 * settings.dpi));
                        break;
                    case 6152: //vertical extent 
                        SetProperty(prop, 11 * settings.dpi);
                        break;
                }
            }
        }

        /// <summary>
        /// Use scanner to scan an image (scanner is selected by its unique id).
        /// </summary>
        /// <param name="scannerId"></param>
        /// <returns>List of scanned images.</returns>
        private static List<Image> Acquire(Device device, ScanSettings settings)
        {
            String description = device.Properties["Name"].get_Value().ToString();

            if (description.ToLower().Contains("brother") || description.Contains("Canon MF4500"))
            {
                Logger.Log("Starting acquisition (Brother)");
                return AcquireBrother(device, settings);
            }
            else
            {
                Logger.Log("Starting acquisition (Normal)");
                return AcquireNormal(device, settings);
            }
        }

        private static List<Image> AcquireBrother(Device device, ScanSettings settings)
        {
            List<Image> images = new List<Image>();
            bool hasMorePages = true;
            Item scan = null;

            SetDeviceHandling(ref device, settings);
            SetDeviceProperties(ref device, settings);

            try
            {   //Connect to scanner
                scan = device.Items[1] as Item;
            }
            catch (Exception)
            {
                throw new Exception("Cannot connect to scanner, please check your device and try again.");
            }

            //Acquisition iteration
            ICommonDialog wiaCommonDialog = new CommonDialog();
            while (hasMorePages)
            {
                Logger.Log("DEBUG: document handling " + GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT));
                Logger.Log("DEBUG: feeder status " + GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS));

                try
                {
                    //Some scanner need WIA_DPS_PAGES to be set to 1, otherwise all pages are acquired but only one is returned as ImageFile
                    SetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_PAGES, 1);

                    //Scan image
                    ImageFile image = (ImageFile)wiaCommonDialog.ShowTransfer(scan, wiaFormatBMP, false);

                    if (image != null)
                    {
                        // convert to byte array
                        Byte[] imageBytes = (byte[])image.FileData.get_BinaryData();

                        // add file to output list
                        images.Add(Image.FromStream(new MemoryStream(imageBytes)));

                        //Cleanup
                        image = null;
                        imageBytes = null;
                    }
                    else
                    {
                        Logger.Log("Scan cancelled");
                        break;
                    }

                    // assume there are no more pages
                    hasMorePages = false;
                    if (settings.adf)
                    {
                        try
                        {   //try to read feed ready property (some scanners report ready even if no more pages)
                            int status = GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS);
                            hasMorePages = (status & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0;
                        }
                        catch { }
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    switch ((uint)ex.ErrorCode)
                    {
                        case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY:
                            Logger.Log("Paper feed empty");
                            if (images.Count == 0 && settings.adf && settings.tryFlatbed)
                            {   //if no page scanned try flatbed instead
                                SetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT, WIA_DPS_DOCUMENT_HANDLING_SELECT.FLATBED);
                                settings.adf = false;
                            }
                            else
                                hasMorePages = false;
                            break;

                        case WIA_ERRORS.WIA_ERROR_PAPER_JAM:
                            Program.ShowError("Paper jam inside the scanner feeder");
                            break;

                        case WIA_ERRORS.WIA_ERROR_BUSY:
                            Logger.Log("Device is busy, retrying in 2s...");
                            System.Threading.Thread.Sleep(2000);
                            break;

                        default:
                            throw ex;
                    }
                }
            }

            device = null;
            return images;
        }

        private static List<Image> AcquireNormal(Device device, ScanSettings settings)
        {
            DeviceManager manager = new DeviceManager();
            List<Image> images = new List<Image>();
            bool hasMorePages = true;
            Item scan = null;

            //Acquisition iteration
            ICommonDialog wiaCommonDialog = new CommonDialog();
            while (hasMorePages)
            {
                try
                {   //Looks like these need to be done for each iteration
                    SetDeviceHandling(ref device, settings);
                    scan = device.Items[1] as Item;
                    SetDeviceProperties(ref device, settings);
                }
                catch (Exception)
                {
                    throw new Exception("Cannot connect to scanner, please check your device and try again.");
                }

                Logger.Log("DEBUG: document handling " + GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT));
                Logger.Log("DEBUG: feeder status " + GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS));

                try
                {
                    //Scan image
                    ImageFile image = (ImageFile)wiaCommonDialog.ShowTransfer(scan, wiaFormatBMP, false);

                    if (image != null)
                    {
                        // convert to byte array
                        Byte[] imageBytes = (byte[])image.FileData.get_BinaryData();

                        // add file to output list
                        images.Add(Image.FromStream(new MemoryStream(imageBytes)));

                        //Cleanup
                        image = null;
                        imageBytes = null;
                    }
                    else
                    {
                        Logger.Log("Scan cancelled");
                        break;
                    }

                    // assume there are no more pages
                    hasMorePages = false;
                    if (settings.adf)
                    {
                        try
                        {   //try to read feed ready property (some scanners report ready even if no more pages)
                            int status = GetDeviceIntProperty(ref device, WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS);
                            hasMorePages = (status & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0;

                            Logger.Log("ADF has more pages: " + (hasMorePages ? "Yes" : "No"));
                        }
                        catch { }
                    }
                }
                catch (System.Runtime.InteropServices.COMException ex)
                {
                    switch ((uint)ex.ErrorCode)
                    {
                        case WIA_ERRORS.WIA_ERROR_PAPER_EMPTY:
                            Logger.Log("Paper feed empty");
                            if (images.Count == 0 && settings.adf && settings.tryFlatbed)
                            {   //if no page scanned try try flatbed
                                settings.adf = false;
                            }
                            else
                                hasMorePages = false;
                            break;

                        case WIA_ERRORS.WIA_ERROR_PAPER_JAM:
                            Program.ShowError("Paper jam inside the scanner feeder");
                            break;

                        case WIA_ERRORS.WIA_ERROR_BUSY:
                            Logger.Log("Device is busy, retrying in 2s...");
                            System.Threading.Thread.Sleep(2000);
                            break;

                        default:
                            throw ex;
                    }
                }
            }

            device = null;
            return images;
        }


        private static void SetProperty(Property property, int value)
        {
            IProperty x = (IProperty)property;
            Object val = value;
            x.set_Value(ref val);
        }

        private static void SetDeviceIntProperty(ref Device device, int propertyID, int propertyValue)
        {
            foreach (Property p in device.Properties)
            {
                if (p.PropertyID == propertyID)
                {
                    object value = propertyValue;
                    p.set_Value(ref value);
                    break;
                }
            }
        }

        private static int GetDeviceIntProperty(ref Device device, int propertyID)
        {
            int ret = -1;

            foreach (Property p in device.Properties)
            {
                if (p.PropertyID == propertyID)
                {
                    ret = (int)p.get_Value();
                    break;
                }
            }

            return ret;
        }
    }
}
