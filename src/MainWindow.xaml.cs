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
        public static DateTime CurrentTime = DateTime.Now;
        FileSystemWatcher ConfigWatchDog = new FileSystemWatcher();
        bool builtflag;
        string Language = null;
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
            Activated += (s, e) =>
            {
                if (flg)
                {
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
        private void MainWindow_Load(object sender, EventArgs e)
        {
            
            Thread.CurrentThread.CurrentCulture = new CultureInfo("ja-JP");
            //this.languages.Add(new MainWindow.Language("English", "english"));     //LANGUAGE1
            //this.languages.Add(new MainWindow.Language("Polski", "polish"));       //LANGUAGE2
            //this.languages.Add(new MainWindow.Language("Japanise", "japanise"));   //LANGUAGE3

            // If you want remove languages, remove code or add "//" before code like in Language3
            //this.languageList. = "Value";
            //this.languageList.DisplayMember = "Display";
            //this.languageList.DataSource = this.languages;
            this.languageList.SelectedIndex = 0;
           
            string currPatchVersion = ((AssemblyFileVersionAttribute)Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0]).Version.ToString();
            string serverPatchVersion;
            try
            {
                serverPatchVersion = new WebClient
                {
                    Proxy = this.proxySetting
                    // localisation of curretversion.php file. This code will check there is new updated .exe patcher. 
                }.DownloadString("http://files.indigoflare.net/BDOToolBox/system/CurrentVersion.php").Replace("\r", "").Replace("\n", "");
            }
            catch
            {
                serverPatchVersion = null;
            }
            if (serverPatchVersion == null || serverPatchVersion.Equals(currPatchVersion))
                return;

            // localisation of .exe file with patcher. Use instalshield to build patcher
            DownloadProgramUpdate downloadProgramUpdate = new DownloadProgramUpdate("http://blackdesert.com.pl/patch/ru/exe/setup.exe");
            try
            {
               // int num = (int)downloadProgramUpdate.ShowDialog();
            }
            catch (TargetInvocationException ex)
            {

            }
        }
        public void ReadIni()
        {
            StreamReader SettingRead = new StreamReader("config.ini", false);
            string stresult = string.Empty;
            
            var ReadLineCount = 0;
            string sttemp = string.Empty;
            bdo_toolbox.config conf = new config();
            while (SettingRead.Peek() >= 0)
            {
                // ファイルを 1 行ずつ読み込む
                string stBuffer = SettingRead.ReadToEnd();
                if(stBuffer.Contains("UILang = Japanese"))
                {
                    conf.ChangeLangToJapanese();
                    Language = "Japanese";
                }
                if(stBuffer.Contains("UILang = English"))
                {
                    bdo_toolbox.config confg = new bdo_toolbox.config();
                    Language = "English";
                    config.Content = "Settings";
                    lang_en.Content = "English";
                    lang_ja.Content = "Japanese";
                    lang_scn.Content = "Chinese(simplified)";
                    lang_tcn.Content = "Chinese(traditional)";
                    lang_ru.Content = "Russian";
                    targersrv_kr.Content = "Korea";
                    targetsrv_eu.Content = "EU/NA";
                    targetsrv_jp.Content = "Japan";
                    targetsrv_ru.Content = "Russia";
                    Install.Content = "Install Patch";
                    Uninstall.Content = "Uninstall Patch";
                    RoutingAssigner.Content = "Routing Assigner";
                    Ping_Show.Content = "Show Ping";
                    confg.UILangFrame.Content = "";
                    confg.UILangFrame.Content = "UI Language";
                    confg.UILang_English.Content = "English";
                    confg.UILang_Japanese.Content = "Japanese";
                    confg.UILang_ChineseS.Content = "Chinese(simplified)";
                    confg.PatchFrame.Content = "";
                    confg.PatchFrame.Content = "Patch Settings";
                    confg.upd.Content = "Update Server";
                    confg.usebuiltdata.Content = "Use Built Data";
                    confg.Apply.Content = "Apply";
                }
                if (stBuffer.Contains("UILang = T_Chinese"))
                {
                    bdo_toolbox.config confg = new bdo_toolbox.config();
                    Language = "T_Chinese";
                    config.Content = "設置";
                    lang_en.Content = "英語";
                    lang_ja.Content = "日本語";
                    lang_scn.Content = "簡體中文";
                    lang_tcn.Content = "繁體中文";
                    lang_ru.Content = "俄語";
                    targersrv_kr.Content = "韓服";
                    targetsrv_eu.Content = "欧美服";
                    targetsrv_jp.Content = "日服";
                    targetsrv_ru.Content = "俄服";
                    Install.Content = "安裝補丁";
                    Uninstall.Content = "卸載補丁";
                    RoutingAssigner.Content = "路由分配器";
                    Ping_Show.Content = "顯示Ping";
                    confg.UILangFrame.Content = "";
                    confg.UILangFrame.Content = "UI Language";
                    confg.UILang_English.Content = "English";
                    confg.UILang_Japanese.Content = "Japanese";
                    confg.UILang_ChineseS.Content = "Chinese(simplified)";
                    confg.PatchFrame.Content = "";
                    confg.PatchFrame.Content = "Patch Settings";
                    confg.upd.Content = "Update Server";
                    confg.usebuiltdata.Content = "Use Built Data";
                    confg.Apply.Content = "Apply";
                }
                if (stBuffer.Contains("UseBuiltData = 1"))
                {
                    builtflag = true;
                }
                
            }
            SettingRead.Close();
            SettingRead.Dispose();
            SettingRead = null;

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
                    MessageBox.Show("1");
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
                int num2 = (int)System.Windows.MessageBox.Show("ゲームのインストールフォルダを検出できませんでした。黒い砂漠が正しくインストールされているかご確認ください。");
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
                    int num2 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                }
                else
                {
                    int num3 = (int)System.Windows.MessageBox.Show("フォルダは正常に削除されました。");
                }
            }
            else
            {
                int num4 = (int)System.Windows.MessageBox.Show("インストールフォルダが見つかりません。");
            }
        }

        private void languageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updatePatchInfo();
        }
        private void startPatchingNoBuild(string folder)
        {
            
            WebClient webclient = new WebClient();
            if (targetsrv_jp.IsChecked == true)
            {
                
            }
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

                }
                catch
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
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");

                }
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
            if (targetsrv_jp.IsChecked == true && lang_scn.IsChecked == true)
            {
                if (Language == "S_Chinese")
                {
                    MessageBox.Show("这个功能现在无法使用.");
                }
            } 
            {

            }
            if (targetsrv_eu.IsChecked == true)
            {
               
            }
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

                }
                catch
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
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    
                }
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
                    
                }
                catch
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
                    ZipFile zipFile = ZipFile.Read("data/LD_JP_TCN.zip");
                    zipFile["LanguageData.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_cutscene_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["stringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");
                    zipFile["symbolnostringtable_jp.xlsm"].Extract(folder + "prestringtable/jp/");

                    
                }
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
            this.PatchInfo.Content = "Infomation: "+text;
        }

        private string officialVersion()
        {
            string result;
            try
            {
                result = new WebClient
                {
                    Proxy = this.proxySetting
                    // Here you got information window with text
                }.DownloadString("http://files.indigoflare.net/BDOToolBox/system/config.patch.version").Replace("\r", "").Replace("\n", "");
            }
            catch
            {
                result = "";
            }
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
            string keyNameNAEU = string.Format("HKEY_CURRENT_USER\\SOFTWARE\\DaumGames EU\\BlackDesertOnlineCBT1", Wow.Is64BitOperatingSystem ? "Wow6432Node\\" : "");
            if(targetsrv_jp.IsChecked == true)
            {
                TargetSrv = (string)Registry.GetValue(KeyNameJP, "location", "");
            }
            if (targetsrv_eu.IsChecked == true)
            {
                TargetSrv = (string)Registry.GetValue(keyNameNAEU, "InstallPath", "");
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
            //RoutingAssigner RA = new RoutingAssigner();
            //RA.Show();
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

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void PingShow_Click(object sender, RoutedEventArgs e)
        {
            Ping Ping = new Ping();
            Ping.Show();
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            bdo_toolbox.config conf = new config();
            conf.Show();

        }

        private void targetsrv_jp_Checked(object sender, RoutedEventArgs e)
        {
            updatePatchInfo();
        }

        private void lang_en_Checked(object sender, RoutedEventArgs e)
        {
            updatePatchInfo();
        }
    }
}
