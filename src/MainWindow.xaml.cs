using BlackDesert.SharedLibs;
using BlackDesert_Patcher.Properties;
using BlackDesert_Patcher;
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
        bool builtflag;
       
        string return_value;
        public string Language = null;
        //private ObservableCollection<MainWindow.Language> languages = new ObservableCollection<MainWindow.Language>();
        public string PingDestination ="";
        private IWebProxy proxySetting;
        bool flg = true;
        private IContainer components;
        private ListBox languageList;
        public MainWindow()
        {
            InitializeComponent();
            // ConfigWatchDog.Filter = "config.ini";
            // ConfigWatchDog.Path = "/";
            //ConfigWatchDog.IncludeSubdirectories = false;
            //WaitForChangedResult WatchDog = ConfigWatchDog.WaitForChanged(WatcherChangeTypes.Changed);
            //if (WatchDog.TimedOut)
            // {
            //     return;
            // }
            // switch (WatchDog.ChangeType)
            //{
            //     case WatcherChangeTypes.Changed:
            //        ReadIni();
            //       break;
            // }
            //this.Dispatcher.BeginInvoke(new Action(() =>
           // {
                
                


                    //イベントハンドラの削除
                    
               // }
               // finally
               // {
                 //   if (ConfigWatchDog != null)
                 //   {
                    //後始末
                    //ConfigWatchDog.Changed -= watcher_Changed;
                    //ConfigWatchDog.Dispose();
                       // ConfigWatchDog = null;
                  //  }
                //}
        //
            //UIスレッドで実行すべき処
       //}));
           
            
            Activated += (s, e) =>
            {
                if (flg)
                {
                    string test = this.officialVersion();
                    //MessageBox.Show(test);
                    flg = false;
                    ReadIni();
                    updatePatchInfo();
                }
            };
        }
        void config_Closing(object sender, CancelEventArgs e)
        {
            ReadIni();
        }
       
        public void ReadIni()
        {
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                StreamReader SettingRead = new StreamReader(new FileStream("config.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                string stresult = string.Empty;

                var ReadLineCount = 0;
                string sttemp = string.Empty;
                bdo_toolbox.config conf = new config();
                while (SettingRead.Peek() >= 0)
                {
                    // ファイルを 1 行ずつ読み込む
                    string stBuffer = SettingRead.ReadLine();
                    if (stBuffer.Contains("UILang = Japanese"))
                    {
                       
                        lang_en.Content = "英語";
                        lang_ja.Content = "日本語";
                        lang_scn.Content = "中国語(簡体)";
                        lang_tcn.Content = "中国語(繁体α）";
                        lang_ru.Content = "ロシア語";
                        targersrv_kr.Content = "韓国";
                        targetsrv_eu.Content = "北米/欧州";
                        targetsrv_jp.Content = "日本";
                        targetsrv_ru.Content = "ロシア";
                        Install.Content = "パッチインストール";
                        Uninstall.Content = "パッチアンインストール";
                        RoutingAssigner.Content = "ルーティングアサイナ";
                        Ping_Show.Content = "Ping表示";
                        Language = "Japanese";
                    }
                    if (stBuffer.Contains("UILang = English"))
                    {
                        
                        Language = "English";
                        
                        lang_en.Content = "English";
                        lang_ja.Content = "Japanese";
                        lang_scn.Content = "Chinese(simplified)";
                        lang_tcn.Content = "Chinese(traditional α)";
                        lang_ru.Content = "Russian";
                        targersrv_kr.Content = "Korea";
                        targetsrv_eu.Content = "EU/NA";
                        targetsrv_jp.Content = "Japan";
                        targetsrv_ru.Content = "Russia";
                        Install.Content = "Install Patch";
                        Uninstall.Content = "Uninstall Patch";
                        RoutingAssigner.Content = "Routing Assigner";
                        Ping_Show.Content = "Show Ping";
                        
                    }
                    if (stBuffer.Contains("UILang = T_Chinese"))
                    {
                        
                        Language = "T_Chinese";
                        
                        lang_en.Content = "英語";
                        lang_ja.Content = "日本語";
                        lang_scn.Content = "簡體中文";
                        lang_tcn.Content = "繁體中文(α)";
                        lang_ru.Content = "俄語";
                        targersrv_kr.Content = "韓服";
                        targetsrv_eu.Content = "欧美服";
                        targetsrv_jp.Content = "日服";
                        targetsrv_ru.Content = "俄服";
                        Install.Content = "安裝補丁";
                        Uninstall.Content = "卸載補丁";
                        RoutingAssigner.Content = "路由分配器";
                        Ping_Show.Content = "顯示Ping";
                        
                    }
                    if (stBuffer.Contains("UILang = S_Chinese"))
                    {
                        
                        Language = "S_Chinese";
                        
                        lang_en.Content = "英语";
                        lang_ja.Content = "日语";
                        lang_scn.Content = "简体中文";
                        lang_tcn.Content = "繁体中文(α)";
                        lang_ru.Content = "俄语";
                        targersrv_kr.Content = "韩服";
                        targetsrv_eu.Content = "欧美服";
                        targetsrv_jp.Content = "日服";
                        targetsrv_ru.Content = "俄服";
                        Install.Content = "安装补丁";
                        Uninstall.Content = "卸载补丁";
                        RoutingAssigner.Content = "路由分配器";
                        Ping_Show.Content = "显示Ping";
                        
                    }
                    if (stBuffer.Contains("UseBuiltData = 1"))
                    {
                        builtflag = true;
                    }

                }
                SettingRead.Close();
                SettingRead.Dispose();
                SettingRead = null;
            }));
    

        }
        private void applyBtn_Click(object sender, EventArgs e)
        {
            ReadIni();
            string text = this.gameInstallPath();
            bool flag = Directory.Exists(text);
            bdo_toolbox.config conf = new bdo_toolbox.config();

            if (flag)
            {
                bool flag2 = Directory.Exists(text + "\\stringtable");
                if (flag2)
                {
                    try
                    {
                        Directory.Delete(text + "\\stringtable", true);
                    }
                    catch
                    {
                    }
                }
                bool flag3 = !Directory.Exists(text + "\\prestringtable");
                if (flag3)
                {
                    Directory.CreateDirectory(text + "\\prestringtable");
                }
                bool flag4 = !Directory.Exists(text + "\\prestringtable\\eu");
                if (flag4)
                {
                    Directory.CreateDirectory(text + "\\prestringtable\\eu");
                }
                //bool flag5 = this.officialVersion().Equals(this.localVersion());
                //if (flag5)
                //{
                
                if(builtflag == true)
                {
                    
                    this.startPatchingNoBuild(text);
                }
                else
                {
                    this.startPatchingNoBuild(text);
                }
                

                //}
                //else
                //{
                //	int num = (int)MessageBox.Show("Proszę zaktualizować grę przed próbą generowania patcha.");
                //}
            }
            else
            {
                InstallFolder_NotFound();
            }
        }
        // here you got pup op dialgos with errors
        private void deleteBtn_Click(object sender, EventArgs e)
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
        private void RepaitClient_Click(object sender,EventArgs e)
        {
        }
        private void startPatchingNoBuild(string folder)
        {
            
            WebClient webclient = new WebClient();
            if (targetsrv_jp.IsChecked == true)
            {
                
            }
            // Target Server: Japan | Target Language: English
            if (targetsrv_jp.IsChecked == true && lang_en.IsChecked == true)
            {
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_EN.zip", "data/LD_JP_EN.zip");
                try
                {
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;
                }
                catch
                {
                    ResetPatch();
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;

                }
                FinishedPatching_Message();
                webclient.Dispose();
                webclient = null;
            }
            
            if (targetsrv_jp.IsChecked == true && lang_scn.IsChecked == true)
            {
                UnAvailable_Message();
            } 
            {

            }
            if (targetsrv_eu.IsChecked == true)
            {
                UnAvailable_Message();
            }
            // Target Server: Japan | Target Language: Japanese(Modified)
            if (targetsrv_jp.IsChecked == true && lang_ja.IsChecked == true)
            {
                
                //webClient.DownloadDataAsync(new Uri(string.Format("http://files.indigoflare.net/bdotoolbox/patch/patch.zip")));
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_EN.zip", "data/LD_JP_EN.zip");
                try
                {
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;
                }
                catch
                {
                    ResetPatch();
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;
                }
                FinishedPatching_Message();
                webclient.Dispose();
                webclient = null;
            }
           
            
            // Target Server: Japan | Target Language: Traditional Chinese
            if (targetsrv_jp.IsChecked == true && lang_tcn.IsChecked == true)
            {

                //webClient.DownloadDataAsync(new Uri(string.Format("http://files.indigoflare.net/bdotoolbox/patch/patch.zip")));
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_TCN.zip", "data/LD_JP_TCN.zip");
                try
                {
                    
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;
                }
                catch
                {
                    ResetPatch();
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;

                }
                FinishedPatching_Message();
                webclient.Dispose();
                webclient = null;
            }
            // Targer Server: EU/NA | Target Language: Japanese
            if (targetsrv_eu.IsChecked == true && lang_ja.IsChecked == true)
            {

                //webClient.DownloadDataAsync(new Uri(string.Format("http://files.indigoflare.net/bdotoolbox/patch/patch.zip")));
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_EN_JP.zip", "data/LD_EN_JP.zip");
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/paz_mod.zip","data/paz_mod.zip");
                try
                {
                    //Original PAZ Backup for recover the client state, won't do it if patch already installed. for protect the original PAZ.
                    if (!File.Exists(folder + "PatchInstalled.bdotoolbox"))
                    {
                        File.Copy(folder + "paz/pad00000.meta", "data/backup/pad00000.meta");
                        File.Copy(folder + "paz/PAD03254.PAZ", "data/backup/PAD03254.PAZ");
                        File.Copy(folder + "paz/PAD03256.PAZ", "data/backup/PAD03256.PAZ");
                        File.Copy(folder + "paz/PAD03307.PAZ", "data/backup/PAD03307.PAZ");
                    }
                    ZipFile zipFile = ZipFile.Read("data/LD_EN_JP.zip");
                    ZipFile zip_pazmod = ZipFile.Read("data/paz_mod.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["stringtable_cutscene_en.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["stringtable_en.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["symbolnostringtable_en.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile.Dispose();
                    zip_pazmod.ExtractAll(folder + "paz",ExtractExistingFileAction.OverwriteSilently);
                    zip_pazmod.Dispose();
                    File.Create(folder + "PatchInstalled.bdotoolbox");
                    zip_pazmod = null;
                    zipFile = null;
                    
                }
                catch
                {
                    ResetPatch();
                    ZipFile zipFile = ZipFile.Read("data/LD_EN_JP.zip");
                    ZipFile zip_pazmod = ZipFile.Read("data/paz_mod.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/en/");
                    zipFile.Dispose();
                    zipFile = null;
                    zip_pazmod.ExtractAll(folder + "paz", ExtractExistingFileAction.OverwriteSilently);
                    zip_pazmod.Dispose();
                    zip_pazmod = null;

                }
                FinishedPatching_Message();
                webclient.Dispose();
                webclient = null;
            }
            //end


        }
        private void startPatching(string folder)
        {
            if(targetsrv_jp.IsChecked == true)
            {
                
            }
            if(targetsrv_eu.IsChecked == true)
            {
                
            }
            bool flag = this.checkReadOnly(folder);
            if (!flag)
            {
                MemoryStream xlsmTraslations = new MemoryStream();
                MemoryStream languagedataTranslations = new MemoryStream();
                //try
                //{
                    
                    //download.Text = string.Format("Downloading {0} Patch", ((MainWindow.Language)this.languageList.SelectedValue).Display);
                    WebClient webClient = new WebClient();
                    webClient.Proxy = this.proxySetting;
                    webClient.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
                    {
                       // download.progressBarForm.Value = int.Parse(Math.Truncate(double.Parse(e.BytesReceived.ToString()) / double.Parse(e.TotalBytesToReceive.ToString()) * 100.0).ToString());
                    };
                    webClient.DownloadDataCompleted += delegate (object sender, DownloadDataCompletedEventArgs e)
                    {
                        //ZipFile zipFile = ZipFile.Read(new MemoryStream(e.Result));
                        //zipFile["excel.txt"].Extract(xlsmTraslations);
                       // zipFile["bexcel.txt"].Extract(languagedataTranslations);
                    };
                    // Here you got localisation of getzip.php 
                    if(targetsrv_jp.IsChecked == true)
                    {
                        
                        //webClient.DownloadDataAsync(new Uri(string.Format("http://files.indigoflare.net/bdotoolbox/patch/patch.zip")));
                        webClient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/patch.zip", "data/patch.zip");
                        
                    }
                    ZipFile zipFile = ZipFile.Read("data/patch.zip");
                    zipFile["excel.txt"].Extract(xlsmTraslations);
                    zipFile["bexcel.txt"].Extract(languagedataTranslations);
                    // int num = (int)download.ShowDialog(this);
               // }
               // catch
                //{
                   // int num2 = (int)System.Windows.MessageBox.Show("翻訳データのダウンロードに失敗しました。再試行してください。");
                    //return;
                //}
                bool flag2 = File.Exists(folder + "override.txt");
                
                if (flag2)
                {
                    languagedataTranslations = new MemoryStream(File.ReadAllBytes(folder + "override.txt"));
                }
                bool flag3 = !File.Exists(folder + "paz\\pad00000.meta");
                if (flag3)
                {
                    int num3 = (int)System.Windows.MessageBox.Show(string.Format("pad00000.metaが見つかりませんでした。正しくインストールされているかご確認ください。", folder));
                }
                else
                {
                    
                    PazMeta pazMeta = PazMeta.Read(folder + "paz\\pad00000.meta", new PazMeta.Crypt(0));
                    byte[] buffer = pazMeta.SaveFile(pazMeta.PazFiles[folder+"stringtable/jp/stringtable_jp.xlsm"]);
                    byte[] buffer2 = pazMeta.SaveFile(pazMeta.PazFiles[folder+"stringtable/jp/stringtable_cutscene_jp.xlsm"]);
                    byte[] buffer3 = pazMeta.SaveFile(pazMeta.PazFiles[folder+"stringtable/jp/symbolnostringtable_jp.xlsm"]);
                    byte[] buffer4 = pazMeta.SaveFile(pazMeta.PazFiles[folder+"gamecommondata/datasheets.bexcel"]);
                    Util util = new Util();
                    List<string> list = Encoding.UTF8.GetString(xlsmTraslations.ToArray()).Replace("\r", "").Split(new char[]
                    {
                        '\n'
                    }).ToList<string>();
                    Dictionary<string, Util.translation> dictionary = new Dictionary<string, Util.translation>();
                    foreach (string current in list)
                    {
                        char[] separator = new char[]
                        {
                            '\t'
                        };
                        string[] array = current.Split(separator);
                        bool flag4 = array.Count<string>() == 3 && !string.IsNullOrWhiteSpace(array[1]) && !string.IsNullOrWhiteSpace(array[2]) && !dictionary.ContainsKey(array[0]);
                        if (flag4)
                        {
                            dictionary.Add(array[0], new Util.translation(array[1], array[2]));
                        }
                    }
                    MemoryStream memoryStream = util.TranslateFile(new MemoryStream(buffer), dictionary, FileTypes.stringTable);
                    File.WriteAllBytes(string.Format("{0}\\prestringtable\\en\\{1}", folder, "stringtable_en.xlsm"), memoryStream.ToArray());
                    memoryStream.Close();
                    MemoryStream memoryStream2 = util.TranslateFile(new MemoryStream(buffer2), dictionary, FileTypes.cutScene);
                    File.WriteAllBytes(string.Format("{0}\\prestringtable\\eu\\{1}", folder, "stringtable_cutscene_eu.xlsm"), memoryStream2.ToArray());
                    memoryStream2.Close();
                    MemoryStream memoryStream3 = util.TranslateFile(new MemoryStream(buffer3), dictionary, FileTypes.symbolTable);
                    File.WriteAllBytes(string.Format("{0}\\prestringtable\\eu\\{1}", folder, "symbolnostringtable_eu.xlsm"), memoryStream3.ToArray());
                    memoryStream3.Close();
                    MemoryStream memoryStream4 = new MemoryStream(buffer4);
                    MemoryStream memoryStream5 = new MemoryStream();
                    Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
                    string text = Encoding.UTF8.GetString(languagedataTranslations.ToArray()).Replace("\r", "");
                    char[] separator2 = new char[]
                    {
                        '\n'
                    };
                    string[] array2 = text.Split(separator2);
                    for (int i = 0; i < array2.Length; i++)
                    {
                        string text2 = array2[i];
                        string[] array3 = text2.Replace("@:@", "\t").Split(new char[]
                        {
                            '\t'
                        });
                        bool flag5 = array3.Count<string>() == 2 && !string.IsNullOrWhiteSpace(array3[1]) && !dictionary2.ContainsKey(array3[0]);
                        if (flag5)
                        {
                            dictionary2.Add(array3[0], array3[1]);
                        }
                    }
                    new newLanguageData().MakeFile(dictionary2, memoryStream4, memoryStream5);
                    File.WriteAllBytes(string.Format("{0}\\prestringtable\\en\\{1}", folder, "LanguageData.xlsm"), memoryStream5.ToArray());
                    memoryStream4.Close();
                    memoryStream5.Close();
                    int num4 = (int)System.Windows.MessageBox.Show("Pomyślnie utworzone pliki");
                }
            }
        }
        
        private bool checkReadOnly(string folder)
        {
            FileInfo fileInfo = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "stringtable_en.xlsm"));
            FileInfo fileInfo2 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "stringtable_cutscene_en.xlsm"));
            FileInfo fileInfo3 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "symbolnostringtable_en.xlsm"));
            FileInfo fileInfo4 = new FileInfo(string.Format("{0}\\prestringtable\\en\\{1}", folder, "LanguageData.xlsm"));
            bool exists = fileInfo.Exists;
            bool result;
            if (exists)
            {
                bool isReadOnly = fileInfo.IsReadOnly;
                if (isReadOnly)
                {
                    try
                    {
                        fileInfo.IsReadOnly = false;
                    }
                    catch
                    {
                        int num = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo.FullName));
                        result = true;
                        return result;
                    }
                }
            }
            bool exists2 = fileInfo2.Exists;
            if (exists2)
            {
                bool isReadOnly2 = fileInfo2.IsReadOnly;
                if (isReadOnly2)
                {
                    try
                    {
                        fileInfo2.IsReadOnly = false;
                    }
                    catch
                    {
                        int num2 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo2.FullName));
                        result = true;
                        return result;
                    }
                }
            }
            bool exists3 = fileInfo3.Exists;
            if (exists3)
            {
                bool isReadOnly3 = fileInfo3.IsReadOnly;
                if (isReadOnly3)
                {
                    try
                    {
                        fileInfo3.IsReadOnly = false;
                    }
                    catch
                    {
                        int num3 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo3.FullName));
                        result = true;
                        return result;
                    }
                }
            }
            bool exists4 = fileInfo4.Exists;
            if (exists4)
            {
                bool isReadOnly4 = fileInfo4.IsReadOnly;
                if (isReadOnly4)
                {
                    try
                    {
                        fileInfo4.IsReadOnly = false;
                    }
                    catch
                    {
                        int num4 = (int)System.Windows.MessageBox.Show(string.Format("The file {0} is set to read-only and the program was unable to fix this, please correct the issue. The patcher can't continue.", fileInfo4.FullName));
                        result = true;
                        return result;
                    }
                }
            }
            result = false;
            return result;
        }

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

            if (lang_tcn.IsChecked == true)
            {
                return_value = Download.DownloadString("http://files.indigoflare.net/BDOToolBox/system/patchinfo.tcn");

            }
            if(lang_ja.IsChecked == true)
            {
                return_value = Download.DownloadString("http://files.indigoflare.net/BDOToolBox/system/patchinfo_en_jp.html");
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
            if (targetsrv_jp.IsChecked == true)
            {
                TargetSrv = (string)Registry.GetValue(KeyNameJP, "location", "");
            }
            if (targetsrv_eu.IsChecked == true)
            {
                //TargetSrv = (string)Registry.GetValue(keyNameNAEU, "InstallLocation", "");
            }
            return TargetSrv;
            
        }

        private DateTime getFileTime()
        {
            string text = "";
            try
            {
                text = new WebClient
                {
                    Proxy = this.proxySetting
                }.DownloadString("http://files.indigoflare.net/BDOToolBox/system/FileDataTime.php?language="); //+ ((MainWindow.Language)this.languageList.SelectedValue).Value.ToLower()).Replace("\r", "").Replace("\n", "");
            }
            catch
            {
            }
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int num = (int)System.Windows.MessageBox.Show("This patcher was developed by LokiReborn and is to be distributed freely. If you were charged for this ask for a refund, you got ripped off.\r\nAdditionally I'd like to thank Seraphy & Billy for helping me out with my translations from the start and to Xennma along with everyone else helping out with the public translations.", "About Us");
        }

        //protected override void Dispose(bool disposing)
        //{
          //  bool flag = disposing && this.components != null;
           // if (flag)
            //{
             //   this.components.Dispose();
            //}
            //base.Dispose(disposing);
        //}
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }

        private void Uninstall_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RoutingAssigner_Click(object sender, RoutedEventArgs e)
        {
            RoutingAssigner RA = new RoutingAssigner();
            //RA.Show();
            //RoutingAssigner_Guides();
            UnAvailable_Message();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PingShow_Click(object sender, RoutedEventArgs e)
        {
            ReadIni();
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
            }

        }
        private void Config_Click(object sender, RoutedEventArgs e)
        {
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
            }

        private void targetsrv_jp_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void lang_en_Checked(object sender, RoutedEventArgs e)
        {
            WebClient Download = new WebClient();
            string result;
            return_value = Download.DownloadString("http://files.indigoflare.net/BDOToolBox/system/patchinfo.en").Replace("\\n", "");
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
            this.Dispatcher.BeginInvoke(new
Action(() =>

{
    switch (e.ChangeType)
    {
        case System.IO.WatcherChangeTypes.Changed:
            ReadIni();
           // ConfigWatchDog.Dispose();
           // ConfigWatchDog = null;
            break;
        case System.IO.WatcherChangeTypes.Created:
            ReadIni();
           // ConfigWatchDog.Dispose();
           // ConfigWatchDog = null;
            break;
        case System.IO.WatcherChangeTypes.Deleted:
            ReadIni();
          //  ConfigWatchDog.Dispose();
           // ConfigWatchDog = null;
            break;
    }

        //UIスレッドで実行すべき処理

}));
           
            
        }
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string pushedKey = "";
            switch (e.Key)
            {
                case Key.F1:
                    DebugRefresh_JPToTCN();
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
                return_value = Download.DownloadString("http://files.indigoflare.net/BDOToolBox/system/patchinfo.tcn").Replace("\\n", "");
                this.PatchInfo.Content = return_value;
            }
            catch
            {
                this.PatchInfo.Content = FailedToReceivedDataFromServer();
            }
            
        }

        private void lang_ja_Checked(object sender, RoutedEventArgs e)
        {
            

            try {
                WebClient Download = new WebClient();
                string result;
                return_value = Download.DownloadString("http://files.indigoflare.net/BDOToolBox/system/patchinfo.jp");
                this.PatchInfo.Content = return_value;
            }
            catch
            {
                
                this.PatchInfo.Content = FailedToReceivedDataFromServer();
            }
        }
        private void DebugRefresh_JPToTCN()
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
                        //int num = (int)System.Windows.MessageBox.Show(string.Format("パッチャーフォルダを削除できません。, エラーコード:{0}", ex.Message));
                        return;
                    }
                    //int num2 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                    // PatchFolder_Deleted();
                }
                else
                {
                    //int num3 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                    // PatchFolder_Deleted();
                }
            }
            else
            {
                // InstallFolder_NotFound();
                //int num4 = (int)System.Windows.MessageBox.Show("");
            }
            //
            //
            ReadIni();
            text = this.gameInstallPath();
            flag = Directory.Exists(text);
            bdo_toolbox.config conf = new bdo_toolbox.config();

            if (flag)
            {
                bool flag2 = Directory.Exists(text + "\\stringtable");
                if (flag2)
                {
                    try
                    {
                        Directory.Delete(text + "\\stringtable", true);
                    }
                    catch
                    {
                    }
                }
                bool flag3 = !Directory.Exists(text + "\\prestringtable");
                if (flag3)
                {
                    Directory.CreateDirectory(text + "\\prestringtable");
                }
                bool flag4 = !Directory.Exists(text + "\\prestringtable\\jp");
                if (flag4)
                {
                    Directory.CreateDirectory(text + "\\prestringtable\\jp");
                }
                //bool flag5 = this.officialVersion().Equals(this.localVersion());
                //if (flag5)
                //{
                WebClient webclient = new WebClient();
                webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_TCN.zip", "data/LD_JP_TCN.zip");
                try
                {
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
                    zipFile["LanguageData.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;
                }
                catch
                {
                    ResetPatch();
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
                    zipFile["LanguageData.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(text + "prestringtable/jp/");
                    zipFile.Dispose();
                    zipFile = null;

                }
                FinishedPatching_Message();
                webclient.Dispose();
                webclient = null;
            }
        }

    }
}
