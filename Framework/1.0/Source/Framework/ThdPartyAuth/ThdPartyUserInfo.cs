using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Web;

namespace Cdts.Framework.ThdPartyAuth
{
    /// <summary>
    /// 第三方用户信息
    /// </summary>
    public class ThdPartyUserInfo
    {
        /// <summary>
        /// ID
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Nick { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 第三方名称
        /// </summary>
        public string ThdPartyAuthName { get; set; }

        public ThdPartyUserInfo()
        { }
        public ThdPartyUserInfo(string str)
        {
            NameValueCollection qs = HttpUtility.ParseQueryString(str);
            Id = qs["Id"];
            Name = qs["Name"];
            Nick = qs["Nick"];
            Url = qs["Url"];
            ThdPartyAuthName = qs["ThdPartyAuthName"];
        }

        public override string ToString()
        {
            return string.Format("Id={0}&Name={1}&Nick={2}&Url={3}&ThdPartyAuthName={4}",
                HttpUtility.UrlEncode(Id),
                HttpUtility.UrlEncode(Name),
                HttpUtility.UrlEncode(Nick),
                HttpUtility.UrlEncode(Url),
                HttpUtility.UrlEncode(ThdPartyAuthName));
        }


    }
}
