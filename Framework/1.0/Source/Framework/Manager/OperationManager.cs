using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial class OperationManager : IOperationManager
    {
        IPermissionManager permissionManager;
        internal protected virtual IPermissionManager PermissionManager
        {
            get
            {
                if (permissionManager == null)
                    permissionManager = ManagerFactory.Create<IPermissionManager>();
                return permissionManager;
            }
        }
        IResourceTypeManager resourceTypeManager;
        internal protected virtual IResourceTypeManager ResourceTypeManager
        {
            get
            {
                if (resourceTypeManager == null)
                    resourceTypeManager = ManagerFactory.Create<IResourceTypeManager>();
                return resourceTypeManager;
            }
        }

        /// <summary>
        /// 获取操作
        /// </summary>
        /// <param name="code">操作编码</param>
        /// <returns>返回操作</returns>
        public IOperation LoadByCode(string code)
        {
            var query = CreateQuery();
            return query.FirstOrDefault(r => r.Code == code);
        }

        /// <summary>
        /// 业务模块不能为空
        /// 资源类别不能为空
        /// </summary>
        protected override void ValidateEntity(IOperation entity)
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
            if (entity.ResourceType.Parent != null)
            {
                throw new ArgumentNullException("ResourceType", "资源类别必须没有父类");
            }
        }

        /// <summary>
        /// 添加对应许可
        /// </summary>
        protected override void CreateEntity(IOperation entity)
        {
            base.CreateEntity(entity);

            IPermission permission = PermissionManager.NewEntity();
            permission.Id = Guid.NewGuid();
            permission.BusinessModule = entity.BusinessModule;
            permission.Operation = entity;
            permission.ResourceType = entity.ResourceType;
            PermissionManager.Create(permission);

            if (entity.ResourceType.Reference != null)
            {
                CreatePermissionFromReference(entity, ResourceTypeManager.CreateQuery().Where(r =>
                    r.Code.StartsWith(entity.ResourceType.Reference.Code + ".")).ToList());
            }
        }

        void CreatePermissionFromReference(IOperation entity, List<IResourceType> references)
        {
            if (entity == null || references == null)
                return;

            references.ForEach(r =>
            {
                IPermission permission = PermissionManager.NewEntity();
                permission.Id = Guid.NewGuid();
                permission.BusinessModule = entity.BusinessModule;
                permission.Operation = entity;
                permission.ResourceType = r;
                PermissionManager.Create(permission);
            });
        }

        protected override void PhysicalDeleteEntity(IOperation entity)
        {
            return;
        }
        protected override void LogicalDeleteEntity(IOperation entity)
        {
            return;
        }
    }
}
