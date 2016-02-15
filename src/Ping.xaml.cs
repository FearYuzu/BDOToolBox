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

namespace bdo_toolbox
{
    /// <summary>
    /// Ping.xaml の相互作用ロジック
    /// </summary>
    public partial class Ping : Window
    {
        bool flg = true;
        MainWindow main = new MainWindow();
        public Ping()
        {
            
            InitializeComponent();
            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            PingProcess();
            Activated += (s, e) =>
            {
                if (flg)
                {
                    flg = false;
                    main.ReadIni();
                    //MessageBox.Show(main.Language);
                    switch (main.Language)
                    {
                        case "Japanese":
                            
                            MenuClose.Header = "閉じる";
                            break;
                        case "English":
                            MenuClose.Header = "Close";
                            break;
                        case "T_Chinese":
                            MenuClose.Header = "關閉";
                            break;
                        case "S_Chinese":
                            MenuClose.Header = "关闭";
                            break;
                    }
                }
            };
        }
        public async void PingProcess()
        {
            while (true)
            {
                await Task.Run(() =>
                {
                    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
                    PingReply Reply = p.Send("133.130.113.6");
                    if (Reply.Status == IPStatus.Success)
                    {
                        ping.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            ping.Content = "Ping:" + Reply.RoundtripTime + "ms";
                        })
                        );
                        
                    }
                });
            }
            
            

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
