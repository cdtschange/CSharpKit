using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Cdts.Utility.Hook
{
    public class MouseHelper
    {
        /// <summary>
        /// 获取鼠标于屏幕的坐标
        /// </summary>
        /// <param name="cPoint">鼠标坐标</param>
        /// <returns></returns>
        [DllImport("User32")]
        public extern static bool GetCursorPos(ref Point cPoint);

        /// <summary>
        /// 限制鼠标范围
        /// </summary>
        /// <param name="r">范围区域</param>
        /// <returns></returns>
        [DllImport("User32")]
        public extern static int ClipCursor(ref Rect r);

        /// <summary>
        /// 获取窗体的范围区域
        /// </summary>
        /// <param name="h">窗体句柄（form.Handle.ToInt32()）</param>
        /// <param name="r">范围区域</param>
        /// <returns></returns>
        [DllImport("User32")]
        public extern static int GetWindowRect(int h, ref Rect r);

        /// <summary>
        /// 鼠标定位
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        /// <summary>
        /// 显示/隐藏鼠标
        /// </summary>
        /// <param name="bshow">是否显示</param>
        /// <returns></returns>
        [DllImport("User32")]
        public static extern int ShowCursor(bool bshow);

        /// <summary>
        /// 鼠标事件
        /// </summary>
        /// <param name="flags"></param>
        /// <param name="dx"></param>
        /// <param name="dy"></param>
        /// <param name="data"></param>
        /// <param name="extraInfo"></param>
        [DllImport("user32.dll")]
        public static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, IntPtr extraInfo);

        /// <summary>
        /// 锁定鼠标活动范围
        /// </summary>
        /// <param name="form">范围窗体</param>
        public static void LockMouse(Form form)
        {
            Rect formRect = new Rect();
            GetWindowRect(form.Handle.ToInt32(), ref formRect);
            ClipCursor(ref formRect);
        }
        /// <summary>
        /// 解除锁定鼠标活动范围
        /// </summary>
        public static void UnlockMouse()
        {
            Rect ur = new Rect();
            ur.Left = 0;
            ur.Top = 0;
            ur.Bottom = Screen.PrimaryScreen.Bounds.Bottom;
            ur.Right = Screen.PrimaryScreen.Bounds.Right;
            ClipCursor(ref ur);
        }
    }

    public struct Rect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [Flags]
    public enum MouseEventFlag : uint
    {
        Move = 0x0001,
        LeftDown = 0x0002,
        LeftUp = 0x0004,
        RightDown = 0x0008,
        RightUp = 0x0010,
        MiddleDown = 0x0020,
        MiddleUp = 0x040,
        XDown = 0x0080,
        XUp = 0x0100,
        Wheel = 0x0800,
        VirtualDesk = 0x4000,
        Absolute = 0x8000 //标示是否采用绝对坐标
    }
}
