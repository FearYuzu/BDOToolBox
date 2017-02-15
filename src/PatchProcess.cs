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

namespace bdo_toolbox
{
    class PatchProcess
    {
        static WebClient webclient = new WebClient();
        private string InstallPath;

        public static void JPForTaiwan(string InstallPath)
        {


            processing pr = new processing();
            pr.Show();
            pr.ProcessBar.Maximum = 20;
            pr.ProcessBar.Minimum = 0;
            pr.ProcessBar.Value = 0;
            pr.ProgressBox.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProgressBox.AppendText("パッチファイルをダウンロードしています。");

            }));

            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;

            }));
            pr.ProgressBox.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProgressBox.AppendText("http://files.indigoflare.net/download/bdo/prestringtable_tw.zip");

            }));

            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;

            }));
            webclient.DownloadFile("http://files.indigoflare.net/download/bdo/prestringtable_tw.zip", "data/prestringtable_tw.zip");
            pr.ProgressBox.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProgressBox.AppendText("ダウンロード完了");

            }));
            
            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;
                pr.ProcessBar.Value++;

            }));
            
            pr.ProgressBox.AppendText("パッチインストールの準備をしています。");
            ZipFile zipFile = ZipFile.Read("data/prestringtable_tw.zip");
            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;
                pr.ProcessBar.Value++;

            }));
            pr.ProgressBox.AppendText("パッチをインストール中です。\n");
            zipFile["languagedata.xlsm"].Extract(InstallPath + "/prestringtable/tw/",ExtractExistingFileAction.OverwriteSilently);
            
            pr.ProgressBox.AppendText("インストール中... languagedata.xlsm\n");
            Thread.Sleep(100);
            pr.ProcessBar.Value++;
            pr.ProcessBar.Value++;
            zipFile["stringtable_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            pr.ProgressBox.AppendText("インストール中... stringtable_tw.xlsm\n");
            Thread.Sleep(100);
            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;
                pr.ProcessBar.Value++;

            }));
            zipFile["symbolnostringtable_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            pr.ProgressBox.AppendText("インストール中... symbolnostringtable_tw.xlsm\n");
            Thread.Sleep(100);
            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;
                pr.ProcessBar.Value++;

            }));
            zipFile["stringtable_cutscene_tw.xlsm"].Extract(InstallPath + "/prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            pr.ProgressBox.AppendText("インストール中... stringtable_cutscene_tw.xlsm\n");
            Thread.Sleep(100);
            pr.ProcessBar.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                pr.ProcessBar.Value++;
                pr.ProcessBar.Value++;

            }));
            zipFile["pearl.ttf"].Extract(InstallPath + "/prestringtable/font/", ExtractExistingFileAction.OverwriteSilently);
            pr.ProgressBox.AppendText("インストール中... pearl.ttf\n");
            Thread.Sleep(100);
            pr.ProgressBox.AppendText("インストールの終了処理をしています。。");
            zipFile.Dispose();
            zipFile = null;
            
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
    }
}
