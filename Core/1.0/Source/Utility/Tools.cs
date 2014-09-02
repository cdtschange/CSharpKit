using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Cdts.Utility
{
    public class Tools
    {
        public static string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public static Image Base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            Image image = null;
            using (MemoryStream ms = new MemoryStream(imageBytes, 0,
               imageBytes.Length))
            {
                // Convert byte[] to Image
                ms.Write(imageBytes, 0, imageBytes.Length);
                image = Image.FromStream(ms, true);
            }
            return image;
        }

        public class BinaryConversion
        {
            /// <summary>
            /// 十进制转换为其他进制
            /// </summary>
            /// <param name="source">十进制数</param>
            /// <param name="tartget">其他进制</param>
            /// <returns>返回转换后的结果</returns>
            public static uint DecToOther(uint source, uint tartget)
            {
                Stack<int> s = new Stack<int>();
                while (source > 0)
                {
                    s.Push((int)(source % tartget));
                    source /= tartget;
                }
                string result = "";
                while (s.Count > 0)
                {
                    result += s.Pop();
                }
                return uint.Parse(result);
            }
        }

        /// <summary>
        /// 字符对匹配
        /// </summary>
        /// <param name="left">左字符对，例如{[(</param>
        /// <param name="right">右字符对，例如}])</param>
        /// <param name="str">需要验证的字符串</param>
        /// <returns>返回字符对匹配验证结果</returns>
        public static bool CharsMatches(string left, string right, string str)
        {
            Stack<char> st = new Stack<char>();
            try
            {
                foreach (var c in str)
                {
                    if (left.Contains(c))
                    {
                        st.Push(c);
                    }
                    else if (right.Contains(c))
                    {
                        if (left.IndexOf(st.Peek()) == right.IndexOf(c))
                        {
                            st.Pop();
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            if (st.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
