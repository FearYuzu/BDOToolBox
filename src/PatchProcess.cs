using BlackDesert.SharedLibs;
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
        static WebClient webclient = new WebClient();
        private string InstallPath;
        

        public static void JPForTaiwan(string InstallPath)
        {

            var DownloadFile = MainWindow.PatchStreamURI + MainWindow.PatchFileName_TW;
            processing pr = new processing();
            pr.Show();
            pr.ProcessBar.Maximum = 20;
            pr.ProcessBar.Minimum = 0;
            pr.ProcessBar.Value = 0;

            webclient.DownloadFile(DownloadFile, "data/prestringtable_tw.zip");
            ZipFile zipFile = ZipFile.Read("data/prestringtable_tw.zip");
            //if(!Directory.Exists(InstallPath + "/stringtable/"))
            //{
            //    Directory.CreateDirectory(InstallPath + "/stringtable/");
            //}
            zipFile["languagedata.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile["stringtable_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            //zipFile["stringtable_tw.xlsm"].Extract(InstallPath + "/stringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile["symbolnostringtable_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            //zipFile["symbolnostringtable_tw.xlsm"].Extract(InstallPath + "/stringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile["stringtable_cutscene_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            //zipFile["stringtable_cutscene_tw.xlsm"].Extract(InstallPath + "/stringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            //zipFile["pad00000.meta"].Extract(InstallPath + "/paz/", ExtractExistingFileAction.OverwriteSilently);
            zipFile["pearl.ttf"].Extract(InstallPath + "/prestringtable/font/", ExtractExistingFileAction.OverwriteSilently);

            zipFile.Dispose();
            zipFile = null;
            webclient.Dispose();

            pr.Visibility = Visibility.Hidden;
        }
        public static void ENForTaiwan(string InstallPath)
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
            processing pr = new processing();
            pr.Show();
            pr.ProcessBar.Maximum = 20;
            pr.ProcessBar.Minimum = 0;
            pr.ProcessBar.Value = 0;
            var p = new Progress<int>(ShowProgress);
            await Task.Run(() => GamezToJP_Process(p,InstallPath));
            MessageBox.Show("パッチインストールが完了しました。");
            pr.Close();
            
        }
        private static void GamezToJP_Process(IProgress<int> progress,string InstallPath)
        {
            WebClient Web = new WebClient();
            var DownloadFile = MainWindow.PatchStreamURI + MainWindow.PatchFileName_Gamez;
            Web.DownloadFile(DownloadFile, "data/JPModForGamez.zip");
            File.Copy(InstallPath + "/Paz/pad00000.meta", "data/gamez_pad00000.meta",true);
            progress.Report(15);
            ZipFile zipFile = ZipFile.Read("data/JPModForGamez.zip");
            progress.Report(25);
            zipFile["languagedata_en.txt"].Extract(InstallPath + "/stringtable/en/", ExtractExistingFileAction.OverwriteSilently);
            progress.Report(40);
            //if(File.Exists(InstallPath + "Paz/pad00000.meta"))
            //{
             //   File.Delete(InstallPath + "Paz/pad00000.meta");
            //}
            zipFile["pad00000.meta"].Extract(InstallPath + "/Paz/", ExtractExistingFileAction.OverwriteSilently);
            progress.Report(60);
            //if(File.Exists(InstallPath + "prestringtable/font/pearl.ttf"))
            //{
              //  File.Delete(InstallPath + "prestringtable/font/pearl.ttf");
            //}
            zipFile["pearl.ttf"].Extract(InstallPath + "/prestringtable/font/",ExtractExistingFileAction.OverwriteSilently);
            progress.Report(80);
            zipFile.Dispose();
            progress.Report(85);
            zipFile = null;
            Web.Dispose();
            progress.Report(95);
            Web = null;
            progress.Report(100);
            

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

            //pr.ProcessBar.Value = percent;
        }
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
