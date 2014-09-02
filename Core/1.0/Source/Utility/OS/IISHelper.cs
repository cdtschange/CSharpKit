using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.IO;
using System.Diagnostics;
using Microsoft.Win32;

namespace Cdts.Utility.OS
{
    /// <summary>
    /// IIS工具
    /// </summary>
    public class IISHelper
    {
        private static OSName operatingSystemName = OSName.Empty;
        /// <summary>
        /// 操作系统名称
        /// </summary>
        protected static OSName OperatingSystemName
        {
            get
            {
                if (operatingSystemName == OSName.Empty)
                {
                    operatingSystemName = OSInfo.GetOSName();
                }
                return operatingSystemName;
            }
        }

        private static bool? is64Bitness = null;
        /// <summary>
        /// 是否是64位操作系统
        /// </summary>
        protected static bool Is64Bitness
        {
            get
            {
                if (is64Bitness == null)
                {
                    is64Bitness = OSBitness.Is64BitOperatingSystem();
                }
                return (bool)is64Bitness;
            }
        }

        /// <summary>
        /// 安装IIS执行命令（IIS7）
        /// </summary>
        protected static string SysocmgrCmdIIS7
        {
            get
            {
                return "start /w pkgmgr /norestart /[option]:" +
                    "IIS-WebServerRole;" +
                    "IIS-WebServer;" +
                    "IIS-CommonHttpFeatures;" +
                    "IIS-StaticContent;" +
                    "IIS-DefaultDocument;" +
                    "IIS-DirectoryBrowsing;" +
                    "IIS-HttpErrors;" +
                    "IIS-HttpRedirect;" +
                    "IIS-ApplicationDevelopment;" +
                    "IIS-ASPNET;" +
                    "IIS-NetFxExtensibility;" +
                    "IIS-ASP;" +
                    "IIS-ISAPIExtensions;" +
                    "IIS-ISAPIFilter;" +
                    "IIS-ServerSideIncludes;" +
                    "IIS-HealthAndDiagnostics;" +
                    "IIS-HttpLogging;" +
                    "IIS-LoggingLibraries;" +
                    "IIS-RequestMonitor;" +
                    "IIS-HttpTracing;" +
                    "IIS-CustomLogging;" +
                    "IIS-ODBCLogging;" +
                    "IIS-Security;" +
                    "IIS-BasicAuthentication;" +
                    "IIS-WindowsAuthentication;" +
                    "IIS-DigestAuthentication;" +
                    "IIS-ClientCertificateMappingAuthentication;" +
                    "IIS-IISCertificateMappingAuthentication;" +
                    "IIS-URLAuthorization;" +
                    "IIS-RequestFiltering;" +
                    "IIS-IPSecurity;" +
                    "IIS-Performance;" +
                    "IIS-WebServerManagementTools;" +
                    "IIS-ManagementConsole;" +
                    "IIS-ManagementScriptingTools;" +
                    "IIS-ManagementService;" +
                    "IIS-IIS6ManagementCompatibility;" +
                    "IIS-Metabase;" +
                    "IIS-WMICompatibility;" +
                    "IIS-LegacyScripts;" +
                    "IIS-LegacySnapIn;" +
                    "WAS-WindowsActivationService;" +
                    "WAS-ProcessModel;" +
                    "WAS-NetFxEnvironment;" +
                    "WAS-ConfigurationAPI;";
            }
        }
        /// <summary>
        /// 安装IIS执行命令（IISxp）
        /// </summary>
        protected static string SysocmgrCmdIISxp
        {
            get
            {
                return @"sysocmgr /i:%windir%\inf\sysoc.inf /u:";
            }
        }
        /// <summary>
        /// 注册表安装查找路径
        /// </summary>
        protected static string regServicePackSourcePath;
        /// <summary>
        /// 注册表安装查找路径
        /// </summary>
        protected static string regSourcePath;

