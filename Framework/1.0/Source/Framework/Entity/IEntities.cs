
using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using Cdts.Core;

namespace Cdts.Framework
{

    /// <summary>
    /// 业务模块
    /// </summary>
    public partial interface IBusinessModule : Cdts.Core.IPKey<Guid>, Cdts.Core.ITree<IBusinessModule>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }

    }

    /// <summary>
    /// 设备验证
    /// </summary>
    public partial interface IDeviceValidation : Cdts.Core.IPKey<Guid>
    {

        /// <summary>
        /// 验证类型
        /// </summary>
        global::System.Int32 ValidationType { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        global::System.String DeviceName { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        global::System.String Device { get; set; }

        /// <summary>
        /// 验证码
        /// </summary>
        global::System.String ValidationCode { get; set; }

        /// <summary>
        /// 验证时间
        /// </summary>
        Nullable<global::System.DateTime> ValidateTime { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        Nullable<global::System.DateTime> ExpireTime { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        IUser User { get; set; }
    }

    /// <summary>
    /// 配置
    /// </summary>
    public partial interface IFrameworkSetting : Cdts.Core.IPKey<Guid>
    {

        /// <summary>
        /// 模块编码
        /// </summary>
        global::System.String BusinessModuleCode { get; set; }

        /// <summary>
        /// 管理员角色编码
        /// </summary>
        global::System.String AdministratorsRoleCode { get; set; }

        /// <summary>
        /// Everyone角色编码
        /// </summary>
        global::System.String EveryoneRoleCode { get; set; }

        /// <summary>
        /// 管理员账号
        /// </summary>
        global::System.String AdministratorCode { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        global::System.String Version { get; set; }

    }

    /// <summary>
    /// 操作
    /// </summary>
    public partial interface IOperation : Cdts.Core.IPKey<Guid>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 编码
        /// </summary>
        global::System.String Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }


        /// <summary>
        /// 业务模块
        /// </summary>
        IBusinessModule BusinessModule { get; set; }

        /// <summary>
        /// 资源类别
        /// </summary>
        IResourceType ResourceType { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IPermission> Permissions { get; set; }
    }

    /// <summary>
    /// 许可
    /// </summary>
    public partial interface IPermission : Cdts.Core.IPKey<Guid>
    {


        /// <summary>
        /// 业务模块
        /// </summary>
        IBusinessModule BusinessModule { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        IOperation Operation { get; set; }

        /// <summary>
        /// 资源状态
        /// </summary>
        IResourceStatus ResourceStatus { get; set; }

        /// <summary>
        /// 资源类别
        /// </summary>
        IResourceType ResourceType { get; set; }
    }

    /// <summary>
    /// 地区
    /// </summary>
    public partial interface IRegion : Cdts.Core.IPKey<Guid>, Cdts.Core.ITree<IRegion>, Cdts.Core.IOrdered
    {

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 等级
        /// </summary>
        global::System.Int32 CodeLevel { get; set; }

    }

    /// <summary>
    /// 资源状态
    /// </summary>
    public partial interface IResourceStatus : Cdts.Core.IPKey<Guid>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 编码
        /// </summary>
        global::System.String Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }


        /// <summary>
        /// 业务模块
        /// </summary>
        IBusinessModule BusinessModule { get; set; }

        /// <summary>
        /// 资源类别
        /// </summary>
        IResourceType ResourceType { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IOperation> Operations { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IPermission> Permissions { get; set; }
    }

    /// <summary>
    /// 资源类别
    /// </summary>
    public partial interface IResourceType : Cdts.Core.IPKey<Guid>, Cdts.Core.ITree<IResourceType>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }


        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IOperation> Operations { get; set; }

        /// <summary>
        /// 业务模块
        /// </summary>
        IBusinessModule BusinessModule { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IResourceType> ReferencesTo { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        IResourceType Reference { get; set; }
    }

    /// <summary>
    /// 角色
    /// </summary>
    public partial interface IRole : Cdts.Core.IPKey<Guid>, Cdts.Core.ITree<IRole>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }


        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IUser> Users { get; set; }

        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IPermission> Permissions { get; set; }
    }

    /// <summary>
    /// 第三方验证
    /// </summary>
    public partial interface IThirdPartyAuthentication : Cdts.Core.IPKey<Guid>
    {

        /// <summary>
        /// 第三方名称
        /// </summary>
        global::System.String ThirdPartyName { get; set; }

        /// <summary>
        /// 第三方ID
        /// </summary>
        global::System.String ThirdPartyId { get; set; }

        /// <summary>
        /// 第三方用户名
        /// </summary>
        global::System.String ThirdPartyUserName { get; set; }

        /// <summary>
        /// 验证
        /// </summary>
        global::System.Boolean Verified { get; set; }


        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        IUser User { get; set; }
    }

    /// <summary>
    /// 用户
    /// </summary>
    public partial interface IUser : Cdts.Core.IPKey<Guid>, Cdts.Core.ILogicalDeleted
    {

        /// <summary>
        /// 账号
        /// </summary>
        global::System.String Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        global::System.String Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        global::System.String Email { get; set; }

        /// <summary>
        /// 邮箱验证
        /// </summary>
        global::System.Boolean EmailValidated { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        global::System.String Password { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        global::System.String Mobile { get; set; }

        /// <summary>
        /// 手机号码验证
        /// </summary>
        global::System.Boolean MobileValidated { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        global::System.DateTime RegisterTime { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        global::System.String LastLoginIp { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        global::System.DateTime LastLoginTime { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        global::System.String Nick { get; set; }

        /// <summary>
        /// 现在登录IP
        /// </summary>
        global::System.String CurrentLoginIp { get; set; }

        /// <summary>
        /// 现在登录时间
        /// </summary>
        global::System.DateTime CurrentLoginTime { get; set; }

        /// <summary>
        /// 是否是第三方用户
        /// </summary>
        global::System.Boolean IsThdParty { get; set; }

        /// <summary>
        /// 推荐人ID
        /// </summary>
        Nullable<global::System.Guid> IntroducerId { get; set; }


        /// <summary>
        /// 没有元数据文档可用。
        /// </summary>
        ICollection<IRole> Roles { get; set; }
    }

    /// <summary>
    /// 用户日志
    /// </summary>
    public partial interface IUserLog : Cdts.Core.IPKey<Guid>
    {

        /// <summary>
        /// 时间
        /// </summary>
        global::System.DateTime LogTime { get; set; }

        /// <summary>
        /// 资源ID
        /// </summary>
        global::System.String ResourceId { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        global::System.String Description { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        global::System.String LogIp { get; set; }


        /// <summary>
        /// 用户
        /// </summary>
        IUser User { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        IResourceType ResourceType { get; set; }

        /// <summary>
        /// 业务模块
        /// </summary>
        IBusinessModule BusinessModule { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        IOperation Operation { get; set; }
    }

}

