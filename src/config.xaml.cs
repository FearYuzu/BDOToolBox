using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;

namespace bdo_toolbox
{
    /// <summary>
    /// Interaction logic for config.xaml
    /// </summary>
    public partial class config : Window
    {
        bool flg = true;
        MainWindow Main = new MainWindow();
        public config()
        {
            InitializeComponent();
            
            Activated += (s, e) =>
            {
                if (flg)
                {
                    flg = false;
                    ReadConfigIni();
                }
            };
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
        public void ReadConfigIni()
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
                    MainWindow Main = new MainWindow();
                    Main.Install.Content = "パッチインストール";
                    UILang_English.Content = "英語";
                    UILang_Japanese.Content = "日本語";
                    UILang_ChineseS.Content = "中国語(簡体）";
                    UILang_ChineseT.Content = "中国語(繁体）";
                    upd.Content = "アップデートサーバー";
                    usebuiltdata.Content = "構築されたデータを使用";
                    Apply.Content = "適用";
                }
                if(stBuffer.Contains("UILang = English"))
                {


                    MainWindow Main = new MainWindow();
                    Main.Install.Content ="Patch Install";
                    UILang_English.Content = "English";
                    UILang_Japanese.Content = "Japanese";
                    UILang_ChineseS.Content = "Chinese(simplified)";
                    UILang_ChineseT.Content = "Chinese(Traditional)";
                    upd.Content = "Update Server";
                    usebuiltdata.Content = "Use Built Data";
                    Apply.Content = "Apply";
                }
                if (stBuffer.Contains("UILang = S_Chinese"))
                {
                    UILang_English.Content = "英语";
                    UILang_Japanese.Content = "日本语";
                    UILang_ChineseS.Content = "简体中文";
                    UILang_ChineseT.Content = "繁体中文";
                    upd.Content = "更新服务器URL";
                    usebuiltdata.Content = "使用构建的数据";
                    Apply.Content = "应用";
                }
                if (stBuffer.Contains("UILang = T_Chinese"))
                {
                    UILang_English.Content = "英語";
                    UILang_Japanese.Content = "日本語";
                    UILang_ChineseS.Content = "簡體中文";
                    UILang_ChineseT.Content = "繁體中文";
                    upd.Content = "更新服務器URL";
                    usebuiltdata.Content = "使用構建的數據";
                    Apply.Content = "應用";
                }
                if (stBuffer.Contains("UseBuiltData = 1"))
                {
                    //builtflag = true;
                }
                //MessageBox.Show(stBuffer);
            }
            
            SettingRead.Close();
            SettingRead.Dispose();

        }
        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            
            if(UILang_Japanese.IsChecked == true)
            {
                ChangeLangToJapanese();
                WriteTheSettingsToIni();
                this.Close();
                
                Main.ReadIni();
            }
            if(UILang_English.IsChecked == true)
            {
                WriteTheSettingsToIni();
                ChangeLangToEnglish();
                this.Close();
                
                Main.ReadIni();
                
            }
        }
        public void ChangeLangToJapanese()
        {
            MainWindow Mainwindow = new MainWindow();
            Mainwindow.config.Content = "設定";
            Mainwindow.lang_en.Content = "英語";
            Mainwindow.lang_ja.Content = "日本語";
            Mainwindow.lang_scn.Content = "中国語(簡体)";
            Mainwindow.lang_tcn.Content = "中国語(繁体）";
            Mainwindow.lang_ru.Content = "ロシア語";
            Mainwindow.targersrv_kr.Content = "韓国";
            Mainwindow.targetsrv_eu.Content = "北米/欧州";
            Mainwindow.targetsrv_jp.Content = "日本";
            Mainwindow.targetsrv_ru.Content = "ロシア";
            Mainwindow.Install.Content = "パッチインストール";
            Mainwindow.Uninstall.Content = "パッチアンインストール";
            Mainwindow.RoutingAssigner.Content = "ルーティングアサイナ";
            Mainwindow.Ping_Show.Content = "Ping表示";
            UILangFrame.Content = "";
            UILangFrame.Content = "UI言語";
            UILang_English.Content = "英語";
            UILang_Japanese.Content = "日本語";
            UILang_ChineseS.Content = "中国語(簡体）";
            PatchFrame.Content = "";
            PatchFrame.Content = "パッチ設定";
            upd.Content = "アップデートサーバー";
            usebuiltdata.Content = "構築されたデータを使用";
            Apply.Content = "適用";
        }
        public void ChangeLangToEnglish()
        {
            MainWindow Mainwindow = new MainWindow();
            Mainwindow.config.Content = "Settings";
            Mainwindow.lang_en.Content = "English";
            Mainwindow.lang_ja.Content = "Japanese";
            Mainwindow.lang_scn.Content = "Chinese(simplified)";
            Mainwindow.lang_tcn.Content = "Chinese(traditional)";
            Mainwindow.lang_ru.Content = "Russian";
            Mainwindow.targersrv_kr.Content = "Korea";
            Mainwindow.targetsrv_eu.Content = "EU/NA";
            Mainwindow.targetsrv_jp.Content = "Japan";
            Mainwindow.targetsrv_ru.Content = "Russia";
            Mainwindow.Install.Content = "Install Patch";
            Mainwindow.Uninstall.Content = "Uninstall Patch";
            Mainwindow.RoutingAssigner.Content = "Routing Assigner";
            Mainwindow.Ping_Show.Content = "Show Ping";
            UILangFrame.Content = "";
            UILangFrame.Content = "UI Language";
            UILang_English.Content = "English";
            UILang_Japanese.Content = "Japanese";
            UILang_ChineseS.Content = "Chinese(simplified)";
            PatchFrame.Content = "";
            PatchFrame.Content = "Patch Settings";
            upd.Content = "Update Server";
            usebuiltdata.Content = "Use Built Data";
            Apply.Content = "Apply";
        }
        public void WriteTheSettingsToIni()
        {
            StreamWriter Write = new StreamWriter("config.ini", false);
            Write.WriteLine("[General]");
            if(UILang_Japanese.IsChecked == true)
            {
                Write.WriteLine("UILang = Japanese");
            }
            if(UILang_English.IsChecked == true)
            {
                Write.WriteLine("UILang = English");
            }
            if(UILang_ChineseS.IsChecked == true)
            {
                Write.WriteLine("UILang = S_Chinese");
            }
            if(UILang_ChineseT.IsChecked == true)
            {
                Write.WriteLine("UILang = T_Chinese");
            }
            Write.WriteLine("[Patch]");
            if(usebuiltdata.IsChecked == true)
            {
                Write.WriteLine("UseBuiltData = 1");
            }
            else
            {
                Write.WriteLine("UseBuiltData = 0");
            }
            Write.WriteLine("UpdateURL = " + UpdateURL.Text);
            Write.Close();
            Write.Dispose();
            
        }

        private void UILang_Japanese_Clicked(object sender, RoutedEventArgs e)
        {
            WriteTheSettingsToIni() ;
            ReadConfigIni();
        }

        private void UILang_English_Clicked(object sender, RoutedEventArgs e)
        {
            WriteTheSettingsToIni();
            ReadConfigIni();
        }

        private void UILang_ChineseS_Clicked(object sender, RoutedEventArgs e)
        {
            WriteTheSettingsToIni();
            ReadConfigIni();
        }
    }
}
