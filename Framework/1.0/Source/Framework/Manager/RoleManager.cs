using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public partial class RoleManager : IRoleManager
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

        /// <summary>
        /// 分配用户
        /// </summary>
        [CoreTransaction]
        public void AssignUser(IRole entity, List<IUser> users)
        {
            users.ForEach(u => entity.Users.Add(u));
            Update(entity);
            SaveChanges();
        }
        /// <summary>
        /// 移除用户
        /// </summary>
        [CoreTransaction]
        public void RemoveUser(IRole entity, List<IUser> users)
        {
            if (entity.Code == FrameworkSetting.AdministratorsRoleCode)
            {
                IUser adminUser = users.Where(u => u.Code == FrameworkSetting.AdministratorCode).FirstOrDefault();
                users.Remove(adminUser);
            }
            users.ForEach(u => entity.Users.Remove(u));
            Update(entity);
            SaveChanges();
        }
        /// <summary>
        /// 分配许可
        /// </summary>
        [CoreTransaction]
        public void AssignPermission(IRole entity, List<IPermission> permission)
        {
            if (entity.Parent != null)
                permission = permission.Intersect(entity.Parent.Permissions).ToList();
            permission.ForEach(p => entity.Permissions.Add(p));
            Update(entity);
            SaveChanges();
        }

        /// <summary>
        /// 移除许可
        /// </summary>
        [CoreTransaction]
        public void RemovePermission(IRole entity, List<IPermission> permission)
        {
            if (entity.Code == FrameworkSetting.AdministratorsRoleCode)
            { //不能删除管理员角色的框架业务模块的许可
                var query = PermissionManager.CreateQuery();
                List<IPermission> permissions = query
                    .Where(p => p.BusinessModule.Code == FrameworkSetting.BusinessModuleCode).ToList();
                permission = permission.Except(permissions).ToList();
            }
            permission.ForEach(p => entity.Permissions.Remove(p));
            IList<IRole> children = GetAllChildren(entity);
            children.ToList().ForEach(c =>
            {
                permission.ForEach(p => c.Permissions.Remove(p));
            });
            Update(entity);
            SaveChanges();
        }

        #region Override Method

        protected override void ValidateEntityForDelete(IRole entity)
        {
            base.ValidateEntityForDelete(entity);
            if (entity.Code == FrameworkSetting.AdministratorsRoleCode
               || entity.Code == FrameworkSetting.EveryoneRoleCode)
            {//TODO:需要多语言
                throw new InvalidOperationException("不能删除管理员或者Everyone的角色");
            }
        }
        protected override void PhysicalDeleteEntity(IRole entity)
        {
            entity.Permissions.Clear();
            entity.Users.Clear();

            base.PhysicalDeleteEntity(entity);
        }

        protected override void ValidateEntityForUpdate(IRole entity)
        {
            base.ValidateEntityForUpdate(entity);
            IRole oldEntity = LoadFromDatabase(entity.Id);
            if (oldEntity.Code == FrameworkSetting.AdministratorsRoleCode ||
               oldEntity.Code == FrameworkSetting.EveryoneRoleCode)
            {
                if (entity.Code != oldEntity.Code)
                {//TODO:需要多语言
                    throw new InvalidOperationException("不能修改管理员或者Everyone角色的编码");
                }
                if (entity.Parent != null)
                {//TODO:需要多语言
                    throw new InvalidOperationException("不能修改管理员或者Everyone角色的上级");
                }
            }
        }

        #endregion
    }
}
