using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Collections;

namespace Cdts.Web
{
    public class Common
    {
        /// <summary>
        /// 获取服务器的当前时间
        /// </summary>
        /// <param name="length">获取时间长度：13：精确到毫秒，10或者不填为秒</param>
        /// <returns></returns>
        public static string GetServerTime(int length)
        {
            TimeSpan ts = DateTime.Now.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0);
            long l = 0;
            switch (length)
            {
                case 10:
                    l = (long)(ts.TotalSeconds);
                    break;
                case 13:
                    l = (long)(ts.TotalMilliseconds);
                    break;
                default:
                    l = (long)(ts.TotalSeconds);
                    break;
            }

            return l.ToString();
        }

        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();

            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, cc, new object[] { });

            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }

            return lstCookies;
        }
    }
}
