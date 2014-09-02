using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Cdts.Web
{
    public interface IHttpSendBase
    {
        /// <summary>
        /// 获取代理配置
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        bool GetProxyConfig(ref WebProxy proxy);
        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="HWResp"></param>
        /// <returns></returns>
        Stream Gzip(HttpWebResponse HWResp);

        void AbortRequest(out string message);
        string SendPost(string url, byte[] date, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, string referer, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, ref System.Net.CookieContainer cookie, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, string referer, ref System.Net.CookieContainer cookie, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, string referer, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, out string message);
        string SendPost(string url, byte[] date, Encoding encoding, string referer, bool allowAutoRedirect, string contentType, ref System.Net.CookieContainer cookie, out string message);

        string SendGet(string url, out string message);
        string SendGet(string url, Encoding encoding, out string message);
        string SendGet(string url, Encoding encoding, ref System.Net.CookieContainer cookie, out string message);
        string SendGet(string url, string referer, Encoding encoding, ref System.Net.CookieContainer cookie, out string message);
        string SendGet(string url, string referer, Encoding encoding, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, out string message);
        string SendGet(string url, string referer, Encoding encoding, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, int timeout, out string message);
        byte[] SendGetStream(string url, string referer, Encoding encoding, ref System.Net.CookieContainer cookie, out string message);
    }
}
