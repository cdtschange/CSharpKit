using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public partial interface IBusinessModuleManager : IFrameworkManager<IBusinessModule>
    {
    }
    public partial interface IDeviceValidationManager : IFrameworkManager<IDeviceValidation>
    {
    }
    public partial interface IFrameworkSettingManager : IFrameworkManager<IFrameworkSetting>
    {
    }
    public partial interface IOperationManager : IFrameworkManager<IOperation>
    {
    }
    public partial interface IPermissionManager : IFrameworkManager<IPermission>
    {
    }
    public partial interface IRegionManager : IFrameworkManager<IRegion>
    {
    }
    public partial interface IResourceStatusManager : IFrameworkManager<IResourceStatus>
    {
    }
    public partial interface IResourceTypeManager : IFrameworkManager<IResourceType>
    {
    }
    public partial interface IRoleManager : IFrameworkManager<IRole>
    {
    }
    public partial interface IThirdPartyAuthenticationManager : IFrameworkManager<IThirdPartyAuthentication>
    {
    }
    public partial interface IUserManager : IFrameworkManager<IUser>
    {
    }
    public partial interface IUserLogManager : IFrameworkManager<IUserLog>
    {
    }
}
