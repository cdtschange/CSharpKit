using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial class BusinessModuleManager : IBusinessModuleManager
    {
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

        protected override void PhysicalDeleteEntity(IBusinessModule entity)
        {
            return;
        }
        protected override void LogicalDeleteEntity(IBusinessModule entity)
        {
            return;
        }

        /// <summary>
        /// 更新资源类别的编码
        /// </summary>
        protected override void UpdateEntity(IBusinessModule entity)
        {
            IBusinessModule oldEntity = LoadFromDatabase(entity.Id);

            var query = ResourceTypeManager.CreateQuery();
            List<IResourceType> resourcesTypes = query.Where(r => r.BusinessModule.Id == entity.Id).ToList();
            if (oldEntity.Code != entity.Code && resourcesTypes != null)
            {
                resourcesTypes.ForEach(r =>
                {
                    r.Code = entity.Code + r.Code.Substring(oldEntity.Code.Length);
                    ResourceTypeManager.Update(r);
                });
            }
            base.UpdateEntity(entity);
        }
    }
}
