using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Threading;
using System.IO;

namespace bdo_toolbox
{
    class Util
    {
        public static int availableVersion;
        public static int currentVersion; 
        public static void RunUpdater(string UpdaterPath)
        {
            Process.Start(UpdaterPath);

        }
        public static void CheckUpdate(string updatedPatcherURI, bool initialCheck)
        {
            try
            {
                WebClient web = new WebClient();
                availableVersion = int.Parse(web.DownloadString(updatedPatcherURI));
                currentVersion = MainWindow.Ver;
                //MessageBox.Show(availableVersion.ToString() + currentVersion.ToString());
                if (availableVersion > currentVersion)
                {
                    Message.PatcherUpdateNotice();
                }
                else
                {
                    if (!initialCheck)
                    {
                        Message.AlreadyLatest();
                    }
                }
                web.Dispose();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                ErrorLogWriter.WriteLog(ex);
            }
           
        }
        public static void WriteSettings()
        {
            //MessageBox.Show(MainWindow.IsUseMetaInjector.ToString());
            StreamWriter Write = new StreamWriter("config.ini", false);
            //Write.WriteLine("[key]=[content]");
            switch (MainWindow.Language)
            {
                default:
                    Write.WriteLine("Language=Japanese");
                    break;
                case "Japanese":
                    Write.WriteLine("Language=Japanese");
                    break;
            }
            Write.WriteLine("PatchMode=" + MainWindow.PatchMode);
            Write.WriteLine("PingDestination=" + MainWindow.PingDestination);
            Write.WriteLine("PingingTimeSpan=" + MainWindow.TimeSpanSec);
            Write.WriteLine("UpdatedPatcherURI=" + MainWindow.UpdatedPatcherURI);
            Write.WriteLine("PatchStreamURI=" + MainWindow.PatchStreamURI);
            Write.WriteLine("PatchInfoURI_JP=" + MainWindow.PatchInformationURI_JP);
            Write.WriteLine("PatchInfoURI_EN=" + MainWindow.PatchInformationURI_EN);
            Write.WriteLine("PatchInfoURI_TW=" + MainWindow.PatchInformationURI_TW);
            Write.WriteLine("PatchInfoURI_Gamez=" + MainWindow.PatchInformationURI_Gamez);
            Write.WriteLine("BDONAEU_ClientPath=" + MainWindow.BDONAEU_ClientPath);
            Write.WriteLine("BDOGamez_ClientPath=" + MainWindow.BDOGamez_ClientPath);
            Write.WriteLine("PatchFileName_TW=" + MainWindow.PatchFileName_TW);
            Write.WriteLine("PatchFileName_Gamez=" + MainWindow.PatchFileName_Gamez);
            Write.Close();
            Write.Dispose();

        }
        public static string GetPatchVersion(string region)
        {
            string patchversion = "";
            try
            {
                WebClient web = new WebClient();
                patchversion = web.DownloadString(region);
                web.Dispose();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "Patch : N/A";
            }
            return "Patch : " + patchversion;
        }

    }
}
