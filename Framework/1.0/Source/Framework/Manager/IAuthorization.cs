using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    /// <summary>
    /// 授权
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// 是否授权
        /// </summary>
        /// <param name="authorizationName">名称</param>
        /// <param name="user">用户</param>
        /// <returns>返回是否授权</returns>
        bool Authorization(string authorizationName, IUser user);
    }
}
