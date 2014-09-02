using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace UI
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            App.Current.CheckAndDownloadUpdateAsync();
            setupBtn.Click += new RoutedEventHandler(setupBtn_Click);
            App.Current.CheckAndDownloadUpdateCompleted += new CheckAndDownloadUpdateCompletedEventHandler(Current_CheckAndDownloadUpdateCompleted);
            if (App.Current.IsRunningOutOfBrowser && App.Current.InstallState == InstallState.Installed)
            {
                table.Visibility = Visibility.Visible;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string rsp = System.IO.Path.Combine(path, "RockScissorsPaper");
                string fileName = System.IO.Path.Combine(rsp, "record.txt");
                string record = "";
                if (File.Exists(fileName))
                {
                    StreamReader sr = new StreamReader(fileName);
                    record = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                if (record.Length > 10000)
                {
                    record = record.Substring(record.Length - 10000);
                }
                table.RecordTxt = record;
                fileName = System.IO.Path.Combine(rsp, "grade.txt");
                string grade = "";
                if (File.Exists(fileName))
                {
                    StreamReader sr = new StreamReader(fileName);
                    grade = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                table.GradeLevel = grade;
            }
            else
            {
                iniGrid.Visibility = Visibility.Visible;
            }
        }

        void Current_CheckAndDownloadUpdateCompleted(object sender, CheckAndDownloadUpdateCompletedEventArgs e)
        {
            if (e.UpdateAvailable)
            {
                MessageBox.Show("程序有新的更新已经安装，请重新启动程序以便完成更新！");
            }
        }
        void setupBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!App.Current.IsRunningOutOfBrowser && App.Current.InstallState == InstallState.NotInstalled)
                App.Current.Install();
            else
                MessageBox.Show("程序已经安装，你可以点击桌面上的图标运行程序。如果要卸载程序，请使用右键卸载！");
        }
    }
}
