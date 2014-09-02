using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;
using System.Collections.Specialized;

namespace Cdts.Framework.ThdPartyAuth
{
    public abstract class AbstractThdPartyAuth : IThdPartyAuth, ITransactionable
    {
        protected IUserManager userManager;
        protected IThirdPartyAuthenticationManager thdPartAuthManager;
        public AbstractThdPartyAuth(IUserManager userManager, IThirdPartyAuthenticationManager thdPartAuthManager)
        {
            this.userManager = userManager;
            this.thdPartAuthManager = thdPartAuthManager;
        }
        /// <summary>
        /// 获取登录Url
        /// </summary>
        /// <returns></returns>
        public abstract string GetLoginUrl();
        /// <summary>
        /// 第三方名称
        /// </summary>
        public abstract string ThdPartyName
        {
            get;
        }
        /// <summary>
        /// 获取用户的第三方信息
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public abstract ThdPartyUserInfo GetThdPartyUserInfo(NameValueCollection queryString);
        /// <summary>
        /// 是否是第一次登录
        /// </summary>
        /// <param name="thdPartyUserId">第三方用户ID</param>
        /// <returns>返回结果</returns>
        public virtual bool IsFirstLogin(string thdPartyUserId)
        {
            return Load(thdPartyUserId) == null;
        }
        /// <summary>
        /// 获取第三方用户
        /// </summary>
        /// <param name="thdPartyUserId">第三方用户ID</param>
        /// <returns>返回第三方用户</returns>
        protected virtual IThirdPartyAuthentication Load(string thdPartyUserId)
        {
            var query = thdPartAuthManager.CreateQuery();
            return query.Where(i => i.ThirdPartyName == ThdPartyName && i.ThirdPartyId == thdPartyUserId).FirstOrDefault();
        }
        /// <summary>
        /// 获取可能的用户
        /// </summary>
        /// <param name="thdPartyUserName">第三方用户名</param>
        /// <returns>可能的用户</returns>
        public virtual IList<IUser> GetPossibleUsers(string thdPartyUserName)
        {
            CoreExpression expression = CoreExpression.Or(
                CoreExpression.Equal("Name", thdPartyUserName),
                CoreExpression.Equal("Nick", thdPartyUserName));
            int totalRecords;
            return userManager.Load(expression, null, null, 1, int.MaxValue, out totalRecords);
        }
        /// <summary>
        /// 获取关联用户
        /// </summary>
        /// <param name="thdPartyUserId">第三方用户ID</param>
        /// <returns>关联用户</returns>
        public virtual IUser GetAssociatedUser(string thdPartyUserId)
        {
            IThirdPartyAuthentication thd = Load(thdPartyUserId);
            return thd != null ? thd.User : null;
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns></returns>
        public IDataTransaction BeginTransaction()
        {
            return userManager.BeginTransaction();
        }
    }
}
