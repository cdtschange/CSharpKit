using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Security.Cryptography;

namespace Cdts.Framework.ThdPartyAuth
{
    public class TaobaoAuth : AbstractThdPartyAuth
    {
        public TaobaoAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager)
            : base(userManager, thdPartAuthManager)
        {
        }

        private static string consumerKey = ConfigurationManager.AppSettings["TaobaoAppKey"];
        private static string consumerSecret = ConfigurationManager.AppSettings["TaobaoAppSecret"];
        private static string loginUrl = ConfigurationManager.AppSettings["TaobaoLoginUrl"];

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

        public override string ThdPartyName
        {
            get { return "taobao.com"; }
        }

        /// <summary>
        /// 创建签名
        /// </summary>
        /// <param name="parameters">参数</param>
        /// <param name="secret">密匙</param>
        /// <returns>返回签名</returns>
        public string CreateSign(IDictionary<string, string> parameters, string secret)
        {
            //API2.0 签名算法详见：http://open.taobao.com/dev/index.php/API%E7%AD%BE%E5%90%8D%E7%AE%97%E6%B3%95

            //md5:将secretcode同时拼接到参数字符串头、尾部进行md5加密，再转化成大写，格式是：uppercase(md5(secretkey1value1key2value2...secret)。例如:uppercase(md5（secretbar2baz3foo1secret)) 

            parameters.Remove("sign");

            // 第一步：把字典按Key的字母顺序排序
            IDictionary<string, string> sortedParams = new SortedDictionary<string, string>(parameters);
            IEnumerator<KeyValuePair<string, string>> dem = sortedParams.GetEnumerator();

            // 第二步：把所有参数名和参数值串在一起
            StringBuilder query = new StringBuilder(secret);
            while (dem.MoveNext())
            {
                string key = dem.Current.Key;
                string value = dem.Current.Value;
                if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
                {
                    query.Append(key).Append(value);
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

            return result.ToString();
        }

        public override string GetLoginUrl()
        {
            IDictionary<string, string> param = new Dictionary<string, string>();
            param.Add("app_key", ConsumerKey);//应用key
            param.Add("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));//时间戳，格式为yyyy-mm-dd hh:mm:ss，例如：2008-01-25 20:23:30。淘宝API服务端允许客户端请求时间误差为10分钟。 
            param.Add("sign_method", "md5");//签名方法
            param.Add("sign", CreateSign(param, ConsumerSecret));//生成签名
            StringBuilder result = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in param)
            {
                result.Append("&" + kv.Key + "=" + kv.Value);
            }
            return LoginUrl + "?" + result.ToString();
        }

        public override ThdPartyUserInfo GetThdPartyUserInfo(System.Collections.Specialized.NameValueCollection queryString)
        {
            ThdPartyUserInfo userInfo = null;
            IDictionary<string, string> param = new Dictionary<string, string>();
            for (int i = 0; i < queryString.Count; i++)
            {
                param.Add(queryString.Keys[i], queryString[queryString.Keys[i]]);
            }
            // 验证回调地址的签名是否合法
            string result = CreateSign(param, ConsumerSecret);
            if (result == queryString["sign"])
            {
                userInfo = new ThdPartyUserInfo();
                userInfo.Id = queryString["taobao_user_id"];
                userInfo.Nick = queryString["taobao_user_nick"];
                userInfo.Name = queryString["taobao_user_nick"];
                userInfo.ThdPartyAuthName = this.ThdPartyName;
            }
            return userInfo;
        }
    }
}
