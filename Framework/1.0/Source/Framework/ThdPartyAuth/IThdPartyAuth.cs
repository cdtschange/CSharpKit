using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace Cdts.Framework.ThdPartyAuth
{
    /// <summary>
    /// 第三方登录
    /// </summary>
    public interface IThdPartyAuth
    {
        /// <summary>
        /// 获取登录Url
        /// </summary>
        /// <returns></returns>
        string GetLoginUrl();
        /// <summary>
        /// 第三方名称
        /// </summary>
        string ThdPartyName { get; }
        /// <summary>
        /// 获取用户的第三方信息
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        ThdPartyUserInfo GetThdPartyUserInfo(NameValueCollection queryString);
        /// <summary>
        /// 获取关联用户
        /// </summary>
        /// <param name="thdPartyUserId">第三方用户ID</param>
        /// <returns>关联用户</returns>
        IUser GetAssociatedUser(string thdPartyUserId);
        /// <summary>
        /// 获取可能的用户
        /// </summary>
        /// <param name="thdPartyUserName">第三方用户名</param>
        /// <returns>可能的用户</returns>
        IList<IUser> GetPossibleUsers(string thdPartyUserName);
        /// <summary>
        /// 是否是第一次登录
        /// </summary>
        /// <param name="thdPartyUserId">第三方用户ID</param>
        /// <returns>返回结果</returns>
        bool IsFirstLogin(string thdPartyUserId);
    }
}
