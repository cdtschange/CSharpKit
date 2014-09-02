using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;
using Cdts.Framework.ThdPartyAuth;

namespace Cdts.Framework
{
    public partial class ThirdPartyAuthenticationManager
    {
        IUserManager userManager;
        internal protected virtual IUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = ManagerFactory.Create<IUserManager>();
                return userManager;
            }
        }

        /// <summary>
        /// 设置关联用户
        /// </summary>
        /// <param name="thdPartyUserInfo">第三方用户</param>
        /// <param name="user">用户（如果用户为空，则根据第三方用户信息创建新用户）</param>
        /// <returns>返回用户</returns>
        [CoreTransaction]
        public IUser SetAssociatedUser(ThdPartyUserInfo thdPartyUserInfo, IUser user)
        {
            IThirdPartyAuthentication thdPartyAuth = NewEntity();
            thdPartyAuth.Id = Guid.NewGuid();
            thdPartyAuth.ThirdPartyName = thdPartyUserInfo.ThdPartyAuthName;
            thdPartyAuth.ThirdPartyId = thdPartyUserInfo.Id;
            thdPartyAuth.ThirdPartyUserName = thdPartyUserInfo.Name;
            thdPartyAuth.Verified = true;
            if (user == null)
            {
                user = UserManager.NewEntity();
                user.Id = Guid.NewGuid();
                user.Name = thdPartyUserInfo.Name;
                user.Nick = thdPartyUserInfo.Nick;
                user.Code = thdPartyUserInfo.Nick;
                user.Description = "";
                user.Email = thdPartyUserInfo.Id + "@" + thdPartyUserInfo.ThdPartyAuthName;
                user.EmailValidated = false;
                user.CurrentLoginIp = "";
                user.CurrentLoginTime = DateTime.Now;
                user.Invalid = false;
                user.LastLoginIp = "";
                user.LastLoginTime = DateTime.Now;
                user.Mobile = "";
                user.MobileValidated = false;
                user.Password = Guid.NewGuid().ToString().Replace("-", "");
                user.RegisterTime = DateTime.Now;
                user.IsThdParty = true;
                UserManager.Create(user);
            }
            thdPartyAuth.User = user;
            Create(thdPartyAuth);
            SaveChanges();
            return user;
        }
    }
}
