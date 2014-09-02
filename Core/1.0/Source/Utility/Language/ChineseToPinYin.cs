using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.International.Converters.PinYinConverter;

namespace Cdts.Utility.Language
{
    public class ChineseToPinYin
    {
        /// <summary>
        /// 把汉字转换成拼音(全拼)
        /// </summary>
        /// <param name="hzString">汉字字符串</param>
        /// <returns>转换后的拼音(全拼)字符串</returns>
        public static string Convert(string hzString)
        {
            string r = string.Empty;
            foreach (char c in hzString)
            {
                try
                {
                    ChineseChar chineseChar = new ChineseChar(c);
                    string t = chineseChar.Pinyins[0].ToString();
                    r += t.Substring(0, t.Length - 1);
                }
                catch
                {
                    r += c.ToString();
                }
            }
            return r;
        }
    }
}
