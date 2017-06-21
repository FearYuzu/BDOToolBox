using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;

namespace bdo_toolbox
{
    class ErrorLogWriter
    {
        public static void WriteLog(Exception ex) //
        {
            DateTime time = DateTime.Now;
            var sl = '_'; //Split
            var LogFolder = MainWindow.BDOToolBoxStartupPath + "\\Log\\";
            string FileTimeStamp = time.Year.ToString() + sl + time.Month.ToString() + sl + time.Day.ToString() + sl + time.Hour.ToString() + sl + time.Minute.ToString() + sl + time.Second.ToString();
            var LogFile = LogFolder + "ErrorLog_" + FileTimeStamp + ".txt";
            File.Create(LogFile).Close();
            StreamWriter sw = new StreamWriter(LogFile, true);
            sw.WriteLine("----------------Error Log-------------------------------");
            sw.WriteLine("\n" + DateTime.Now);
            sw.WriteLine("\nData:\n" + ex.Data);
            sw.WriteLine("\nHelpLink:\n" + ex.HelpLink);
            sw.WriteLine("\nHResult:\n" + ex.HResult);
            sw.WriteLine("\nInnerException:\n" + ex.InnerException);
            sw.WriteLine("\nMessage:\n" + ex.Message);
            sw.WriteLine("\nSource:\n" + ex.Source);
            sw.WriteLine("\nStackTrace:\n" + ex.StackTrace);
            sw.WriteLine("\nTargetSite:\n" + ex.TargetSite);
            sw.Close();
            sw = null;
        }
    }
}
