using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Cdts.Utility.Hook
{
    public class KeyboardHelper
    {
        public const int KEYEVENTF_KEYUP = 2;

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern byte MapVirtualKey(byte wCode, int wMap);
    }
}
