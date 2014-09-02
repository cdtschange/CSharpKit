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
    public partial class App : Application
    {

        public App()
        {
            this.Startup += this.Application_Startup;
            this.Exit += this.Application_Exit;
            this.UnhandledException += this.Application_UnhandledException;

            InitializeComponent();
        }
        MainPage m;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            m = new MainPage();
            this.RootVisual = m;
        }

        private void Application_Exit(object sender, EventArgs e)
        {
            if (App.Current.IsRunningOutOfBrowser && App.Current.InstallState == InstallState.Installed)
            {
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                string rsp = System.IO.Path.Combine(path, "RockScissorsPaper");
                if (!Directory.Exists(rsp))//如果不存在就创建file文件夹
                {
                    Directory.CreateDirectory(rsp);
                }
                string fileName = System.IO.Path.Combine(rsp, "record.txt");
                StreamWriter sw = new StreamWriter(fileName);
                sw.Write(m.table.RecordTxt);
                sw.Close();
                sw.Dispose();
                fileName = System.IO.Path.Combine(rsp, "grade.txt");
                sw = new StreamWriter(fileName);
                sw.Write(m.table.GradeLevel);
                sw.Close();
                sw.Dispose();
            }
        }

        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            // If the app is running outside of the debugger then report the exception using
            // the browser's exception mechanism. On IE this will display it a yellow alert 
            // icon in the status bar and Firefox will display a script error.
            if (!System.Diagnostics.Debugger.IsAttached)
            {

                // NOTE: This will allow the application to continue running after an exception has been thrown
                // but not handled. 
                // For production applications this error handling should be replaced with something that will 
                // report the error to the website and stop the application.
                e.Handled = true;
                Deployment.Current.Dispatcher.BeginInvoke(delegate { ReportErrorToDOM(e); });
            }
        }

        private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
        {
            try
            {
                string errorMsg = e.ExceptionObject.Message + e.ExceptionObject.StackTrace;
                errorMsg = errorMsg.Replace('"', '\'').Replace("\r\n", @"\n");

                System.Windows.Browser.HtmlPage.Window.Eval("throw new Error(\"Unhandled Error in Silverlight Application " + errorMsg + "\");");
            }
            catch (Exception)
            {
            }
        }
    }
}
