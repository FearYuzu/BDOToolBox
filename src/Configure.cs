using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Windows;


namespace bdo_toolbox
{
    class Configure
    {
        private static string HeaderString = "["; //ロードを無視する文字列
        private static char SplitChar = '='; //分割基準の文字
        public static List<Settings> ConfigureTable = new List<Settings>(); //設定内容を格納するListを定義
        //
        public static void LoadConfigure(string LoadPath) //設定ファイルをロード
        {
            try 
            {
                //MessageBox.Show("Starts Load");
                //ファイルストリームオープン
                StreamReader sr = new StreamReader(new FileStream(LoadPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                string line = "";
                while ((line = sr.ReadLine()) != null) //ファイルを最後まで読む
                {
                    if (line.Contains(HeaderString))
                    {
                        continue;
                    }
                    string[] fields = line.Split(SplitChar); //ロードした文字列を分割
                    string Key = fields[0]; //Key文字列はここ
                    string Content = fields[1]; //Content文字列はここ
                    if (Content.Contains(HeaderString))
                    {
                        continue;
                    }
                    ConfigureTable.Add(new Settings(Key, Content)); //設定内容格納用Listに書き込み
                }
                //後始末
                sr.Close();
                sr = null;
            }
            catch //例外処理
            {
                System.Windows.MessageBox.Show("Failed to Load Configure.");
                Environment.Exit(0);
            }
        }
    }
    class Settings //設定ファイル格納List用クラス定義
    {
        public string Key;
        public string Content;
        public Settings(string key,string content)
        {
            Key = key;
            Content = content;
        }
    }
}
