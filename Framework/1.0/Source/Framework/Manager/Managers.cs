using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public partial class BusinessModuleManager : FrameworkManager<IBusinessModule>, IBusinessModuleManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("BusinessModuleEntityName");
            }
        }

    }
    public partial class DeviceValidationManager : FrameworkManager<IDeviceValidation>, IDeviceValidationManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("DeviceValidationEntityName");
            }
        }

    }
    public partial class FrameworkSettingManager : FrameworkManager<IFrameworkSetting>, IFrameworkSettingManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("FrameworkSettingEntityName");
            }
        }

    }
    public partial class OperationManager : FrameworkManager<IOperation>, IOperationManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("OperationEntityName");
            }
        }

    }
    public partial class PermissionManager : FrameworkManager<IPermission>, IPermissionManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("PermissionEntityName");
            }
        }

    }
    public partial class RegionManager : FrameworkManager<IRegion>, IRegionManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("RegionEntityName");
            }
        }

    }
    public partial class ResourceStatusManager : FrameworkManager<IResourceStatus>, IResourceStatusManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("ResourceStatusEntityName");
            }
        }

    }
    public partial class ResourceTypeManager : FrameworkManager<IResourceType>, IResourceTypeManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("ResourceTypeEntityName");
            }
        }

    }
    public partial class RoleManager : FrameworkManager<IRole>, IRoleManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("RoleEntityName");
            }
        }

    }
    public partial class ThirdPartyAuthenticationManager : FrameworkManager<IThirdPartyAuthentication>, IThirdPartyAuthenticationManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("ThirdPartyAuthenticationEntityName");
            }
        }

    }
    public partial class UserManager : FrameworkManager<IUser>, IUserManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("UserEntityName");
            }
        }

    }
    public partial class UserLogManager : FrameworkManager<IUserLog>, IUserLogManager
    {
        protected override string EntityName
        {
            get
            {
                return Resources.Resource("UserLogEntityName");
            }
        }

    }
}
