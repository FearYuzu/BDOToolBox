using BlackDesert.SharedLibs;
using BlackDesert_Patcher.Properties;
using BlackDesert_Patcher;
using Ionic.Zip;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace BlackDesert_Patcher
{
	public class progressBarForm : Window
	{
		private IContainer components;

        public ProgressBar progressBar;

		public progressBarForm()
		{
			this.InitializeComponent();
		}

		//private void progressBarForm_Load(object sender, EventArgs e)
		//{
		//	base.ControlBox = false;
		//}

		//protected override void Dispose(bool disposing)
		//{
		//	bool flag = disposing && this.components != null;
		//	if (flag)
		//	{
			//	this.components.Dispose();
			//}
			//base.Dispose(disposing);
		//}

		private void InitializeComponent()
		{
			//this.progressBar = new ProgressBar();
			//base.SuspendLayout();
			//this.progressBar.Location = new Point(12, 12);
			//this.progressBar.Name = "progressBar";
			//this.progressBar.Size = new Size(342, 50);
			//this.progressBar.TabIndex = 0;
			//base.AutoScaleDimensions = new SizeF(6f, 13f);
			//base.AutoScaleMode = AutoScaleMode.Font;
			//base.ClientSize = new Size(366, 74);
			//base.Controls.Add(this.progressBar);
			//base.FormBorderStyle = FormBorderStyle.FixedDialog;
			//base.Name = "progressBarForm";
			//base.StartPosition = FormStartPosition.CenterScreen;
			//this.Text = "Updating / Aktualizuje";
			//base.Load += new EventHandler(this.progressBarForm_Load);
			//base.ResumeLayout(false);
		}
	}
}
