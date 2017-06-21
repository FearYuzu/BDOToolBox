using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Windows;


namespace Updater
{
    class Configure
    {
        private static string HeaderString = "["; //ロードを無視する文字列 (Ignore Strings)
        private static char SplitChar = '='; //分割基準の文字 (Use this character for split csv)
        public static List<Settings> ConfigureTable = new List<Settings>(1000); //設定内容を格納するListを定義 (Define List for save configure)
        //
        public static void LoadConfigure(string LoadPath) //設定ファイルをロード (Load Configure File)
        {
            Console.WriteLine("Loading Configure File. Please wait...");
            try 
            {
                //MessageBox.Show("Starts Load");
                //ファイルストリームオープン (Open Filestream)
                StreamReader sr = new StreamReader(new FileStream(LoadPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite));
                string line = "";
                while ((line = sr.ReadLine()) != null) //ファイルを最後まで読む (Read All Contents)
                {
                    if (line.Contains(HeaderString))
                    {
                        continue; 
                    }
                    string[] fields = line.Split(SplitChar); //ロードした文字列を分割 (Split Loaded Strings)
                    string Key = fields[0]; //Key文字列はここ (Key Strings)
                    string Content = fields[1]; //Content文字列はここ (Content Strings)
                    if (Content.Contains(HeaderString))
                    {
                        continue; //Ignore
                    }
                    ConfigureTable.Add(new Settings(Key, Content)); //設定内容格納用Listに書き込み (Write Loaded Configure to List)
                }
                //後始末
                sr.Close(); //Close Stream
                sr = null;  //Release
                Console.WriteLine("Done.");
            }
            catch (Exception ex) //例外処理 (Exception)
            {
                Console.WriteLine("An Error occured while loading configure.\n" + ex.Message);
            }
        }
    }
    class Settings //設定ファイル格納List用クラス定義 (Class Define for Configure List)
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
