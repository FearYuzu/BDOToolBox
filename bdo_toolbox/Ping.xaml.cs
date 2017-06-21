using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Net.NetworkInformation;
using System.IO;
using System.Windows.Threading;

namespace bdo_toolbox
{
    /// <summary>
    /// Ping.xaml の相互作用ロジック
    /// </summary>
    public partial class Ping : Window
    {
        bool flg = true;
        private DispatcherTimer mTimer;
        config conf = new config();
        MainWindow main = new MainWindow();
        public Ping()
        {
            
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            Activated += (s, e) =>
            {
                if (flg)
                {
                    flg = false;
                    PingUIUpdate();
                }
            };
            mTimer = new DispatcherTimer();
            mTimer.Interval = TimeSpan.FromSeconds(MainWindow.TimeSpanSec);
           
            mTimer.Tick += new EventHandler(PingProcess);
            mTimer.Start();
        }
        public void PingProcess(object sender,EventArgs e)
        {
            PingReply Reply;
            System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            switch (MainWindow.PingDestination)
            {
                case "JP-Auth":
                    
                    Reply = p.Send("122.129.233.160");
                    if (Reply.Status == IPStatus.Success)
                    {
                        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
                    }
                    ping.Foreground = Brushes.Green;
                    if(Reply.RoundtripTime >= 200)
                    {
                        ping.Foreground = Brushes.Yellow;
                    }
                    if (Reply.RoundtripTime >= 300)
                    {
                        ping.Foreground = Brushes.Red;
                    }
                    if (MainWindow.TimeSpanSec < 3)
                    {
                        Message.PingingTimeSpan_NotAllowed();
                        mTimer.Stop();
                        mTimer.Interval = TimeSpan.FromSeconds(3);
                        MainWindow.TimeSpanSec = 3;
                        //MessageBox.Show("Alert" + "TimeSpan:" + main.TimeSpanSec);
                        Util.WriteSettings();
                        mTimer.Start();
                    }
                    break;
                case "Taiwan-KR":
                    
                    Reply = p.Send("inven.co.kr");
                    if (Reply.Status == IPStatus.Success)
                    {
                        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
                    }
                    if (Reply.RoundtripTime <= 100)
                    {
                        ping.Foreground = Brushes.GreenYellow;

                    }
                    if (Reply.RoundtripTime >= 100)
                    {
                        ping.Foreground = Brushes.Yellow;

                    }
                    if (Reply.RoundtripTime >= 200)
                    {
                        ping.Foreground = Brushes.Red;
                    }
                    if (MainWindow.TimeSpanSec < 3)
                    {
                        Message.PingingTimeSpan_NotAllowed();
                        mTimer.Stop();
                        mTimer.Interval = TimeSpan.FromSeconds(3);
                        MainWindow.TimeSpanSec = 2;
                        //MessageBox.Show("Alert" + "TimeSpan:" + main.TimeSpanSec);
                        Util.WriteSettings();
                        mTimer.Start();
                    }
                    break;
                case "JP-Tokyo":
                    if (MainWindow.TimeSpanSec < 3)
                    {
                        Message.PingingTimeSpan_NotAllowed();
                        mTimer.Stop();
                        mTimer.Interval = TimeSpan.FromSeconds(3);
                        MainWindow.TimeSpanSec = 3;
                        //MessageBox.Show("Alert" + "TimeSpan:" + main.TimeSpanSec);
                        Util.WriteSettings();
                        mTimer.Start();
                    }
                    break;
                case "NA-Sanjose":
                    if (MainWindow.TimeSpanSec < 3)
                    {
                        Message.PingingTimeSpan_NotAllowed();
                        mTimer.Stop();
                        mTimer.Interval = TimeSpan.FromSeconds(3);
                        MainWindow.TimeSpanSec = 3;
                        //MessageBox.Show("Alert" + "TimeSpan:" + main.TimeSpanSec);
                        Util.WriteSettings();
                        mTimer.Start();
                    }
                    break;
                case "GamezBD":
                    Reply = p.Send("64.91.227.116");
                    if (Reply.Status == IPStatus.Success)
                    {
                        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
                    }
                    Server.Content = "(Gamez BD)";
                    Server.Foreground = Brushes.Aqua;
                    ping.Foreground = Brushes.Aqua;
                    if (Reply.RoundtripTime <= 100)
                    {
                        ping.Foreground = Brushes.Aqua;

                    }
                    if (Reply.RoundtripTime >= 200)
                    {
                        ping.Foreground = Brushes.Yellow;
                    }
                    if (Reply.RoundtripTime >= 300)
                    {
                        ping.Foreground = Brushes.Red;
                    }
                    break;

            }
            
            //if (main.Destination == "KR-Auth" && main.TimeSpanSec < 3)
            //{
            //    main.PingingTimeSpan_NotAllowed();
            //    mTimer.Stop();
            //    mTimer.Interval = TimeSpan.FromSeconds(3);
            //    main.TimeSpanSec = 3;

            //    conf.WriteTheSettingsToIni();
            //    mTimer.Start();
               
            //}
            //if (main.Destination == "JP-Tokyo")
            //{
            //    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            //    PingReply Reply = p.Send("133.130.113.6");
            //    if (Reply.Status == IPStatus.Success)
            //    {
            //        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
            //    }
            //}
            //if(main.Destination == "NA-Sanjose")
            //{
            //    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            //    PingReply Reply = p.Send("163.44.119.33");
            //    if (Reply.Status == IPStatus.Success)
            //    {
            //        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
            //    }
            //}
            //if(main.Destination == "JP-Auth")
            //{
            //    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            //    PingReply Reply = p.Send("122.129.233.160");
            //    if (Reply.Status == IPStatus.Success)
            //    {
            //        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
            //    }
            //}
            //if(main.Destination == "KR-Auth")
            //{
            //    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
            //    PingReply Reply = p.Send("blackauth.black.game.daum.net");
            //    if (Reply.Status == IPStatus.Success)
            //    {
            //        ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
            //    }
               
            //}
        }
      

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void PingUIUpdate()
        {
            switch (MainWindow.Language)
            {
                case "Japanese":
                    MenuClose.Header = "閉じる";
                    break;
                case "English":
                    MenuClose.Header = "Close";
                    break;
                case "HanT":
                    MenuClose.Header = "關閉";
                    break;
                case "HanS":
                    MenuClose.Header = "关闭";
                    break;
            }
        }
    }
}
