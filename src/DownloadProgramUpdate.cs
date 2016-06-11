using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Windows;
using System.Windows.Controls;

namespace BlackDesert_Patcher
{
	internal class DownloadProgramUpdate : Window
	{
		private readonly string _downloadURL;

		private string _tempPath;

		private ProgressBar progressBar;

		public DownloadProgramUpdate(string downloadURL)
		{
			this.InitializeComponent();
			this._downloadURL = downloadURL;
		}

		//protected override void Dispose(bool disposing)
		//{
		//	base.Dispose(disposing);
		//}

		private void downloadProgramUpdate_Load(object sender, EventArgs e)
		{
			//base.ControlBox = false;
			WebClient webClient = new WebClient();
			Uri address = new Uri(this._downloadURL);
			string fileName = DownloadProgramUpdate.GetFileName(this._downloadURL);
			bool flag = fileName == null;
			if (flag)
			{
				int num = (int)MessageBox.Show("Unable to find update file, please make sure you're connected to the internet.", "Error");
				base.Close();
			}
			else
			{
				this._tempPath = string.Format("{0}{1}", Path.GetTempPath(), fileName);
				webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.OnDownloadProgressChanged);
				webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.OnDownloadComplete);
				webClient.DownloadFileAsync(address, this._tempPath);
			}
		}

		private static string GetFileName(string url)
		{
			string text = string.Empty;
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
			httpWebRequest.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
			httpWebRequest.Method = "HEAD";
			httpWebRequest.AllowAutoRedirect = false;
			string result;
			try
			{
				HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
				bool flag = (httpWebResponse.StatusCode.Equals(HttpStatusCode.Found) || httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently) || httpWebResponse.StatusCode.Equals(HttpStatusCode.MovedPermanently)) && httpWebResponse.Headers["Location"] != null;
				if (flag)
				{
					result = DownloadProgramUpdate.GetFileName(httpWebResponse.Headers["Location"]);
				}
				else
				{
					bool flag2 = httpWebResponse.Headers["content-disposition"] != null;
					if (flag2)
					{
						string text2 = httpWebResponse.Headers["content-disposition"];
						bool flag3 = !string.IsNullOrEmpty(text2);
						if (flag3)
						{
							int num = text2.IndexOf("filename=", StringComparison.CurrentCultureIgnoreCase);
							bool flag4 = num >= 0;
							if (flag4)
							{
								text = text2.Substring(num + "filename=".Length);
							}
							bool flag5 = text.StartsWith("\"") && text.EndsWith("\"");
							if (flag5)
							{
								text = text.Substring(1, text.Length - 2);
							}
						}
					}
					bool flag6 = string.IsNullOrEmpty(text);
					if (flag6)
					{
						text = Path.GetFileName(new Uri(url).LocalPath);
					}
					result = text;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		private void OnDownloadComplete(object sender, AsyncCompletedEventArgs e)
		{
			//Process.Start(new ProcessStartInfo
			//{
			//	FileName = this._tempPath,
			//	UseShellExecute = true
			//});
			//bool messageLoop = Application.MessageLoop;
			//if (messageLoop)
			//{
			//	Application.Exit();
			//}
			//else
			//{
			//	Environment.Exit(1);
			//}
		}

		private void OnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			this.progressBar.Value = e.ProgressPercentage;
		}

		private void InitializeComponent()
		{
			this.progressBar = new ProgressBar();
			//base.SuspendLayout();
			//this.progressBar.Location = new Point(12, 12);
			//this.progressBar.Name = "progressBar";
			//this.progressBar.Size = new Size(364, 36);
			//this.progressBar.TabIndex = 0;
			//base.ClientSize = new Size(388, 60);
			//base.Controls.Add(this.progressBar);
			//base.FormBorderStyle = FormBorderStyle.FixedDialog;
			//base.Name = "DownloadProgramUpdate";
			//base.ShowIcon = false;
			//base.StartPosition = FormStartPosition.CenterScreen;
			//this.Text = "Pobieram słownik / Downloading files";
			//base.TopMost = true;
			///base.Load += new EventHandler(this.downloadProgramUpdate_Load);
			//base.ResumeLayout(false);
		}
	}
}
