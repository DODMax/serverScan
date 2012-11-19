using System;
using System.Text;
using System.Threading;
using LibUsbDotNet;
using LibUsbDotNet.Main;

namespace ServerScan
{
    internal class USBRead
    {
        private static UsbDevice usbDevice = null;
        private static Boolean run = false;
        private static Thread oThread = new Thread(new ThreadStart(USBRead.ReadDevice));

        public static Boolean StartReading(Int32 vid, Int32 pid)
        {
            try
            {
                OpenDevice(vid, pid);
                oThread.Start();
                Thread.Sleep(1);
            }
            catch (Exception ex)
            {
                Program.ShowError(ex);
                return false;
            }

            return true;
        }

        public static void StopReading()
        {
            run = false;
            if (oThread.IsAlive)
                oThread.Join();
        }

        public static Boolean IsReading()
        {
            return run;
        }

        public static void ReadDevice()
        {
            if (usbDevice == null)
                Program.ShowError("Open device before trying to read.");            

            // open read endpoint 1.
            UsbEndpointReader reader = usbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
            Logger.Log("Starting listening to device endpoint " + reader.EpNum);

            try
            {
                run = true;
                ErrorCode ec = ErrorCode.None;
                byte[] readBuffer = new byte[128];
                while (usbDevice != null && run)
                {
                    int bytesRead;

                    // If the device hasn't sent data in the last 5 seconds,
                    // a timeout error (ec = IoTimedOut) will occur. 
                    ec = reader.Read(readBuffer, Program.config.ButtonReadSize, out bytesRead);

                    // Start scan on signal.
                    if (ec == ErrorCode.None && bytesRead != 0)
                    {
                        Logger.Log("Received button signal");
                        Scan.StartScan();
                    }
                }
            }
            finally
            {
                run = false;
                CloseDevice();
            }
        }

        public static void OpenDevice(Int32 vid, Int32 pid)
        {
            if (usbDevice != null)
                Program.ShowError("A device is already openned, please close it first.");

            Logger.Log("Connecting to device vid." + vid + " pid." + pid);
            UsbDeviceFinder usbFinder = new UsbDeviceFinder(vid, pid);

            // Find and open the usb device.
            usbDevice = UsbDevice.OpenUsbDevice(usbFinder);

            // If the device is open and ready
            if (usbDevice == null) throw new Exception("Device Not Found.");

            // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
            // it exposes an IUsbDevice interface. If not (WinUSB) the 
            // 'wholeUsbDevice' variable will be null indicating this is 
            // an interface of a device; it does not require or support 
            // configuration and interface selection.
            IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
            if (!ReferenceEquals(wholeUsbDevice, null))
            {
                // This is a "whole" USB device. Before it can be used, 
                // the desired configuration and interface must be selected.

                // Select config #1
                wholeUsbDevice.SetConfiguration(1);

                // Claim interface #0.
                wholeUsbDevice.ClaimInterface(0);
            }
        }

        public static void CloseDevice()
        {
            if (usbDevice != null)
            {
                if (usbDevice.IsOpen)
                {
                    Logger.Log("Closing device");

                    // If this is a "whole" usb device (libusb-win32, linux libusb-1.0)
                    // it exposes an IUsbDevice interface. If not (WinUSB) the 
                    // 'wholeUsbDevice' variable will be null indicating this is 
                    // an interface of a device; it does not require or support 
                    // configuration and interface selection.
                    IUsbDevice wholeUsbDevice = usbDevice as IUsbDevice;
                    if (!ReferenceEquals(wholeUsbDevice, null))
                    {
                        // Release interface #0.
                        wholeUsbDevice.ReleaseInterface(0);
                    }

                    usbDevice.Close();
                }
                usbDevice = null;

                // Free usb resources
                UsbDevice.Exit();
            }
        }
    }
}