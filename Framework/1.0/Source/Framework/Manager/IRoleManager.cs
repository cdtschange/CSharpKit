using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial interface IRoleManager
    {
        /// <summary>
        /// 分配用户
        /// </summary>
        void AssignUser(IRole entity, List<IUser> users);
        /// <summary>
        /// 移除用户
        /// </summary>
        void RemoveUser(IRole entity, List<IUser> users);
        /// <summary>
        /// 分配许可
        /// </summary>
        void AssignPermission(IRole entity, List<IPermission> permission);
        /// <summary>
        /// 移除许可
        /// </summary>
        void RemovePermission(IRole entity, List<IPermission> permission);
    }
}
