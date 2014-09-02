using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Cdts.Utility
{
    public static class Cmd
    {
        public static string ExecuteCmd(string command)
        {
            string myResult;
            Process p = new Process();
            try
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;//关闭Shell的使用
                p.StartInfo.RedirectStandardInput = true;//重定向标准输入
                p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
                p.StartInfo.RedirectStandardError = true;//重定向错误输出
                p.StartInfo.CreateNoWindow = true;//设置不显示窗口

                p.Start();
                p.StandardInput.WriteLine("D:");
                p.StandardInput.WriteLine("cd D:\\htk0");
                p.StandardInput.WriteLine(command);

                p.StandardInput.WriteLine("exit");
                myResult = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            finally
            {
                p.Close();
            }
            return myResult;
        }

        public static string ExecuteCmd(string[] commands)
        {
            string myResult;
            Process p = new Process();
            try
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.UseShellExecute = false;//关闭Shell的使用
                p.StartInfo.RedirectStandardInput = true;//重定向标准输入
                p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
                p.StartInfo.RedirectStandardError = true;//重定向错误输出
                p.StartInfo.CreateNoWindow = true;//设置不显示窗口

                p.Start();
                for (int i = 0; i < commands.Length; i++)
                {
                    p.StandardInput.WriteLine(commands[i]);
                }
                p.StandardInput.WriteLine("exit");
                myResult = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            finally
            {
                p.Close();
            }
            return myResult;
        }

        public static string ExecuteCmd(string fileName, params string[] args)
        {
            string myResult = "";
            Process p = new Process();
            try
            {
                string arg = "";
                if (args != null)
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        arg += args[i] + " ";
                    }
                }
                arg = arg.Trim();
                p.StartInfo = new ProcessStartInfo(fileName, arg);
                p.StartInfo.UseShellExecute = false;//关闭Shell的使用
                p.StartInfo.RedirectStandardInput = true;//重定向标准输入
                p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
                p.StartInfo.RedirectStandardError = true;//重定向错误输出
                p.StartInfo.CreateNoWindow = true;//设置不显示窗口
                p.Start();
                string line;
                StreamReader myStreamReader = p.StandardOutput;
                while ((line = myStreamReader.ReadLine()) != null)
                {
                    myResult += line + "\r\n";
                }
                p.WaitForExit();
            }
            finally
            {
                p.Close();
            }
            return myResult;
        }

        public static void OpenApplication(string processName)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(processName);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            Process.Start(startInfo);
        }
        public static void OpenApplication(string processName, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(processName, args);
            startInfo.WindowStyle = ProcessWindowStyle.Minimized;
            Process.Start(startInfo);
        }
    }
}
