using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;


namespace Cdts.Web
{
    public class HttpSendBase : IHttpSendBase
    {
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;
        public static string proxyaddress = string.Empty;
        public static string proxyport = string.Empty;
        public static bool proxyuse = false;
        public static string proxyaccount = string.Empty;
        public static string proxypwd = string.Empty;

        #region IHttpSendBase 成员

        /// <summary>
        /// 获取代理配置
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public bool GetProxyConfig(ref WebProxy proxy)
        {
            try
            {
                if (proxyuse)
                {
                    string url = string.Format("http://{0}:{1}", proxyaddress, proxyport);
                    proxy.Address = new Uri(url);
                    if (!string.IsNullOrEmpty(proxyaccount) && proxypwd != null)
                    {
                        proxy.Credentials = new NetworkCredential(proxyaccount, proxypwd);
                        proxy.UseDefaultCredentials = false;
                    }
                    else
                        proxy.UseDefaultCredentials = true;
                    return true;
                }
                else return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="HWResp"></param>
        /// <returns></returns>
        public Stream Gzip(HttpWebResponse HWResp)
        {
            Stream stream1 = null;
            if (HWResp.ContentEncoding == "gzip")
            {
                stream1 = new GZipInputStream(HWResp.GetResponseStream());
            }
            else
            {
                if (HWResp.ContentEncoding == "deflate")
                {
                    stream1 = new InflaterInputStream(HWResp.GetResponseStream());
                }
            }
            if (stream1 == null)
            {
                return HWResp.GetResponseStream();
            }
            MemoryStream stream2 = new MemoryStream();
            int count = 0x800;
            byte[] buffer = new byte[0x800];
            bool getAll = false;
            while (!getAll)
            {
                count = stream1.Read(buffer, 0, count);
                if (count > 0)
                {
                    stream2.Write(buffer, 0, count);
                    continue;
                }
                getAll = true;
            }
            stream2.Seek((long)0, SeekOrigin.Begin);
            return stream2;
        }

        public void AbortRequest(out string message)
        {
            message = "";
            if (request == null)
                return;
            try
            {
                request.Abort();
            }
            catch (System.Exception ex)
            {
                message = ex.Message;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public string SendPost(string url, byte[] date, out string message)
        {
            return SendPost(url, date, Encoding.Default, out message);
        }
        public string SendPost(string url, byte[] date, Encoding encoding, out string message)
        {
            return SendPost(url, date, encoding, "", out message);
        }
        public string SendPost(string url, byte[] date, Encoding encoding, string referer, out string message)
        {
            CookieContainer cookie = new CookieContainer();
            return SendPost(url, date, encoding, referer, ref cookie, out message);
        }

        public string SendPost(string url, byte[] date, Encoding encoding, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendPost(url, date, encoding, "", ref cookie, out message);
        }
        public string SendPost(string url, byte[] date, Encoding encoding, string referer, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendPost(url, date, encoding, referer, true, ref cookie, out message);
        }
        public string SendPost(string url, byte[] date, Encoding encoding, string referer, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendPost(url, date, encoding, referer, allowAutoRedirect, "", ref cookie, out message);
        }
        public string SendPost(string url, byte[] date, Encoding encoding, string referer, bool allowAutoRedirect, string contentType, ref System.Net.CookieContainer cookie, out string message)
        {
            message = "";
            response = null;
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0)";
                request.Headers.Add("Accept-Language", "zh-CN");
                request.Headers.Add("Cache-Control", "no-cache");
                if (!string.IsNullOrEmpty(contentType))
                    request.ContentType = contentType;
                else
                    request.ContentType = "application/x-www-form-urlencoded";
                request.KeepAlive = true;
                request.CookieContainer = cookie;
                request.ReadWriteTimeout = 120000;
                request.Timeout = 120000;
                request.ServicePoint.Expect100Continue = false;
                request.Method = "POST";
                if (!string.IsNullOrEmpty(referer))
                {
                    request.Referer = referer;
                }

                //增加代理
                WebProxy proxy = new WebProxy();
                if (GetProxyConfig(ref proxy))
                {
                    request.Proxy = proxy;
                    if (!string.IsNullOrEmpty(proxyaccount) && !string.IsNullOrEmpty(proxypwd))
                        request.Credentials = new NetworkCredential(proxyaccount, proxypwd);
                    else
                        request.UseDefaultCredentials = true;
                }
                else
                    request.Proxy = null;
                request.AllowAutoRedirect = allowAutoRedirect;
                request.Headers.Add("Accept-Encoding", "gzip, deflate");
                request.ContentLength = date.Length;

                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(date, 0, date.Length);
                }

                //   处理响应   
                response = (HttpWebResponse)request.GetResponse();

                cookie.Add(response.Cookies);

                string html = string.Empty;
                using (Stream receiveStream = Gzip(response))
                {
                    using (StreamReader sr = new StreamReader(receiveStream, encoding))
                    {
                        html = sr.ReadToEnd();
                    }
                }
                return html;
            }
            catch (System.Exception ex)
            {
                message = ex.Message;
                return string.Empty;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }

        }

        public string SendGet(string url, out string message)
        {
            return SendGet(url, Encoding.Default, out message);
        }
        public string SendGet(string url, Encoding encoding, out string message)
        {
            CookieContainer cookie = new CookieContainer();
            return SendGet(url, encoding, ref cookie, out message);
        }
        public string SendGet(string url, Encoding encoding, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendGet(url, null, encoding, ref cookie, out message);
        }
        public string SendGet(string url, string referer, Encoding encoding, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendGet(url, referer, encoding, true, ref cookie, out message);
        }
        public string SendGet(string url, string referer, Encoding encoding, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, out string message)
        {
            return SendGet(url, referer, encoding, allowAutoRedirect, ref cookie, 0, out message);
        }
        public string SendGet(string url, string referer, Encoding encoding, bool allowAutoRedirect, ref System.Net.CookieContainer cookie, int timeout, out string message)
        {
            message = "";
            request = null;
            response = null;
            WebProxy proxy = new WebProxy();
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0)";
                request.Headers.Add("Accept-Language", "zh-CN");
                request.KeepAlive = false;
                request.ReadWriteTimeout = 120000;
                request.Timeout = timeout <= 0 ? 120000 : timeout;
                request.Headers.Add("Accept-Encoding", "gzip, deflate");

                //增加代理
                if (GetProxyConfig(ref proxy))
                {
                    request.Proxy = proxy;
                    if (!string.IsNullOrEmpty(proxyaccount) && !string.IsNullOrEmpty(proxypwd))
                        request.Credentials = new NetworkCredential(proxyaccount, proxypwd);
                    else
                        request.UseDefaultCredentials = true;
                }
                else
                    request.Proxy = null;

                if (!String.IsNullOrEmpty(referer))
                    request.Referer = referer;
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.Headers.Add("Cache-Control", "no-cache");
                request.CookieContainer = cookie;
                request.AllowAutoRedirect = allowAutoRedirect;

                request.Method = "GET";

                //   处理响应   
                response = (HttpWebResponse)request.GetResponse();
                //cookie = request.CookieContainer;
                cookie.Add(response.Cookies);
                string html = string.Empty;

                using (Stream receiveStream = Gzip(response))
                {
                    using (StreamReader sr = new StreamReader(receiveStream, encoding))
                    {
                        html = sr.ReadToEnd();
                    }
                }

                return html;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return string.Empty;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public byte[] SendGetStream(string url, string referer, Encoding encoding, ref System.Net.CookieContainer cookie, out string message)
        {
            message = null;
            request = null;
            response = null;
            WebProxy proxy = new WebProxy();
            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Accept = "image/jpeg, application/x-ms-application, image/gif, application/xaml+xml, image/pjpeg, application/x-ms-xbap, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; Tablet PC 2.0)";
                request.Headers.Add("Accept-Language", "zh-CN");
                request.KeepAlive = true;
                request.ReadWriteTimeout = 120000;
                request.Timeout = 120000;
                //request.Headers.Add("Accept-Encoding", "gzip, deflate");

                //增加代理
                if (GetProxyConfig(ref proxy))
                {
                    request.UseDefaultCredentials = true;                                      //启用代理认证
                    request.Proxy = proxy;
                }
                else
                    request.Proxy = null;

                if (!String.IsNullOrEmpty(referer))
                    request.Referer = referer;
                //request.ContentType = "application/x-www-form-urlencoded";
                //request.Headers.Add("Cache-Control", "no-cache");
                request.CookieContainer = cookie;
                request.AllowAutoRedirect = true;

                request.Method = "GET";

                //   处理响应   
                response = (HttpWebResponse)request.GetResponse();
                //cookie = request.CookieContainer;
                cookie.Add(response.Cookies);

                byte[] buffer = ResponseAsBytes(response);

                response.Close();
                return buffer;
            }
            catch (System.Threading.ThreadAbortException)
            {
                return null;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return null;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public byte[] ResponseAsBytes(HttpWebResponse responseS)
        {
            byte[] buffer = new byte[2048];
            List<byte> responseData = new List<byte>(2048);
            using (Stream responseStream = responseS.GetResponseStream())
            {
                int readcount = -1;
                while (true)
                {
                    readcount = responseStream.Read(buffer, 0, buffer.Length);
                    if (readcount == 0)
                        break;

                    byte[] temp = new byte[readcount];
                    Array.Copy(buffer, 0, temp, 0, readcount);
                    responseData.AddRange(temp);
                    //Array.Clear(buffer, 0, buffer.Length);
                }
            }

            responseData.TrimExcess();
            return responseData.ToArray();
        }

        #endregion
    }
}
