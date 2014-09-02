using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Framework.ThdPartyAuth;

namespace Cdts.Framework
{
    public partial interface IThirdPartyAuthenticationManager
    {
        /// <summary>
        /// 设置关联用户
        /// </summary>
        /// <param name="thdPartyUserInfo">第三方用户</param>
        /// <param name="user">用户（如果用户为空，则根据第三方用户信息创建新用户）</param>
        /// <returns>返回用户</returns>
        IUser SetAssociatedUser(ThdPartyUserInfo thdPartyUserInfo, IUser user);
    }
}
