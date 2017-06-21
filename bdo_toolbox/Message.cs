using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace bdo_toolbox
{
    class Message
    {
        public static void UnAvailable_Message()
        {
            switch (MainWindow.Language)
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
        public static void PingingTimeSpan_NotAllowed()
        {
            switch (MainWindow.Language)
            {
                case "Japanese":
                    MessageBox.Show("各国サービスの認証サーバを利用しPing計測を行っている場合、サーバー負荷への配慮のため3秒以下へのPing間隔の設定は出来ません。3秒へと自動的に変更されます。", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "English":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "T_Chinese":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
                case "S_Chinese":
                    MessageBox.Show("When using an each services auth server for pinging, doesn't allow less than 3 sec pinging interval for Consideration of server load. Pinging interval will be changed to 3 sec automatically.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    break;
            }
        }
        public static void FinishedPatching_Message()
        {
            switch (MainWindow.Language)
            {
                case "English":
                    MessageBox.Show("Patching is Finished.");
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
        public static void InstallFolder_NotExist()
        {
            switch (MainWindow.Language)
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
        public static void InstallFolder_NotFound()
        {
            switch (MainWindow.Language)
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
        public static void RoutingAssigner_Guides() //Won't be used.
        {
            switch (MainWindow.Language)
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
        public static void MetaRefresh_Done()
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("Meta Refresh Finished.");
                    break;
                case "Japanese":
                    MessageBox.Show("Metaファイルのリフレッシュが完了しました。");
                    break;
            }
        }
        public static void PatchFolder_Deleted()
        {
            switch (MainWindow.Language)
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
        public static void InvalidPatchPackage(string ErrorFile)
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("Invalid Patch Package.\n(Load Failed : " + ErrorFile, "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Japanese":
                    MessageBox.Show("不正なパッチパッケージです。\n(ロード失敗 : " + ErrorFile, "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        public static void FailedByPermission()
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("Failed to Install the patch files. try run as administrator.\n", "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Japanese":
                    MessageBox.Show("パッチファイルの書き込みに失敗しました。管理者権限で再度試行して下さい。", "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        public static void UnknownErrorWhilePatching()
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("Unknown Error Happened while patching.\nDetails is in log text on Log Folder.\nif you can't solve the problem, please contact to the Developer with content of log text.", "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case "Japanese":
                    MessageBox.Show("パッチ中に不明なエラーが発生しました。\n詳細はLogフォルダ内のログファイルを閲覧してください。\nまた解決が困難な場合はログファイルの内容を開発者に報告してください。", "Patch Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }
        public static void BackupMetaIsNotOriginal()
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("Backed up Meta File is not original.\nWould you do get original meta file from the patch server?", "Attention", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                    break;
                case "Japanese":
                    if (MessageBox.Show("バックアップされたMetaファイルはオリジナルではありません。オリジナルのMetaファイルをパッチサーバーから取得しますか？", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                    {
                        break;
                    }
                    if (MessageBox.Show("バックアップされたMetaファイルはオリジナルではありません。オリジナルのMetaファイルをパッチサーバーから取得しますか？", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {

                        break;
                    }
                    break;
            }
        }
        public static string FailedToReceivedDataFromServer()
        {
            var unavailable_message = "";
            switch (MainWindow.Language)
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
        public static void PatcherUpdateNotice()
        {
            switch (MainWindow.Language)
            {
                default:
                    var result = MessageBox.Show("The new version of BDO ToolBox is available now for improve function and bug fixes.\n Would you update the patcher?", "New Version Notice", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        //MessageBox.Show("Yes");
                        Util.RunUpdater(MainWindow.UpdaterPath);
                        break;
                    }
                    else
                    {
                        //MessageBox.Show("No");
                        break;
                    }
                    
                case "Japanese":
                    var result_jp = MessageBox.Show("機能向上・バグ修正が行われたBDO ToolBoxの新しいバージョンが利用可能です。\nアップデートしますか？", "新バージョン告知", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result_jp == MessageBoxResult.Yes)
                    {
                        //MessageBox.Show("Yes");
                        Util.RunUpdater(MainWindow.UpdaterPath);
                        break;
                    }
                    else
                    {
                        //MessageBox.Show("No");
                        break;
                    }
            }
        }
        public static void AlreadyLatest()
        {
            switch (MainWindow.Language)
            {
                default:
                    MessageBox.Show("BDO ToolBox is already latest version.");
                    break;
                case "Japanese":
                    MessageBox.Show("BDO ToolBoxは既に最新のバージョンです。");
                    break;
            }
        }
    }
}
