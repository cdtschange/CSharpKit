using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;
using System.Xml;

namespace Cdts.Framework.ThdPartyAuth
{
    public class Kaixin001Auth : AbstractThdPartyAuth
    {
        public Kaixin001Auth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager)
            : base(userManager, thdPartAuthManager)
        {
            oAuth = new OAuthHelper(ConsumerKey, ConsumerSecret, "", "", "", "");
        }

        protected OAuthHelper oAuth;
        private static string consumerKey = ConfigurationManager.AppSettings["Kaixin001AppKey"];
        private static string consumerSecret = ConfigurationManager.AppSettings["Kaixin001AppSecret"];
        private static string loginUrl = ConfigurationManager.AppSettings["Kaixin001LoginUrl"];
        private static string userInfoUrl = ConfigurationManager.AppSettings["Kaixin001UserInfoUrl"];

        protected string ConsumerKey
        {
            get { return consumerKey; }
        }

        protected string ConsumerSecret
        {
            get { return consumerSecret; }
        }

        protected string LoginUrl
        {
            get { return loginUrl; }
        }

        protected string UserInfoUrl
        {
            get { return userInfoUrl; }
        }

        public override string ThdPartyName
        {
            get { return "kaixin001.com"; }
        }

        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="secret">密匙</param>
        /// <returns>返回签名</returns>
        public static string CreateSign(IDictionary<string, string> parameters, string secret)
        {
            //API2.0 签名算法详见：http://open.taobao.com/dev/index.php/API%E7%AD%BE%E5%90%8D%E7%AE%97%E6%B3%95

            //md5:将secretcode同时拼接到参数字符串头、尾部进行md5加密，再转化成大写，格式是：uppercase(md5(secretkey1value1key2value2...secret)。例如:uppercase(md5（secretbar2baz3foo1secret)) 

            parameters.Remove("sig");

            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder();
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append("=").Append(value);
                }
            }
            query.Append(secret);// API 2.0 新签名方法            

            // 第三步：使用MD5加密
            MD5 md5 = MD5.Create();
            byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(query.ToString()));

            // 第四步：把二进制转化为大写的十六进制
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                string hex = bytes[i].ToString("X");
                if (hex.Length == 1)
                {
                    result.Append("0");
                }
                result.Append(hex);
            }

            return result.ToString().ToLower();
        }

        public override string GetLoginUrl()
        {
            return string.Format(LoginUrl, ConsumerKey);
        }

        public override ThdPartyUserInfo GetThdPartyUserInfo(System.Collections.Specialized.NameValueCollection queryString)
        {
            string sessionKey = queryString["hash"].Replace("#", "");
            IDictionary<string, string> param = new Dictionary<string, string>();
            param.Add("api_key", ConsumerKey);//应用key
            param.Add("call_id", DateTime.Now.Ticks.ToString());
            param.Add("format", "xml");//签名方法
            param.Add("method", "users.getInfo");//自有会员帐号昵称
            param.Add("uids", sessionKey.Split('_')[0]);//自有会员帐号昵称
            param.Add("session_key", sessionKey);//自有会员帐号昵称
            param.Add("v", "1.0");//自有会员帐号昵称            
            param.Add("sig", CreateSign(param, ConsumerSecret));//生成签名
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in param)
            {
                result.Append(kv.Key + "=" + kv.Value + "&");
            }
            string res = result.ToString().Substring(0, result.ToString().Length - 1);
            string xml = oAuth.WebRequest(OAuthHelper.Method.POST, UserInfoUrl, res);

            ThdPartyUserInfo userInfo = null;
            userInfo = new ThdPartyUserInfo();
            XmlDocument xdocment = new XmlDocument();
            xdocment.LoadXml(xml);
            userInfo = new ThdPartyUserInfo();
            userInfo.Id = xdocment.ChildNodes[0].ChildNodes[0].InnerText;
            userInfo.Nick = xdocment.ChildNodes[0].ChildNodes[1].InnerText;
            userInfo.Name = xdocment.ChildNodes[0].ChildNodes[1].InnerText;
            userInfo.ThdPartyAuthName = this.ThdPartyName;
            return userInfo;
        }
        /*
                private string WebRequest(string url, string postData)
                {
                    HttpWebRequest webRequest = null;
                    StreamWriter requestWriter = null;
                    string responseData = "";

                    webRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                    webRequest.Method = "POST";
                    webRequest.ServicePoint.Expect100Continue = false;
                    webRequest.ContentType = "application/x-www-form-urlencoded";
                    requestWriter = new StreamWriter(webRequest.GetRequestStream());
                    try
                    {
                        requestWriter.Write(postData);
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        requestWriter.Close();
                        requestWriter = null;
                    }
                    responseData = WebResponseGet(webRequest);
                    webRequest = null;
                    return responseData;
                }

                private string WebResponseGet(HttpWebRequest webRequest)
                {
                    StreamReader responseReader = null;
                    string responseData = "";

                    try
                    {
                        responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream());
                        responseData = responseReader.ReadToEnd();
                    }
                    catch
                    {
                        throw;
                    }
                    finally
                    {
                        webRequest.GetResponse().GetResponseStream().Close();
                        responseReader.Close();
                        responseReader = null;
                    }

                    return responseData;
                }
         * */
    }
}
