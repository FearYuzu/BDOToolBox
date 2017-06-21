using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace bdo_toolbox
{
    
    
    class PatchProcess
    {
        static MainWindow Main = new MainWindow();
        static WebClient webclient = new WebClient();
        static processing pr = new processing();
        private string InstallPath;
        static bool isInstalled_Gamez;
        static string targetServer;

        public static async Task TaiwanToJP(string InstallPath)
        {
            pr.Show();
            pr.ProcessBar.Maximum = 100;
            pr.ProcessBar.Minimum = 0;
            pr.ProcessBar.Value = 0;
            pr.ProgressBox.Text = "";
            var p = new Progress<int>(ShowProgress);
            await Task.Run(() => TaiwanToJP_Process(p, InstallPath));
            //MessageBox.Show("パッチインストールが完了しました。");

            pr.Visibility = Visibility.Hidden;
        }
        private static void TaiwanToJP_Process(IProgress<int> progress, string InstallPath)
        {
            WebClient Web = new WebClient();
            try
            {
                isInstalled_Gamez = File.Exists(InstallPath + "/BDOToolBoxPatch.Installed");
                targetServer = "Taiwan";
                progress.Report(5);
                var DownloadFile = MainWindow.PatchStreamURI + MainWindow.PatchFileName_TW;
                progress.Report(10);
                //pr.ProgressBox.AppendText("Patch File URI:");
                Web.DownloadFile(DownloadFile, "data/JPModForTW.zip");
                File.Create(InstallPath + "/BDOToolBoxPatch.Installed").Close();
                progress.Report(15);
                if (!File.Exists(InstallPath + "/BDOToolBoxPatch.Installed"))
                {
                    File.Copy(InstallPath + "/Paz/pad00000.meta", "data/tw_pad00000.meta", true);
                }
                progress.Report(20);
                ZipFile zipFile = ZipFile.Read("data/JPModForTW.zip");
                progress.Report(25);
                zipFile["languagedata_tw.txt"].Extract(InstallPath + "/stringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(40);
                zipFile["pad00000.meta"].Extract(InstallPath + "/Paz/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(60);
                zipFile["pearl.ttf"].Extract(InstallPath + "/prestringtable/font/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(80);
                zipFile.Dispose();
                progress.Report(85);
                zipFile = null;
                Web.Dispose();
                progress.Report(95);
                Web = null;
                progress.Report(100);

            }
            catch (ZipException ex)
            {
                Message.InvalidPatchPackage("aa");

            }
            catch (UnauthorizedAccessException ex)
            {
                Message.FailedByPermission();
            }
            catch (Exception ex)
            {
                Message.UnknownErrorWhilePatching();
                ErrorLogWriter.WriteLog(ex);
            }
        }
        public static void WesternToJP(string InstallPath)
        {
            webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/tw_en_latest.zip", "data/tw_en_latest.zip");
            ZipFile zipFile = ZipFile.Read("data/tw_en_latest.zip");
            zipFile.ExtractAll(InstallPath + "prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile.Dispose();
            zipFile = null;
            webclient.Dispose();
            webclient = null;

        }
        private void ProgressBarUpdate()
        {

        }
        public static void TaiwanMetaRefresh(string InstallPath)
        {
            File.Copy("data/pad00000.meta", InstallPath + "/paz/pad00000.meta", true);
        }
        public static async Task GamezToJP(string InstallPath)
        {
            
            pr.Show();
            pr.ProcessBar.Maximum = 100;
            pr.ProcessBar.Minimum = 0;
            pr.ProcessBar.Value = 0;
            pr.ProgressBox.Text = "";
            var p = new Progress<int>(ShowProgress);
            await Task.Run(() => GamezToJP_Process(p,InstallPath));
            //MessageBox.Show("パッチインストールが完了しました。");
            
            pr.Visibility = Visibility.Hidden;
            
        }
        private static void GamezToJP_Process(IProgress<int> progress,string InstallPath)
        {
            WebClient Web = new WebClient();
            try
            {
                isInstalled_Gamez = File.Exists(InstallPath + "/BDOToolBoxPatch.Installed");
                targetServer = "GamezBD";
                progress.Report(5);
                var DownloadFile = MainWindow.PatchStreamURI + MainWindow.PatchFileName_Gamez;
                progress.Report(10);
                //pr.ProgressBox.AppendText("Patch File URI:");
                Web.DownloadFile(DownloadFile, "data/JPModForGamez.zip");
                File.Create(InstallPath + "/BDOToolBoxPatch.Installed").Close();
                progress.Report(15);
                if (!File.Exists(InstallPath + "/BDOToolBoxPatch.Installed"))
                {
                    File.Copy(InstallPath + "/Paz/pad00000.meta", "data/gamez_pad00000.meta", true);
                }
                progress.Report(20);
                ZipFile zipFile = ZipFile.Read("data/JPModForGamez.zip");
                progress.Report(25);
                zipFile["languagedata_en.txt"].Extract(InstallPath + "/stringtable/en/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(40);
                zipFile["pad00000.meta"].Extract(InstallPath + "/Paz/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(60);
                zipFile["pearl.ttf"].Extract(InstallPath + "/prestringtable/font/", ExtractExistingFileAction.OverwriteSilently);
                progress.Report(80);
                zipFile.Dispose();
                progress.Report(85);
                zipFile = null;
                Web.Dispose();
                progress.Report(95);
                Web = null;
                progress.Report(100);
                
            }
            catch (ZipException ex)
            {
                Message.InvalidPatchPackage("aa");
                
            }
            catch (UnauthorizedAccessException ex)
            {
                Message.FailedByPermission();              
            }
            catch (Exception ex)
            {
                Message.UnknownErrorWhilePatching();
                ErrorLogWriter.WriteLog(ex);
            }
        }
        public static void GamezMetaRefresh(string InstallPath) //ランチャーエラー防止のため、Metaファイルを純正に戻す (Restore Original Meta File for prevent launcher error while updating the client.)
        {
            try
            {
                File.Copy("data/gamez_pad00000.meta", InstallPath + "/paz/pad00000.meta", true);
            }
            catch
            {
                MessageBox.Show("バックアップされているpad00000.metaが存在しません。");
            }
        }
        private static void ShowProgress(int percent)
        {
            pr.ProcessBar.Value = percent;
            switch (percent)
            {
                case 5:
                    pr.ProgressBox.AppendText("Webclient Initialized.\n");
                    break;
                case 10:
                    pr.ProgressBox.AppendText("Download File Determined...");
                    break;
                case 15:
                    pr.ProgressBox.AppendText("\nGetting Files...\n");
                    break;
                case 20:
                    if (isInstalled_Gamez)
                    {
                        pr.ProgressBox.AppendText("\n\nPatch were already installed, skipping backup....\n");
                    }
                    else
                    {
                        pr.ProgressBox.AppendText("\n\nBackup Original Meta Files....\n");
                    }
                    break;
                case 25:
                    pr.ProgressBox.AppendText("Reading Downloaded Patch Files....\n");
                    break;
                case 40:
                    switch (targetServer)
                    {
                        case "GamezBD":
                            pr.ProgressBox.AppendText("Installed... <languagedata_en.txt>\n");
                            break;
                        case "Korea":
                            pr.ProgressBox.AppendText("Installed... <languagedata_kr.txt>\n");
                            break;
                        case "Taiwan":
                            pr.ProgressBox.AppendText("Installed... <languagedata_tw.txt>\n");
                            break;
                        case "Western":
                            pr.ProgressBox.AppendText("Installed... <languagedata_en.txt>\n");
                            break;
                    }
                    break;
                case 60:
                    pr.ProgressBox.AppendText("Installed... <pad00000.meta>\n");
                    break;
                case 80:
                    pr.ProgressBox.AppendText("Installed... <pearl.ttf>\n");
                    break;
                case 85:
                    pr.ProgressBox.AppendText("Finalizing....\n");
                    break;
                case 100:
                    Message.FinishedPatching_Message();
                    break;
            }
        }
        public static void VerifyMetaFile(string InstallPath)
        {
            string backupMeta = "/data/gamez_pad00000.meta";
            string currentMeta = InstallPath + "Paz\\pad00000.meta";
            var bMetaFileInfo_Created = File.GetCreationTime(backupMeta);
            var cMetaFileInfo_Created = File.GetCreationTime(currentMeta);
            if(bMetaFileInfo_Created == cMetaFileInfo_Created)
            {
                Message.BackupMetaIsNotOriginal();
            }
        }
        public static void RepairOriginalMeta(string InstallPath)
        {
            WebClient webclient = new WebClient();
            StreamReader sr = new StreamReader(InstallPath + "version.dat");
            var DownloadMetaFile = "";
            var MetaVersionCheck = "";
            if(Main.targetsrv_gamez.IsChecked == true)
            {
                MetaVersionCheck = MainWindow.PatchStreamURI + "vanillameta/gamez/version.dat";
                int remoteMetaVersion = int.Parse(webclient.DownloadString(MetaVersionCheck));
                int localMetaVersion = int.Parse(sr.ReadToEnd());
                if(remoteMetaVersion < localMetaVersion)
                {
                    //

                }
                else
                {
                    DownloadMetaFile = MainWindow.PatchStreamURI + "vanillameta/gamez/pad00000.meta";
                }
            }
            
            try
            {
                webclient.DownloadFile(DownloadMetaFile, "/data/gamez_pad00000.meta");

            }
            catch
            {

            }
        }
        //
        /// <summary>
        /// 試験用関数エリア（Experimental Function Area）
        /// </summary>
        public static void BuiltinMetaInjection() //MetaインジェクションDLL組み込み試験用 (Experimental Function with Meta Injection DLL) 
        {
            MetaInjector.ReturnPatchedCount("pad00000.meta"); //Display Patched File Count to BDO ToolBox
            MessageBox.Show(MetaInjector.ReturnPatchedCount("pad00000.meta").ToString());
            //
            MetaInjector.CallRunPatcher(13); //Call Meta Injection on DLL
        }
    }
    class MetaInjector
    {
        [DllImport("BuiltinMetaInjector.dll", EntryPoint = "Process")]
        public static extern int Process();
        [DllImport("BuiltinMetaInjector.dll", EntryPoint = "CallRunPatcher")]
        public static extern void CallRunPatcher(int option);
        [DllImport("BuiltinMetaInjector.dll", EntryPoint = "ReturnPatchedCount")]
        public static extern int ReturnPatchedCount(string metapath);
    }
}
