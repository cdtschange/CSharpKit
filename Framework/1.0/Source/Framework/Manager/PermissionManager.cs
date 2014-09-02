using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial class PermissionManager : IPermissionManager
    {
        /// <summary>
        /// 业务模块不能为空
        /// 资源类别不能为空
        /// 操作不能为空
        /// </summary>
        protected override void ValidateEntity(IPermission entity)
        {
            base.ValidateEntity(entity);

            if (entity.BusinessModule == null)
            {
                throw new ArgumentNullException("BusinessModule", "业务模块不能为空");
            }
            if (entity.ResourceType == null)
            {
                throw new ArgumentNullException("ResourceType", "资源类别不能为空");
            }
            if (entity.Operation == null)
            {
                throw new ArgumentNullException("Operation", "操作不能为空");
            }
        }
    }
}
