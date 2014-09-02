using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Cdts.Utility.OS
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct _OSVERSIONINFOEX
    {
        public Int32 dwOSVersionInfoSize;
        public Int32 dwMajorVersion;
        public Int32 dwMinorVersion;
        public Int32 dwBuildNumber;
        public Int32 dwPlatformId;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public Char[] szCSDVersion;
        public Int16 wServicePackMajor;
        public Int16 wServicePackMinor;
        public Int16 wSuiteMask;
        public Byte wProductType;
        public Byte wReserved;
    }

    /// <summary>
    /// 操作系统名称
    /// </summary>
    public enum OSName
    {
        Empty,
        MicrosoftWindows2000,
        MicrosoftWindowsXP,
        MicrosoftWindowsHomeServer,
        MicrosoftWindowsServer2003,
        MicrosoftWindowsServer2003R2,
        MicrosoftWindowsVista,
        MicrosoftWindowsServer2008,
        MicrosoftWindows7,
        MicrosoftWindowsServer2008R2
    }

    /// <summary>
    /// 操作系统信息
    /// </summary>
    public class OSInfo
    {
        [DllImport("kernel32.dll")]
        public static extern bool GetVersionEx(ref _OSVERSIONINFOEX osVersionInfo);
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int sm_SERVERR2);

        /// <summary>
        /// 获取操作系统名称
        /// </summary>
        /// <returns>操作系统名称</returns>
        public static OSName GetOSName()
        {
            _OSVERSIONINFOEX osVersionInfo = new _OSVERSIONINFOEX();
            osVersionInfo.dwOSVersionInfoSize = 156;

            if (GetVersionEx(ref osVersionInfo))
            {
                switch (osVersionInfo.dwMajorVersion)
                {
                    case 5:
                        switch (osVersionInfo.dwMinorVersion)
                        {
                            case 0:
                                return OSName.MicrosoftWindows2000;
                            case 1:
                                return OSName.MicrosoftWindowsXP;
                            case 2:
                                if ((osVersionInfo.wSuiteMask & 0x00008000) != 0)
                                    return OSName.MicrosoftWindowsHomeServer;
                                if (osVersionInfo.wProductType == 1 && OSBitness.Is64BitOperatingSystem())
                                    return OSName.MicrosoftWindowsXP;
                                if (GetSystemMetrics(89) == 0)
                                    return OSName.MicrosoftWindowsServer2003;
                                else
                                    return OSName.MicrosoftWindowsServer2003R2;
                        }
                        break;
                    case 6:
                        switch (osVersionInfo.dwMinorVersion)
                        {
                            case 0:
                                if (osVersionInfo.wProductType == 1)
                                    return OSName.MicrosoftWindowsVista;
                                else
                                    return OSName.MicrosoftWindowsServer2008;
                            case 1:
                                if (osVersionInfo.wProductType == 1)
                                    return OSName.MicrosoftWindows7;
                                else
                                    return OSName.MicrosoftWindowsServer2008R2;
                        }
                        break;
                }
            }
            return OSName.Empty;
        }
    }
}
