using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public sealed class Configurations
    {
        static Configurations() { }
        private static IBusinessModuleManager businessModuleManager = ManagerFactory.Create<IBusinessModuleManager>();
        private static IResourceTypeManager resourceTypeManager = ManagerFactory.Create<IResourceTypeManager>();
        private static IFrameworkSettingManager _FrameworkSettingManager = ManagerFactory.Create<IFrameworkSettingManager>();
        [ThreadStatic]
        private static IFrameworkSetting _FrameworkSetting;
        public static IFrameworkSetting FrameworkSetting
        {
            get
            {
                int totalRecords = 0;
                if (_FrameworkSetting == null)
                {
                    _FrameworkSetting = _FrameworkSettingManager.Load(null, null, null, 1, 1, out totalRecords).FirstOrDefault();
                }
                return _FrameworkSetting;
            }
        }   
    }
}