        /// <summary>
        /// 获取IIS版本
        /// </summary>
        /// <returns>IIS版本</returns>
        public static int GetIISVersion()
        {
            int version = 0;
            string path = "IIS://localhost/W3SVC/INFO";
            OSName os = OSInfo.GetOSName();
            if (os == OSName.MicrosoftWindowsVista
                || os == OSName.MicrosoftWindowsServer2008
                || os == OSName.MicrosoftWindows7
                || os == OSName.MicrosoftWindowsServer2008R2)
            {

                DirectoryEntry entry = null;
                try
                {
                    entry = new DirectoryEntry(path);
                }
                catch
                {
                    return 0;
                }

                try
                {
                    version = (int)entry.Properties["MajorIISVersionNumber"].Value;
                }
                catch
                {
                    version = 0;
                    path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"inetsrv\inetinfo.exe");
                    if (File.Exists(path))
                    {
                        FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
                        version = versionInfo.FileMajorPart;
                        return version;
                    }
                }
            }
            else
            {
                path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.System), @"inetsrv\inetinfo.exe");
                if (File.Exists(path))
                {
                    FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(path);
                    version = versionInfo.FileMajorPart;
                    return version;
                }
            }
            return version;
        }
        /// <summary>
        /// 安装IIS
        /// </summary>
        /// <param name="sourceFilePath">源目录</param>
        /// <returns>返回结果</returns>
        public static bool InstallIIS(string sourceFilePath)
        {
            int iisVersion = GetIISVersion();
            if (iisVersion >= 5)// 已安装过IIS
            { return true; }
            if (!File.Exists(sourceFilePath))
            {
                return false;
            }
            switch (OperatingSystemName)
            {
                case OSName.Empty:
                    break;
                case OSName.MicrosoftWindows2000:
                case OSName.MicrosoftWindowsXP:
                case OSName.MicrosoftWindowsServer2003:
                case OSName.MicrosoftWindowsServer2003R2:
                    string setupPackPath = Is64Bitness ?
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @" x64") :
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName);
                    string inOptionalFilePath = Is64Bitness ?
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @" x64\Config\Install.txt") :
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @"\Config\Install.txt");
                    try
                    {
                        UpdateRegeditPath(setupPackPath);
                        Cmd.ExecuteCmd(SysocmgrCmdIISxp + "\"" + inOptionalFilePath + "\"");
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    finally
                    {
                        ReStoreRegeditPath();
                    }
                    break;
                case OSName.MicrosoftWindowsVista:
                case OSName.MicrosoftWindowsServer2008:
                case OSName.MicrosoftWindows7:
                case OSName.MicrosoftWindowsServer2008R2:
                    Cmd.ExecuteCmd(SysocmgrCmdIIS7.Replace("[option]", "iu"));
                    break;
                default:
                    break;
            }
            return true;
        }
        /// <summary>
        /// 卸载IIS
        /// </summary>
        /// <param name="sourceFilePath">源目录</param>
        /// <returns>返回结果</returns>
        public static bool UnInstallIIS(string sourceFilePath)
        {
            int iisVersion = GetIISVersion();
            if (iisVersion < 5)// 未安装过IIS
            { return true; }
            switch (OperatingSystemName)
            {
                case OSName.Empty:
                    break;
                case OSName.MicrosoftWindows2000:
                case OSName.MicrosoftWindowsXP:
                case OSName.MicrosoftWindowsHomeServer:
                case OSName.MicrosoftWindowsServer2003:
                case OSName.MicrosoftWindowsServer2003R2:
                    string setupPackPath = Is64Bitness ?
                       Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @" x64") :
                       Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName);
                    string unOptionalFilePath = Is64Bitness ?
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @" x64\Config\UnInstall.txt") :
                        Path.Combine(sourceFilePath, @"Source\" + OperatingSystemName + @"\Config\UnInstall.txt");
                    Cmd.ExecuteCmd(SysocmgrCmdIISxp + "\"" + unOptionalFilePath + "\"");
                    break;
                case OSName.MicrosoftWindowsVista:
                case OSName.MicrosoftWindowsServer2008:
                case OSName.MicrosoftWindows7:
                case OSName.MicrosoftWindowsServer2008R2:
                    Cmd.ExecuteCmd(SysocmgrCmdIIS7.Replace("[option]", "uu"));
                    break;
                default:
                    break;
            }
            return true;
        }

        /// <summary>
        /// 修改安装查找路径
        /// </summary>
        private static void UpdateRegeditPath(string setupPackPath)
        {
            RegistryKey pRegKey = Registry.LocalMachine;
            pRegKey = pRegKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Setup", true);
            regServicePackSourcePath = pRegKey.GetValue("ServicePackSourcePath").ToString();
            regSourcePath = pRegKey.GetValue("SourcePath").ToString();
            pRegKey.SetValue("ServicePackSourcePath", setupPackPath);
            pRegKey.SetValue("SourcePath", setupPackPath);
        }
        /// <summary>
        /// 恢复安装查找路径
        /// </summary>
        private static void ReStoreRegeditPath()
        {
            RegistryKey pRegKey = Registry.LocalMachine;
            pRegKey = pRegKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Setup", true);
            pRegKey.SetValue("ServicePackSourcePath", regServicePackSourcePath);
            pRegKey.SetValue("SourcePath", regSourcePath);
        }
        /// <summary>
        /// 设置MimeType属性
        /// </summary>
        /// <param name="path"></param>
        private static void SetMimeTypeProperty(DirectoryEntry path)
        {
            //try
            //{
            //    PropertyValueCollection propValues = path.Properties["MimeMap"];
            //    object exists = null;
            //    foreach (object value in propValues)
            //    {
            //        // IISOle requires a reference to the Active DS IIS Namespace Provider in Visual Studio .NET
            //        IISOle.IISMimeType mimetypeObj = (IISOle.IISMimeType)value;
            //        if (".xap" == mimetypeObj.Extension)
            //            exists = value;
            //    }

            //    if (null != exists)
            //    {
            //        propValues.Remove(exists);
            //    }

            //    IISOle.MimeMapClass newObj = new IISOle.MimeMapClass();
            //    newObj.Extension = ".xap";
            //    newObj.MimeType = "application/x-silverlight-app";
            //    propValues.Add(newObj);
            //    path.CommitChanges();
            //}
            //catch (Exception ex)
            //{
            //    if ("HRESULT 0x80005006" == ex.Message)
            //        Console.WriteLine(" Property MimeMap does not exist at {0}", path.Path);
            //    else
            //        Console.WriteLine("Failed in SetMimeTypeProperty with the following exception: \n{0}", ex.Message);
            //}
        }
    }
}
