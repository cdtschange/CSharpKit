using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial class ResourceTypeManager : IResourceTypeManager
    {
        IOperationManager operationManager;
        internal protected virtual IOperationManager OperationManager
        {
            get
            {
                if (operationManager == null)
                    operationManager = ManagerFactory.Create<IOperationManager>();
                return operationManager;
            }
        }
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

        #region Override Method

        /// <summary>
        /// 业务模块不能为空
        /// </summary>
        protected override void ValidateEntity(IResourceType entity)
        {
            base.ValidateEntity(entity);

            if (entity.BusinessModule == null)
            {
                throw new ArgumentNullException("业务模块不能为空");
            }
            if (entity.Parent != null && entity.Reference != null)
            {
                //TODO:写入资源文件
                throw new InvalidOperationException("资源类别的父类别和引用类别中一个必须为null");
            }
            if (entity.Reference != null && entity.Reference.Parent != null)
            {
                //TODO:写入资源文件
                throw new InvalidOperationException("资源类别的引用类别的父类别必须为null");
            }
            if (entity.Parent != null && entity.Parent.Reference != null)
            {
                //TODO:写入资源文件
                throw new InvalidOperationException("资源类别的引用类别不为null的时候不能作为父类别");
            }

        }
        /// <summary>
        /// 添加对应操作的许可
        /// </summary>
        protected override void CreateEntity(IResourceType entity)
        {
            base.CreateEntity(entity);
            IResourceType root = entity;
            while (root.Parent != null)
            {
                root = root.Parent;
            }
            List<IOperation> operations = root.Operations.ToList();
            if (operations != null)
            {
                operations.ForEach(o =>
                {
                    IPermission permission = PermissionManager.NewEntity();
                    permission.Id = Guid.NewGuid();
                    permission.BusinessModule = entity.BusinessModule;
                    permission.Operation = o;
                    permission.ResourceType = entity;
                    PermissionManager.Create(permission);
                });
            }

            if (entity.Reference != null)
            {
                CreatePermissionFromReference(operations, GetAllChildren(entity.Reference).ToList());
            }
            else if (root.ReferencesTo != null)
            {
                CreatePermissionToReference(entity, root.ReferencesTo.ToList());
            }
        }

        void CreatePermissionFromReference(List<IOperation> operations, List<IResourceType> references)
        {
            if (references == null || operations == null)
                return;

            references.ForEach(r =>
            {
                operations.ForEach(o =>
                {
                    IPermission permission = PermissionManager.NewEntity();
                    permission.Id = Guid.NewGuid();
                    permission.BusinessModule = o.BusinessModule;
                    permission.Operation = o;
                    permission.ResourceType = r;
                    PermissionManager.Create(permission);
                });
            });
        }

        void CreatePermissionToReference(IResourceType entity, List<IResourceType> referencesTo)
        {
            if (entity == null || referencesTo == null)
                return;

            referencesTo.ForEach(r =>
            {
                List<IOperation> operations = r.Operations.ToList();
                if (operations != null)
                {
                    operations.ForEach(o =>
                    {
                        IPermission permission = PermissionManager.NewEntity();
                        permission.Id = Guid.NewGuid();
                        permission.BusinessModule = o.BusinessModule;
                        permission.Operation = o;
                        permission.ResourceType = entity;
                        PermissionManager.Create(permission);
                    });
                }
            });
        }

        /// <summary>
        /// 更新操作的编码
        /// </summary>
        protected override void UpdateEntity(IResourceType entity)
        {
            IResourceType oldEntity = LoadFromDatabase(entity.Id);
            if (oldEntity.Code != entity.Code && entity.Operations != null)
            {
                entity.Operations.ToList().ForEach(o =>
                {
                    o.Code = entity.Code + o.Code.Substring(oldEntity.Code.Length);
                    OperationManager.Update(o);
                });
            }
            base.UpdateEntity(entity);
        }

        /// <summary>
        /// 不能删除顶级的资源类别
        /// </summary>
        protected override void LogicalDeleteEntity(IResourceType entity)
        {
            if (entity.Parent == null)
            {
                throw new InvalidOperationException("不能删除顶级的资源类别");
            }
            base.LogicalDeleteEntity(entity);
        }

        /// <summary>
        /// 删除对应操作的许可
        /// </summary>
        protected override void PhysicalDeleteEntity(IResourceType entity)
        {
            var query = PermissionManager.CreateQuery();
            List<IPermission> list = query.Where(p => p.ResourceType.Id == entity.Id).ToList();
            PermissionManager.Delete(list);

            base.PhysicalDeleteEntity(entity);
        }
        #endregion
    }
}
