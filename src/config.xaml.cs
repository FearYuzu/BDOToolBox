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
            StreamReader SettingRead = new StreamReader(new FileStream("config.ini", FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
            string stresult = string.Empty;
            
            var ReadLineCount = 0;
            string sttemp = string.Empty;
            bdo_toolbox.config conf = new config();
            while (SettingRead.Peek() >= 0)
            {
                // ファイルを 1 行ずつ読み込む
                string stBuffer = SettingRead.ReadLine();
                if(stBuffer.Contains("UILang = Japanese"))
                {
                    MainWindow Main = new MainWindow();
                    Main.Install.Content = "パッチインストール";
                    UILang_English.Content = "英語(English)";
                    UILang_Japanese.Content = "日本語(japanese)";
                    UILang_ChineseS.Content = "中国語(簡体）(S_Chinese)";
                    UILang_ChineseT.Content = "中国語(繁体）(T_Chinese)";
                    upd.Content = "アップデートサーバー";
                    usebuiltdata.Content = "構築されたデータを使用";
                    Apply.Content = "適用(A)";
                    UILang_Japanese.IsChecked = true;
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
                    Apply.Content = "Apply(A)";
                    UILang_English.IsChecked = true;
                }
                if (stBuffer.Contains("UILang = S_Chinese"))
                {
                    UILang_English.Content = "英语(English)";
                    UILang_Japanese.Content = "日本语(Japanese)";
                    UILang_ChineseS.Content = "简体中文(S_Chinese)";
                    UILang_ChineseT.Content = "繁体中文(T_Chinese)";
                    upd.Content = "更新服务器URL";
                    usebuiltdata.Content = "使用构建的数据";
                    Apply.Content = "应用(A)";
                    UILang_ChineseS.IsChecked = true;
                }
                if (stBuffer.Contains("UILang = T_Chinese"))
                {
                    UILang_English.Content = "英語(English)";
                    UILang_Japanese.Content = "日本語（Japanese)";
                    UILang_ChineseS.Content = "簡體中文(S_Chinese)";
                    UILang_ChineseT.Content = "繁體中文(T_Chinese)";
                    upd.Content = "更新服務器URL";
                    usebuiltdata.Content = "使用構建的數據";
                    Apply.Content = "應用(A)";
                    UILang_ChineseT.IsChecked = true;
                }
                if (stBuffer.Contains("UseBuiltData = 1"))
                {
                    //builtflag = true;
                }
                //MessageBox.Show(stBuffer);
            }
            
            SettingRead.Close();
            SettingRead.Dispose();
            SettingRead = null;

        }
        private void Apply_Click(object sender, RoutedEventArgs e)
        {

            WriteTheSettingsToIni();
            this.Close();
        }
        public void ChangeLangToJapanese()
        {
           
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
        private void Window_KeyAssign(object sender, KeyEventArgs e)
        {
            string pushedKey = "";
            switch (e.Key)
            {
                case Key.F1:
                case Key.F2:
                case Key.F3:
                case Key.F4:
                case Key.F5:
                case Key.F6:
                case Key.F7:
                case Key.F8:
                case Key.A:
                    WriteTheSettingsToIni();
                    this.Close();
                    break;
                case Key.C:
                    break;
                case Key.S:
                    UILang_ChineseS.IsChecked = true;
                    WriteTheSettingsToIni();
                    ReadConfigIni();
                    break;
                case Key.T:
                    UILang_ChineseT.IsChecked = true;
                    WriteTheSettingsToIni();
                    ReadConfigIni();
                    break;
                case Key.J:
                    UILang_Japanese.IsChecked = true;
                    WriteTheSettingsToIni();
                    ReadConfigIni();
                    break;
                case Key.E:
                    UILang_English.IsChecked = true;
                    WriteTheSettingsToIni();
                    ReadConfigIni();
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
    }
}
