using System;
using System.IO;

namespace ServerScan
{
    class Logger
    {
        private const String fileName = "log.txt";

        private static bool firstLine = true;
        private static StreamWriter log;

        public static void Log(String str)
        {
            if (!File.Exists(fileName))
            {
                log = new StreamWriter(fileName);
            }
            else
            {
                log = File.AppendText(fileName);
            }

            if (firstLine)
            {
                log.WriteLine();
                firstLine = false;
            }

            log.WriteLine(DateTime.Now + ": " + str);
            log.Close();
        }
    }
}
