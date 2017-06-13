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
       
        static MainWindow Main = new MainWindow();
        
        public config()
        {
            InitializeComponent();
           // UIWatchDog();
            Activated += (s, e) =>
            {
                if (flg)
                {
                    
                    ConfigUIUpdate();
                    InitializeUI();
                    Main.UIUpdate();
                    flg = false;
                    if(MainWindow.IsUseMetaInjector == true)
                    {
                        //UseMetaInjector.IsChecked = true;
                    }
                    if(MainWindow.IsUseMetaInjector == false)
                    {
                        //UseMetaInjector.IsChecked = false;
                    }     
                }
            };
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
        private void InitializeUI()
        {
            switch (MainWindow.Language)
            {
                case "Japanese":
                    UILang_Japanese.IsChecked = true;
                    break;
                case "English":
                    UILang_English.IsChecked = true;
                    break;
                case "HanS":
                    UILang_ChineseS.IsChecked = true;
                    break;
                case "HanT":
                    UILang_ChineseT.IsChecked = true;
                    break;
            }
            UpdateURI.Text = MainWindow.PatchStreamURI;
        }
        private void UIWatchDog()
        {
           // if(UseMetaInjector.IsChecked == false)
           // {
          //      MessageBox.Show("is now false");
            //    MainWindow.IsUseMetaInjector = false;
            //}
           // if(UseMetaInjector.IsChecked == true)
            //{
           //     MessageBox.Show("is now true");
            //    MainWindow.IsUseMetaInjector = true;
           // }
        }
        private void Apply_Click(object sender, RoutedEventArgs e)
        {

            WriteSettings();
            Main.UIUpdate();
            this.Close();
        }
        private void ConfigUIUpdate()
        {
            UILang_Japanese.Content = DefineUIContent.UILang_Japanese;
            UILang_English.Content = DefineUIContent.UILang_English;
            UILang_ChineseT.Content = DefineUIContent.UILang_HanT;
            UILang_ChineseS.Content = DefineUIContent.UILang_HanS;
           // UseMetaInjector.Content = DefineUIContent.UIPatchUseMetaInjector;
            upd.Content = DefineUIContent.UIPatchServer;
            

        }
        public void WriteSettings()
        {
            //MessageBox.Show(MainWindow.IsUseMetaInjector.ToString());
            StreamWriter Write = new StreamWriter("config.ini", false);
            //Write.WriteLine("[key]=[content]");
            if(UILang_Japanese.IsChecked == true)
            {
                Write.WriteLine("Language=Japanese");
            }
            if(UILang_English.IsChecked == true)
            {
                Write.WriteLine("Language=English");
            }
            if(UILang_ChineseS.IsChecked == true)
            {
                Write.WriteLine("Language=HanS");
            }
            if(UILang_ChineseT.IsChecked == true)
            {
                Write.WriteLine("Language=HanT");
            }
            Write.WriteLine("PingDestination=" + MainWindow.PingDestination);
            Write.WriteLine("PingingTimeSpan=" + MainWindow.TimeSpanSec);
            Write.WriteLine("PatchStreamURI=" + UpdateURI.Text);
            Write.WriteLine("PatchInfoURI_JP="+ MainWindow.PatchInformationURI_JP);
            Write.WriteLine("PatchInfoURI_EN="+ MainWindow.PatchInformationURI_EN);
            Write.WriteLine("PatchInfoURI_TW=" + MainWindow.PatchInformationURI_TW);
            Write.WriteLine("PatchInfoURI_Gamez=" + MainWindow.PatchInformationURI_Gamez);
            Write.WriteLine("BDONAEU_ClientPath=" + MainWindow.BDONAEU_ClientPath);
            Write.WriteLine("BDOGamez_ClientPath="+MainWindow.BDOGamez_ClientPath);
            Write.WriteLine("PatchFileName_TW="+ MainWindow.PatchFileName_TW);
            Write.WriteLine("PatchFileName_Gamez=" + MainWindow.PatchFileName_Gamez);
            Write.Close();
            Write.Dispose();
            
        }

        private void UILang_Japanese_Clicked(object sender, RoutedEventArgs e)
        {
           
            WriteSettings();
            
            UILang_English.Content = "英語";
            UILang_Japanese.Content = "日本語";
            UILang_ChineseT.Content = "繁体中国語";
            UILang_ChineseS.Content = "簡体中国語";
            upd.Content = "パッチサーバーアドレス";
            //UseMetaInjector.Content = "MeraInjectorを用いてパッチを適用する";
           
        }

        private void UILang_English_Clicked(object sender, RoutedEventArgs e)
        {
            WriteSettings();
            
            UILang_English.Content = "English";
            UILang_Japanese.Content = "Japanese";
            UILang_ChineseT.Content = "Traditional Chinese";
            UILang_ChineseS.Content = "Simplified Chinese";
            upd.Content = "Patch Server Address";
            //UseMetaInjector.Content = "Use MetaInjector for Patch";

        }

        private void UILang_ChineseS_Clicked(object sender, RoutedEventArgs e)
        {
            WriteSettings();
            
            UILang_English.Content = "英语";
            UILang_Japanese.Content = "日语";
            UILang_ChineseS.Content = "简体中文";
            UILang_ChineseT.Content = "繁体中文";
            upd.Content = "补丁服务器地址";
            //UseMetaInjector.Content = "使用MetaInjector";

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
                    WriteSettings();
                    this.Close();
                    break;
                case Key.C:
                    break;
                case Key.S:
                    UILang_ChineseS.IsChecked = true;
                    WriteSettings();
                    Main.UIUpdate();
                    break;
                case Key.T:
                    UILang_ChineseT.IsChecked = true;
                    WriteSettings();
                    Main.UIUpdate();
                    break;
                case Key.J:
                    UILang_Japanese.IsChecked = true;
                    WriteSettings();
                    Main.UIUpdate();
                    break;
                case Key.E:
                    UILang_English.IsChecked = true;
                    WriteSettings();
                    Main.UIUpdate();
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

        private void UILang_ChineseT_Checked(object sender, RoutedEventArgs e)
        {
            WriteSettings();
            UILang_English.Content = "英語";
            UILang_Japanese.Content = "日語";
            UILang_ChineseS.Content = "簡體中文";
            UILang_ChineseT.Content = "繁體中文";
            upd.Content = "補丁服務器地址";
           // UseMetaInjector.Content = "使用MetaInjector";
        }

        private void UseMetaInjector_Checked(object sender, RoutedEventArgs e)
        {
            MainWindow.IsUseMetaInjector = true;
        }
    }
}
