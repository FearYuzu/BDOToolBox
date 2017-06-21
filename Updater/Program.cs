using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Updater
{
    class Program
    {

        static void Main(string[] args)
        {
            WebClient downloader = new WebClient();
            string BDOToolBoxBaseDir = Environment.GetCommandLineArgs()[0];
            string BDOToolBoxDirFullPath = System.IO.Path.GetFullPath(BDOToolBoxBaseDir);
            string BDOToolBoxStartupPath = System.IO.Path.GetDirectoryName(BDOToolBoxDirFullPath);
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("-              BDO ToolBox Updater               -");
            Console.WriteLine("--------------------------------------------------");
            Configure.LoadConfigure(BDOToolBoxStartupPath + "/config.ini");
            string bdotoolbox = GetConfigureContent("UpdatedPatcherURI");
            string language = GetConfigureContent("Language");
            try
            {
                switch (language)
                {
                    default:
                        Console.WriteLine("Downloading...");
                        break;
                    case "Japanese":
                        Console.WriteLine("ダウンロード中...");
                        break;
                }
                downloader.DownloadFile(bdotoolbox, BDOToolBoxStartupPath + "/data/bdo_toolbox.exe");
                Process[] ps = Process.GetProcessesByName("bdo_toolbox");
                foreach(Process p in ps)
                {
                    p.Kill();
                }
                System.Threading.Thread.Sleep(2000);
                File.Copy(BDOToolBoxStartupPath + "/data/bdo_toolbox.exe", BDOToolBoxStartupPath + "/bdo_toolbox.exe",true);
                switch (language)
                {
                    default:
                        Console.WriteLine("Update Completed. Press any key to restart BDO ToolBox.");
                        break;
                    case "Japanese":
                        Console.WriteLine("アップデートが完了しました。任意のキーを入力することでBDO ToolBoxを再起動します。");
                        break;
                }
                Console.ReadKey();
                Process.Start(BDOToolBoxStartupPath + "/bdo_toolbox.exe");
                File.Delete(BDOToolBoxStartupPath + "/data/bdo_toolbox.exe");
                Environment.Exit(0);

            }
            catch (Exception ex)
            {
                Console.WriteLine("An Error occured while updating.\n" + ex.Message + "\n" + ex.InnerException);
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
        public static string GetConfigureContent(string Key)　//設定ファイルからロードしListに格納したデータを取り出し
        {
            string return_value = "";
            for (int i = 0; i < Configure.ConfigureTable.Count; i++) //設定格納List内を検索
            {
                string preload = Configure.ConfigureTable[i].Key;


                if (String.Equals(Key, preload)) //検索対象と検索結果が一致したら
                {

                    return_value = Configure.ConfigureTable[i].Content;
                    // MessageBox.Show("loaded. " + return_value);

                }

            }
            return return_value; //一致した検索結果を返す

        }
        
    }
}
