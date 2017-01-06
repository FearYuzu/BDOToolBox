using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.Win32;
using System.Net;

namespace bdo_toolbox
{
    class PatchProcess
    {
        static WebClient webclient = new WebClient();
        private string InstallPath;
        public static void JPForTaiwan(string InstallPath)
        {
            webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/LD_JP_EN.zip", "data/LD_JP_EN.zip");
            ZipFile zipFile = ZipFile.Read("data/LD_JP_EN.zip");
            zipFile.ExtractAll(InstallPath + "prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile.Dispose();
            zipFile = null;
            webclient.Dispose();
            webclient = null;
        }
        public static void ENForTaiwan(string InstallPath)
        {
            webclient.DownloadFile("http://files.indigoflare.net/bdotoolbox/patch/tw_en_latest.zip", "data/tw_en_latest.zip");
            ZipFile zipFile = ZipFile.Read("data/tw_en_latest.zip");
            zipFile.ExtractAll(InstallPath + "prestringtable/tw/", ExtractExistingFileAction.OverwriteSilently);
            zipFile.Dispose();
            zipFile = null;
            webclient.Dispose();
            webclient = null;
        }
    }
}
