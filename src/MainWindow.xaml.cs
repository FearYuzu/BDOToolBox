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
using System.Threading.Tasks;

namespace bdo_toolbox
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private static event FileSystemEventHandler WatcherChanged;
        public static DateTime CurrentTime = DateTime.Now;
        FileSystemWatcher ConfigWatchDog = new FileSystemWatcher();
        static string BDOToolBoxBaseDir = Environment.GetCommandLineArgs()[0];
        public static string BDOToolBoxDirFullPath = System.IO.Path.GetFullPath(BDOToolBoxBaseDir);
        public static string BDOToolBoxStartupPath = System.IO.Path.GetDirectoryName(BDOToolBoxDirFullPath);
        public static string BDONAEU_ClientPath = "test";
        public static string BDOGamez_ClientPath = "";
        public static string PatchStreamURI = "";
        public static string PatchInformationURI_KR = "";
        public static string PatchInformationURI_JP = "http://cd.indigoflare.net/bdotb_-";
        public static string PatchInformationURI_EN = "";
        public static string PatchInformationURI_TW = "";
        public static string PatchInformationURI_Gamez = "";
        public static bool IsUseMetaInjector;
        public static string PatchFileName_KR = "";
        public static string PatchFileName_TW = "prestringtable_tw.zip";
        public static string PatchFileName_JP = "";
        public static string PatchFileName_EUNA = "";
        public static string PatchFileName_Gamez = "JPModForGamez.zip";
        private string ConfigureFileName = "config.ini";
        bool builtflag;
        public string Destination = "NA-Sanjose";
        public static int TimeSpanSec = 1;
        string return_value;
        public static string Language;
        public static string PingDestination;
        

        bool flg = true;
        
        public MainWindow()
        {
            InitializeComponent();
            
            //UIUpdate();
            Activated += (s, e) =>
            {
                if (flg)
                {
                    Version.Content = "Version 1.0.3 β";
                    string test = this.officialVersion();
                    //MessageBox.Show(test);
                    flg = false;
                    
                    Configure.LoadConfigure(ConfigureFileName);
                    Language = GetConfigureContent("Language");
                    PingDestination = GetConfigureContent("PingDestination");
                    TimeSpanSec = int.Parse(GetConfigureContent("PingingTimeSpan"));
                    BDONAEU_ClientPath = GetConfigureContent("BDONAEU_ClientPath");
                    BDOGamez_ClientPath = GetConfigureContent("BDOGamez_ClientPath");
                    PatchStreamURI = GetConfigureContent("PatchStreamURI");
                    PatchInformationURI_KR = GetConfigureContent("PatchInfoURI_KR");
                    PatchInformationURI_EN = GetConfigureContent("PatchInfoURI_EN");
                    PatchInformationURI_JP = GetConfigureContent("PatchInfoURI_JP");
                    PatchInformationURI_TW = GetConfigureContent("PatchInfoURI_TW");
                    PatchInformationURI_Gamez = GetConfigureContent("PatchInfoURI_Gamez");
                    PatchFileName_TW = GetConfigureContent("PatchFileName_TW");
                    PatchFileName_KR = GetConfigureContent("PatchFileName_KR");
                    PatchFileName_Gamez = GetConfigureContent("PatchFileName_Gamez");
                    UIUpdate();
                    updatePatchInfo();
                                     
                }
            };
            //Debug
            
        }
        void config_Closing(object sender, CancelEventArgs e)
        {
            UIUpdate();
        }
       
        public void UIUpdate()
        {
            Configure.LoadConfigure(ConfigureFileName);
            Language = GetConfigureContent("Language");
            PingDestination = GetConfigureContent("PingDestination");
            TimeSpanSec = int.Parse(GetConfigureContent("PingingTimeSpan"));
            this.Dispatcher.BeginInvoke(new Action(() => //直接UIを更新できないのでDisPatcher経由で更新
            {
                
                switch (Language)
                {
                    case "Japanese":
                        
                        
                        targersrv_kr.Content = "韓国";
                        targetsrv_jp.Content = "日本";
                        targetsrv_tw.Content = "台灣";
                        targetsrv_eu.Content = "欧米";
                        Install.Content = "パッチインストール";
                        Uninstall.Content = "パッチアンインストール";
                        Ping_Show.Content = "Ping表示";
                        DefineUIContent.UILang_English = "英語";
                        DefineUIContent.UILang_Japanese = "日本語";
                        DefineUIContent.UILang_HanS = "簡体中国語";
                        DefineUIContent.UILang_HanT = "繁体中国語";
                        DefineUIContent.UIPatchUseMetaInjector = "MetaInjectorを用いてパッチを適用する";
                        DefineUIContent.UIPatchServer = "パッチサーバーアドレス";
                        break;
                    case "English":
                        targersrv_kr.Content = "Korea";
                        targetsrv_jp.Content = "Japan";
                        targetsrv_tw.Content = "Taiwan";
                        targetsrv_eu.Content = "EU/NA";
                        Install.Content = "Install Patch";
                        Uninstall.Content = "Uninstall Patch";
                        Ping_Show.Content = "Show Ping";
                        DefineUIContent.UILang_English = "English";
                        DefineUIContent.UILang_Japanese = "Japanese";
                        DefineUIContent.UILang_HanS = "Simplified Chinese";
                        DefineUIContent.UILang_HanT = "Traditional Chinese";
                        DefineUIContent.UIPatchUseMetaInjector = "Use MetaInjector for Patch";
                        DefineUIContent.UIPatchServer = "Patch Server Address";
                        break;
                    case "HanT":
                        
                        
                        targersrv_kr.Content = "韓服";
                        targetsrv_jp.Content = "日服";
                        targetsrv_tw.Content = "台服";
                        targetsrv_eu.Content = "欧美服";
                        Install.Content = "安裝補丁";
                        Uninstall.Content = "卸載補丁";
                        Ping_Show.Content = "顯示Ping";
                        DefineUIContent.UILang_English = "英語";
                        DefineUIContent.UILang_Japanese = "日語";
                        DefineUIContent.UILang_HanS = "簡體中文";
                        DefineUIContent.UILang_HanT = "繁體中文";
                        DefineUIContent.UIPatchUseMetaInjector = "使用MetaInjector";
                        DefineUIContent.UIPatchServer = "補丁服務器地址";

                        break;
                    case "HanS":
                        
                        targersrv_kr.Content = "韩服";
                        targetsrv_jp.Content = "日服";
                        targetsrv_tw.Content = "台服";
                        targetsrv_eu.Content = "欧美服";
                        Install.Content = "安装补丁";
                        Uninstall.Content = "卸载补丁";
                        Ping_Show.Content = "显示Ping";
                        DefineUIContent.UILang_English = "英语";
                        DefineUIContent.UILang_Japanese = "日语";
                        DefineUIContent.UILang_HanS = "简体中文";
                        DefineUIContent.UILang_HanT = "繁体中文";
                        DefineUIContent.UIPatchUseMetaInjector = "使用MetaInjector";
                        DefineUIContent.UIPatchServer = "补丁服务器地址";
                        break;
                }
            }));

        }
        private string GetConfigureContent(string Key)　//設定ファイルからロードしListに格納したデータを取り出し
        {
            string return_value = "";
            for (int i = 0; i < Configure.ConfigureTable.Count; i++) //設定格納List内を検索
            {
                string preload = Configure.ConfigureTable[i].Key; 
                
                
                if (String.Equals(Key,preload)) //検索対象と検索結果が一致したら
                {
                    
                    return_value = Configure.ConfigureTable[i].Content;
                   // MessageBox.Show("loaded. " + return_value);

                }

            }
            return return_value; //一致した検索結果を返す

        }
        private void applyBtn_Click(object sender, EventArgs e)
        {
            UIUpdate();
            string InstallPath = this.gameInstallPath();
            bool isPathValid = Directory.Exists(InstallPath);
            

            if (isPathValid)
            {
                bool IsThereStringtable = Directory.Exists(InstallPath + "\\stringtable");
                if (IsThereStringtable)
                {
                    try
                    {
                        Directory.Delete(InstallPath + "\\stringtable", true);
                    }
                    catch
                    {
                    }
                }
                bool IsTherePrestringtable = !Directory.Exists(InstallPath + "\\prestringtable");
                if (IsTherePrestringtable)
                {
                    Directory.CreateDirectory(InstallPath + "\\prestringtable");
                    //MessageBox.Show("prestringtable created");
                }
                bool flag4 = !Directory.Exists(InstallPath + "\\prestringtable\\tw");
                if (flag4)
                {
                    Directory.CreateDirectory(InstallPath + "\\prestringtable\\tw");
                   // MessageBox.Show("prestringtable_tw created");
                }
                bool font = !Directory.Exists(InstallPath + "\\prestringtable\\font");
                if (font)
                {
                    Directory.CreateDirectory(InstallPath + "\\prestringtable\\font");
                   // MessageBox.Show("font created");
                }
                this.startPatching(InstallPath);
               
            }
            else
            {
                InstallFolder_NotFound();
            }
        }
        // here you got pup op dialgos with errors
        private void deleteBtn_Click(object sender, EventArgs e)
        {
            string folder = this.gameInstallPath();
            bool flag = folder != "" && Directory.Exists(folder);
            if (flag)
            {
                bool flag2 = Directory.Exists(folder + "\\prestringtable");
                if (flag2)
                {
                    try
                    {
                        Directory.Delete(folder + "\\prestringtable", true);
                                                
                    }
                    catch (Exception ex)
                    {
                        int num = (int)System.Windows.MessageBox.Show(string.Format("パッチャーフォルダを削除できません。, エラーコード:{0}", ex.Message));
                        return;
                    }
                    //int num2 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                    PatchFolder_Deleted();
                }
                else
                {
                    //int num3 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                    PatchFolder_Deleted();
                }
            }
            else
            {
                InstallFolder_NotFound();
                //int num4 = (int)System.Windows.MessageBox.Show("");
            }
        }
        private void RepairClient_Click(object sender,EventArgs e)
        {
            MessageBox.Show(gameInstallPath());
        }
        private void startPatching(string folder)
        {
            
            if(targetsrv_tw.IsChecked == true)
            {
                var InstallPath = gameInstallPath();
                try
                {
                    PatchProcess.JPForTaiwan(InstallPath);
                }
                catch
                {
                    ResetPatch();
                    PatchProcess.JPForTaiwan(InstallPath);
                }
                
                FinishedPatching_Message();
            }
            if(targetsrv_tw.IsChecked == true)
            {
                var InstallPath = gameInstallPath();
                try
                {
                    PatchProcess.ENForTaiwan(InstallPath);
                }
                catch
                {
                    ResetPatch();
                    PatchProcess.ENForTaiwan(InstallPath);
                }
            }
            if(targetsrv_gamez.IsChecked == true)
            {
                var InstallPath = gameInstallPath();
                try
                {
                    PatchProcess.GamezToJP(InstallPath);
                    //FinishedPatching_Message();
                }
                catch
                {
                    ResetPatch();
                    PatchProcess.GamezToJP(InstallPath);
                    //FinishedPatching_Message();
                }
            }
            if (targetsrv_jp.IsChecked == true)
            {
                
            }
            // Target Server: Japan | Target Language: English
            if (targetsrv_jp.IsChecked == true)
            {
                
            }
            
            if (targetsrv_jp.IsChecked == true)
            {
                UnAvailable_Message();
            } 
            {

            }
            if (targetsrv_eu.IsChecked == true)
            {
                UnAvailable_Message();
            }
        }
        
        
        //private bool checkReadOnly(string folder) // UNUSED
        //{
        //    FileInfo fileInfo = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "stringtable_en.xlsm"));
        //    FileInfo fileInfo2 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "stringtable_cutscene_en.xlsm"));
        //    FileInfo fileInfo3 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "symbolnostringtable_en.xlsm"));
        //    FileInfo fileInfo4 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "LanguageData.xlsm"));
        //    bool exists = fileInfo.Exists;
        //    bool result;
        //    if (exists)
        //    {
        //        bool isReadOnly = fileInfo.IsReadOnly;
        //        if (isReadOnly)
        //        {
        //            try
        //            {
        //                fileInfo.IsReadOnly = false;
        //            }
        //            catch
        //            {
        //                int num = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo.FullName));
        //                result = true;
        //                return result;
        //            }
        //        }
        //    }
        //    bool exists2 = fileInfo2.Exists;
        //    if (exists2)
        //    {
        //        bool isReadOnly2 = fileInfo2.IsReadOnly;
        //        if (isReadOnly2)
        //        {
        //            try
        //            {
        //                fileInfo2.IsReadOnly = false;
        //            }
        //            catch
        //            {
        //                int num2 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo2.FullName));
        //                result = true;
        //                return result;
        //            }
        //        }
        //    }
        //    bool exists3 = fileInfo3.Exists;
        //    if (exists3)
        //    {
        //        bool isReadOnly3 = fileInfo3.IsReadOnly;
        //        if (isReadOnly3)
        //        {
        //            try
        //            {
        //                fileInfo3.IsReadOnly = false;
        //            }
        //            catch
        //            {
        //                int num3 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo3.FullName));
        //                result = true;
        //                return result;
        //            }
        //        }
        //    }
        //    bool exists4 = fileInfo4.Exists;
        //    if (exists4)
        //    {
        //        bool isReadOnly4 = fileInfo4.IsReadOnly;
        //        if (isReadOnly4)
        //        {
        //            try
        //            {
        //                fileInfo4.IsReadOnly = false;
        //            }
        //            catch
        //            {
        //                int num4 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo4.FullName));
        //                result = true;
        //                return result;
        //            }
        //        }
        //    }
        //    result = false;
        //    return result;
        //}

        private void updatePatchInfo()
        {
            DateTime fileTime = this.getFileTime();
            string text = this.officialVersion();
            string localver = this.localVersion();
            string arg = (text != "") ? text : "N/A";
            //this.PatchInfo.Content = string.Format("Last Updated:\r\n{0}\r\n\r\nInformation:\r\n{1}\r\n\r\nInstalled Version:\r\n{2}", string.Format("{0}", (fileTime.Year == 1970) ? "N/A" : string.Format("Date: {0}\r\nTime: {1}", fileTime.ToString("MM/dd/yy"), fileTime.ToString("hh:mm:ss tt"))), arg, this.localVersion());
            this.PatchInfo.Content = text;
        }

        private string officialVersion()
        {
            WebClient Download = new WebClient();
            
            string result;

            if (targersrv_kr.IsChecked == true)
            {
                return_value = Download.DownloadString(PatchInformationURI_KR);

            }
            if(targetsrv_eu.IsChecked == true)
            {
                return_value = Download.DownloadString(PatchInformationURI_EN);
            }
            result = return_value;
            return result;
        }

        private string localVersion()
        {
            string text = this.gameInstallPath();
            string result = "N/A";
            bool flag = Directory.Exists(text) && File.Exists(text + "version.dat");
            if (flag)
            {
                result = File.ReadAllText(text + "version.dat").Split(new char[]
                {
                    '\n'
                })[0];
            }
            return result;
        }

        // HERE YOU PUT REGEDIT KEY !!!!
        private string gameInstallPath()
        {
            var TargetSrv = "" ;
            string KeyNameJP = string.Format("HKEY_CURRENT_USER\\SOFTWARE\\GameOn\\Pmang\\BlackDesert_live", Wow.Is64BitOperatingSystem ? "Wow6432Node\\" : "");
            //string keyNameNAEU = string.Format("HKEY_LOCAL_MACHINE\\SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{C1F96C92-7B8C-485F-A9CD-37A0708A2A60}", Wow.Is64BitOperatingSystem ? "Wow6432Node\\" : "");
            string keyNameKR = string.Format("HKEY_CURRENT_USER\\SOFTWARE\\DaumGames\\black", Wow.Is64BitOperatingSystem ? "Wow6432Node\\" : "");
            string keyNameTW = string.Format("HKEY_LOCAL_MACHINE\\SOFTWARE\\WOW6432Node\\BlackDesert");
            //var test = (string)Registry.GetValue(keyNameTW, "path", "");
            //debug_twpath.Content = "台湾鯖クライアント位置：\n" + test;

            if (targetsrv_jp.IsChecked == true)
            {
                TargetSrv = (string)Registry.GetValue(KeyNameJP, "location", "");
            }
            if (targetsrv_eu.IsChecked == true)
            {
                //TargetSrv = (string)Registry.GetValue(keyNameNAEU, "InstallLocation", "");
                TargetSrv = BDONAEU_ClientPath;
            }
            if(targetsrv_tw.IsChecked == true)
            {
                TargetSrv = (string)Registry.GetValue(keyNameTW, "path", "");
            }
            if(targetsrv_gamez.IsChecked == true)
            {
                TargetSrv = BDOGamez_ClientPath;
               // TargetSrv = "F:\\testbdo\\Black Desert Online\\";
            }
           // MessageBox.Show("TargetSrv is " + TargetSrv);
            return TargetSrv;
            
        }

        private DateTime getFileTime()
        {
            string text = "";
            
            bool flag = !string.IsNullOrWhiteSpace(text);
            DateTime result;
            if (flag)
            {
                result = new DateTime(1970, 1, 1, 0, 0, 0, 0);//ToLocalTime().AddSeconds((double)long.Parse(text));
            }
            else
            {
                result = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            }
            return result;
        }

        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Uninstall_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MetaRefresh_Click(object sender, RoutedEventArgs e)
        {
            var InstallPath = gameInstallPath();
            if (targetsrv_gamez.IsChecked == true)
            {
                PatchProcess.GamezMetaRefresh(InstallPath);
            }
            if(targetsrv_tw.IsChecked == true)
            {
                PatchProcess.TaiwanMetaRefresh(InstallPath);
            }
            
            MetaRefresh_Done();
            //PatchProcess.BuiltinMetaInjection();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PingShow_Click(object sender, RoutedEventArgs e)
        {
            UIUpdate();
            Ping Ping = new Ping();
            Ping.Show();
        }
        public void UnAvailable_Message()
        {
            switch (Language)
            {
                case "Japanese":
                    MessageBox.Show("この機能は現在未実装です。", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "English":
                    MessageBox.Show("This Function is Unavailable now.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "T_Chinese":
                    MessageBox.Show("這個功能是現在無法使用。", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "S_Chinese":
                    MessageBox.Show("这个功能是现在无法使用。", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        public void PingingTimeSpan_NotAllowed()
        {
            switch (Language)
            {
                case "Japanese":
                    MessageBox.Show("各国サービスの認証サーバを利用しPing計測を行っている場合、サーバー負荷への配慮のため3秒以下へのPing間隔の設定は出来ません。3秒へと自動的に変更されます。", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "English":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "T_Chinese":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "S_Chinese":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        private void FinishedPatching_Message()
        {
            switch (Language)
            {
                case "English":
                    MessageBox.Show("Patching is Finished");
                    break;
                case "Japanese":
                    MessageBox.Show("パッチインストールが完了しました。");
                    break;
                case "S_Chinese":
                    MessageBox.Show("补丁安装完成了。");
                    break;
                case "T_Chinese":
                    MessageBox.Show("補丁安裝完成了。");
                    break;
            }
        }
        private void InstallFolder_NotExist()
        {
            switch (Language)
            {
                case "English":
                    MessageBox.Show("Patching is Finished");
                    break;
                case "Japanese":
                    MessageBox.Show("パッチインストールが完了しました。");
                    break;
                case "S_Chinese":
                    MessageBox.Show("补丁安装完成了。");
                    break;
                case "T_Chinese":
                    MessageBox.Show("補丁安裝完成了。");
                    break;
            }
        }
        private void InstallFolder_NotFound()
        {
            switch (Language)
            {
                case "English":
                    
                    MessageBox.Show("The game was not Installed.");
                    break;
                case "Japanese":
                    MessageBox.Show("ゲームがインストールされていません。");
                    break;
                case "S_Chinese":
                    MessageBox.Show("安裝文件夾不能找到。");
                    break;
                case "T_Chinese":
                    MessageBox.Show("安裝文件夾不能找到。");
                    break;


            }
        }
        private void RoutingAssigner_Guides()
        {
            switch (Language)
            {
                case "Japanese":
                    MessageBox.Show("ルーティングアサイナは黒い砂漠のためにルーティング設定を変更します。\nコマンドプロンプトのrouteコマンドを使用するため、管理者権限が必要です。\nさらにルーティングアサイナを使いルーティング設定を変更すると、一度インターネット接続がリセットされます。\nたとえば、あなたがオンラインゲームを遊んでいた場合、\nサーバーから切断されます。", "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "English":
                    MessageBox.Show("Attention:\n\nRouting Assigner will give the routing settings for Black Desert to your PC.\nmust be run as administrator because Routing Assigner will use route commands.\nand your internet connection will be reset a once if performed the routing change.\nfor example, will be disconnected from server if you are playing a online games.\n", "Attention", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "T_Chinese":
                    MessageBox.Show("注意：\n\n路由分配將給予黑色沙漠的路由設置到PC上。必須以管理員身份運行，因為路由分配器將使用route命令。\n如果執行路由的變化,\n你的互聯網連接將被重置一次。\n例如，如果你正在玩網絡遊戲,將從服務器斷開連接。", "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "S_Chinese":
                    MessageBox.Show("注意：\n\n路由分配将给予黑色沙漠的路由设置到PC上。必须以管理员身份运行，因为路由分配器将使用route命令。 \n如果执行路由的变化,\n你的互联网连接将被重置一次。 \n例如，如果你正在玩网络游戏,将从服务器断开连接。", "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;


        }
        }
        private void MetaRefresh_Done()
        {
            switch (Language)
            {
                default:
                    MessageBox.Show("Meta Refresh Finished.");
                    break;
                case "Japanese":
                    MessageBox.Show("Metaファイルのリフレッシュが完了しました。");
                    break;
            }
        }
        private void PatchFolder_Deleted()
        {
            switch (Language)
            {
                case "English":
                    MessageBox.Show("Folder was deleted successfully.");
                    break;
                case "Japanese":
                    MessageBox.Show("フォルダは正常に削除されました。");
                    break;
                case "S_Chinese":
                    MessageBox.Show("文件夹已成功删除。");
                    break;
                case "T_Chinese":
                    MessageBox.Show("文件夾已成功刪除。");
                    break;


            }
        }
        private void ResetPatch()
        {
            string text = this.gameInstallPath();
            bool flag = text != "" && Directory.Exists(text);
            if (flag)
            {
                bool flag2 = Directory.Exists(text + "\\prestringtable");
                if (flag2)
                {
                    try
                    {
                        Directory.Delete(text + "\\prestringtable", true);
                    }
                    catch (Exception ex)
                    {
                        int num = (int)System.Windows.MessageBox.Show(string.Format("Cannot Delete the Patch Folder., Err Code:{0}", ex.Message));
                        return;
                    }
                    //int num2 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                }
                else
                {
                    //int num3 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                }
            }
            else
            {
                // int num4 = (int)System.Windows.MessageBox.Show("インストールフォルダが見つかりません。");
            }
        }
        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            // 下がったキーがAキーの場合
            
            if (e.Key == Key.C)
            {
               
            }

        }
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            bdo_toolbox.config conf = new config();
            conf.Show();
            StartWatchdog();
        }

        private void targetsrv_jp_Checked(object sender, RoutedEventArgs e)
        {

        }
        void StartWatchdog()
        {
            
            string directoryPath = BDOToolBoxStartupPath;
            string fileName = "config.ini";

            try
            {
                // ConfigWatchDog = new FileSystemWatcher();

                //監視するディレクトリを指定
                ConfigWatchDog.Path = directoryPath;

                //最終更新日時の変更のみを監視する
                ConfigWatchDog.NotifyFilter = NotifyFilters.LastWrite;

                //CheckFile.txtを監視
                ConfigWatchDog.Filter = fileName;

                //イベントハンドラの追加
                ConfigWatchDog.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);
                ConfigWatchDog.Created += new System.IO.FileSystemEventHandler(watcher_Changed);
                ConfigWatchDog.Deleted += new System.IO.FileSystemEventHandler(watcher_Changed);

                //監視を開始する
                ConfigWatchDog.EnableRaisingEvents = true;
            }
            catch
            {

            }
        }
        private void lang_en_Checked(object sender, RoutedEventArgs e)
        {
            WebClient Download = new WebClient();
            string result;
            return_value = Download.DownloadString(PatchInformationURI_EN).Replace("\\n", "");
            this.PatchInfo.Content = return_value;
        }
        private string FailedToReceivedDataFromServer()
        {
            var unavailable_message ="";
            string return_value;
            switch(Language)
            {
                case "Japanese":
                    unavailable_message = "サーバーからデータを受信できませんでした。";
                break;
                case "English":
                    unavailable_message = "couldn't received the data from server.";
                break;
                case "T_Chinese":
                    unavailable_message = "無法從服務器接收數據";
                break;
                case "S_Chinese":
                    unavailable_message = "无法从服务器接收数据";
                break;


            }
            return unavailable_message;
        }
        private void watcher_Changed(System.Object source, System.IO.FileSystemEventArgs e)
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                switch (e.ChangeType)
                {
                    case System.IO.WatcherChangeTypes.Changed:
                        UIUpdate();
                        // ConfigWatchDog.Dispose();
                        // ConfigWatchDog = null;
                        break;
                    case System.IO.WatcherChangeTypes.Created:
                        UIUpdate();
                        // ConfigWatchDog.Dispose();
                        // ConfigWatchDog = null;
                        break;
                    case System.IO.WatcherChangeTypes.Deleted:
                        UIUpdate();
                        //  ConfigWatchDog.Dispose();
                        // ConfigWatchDog = null;
                        break;
                }
                
            }));
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string pushedKey = "";
            switch (e.Key)
            {
                case Key.F1:
                    //DebugRefresh_JPToTCN();
                    break;
                case Key.F2:
                case Key.F3:
                case Key.F4:
                case Key.F5:
                case Key.F6:
                case Key.F7:
                case Key.F8:
                case Key.Escape:
                    Environment.Exit(0);
                    break;
                case Key.V:
                    UnAvailable_Message();
                    break;
                case Key.D:UnAvailable_Message();
                    break;
                case Key.C:
                    bdo_toolbox.config conf = new config();
                    conf.Show();
                    string directoryPath = BDOToolBoxStartupPath;
                    string fileName = "config.ini";

                    try
                    {
                        // ConfigWatchDog = new FileSystemWatcher();

                        //監視するディレクトリを指定
                        ConfigWatchDog.Path = directoryPath;

                        //最終更新日時の変更のみを監視する
                        ConfigWatchDog.NotifyFilter = NotifyFilters.LastWrite;

                        //CheckFile.txtを監視
                        ConfigWatchDog.Filter = fileName;

                        //イベントハンドラの追加
                        ConfigWatchDog.Changed += new System.IO.FileSystemEventHandler(watcher_Changed);
                        ConfigWatchDog.Created += new System.IO.FileSystemEventHandler(watcher_Changed);
                        ConfigWatchDog.Deleted += new System.IO.FileSystemEventHandler(watcher_Changed);

                        //監視を開始する
                        ConfigWatchDog.EnableRaisingEvents = true;
                    }
                    catch
                    {

                    }
                    break;
                case Key.F11:
                case Key.F12:
                    pushedKey = e.Key.ToString();
                    break;
                case Key.System:
                    if (e.SystemKey == Key.F10)
                    {
                        pushedKey = "F10";
                    }
                    break;
            }
            
        }
        private void lang_tcn_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                WebClient Download = new WebClient();
                string result;
                return_value = Download.DownloadString(PatchInformationURI_TW).Replace("\\n", "");
                this.PatchInfo.Content = return_value;
            }
            catch
            {
                this.PatchInfo.Content = FailedToReceivedDataFromServer();
            }
            
        }

        private void lang_ja_Checked(object sender, RoutedEventArgs e)
        {
            if (targetsrv_eu.IsChecked == true)
            {
                try
                {
                    WebClient Download = new WebClient();
                    string result;
                    return_value = Download.DownloadString(PatchInformationURI_JP);
                    this.PatchInfo.Content = return_value;
                }
                catch
                {

                    this.PatchInfo.Content = FailedToReceivedDataFromServer();
                }
            }

           
        }
        //private void DebugRefresh_JPToTCN()
        //{
        //    string text = this.gameInstallPath();
        //    bool flag = text != "" && Directory.Exists(text);
        //    if (flag)
        //    {
        //        bool flag2 = Directory.Exists(text + "\\prestringtable");
        //        if (flag2)
        //        {
        //            try
        //            {
        //                Directory.Delete(text + "\\prestringtable", true);
        //            }
        //            catch (Exception ex)
        //            {
        //                //int num = (int)System.Windows.MessageBox.Show(string.Format("パッチャーフォルダを削除できません。, エラーコード:{0}", ex.Message));
        //                return;
        //            }
        //            //int num2 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
        //            // PatchFolder_Deleted();
        //        }
        //        else
        //        {
        //            //int num3 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
        //            // PatchFolder_Deleted();
        //        }
        //    }
        //    else
        //    {
        //        // InstallFolder_NotFound();
        //        //int num4 = (int)System.Windows.MessageBox.Show("");
        //    }
        //    //
        //    //
        //    UIUpdate();
        //    text = this.gameInstallPath();
        //    flag = Directory.Exists(text);
        //    bdo_toolbox.config conf = new bdo_toolbox.config();

        //    if (flag)
        //    {
        //        bool flag2 = Directory.Exists(text + "\\stringtable");
        //        if (flag2)
        //        {
        //            try
        //            {
        //                Directory.Delete(text + "\\stringtable", true);
        //            }
        //            catch
        //            {
        //            }
        //        }
        //        bool flag3 = !Directory.Exists(text + "\\prestringtable");
        //        if (flag3)
        //        {
        //            Directory.CreateDirectory(text + "\\prestringtable");
        //        }
        //        bool flag4 = !Directory.Exists(text + "\\prestringtable\\jp");
        //        if (flag4)
        //        {
        //            Directory.CreateDirectory(text + "\\prestringtable\\jp");
        //        }
        //        //bool flag5 = this.officialVersion().Equals(this.localVersion());
        //        //if (flag5)
        //        //{
        //        WebClient webclient = new WebClient();
        //        webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_TCN.zip", "data/LD_JP_TCN.zip");
        //        try
        //        {
        //            ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
        //            zipFile["LanguageData.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["stringtable_cutscene_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["stringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["symbolnostringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile.Dispose();
        //            zipFile = null;
        //        }
        //        catch
        //        {
        //            ResetPatch();
        //            ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
        //            zipFile["LanguageData.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["stringtable_cutscene_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["stringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile["symbolnostringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
        //            zipFile.Dispose();
        //            zipFile = null;

        //        }
        //        FinishedPatching_Message();
        //        webclient.Dispose();
        //        webclient = null;
        //    }
        //}

        private void targetsrv_eu_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RepairClient_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void targetsrv_gamez_Checked(object sender, RoutedEventArgs e)
        {
            bool isBDOClientFolder = Directory.Exists(BDOGamez_ClientPath + "/Paz/") && Directory.Exists(BDOGamez_ClientPath + "/bin64/");
            if (BDOGamez_ClientPath == "" || isBDOClientFolder == false)
            {
                MessageBox.Show("フォルダの初期設定が必要です。Gamezのゲームクライアントフォルダを指定してください。", "Attention", MessageBoxButton.OK, MessageBoxImage.Information);
                System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
                fbd.Description = "Select Folder";
                fbd.RootFolder = Environment.SpecialFolder.Desktop;
                fbd.SelectedPath = "";
                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BDOGamez_ClientPath = fbd.SelectedPath;
                    if(Directory.Exists(fbd.SelectedPath + "/Paz/") && Directory.Exists(fbd.SelectedPath + "/bin64/") == false)
                    {
                        MessageBox.Show("黒い砂漠のクライアントフォルダではありません。再度指定しなおしてください。","Error",MessageBoxButton.OK,MessageBoxImage.Warning);
                        targetsrv_gamez.IsChecked = false;
                    }
                    WriteSettings();
                }
                else
                {
                    targetsrv_gamez.IsChecked = false;
                }
            }
            try
            {
                WebClient Download = new WebClient();
                string result;
                return_value = Download.DownloadString(PatchInformationURI_Gamez);
                this.info.Content = return_value;
            }
            catch
            {

                this.info.Content = FailedToReceivedDataFromServer();
            }
        }
        public void WriteSettings()
        {
            //MessageBox.Show(MainWindow.IsUseMetaInjector.ToString());
            StreamWriter Write = new StreamWriter("config.ini", false);
            //Write.WriteLine("[key]=[content]");
            switch (Language)
            {
                default:
                    Write.WriteLine("Language=Japanese");
                    break;
                case "Japanese":
                    Write.WriteLine("Language=Japanese");
                    break;
            }
            Write.WriteLine("PingDestination=" + MainWindow.PingDestination);
            Write.WriteLine("PingingTimeSpan=" + MainWindow.TimeSpanSec);
            Write.WriteLine("PatchStreamURI=" + PatchStreamURI);
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
    }
}
