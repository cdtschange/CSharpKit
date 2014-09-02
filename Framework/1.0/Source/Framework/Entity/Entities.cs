
using System;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Data.EntityClient;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Cdts.Core;
using Cdts.Framework;
[assembly: EdmSchemaAttribute()]

[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleBusinessModule", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(BusinessModule), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(BusinessModule))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleBusinessModule", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IBusinessModule), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IBusinessModule))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserDeviceValidation", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(User), "DeviceValidation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(DeviceValidation))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserDeviceValidation", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IUser), "DeviceValidation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IDeviceValidation))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleResourceStatus", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BusinessModule), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceStatus))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleResourceStatus", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IBusinessModule), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceStatus))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceStatus", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(ResourceType), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceStatus))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceStatus", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IResourceType), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceStatus))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserRole", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(User), "Role", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Role))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserRole", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IUser), "Role", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IRole))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleOperation", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BusinessModule), "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Operation))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleOperation", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IBusinessModule), "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IOperation))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeOperation", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(ResourceType), "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Operation))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeOperation", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IResourceType), "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IOperation))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationResourceStatus1", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Operation), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceStatus))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationResourceStatus1", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IOperation), "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceStatus))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModulePermission", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BusinessModule), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Permission))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModulePermission", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IBusinessModule), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IPermission))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationPermission", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(Operation), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Permission))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationPermission", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IOperation), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IPermission))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceStatusPermission", "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ResourceStatus), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Permission))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceStatusPermission", "ResourceStatus", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IResourceStatus), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IPermission))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypePermission", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ResourceType), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Permission))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypePermission", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IResourceType), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IPermission))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleResourceType", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(BusinessModule), "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceType))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleResourceType", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.One, typeof(IBusinessModule), "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceType))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceType", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ResourceType), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceType))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceType", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IResourceType), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceType))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceType1", "Reference", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ResourceType), "ReferencesTo", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ResourceType))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeResourceType1", "Reference", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IResourceType), "ReferencesTo", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IResourceType))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserUserLog", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(User), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UserLog))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserUserLog", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IUser), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IUserLog))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeUserLog", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(ResourceType), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UserLog))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "ResourceTypeUserLog", "ResourceType", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IResourceType), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IUserLog))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleUserLog", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(BusinessModule), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UserLog))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "BusinessModuleUserLog", "BusinessModule", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IBusinessModule), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IUserLog))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationUserLog", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Operation), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(UserLog))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "OperationUserLog", "Operation", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IOperation), "UserLog", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IUserLog))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "RolePermission1", "Role", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Role), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Permission))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "RolePermission1", "Role", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IRole), "Permission", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IPermission))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "RoleRole", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Role), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Role))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "RoleRole", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IRole), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IRole))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserThirdPartyAuthentication", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(User), "ThirdPartyAuthentication", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(ThirdPartyAuthentication))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "UserThirdPartyAuthentication", "User", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IUser), "ThirdPartyAuthentication", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IThirdPartyAuthentication))]
[assembly: EdmRelationshipAttribute("Cdts.Framework", "RegionRegion", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(Region), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(Region))]
//[assembly: EdmRelationshipAttribute("Cdts.Framework", "RegionRegion", "Parent", System.Data.Metadata.Edm.RelationshipMultiplicity.ZeroOrOne, typeof(IRegion), "Children", System.Data.Metadata.Edm.RelationshipMultiplicity.Many, typeof(IRegion))]

namespace Cdts.Framework
{

    /// <summary>
    /// 业务模块
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "BusinessModule")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class BusinessModule : EntityObject, IBusinessModule
    {

        /// <summary>
        /// Create a new BusinessModule object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        public static BusinessModule CreateBusinessModule(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.Boolean invalid)
        {
            BusinessModule businessModule = new BusinessModule();
            businessModule.Id = id;
            businessModule.Code = code;
            businessModule.Name = name;
            businessModule.Description = description;
            businessModule.Invalid = invalid;
            return businessModule;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();


        /// <summary>
        /// 子类 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleBusinessModule", "Children")]
        public EntityCollection<BusinessModule> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Children");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Children", value);
                }
            }
        }

        ICollection<IBusinessModule> ITree<IBusinessModule>.Children
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Children.IsLoaded)
                    {
                        this.Children.Load();
                    }
                }
                ObservableCollection<IBusinessModule> list = new ObservableCollection<IBusinessModule>();
                foreach (var item in this.Children)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupChildren);
                return list;
            }
            set
            {

            }
        }
        void FixupChildren(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (BusinessModule item in e.NewItems)
                {
                    //item.Parent = this;
                    this.Children.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (BusinessModule item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Parent, this))
                    //{
                    //item.Parent = null;
                    this.Children.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// 父类 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleBusinessModule", "Parent")]
        public BusinessModule Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Parent").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Parent").Value = value;
            }
        }
        /// <summary>
        /// 父类
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Parent");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleBusinessModule", "Parent", value);
                }
            }
        }

        IBusinessModule ITree<IBusinessModule>.Parent
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ParentReference.IsLoaded)
                    {
                        this.ParentReference.Load();
                    }
                }
                return this.Parent;
            }
            set
            {
                this.Parent = (BusinessModule)value;
            }
        }
    }

    /// <summary>
    /// 设备验证
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "DeviceValidation")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class DeviceValidation : EntityObject, IDeviceValidation
    {

        /// <summary>
        /// Create a new DeviceValidation object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="validationType">Initial value of the ValidationType property.</param>
        /// <param name="deviceName">Initial value of the DeviceName property.</param>
        /// <param name="device">Initial value of the Device property.</param>
        /// <param name="validationCode">Initial value of the ValidationCode property.</param>
        public static DeviceValidation CreateDeviceValidation(global::System.Guid id, global::System.Int32 validationType, global::System.String deviceName, global::System.String device, global::System.String validationCode)
        {
            DeviceValidation deviceValidation = new DeviceValidation();
            deviceValidation.Id = id;
            deviceValidation.ValidationType = validationType;
            deviceValidation.DeviceName = deviceName;
            deviceValidation.Device = device;
            deviceValidation.ValidationCode = validationCode;
            return deviceValidation;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 验证类型
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 ValidationType
        {
            get
            {
                return _ValidationType;
            }
            set
            {
                OnValidationTypeChanging(value);
                ReportPropertyChanging("ValidationType");
                _ValidationType = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ValidationType");
                OnValidationTypeChanged();
            }
        }
        private global::System.Int32 _ValidationType;
        partial void OnValidationTypeChanging(global::System.Int32 value);
        partial void OnValidationTypeChanged();

        /// <summary>
        /// 设备名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String DeviceName
        {
            get
            {
                return _DeviceName;
            }
            set
            {
                OnDeviceNameChanging(value);
                ReportPropertyChanging("DeviceName");
                _DeviceName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("DeviceName");
                OnDeviceNameChanged();
            }
        }
        private global::System.String _DeviceName = "";
        partial void OnDeviceNameChanging(global::System.String value);
        partial void OnDeviceNameChanged();

        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Device
        {
            get
            {
                return _Device;
            }
            set
            {
                OnDeviceChanging(value);
                ReportPropertyChanging("Device");
                _Device = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Device");
                OnDeviceChanged();
            }
        }
        private global::System.String _Device = "";
        partial void OnDeviceChanging(global::System.String value);
        partial void OnDeviceChanged();

        /// <summary>
        /// 验证码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String ValidationCode
        {
            get
            {
                return _ValidationCode;
            }
            set
            {
                OnValidationCodeChanging(value);
                ReportPropertyChanging("ValidationCode");
                _ValidationCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ValidationCode");
                OnValidationCodeChanged();
            }
        }
        private global::System.String _ValidationCode = "";
        partial void OnValidationCodeChanging(global::System.String value);
        partial void OnValidationCodeChanged();

        /// <summary>
        /// 验证时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ValidateTime
        {
            get
            {
                return _ValidateTime;
            }
            set
            {
                OnValidateTimeChanging(value);
                ReportPropertyChanging("ValidateTime");
                _ValidateTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ValidateTime");
                OnValidateTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _ValidateTime;
        partial void OnValidateTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnValidateTimeChanged();

        /// <summary>
        /// 过期时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.DateTime> ExpireTime
        {
            get
            {
                return _ExpireTime;
            }
            set
            {
                OnExpireTimeChanging(value);
                ReportPropertyChanging("ExpireTime");
                _ExpireTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("ExpireTime");
                OnExpireTimeChanged();
            }
        }
        private Nullable<global::System.DateTime> _ExpireTime;
        partial void OnExpireTimeChanging(Nullable<global::System.DateTime> value);
        partial void OnExpireTimeChanged();


        /// <summary>
        /// 用户 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "UserDeviceValidation", "User")]
        public User User
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserDeviceValidation", "User").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserDeviceValidation", "User").Value = value;
            }
        }
        /// <summary>
        /// 用户
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<User> UserReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserDeviceValidation", "User");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<User>("Cdts.Framework.UserDeviceValidation", "User", value);
                }
            }
        }

        IUser IDeviceValidation.User
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.UserReference.IsLoaded)
                    {
                        this.UserReference.Load();
                    }
                }
                return this.User;
            }
            set
            {
                this.User = (User)value;
            }
        }
    }

    /// <summary>
    /// 配置
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "FrameworkSetting")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class FrameworkSetting : EntityObject, IFrameworkSetting
    {

        /// <summary>
        /// Create a new FrameworkSetting object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="businessModuleCode">Initial value of the BusinessModuleCode property.</param>
        /// <param name="administratorsRoleCode">Initial value of the AdministratorsRoleCode property.</param>
        /// <param name="everyoneRoleCode">Initial value of the EveryoneRoleCode property.</param>
        /// <param name="administratorCode">Initial value of the AdministratorCode property.</param>
        /// <param name="version">Initial value of the Version property.</param>
        public static FrameworkSetting CreateFrameworkSetting(global::System.Guid id, global::System.String businessModuleCode, global::System.String administratorsRoleCode, global::System.String everyoneRoleCode, global::System.String administratorCode, global::System.String version)
        {
            FrameworkSetting frameworkSetting = new FrameworkSetting();
            frameworkSetting.Id = id;
            frameworkSetting.BusinessModuleCode = businessModuleCode;
            frameworkSetting.AdministratorsRoleCode = administratorsRoleCode;
            frameworkSetting.EveryoneRoleCode = everyoneRoleCode;
            frameworkSetting.AdministratorCode = administratorCode;
            frameworkSetting.Version = version;
            return frameworkSetting;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 模块编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String BusinessModuleCode
        {
            get
            {
                return _BusinessModuleCode;
            }
            set
            {
                OnBusinessModuleCodeChanging(value);
                ReportPropertyChanging("BusinessModuleCode");
                _BusinessModuleCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("BusinessModuleCode");
                OnBusinessModuleCodeChanged();
            }
        }
        private global::System.String _BusinessModuleCode = "";
        partial void OnBusinessModuleCodeChanging(global::System.String value);
        partial void OnBusinessModuleCodeChanged();

        /// <summary>
        /// 管理员角色编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String AdministratorsRoleCode
        {
            get
            {
                return _AdministratorsRoleCode;
            }
            set
            {
                OnAdministratorsRoleCodeChanging(value);
                ReportPropertyChanging("AdministratorsRoleCode");
                _AdministratorsRoleCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AdministratorsRoleCode");
                OnAdministratorsRoleCodeChanged();
            }
        }
        private global::System.String _AdministratorsRoleCode = "";
        partial void OnAdministratorsRoleCodeChanging(global::System.String value);
        partial void OnAdministratorsRoleCodeChanged();

        /// <summary>
        /// Everyone角色编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String EveryoneRoleCode
        {
            get
            {
                return _EveryoneRoleCode;
            }
            set
            {
                OnEveryoneRoleCodeChanging(value);
                ReportPropertyChanging("EveryoneRoleCode");
                _EveryoneRoleCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("EveryoneRoleCode");
                OnEveryoneRoleCodeChanged();
            }
        }
        private global::System.String _EveryoneRoleCode = "";
        partial void OnEveryoneRoleCodeChanging(global::System.String value);
        partial void OnEveryoneRoleCodeChanged();

        /// <summary>
        /// 管理员账号
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String AdministratorCode
        {
            get
            {
                return _AdministratorCode;
            }
            set
            {
                OnAdministratorCodeChanging(value);
                ReportPropertyChanging("AdministratorCode");
                _AdministratorCode = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("AdministratorCode");
                OnAdministratorCodeChanged();
            }
        }
        private global::System.String _AdministratorCode = "";
        partial void OnAdministratorCodeChanging(global::System.String value);
        partial void OnAdministratorCodeChanged();

        /// <summary>
        /// 版本
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Version
        {
            get
            {
                return _Version;
            }
            set
            {
                OnVersionChanging(value);
                ReportPropertyChanging("Version");
                _Version = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Version");
                OnVersionChanged();
            }
        }
        private global::System.String _Version = "";
        partial void OnVersionChanging(global::System.String value);
        partial void OnVersionChanged();

    }

    /// <summary>
    /// 操作
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "Operation")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Operation : EntityObject, IOperation
    {

        /// <summary>
        /// Create a new Operation object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        public static Operation CreateOperation(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.Boolean invalid)
        {
            Operation operation = new Operation();
            operation.Id = id;
            operation.Code = code;
            operation.Name = name;
            operation.Description = description;
            operation.Invalid = invalid;
            return operation;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();


        /// <summary>
        /// 业务模块 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleOperation", "BusinessModule")]
        public BusinessModule BusinessModule
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleOperation", "BusinessModule").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleOperation", "BusinessModule").Value = value;
            }
        }
        /// <summary>
        /// 业务模块
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> BusinessModuleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleOperation", "BusinessModule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleOperation", "BusinessModule", value);
                }
            }
        }

        IBusinessModule IOperation.BusinessModule
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.BusinessModuleReference.IsLoaded)
                    {
                        this.BusinessModuleReference.Load();
                    }
                }
                return this.BusinessModule;
            }
            set
            {
                this.BusinessModule = (BusinessModule)value;
            }
        }

        /// <summary>
        /// 资源类别 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeOperation", "ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeOperation", "ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeOperation", "ResourceType").Value = value;
            }
        }
        /// <summary>
        /// 资源类别
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeOperation", "ResourceType");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeOperation", "ResourceType", value);
                }
            }
        }

        IResourceType IOperation.ResourceType
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ResourceTypeReference.IsLoaded)
                    {
                        this.ResourceTypeReference.Load();
                    }
                }
                return this.ResourceType;
            }
            set
            {
                this.ResourceType = (ResourceType)value;
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "OperationPermission", "Permission")]
        public EntityCollection<Permission> Permissions
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Permission>("Cdts.Framework.OperationPermission", "Permission");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Permission>("Cdts.Framework.OperationPermission", "Permission", value);
                }
            }
        }

        ICollection<IPermission> IOperation.Permissions
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Permissions.IsLoaded)
                    {
                        this.Permissions.Load();
                    }
                }
                ObservableCollection<IPermission> list = new ObservableCollection<IPermission>();
                foreach (var item in this.Permissions)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupPermissions);
                return list;
            }
            set
            {

            }
        }
        void FixupPermissions(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Permission item in e.NewItems)
                {
                    //item.Operation = this;
                    this.Permissions.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Permission item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Operation, this))
                    //{
                    //item.Operation = null;
                    this.Permissions.Remove(item);
                    //}
                }
            }
        }
    }

    /// <summary>
    /// 许可
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "Permission")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Permission : EntityObject, IPermission
    {

        /// <summary>
        /// Create a new Permission object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        public static Permission CreatePermission(global::System.Guid id)
        {
            Permission permission = new Permission();
            permission.Id = id;
            return permission;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();


        /// <summary>
        /// 业务模块 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModulePermission", "BusinessModule")]
        public BusinessModule BusinessModule
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModulePermission", "BusinessModule").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModulePermission", "BusinessModule").Value = value;
            }
        }
        /// <summary>
        /// 业务模块
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> BusinessModuleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModulePermission", "BusinessModule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModulePermission", "BusinessModule", value);
                }
            }
        }

        IBusinessModule IPermission.BusinessModule
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.BusinessModuleReference.IsLoaded)
                    {
                        this.BusinessModuleReference.Load();
                    }
                }
                return this.BusinessModule;
            }
            set
            {
                this.BusinessModule = (BusinessModule)value;
            }
        }

        /// <summary>
        /// 操作 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "OperationPermission", "Operation")]
        public Operation Operation
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationPermission", "Operation").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationPermission", "Operation").Value = value;
            }
        }
        /// <summary>
        /// 操作
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Operation> OperationReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationPermission", "Operation");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Operation>("Cdts.Framework.OperationPermission", "Operation", value);
                }
            }
        }

        IOperation IPermission.Operation
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.OperationReference.IsLoaded)
                    {
                        this.OperationReference.Load();
                    }
                }
                return this.Operation;
            }
            set
            {
                this.Operation = (Operation)value;
            }
        }

        /// <summary>
        /// 资源状态 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceStatusPermission", "ResourceStatus")]
        public ResourceStatus ResourceStatus
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceStatus>("Cdts.Framework.ResourceStatusPermission", "ResourceStatus").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceStatus>("Cdts.Framework.ResourceStatusPermission", "ResourceStatus").Value = value;
            }
        }
        /// <summary>
        /// 资源状态
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceStatus> ResourceStatusReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceStatus>("Cdts.Framework.ResourceStatusPermission", "ResourceStatus");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceStatus>("Cdts.Framework.ResourceStatusPermission", "ResourceStatus", value);
                }
            }
        }

        IResourceStatus IPermission.ResourceStatus
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ResourceStatusReference.IsLoaded)
                    {
                        this.ResourceStatusReference.Load();
                    }
                }
                return this.ResourceStatus;
            }
            set
            {
                this.ResourceStatus = (ResourceStatus)value;
            }
        }

        /// <summary>
        /// 资源类别 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypePermission", "ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypePermission", "ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypePermission", "ResourceType").Value = value;
            }
        }
        /// <summary>
        /// 资源类别
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypePermission", "ResourceType");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypePermission", "ResourceType", value);
                }
            }
        }

        IResourceType IPermission.ResourceType
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ResourceTypeReference.IsLoaded)
                    {
                        this.ResourceTypeReference.Load();
                    }
                }
                return this.ResourceType;
            }
            set
            {
                this.ResourceType = (ResourceType)value;
            }
        }
    }

    /// <summary>
    /// 地区
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "Region")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Region : EntityObject, IRegion
    {

        /// <summary>
        /// Create a new Region object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="order">Initial value of the Order property.</param>
        /// <param name="codeLevel">Initial value of the CodeLevel property.</param>
        public static Region CreateRegion(global::System.Guid id, global::System.String name, global::System.String code, global::System.Int32 order, global::System.Int32 codeLevel)
        {
            Region region = new Region();
            region.Id = id;
            region.Name = name;
            region.Code = code;
            region.Order = order;
            region.CodeLevel = codeLevel;
            return region;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 排序
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 Order
        {
            get
            {
                return _Order;
            }
            set
            {
                OnOrderChanging(value);
                ReportPropertyChanging("Order");
                _Order = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Order");
                OnOrderChanged();
            }
        }
        private global::System.Int32 _Order;
        partial void OnOrderChanging(global::System.Int32 value);
        partial void OnOrderChanged();

        /// <summary>
        /// 等级
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Int32 CodeLevel
        {
            get
            {
                return _CodeLevel;
            }
            set
            {
                OnCodeLevelChanging(value);
                ReportPropertyChanging("CodeLevel");
                _CodeLevel = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CodeLevel");
                OnCodeLevelChanged();
            }
        }
        private global::System.Int32 _CodeLevel;
        partial void OnCodeLevelChanging(global::System.Int32 value);
        partial void OnCodeLevelChanged();


        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "RegionRegion", "Children")]
        public EntityCollection<Region> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Region>("Cdts.Framework.RegionRegion", "Children");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Region>("Cdts.Framework.RegionRegion", "Children", value);
                }
            }
        }

        ICollection<IRegion> ITree<IRegion>.Children
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Children.IsLoaded)
                    {
                        this.Children.Load();
                    }
                }
                ObservableCollection<IRegion> list = new ObservableCollection<IRegion>();
                foreach (var item in this.Children)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupChildren);
                return list;
            }
            set
            {

            }
        }
        void FixupChildren(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Region item in e.NewItems)
                {
                    //item.Parent = this;
                    this.Children.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Region item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Parent, this))
                    //{
                    //item.Parent = null;
                    this.Children.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "RegionRegion", "Parent")]
        public Region Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Region>("Cdts.Framework.RegionRegion", "Parent").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Region>("Cdts.Framework.RegionRegion", "Parent").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Region> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Region>("Cdts.Framework.RegionRegion", "Parent");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Region>("Cdts.Framework.RegionRegion", "Parent", value);
                }
            }
        }

        IRegion ITree<IRegion>.Parent
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ParentReference.IsLoaded)
                    {
                        this.ParentReference.Load();
                    }
                }
                return this.Parent;
            }
            set
            {
                this.Parent = (Region)value;
            }
        }
    }

    /// <summary>
    /// 资源状态
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "ResourceStatus")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class ResourceStatus : EntityObject, IResourceStatus
    {

        /// <summary>
        /// Create a new ResourceStatus object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        public static ResourceStatus CreateResourceStatus(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.Boolean invalid)
        {
            ResourceStatus resourceStatus = new ResourceStatus();
            resourceStatus.Id = id;
            resourceStatus.Code = code;
            resourceStatus.Name = name;
            resourceStatus.Description = description;
            resourceStatus.Invalid = invalid;
            return resourceStatus;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();


        /// <summary>
        /// 业务模块 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleResourceStatus", "BusinessModule")]
        public BusinessModule BusinessModule
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceStatus", "BusinessModule").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceStatus", "BusinessModule").Value = value;
            }
        }
        /// <summary>
        /// 业务模块
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> BusinessModuleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceStatus", "BusinessModule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceStatus", "BusinessModule", value);
                }
            }
        }

        IBusinessModule IResourceStatus.BusinessModule
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.BusinessModuleReference.IsLoaded)
                    {
                        this.BusinessModuleReference.Load();
                    }
                }
                return this.BusinessModule;
            }
            set
            {
                this.BusinessModule = (BusinessModule)value;
            }
        }

        /// <summary>
        /// 资源类别 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeResourceStatus", "ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceStatus", "ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceStatus", "ResourceType").Value = value;
            }
        }
        /// <summary>
        /// 资源类别
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceStatus", "ResourceType");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceStatus", "ResourceType", value);
                }
            }
        }

        IResourceType IResourceStatus.ResourceType
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ResourceTypeReference.IsLoaded)
                    {
                        this.ResourceTypeReference.Load();
                    }
                }
                return this.ResourceType;
            }
            set
            {
                this.ResourceType = (ResourceType)value;
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "OperationResourceStatus1", "Operation")]
        public EntityCollection<Operation> Operations
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Operation>("Cdts.Framework.OperationResourceStatus1", "Operation");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Operation>("Cdts.Framework.OperationResourceStatus1", "Operation", value);
                }
            }
        }

        ICollection<IOperation> IResourceStatus.Operations
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Operations.IsLoaded)
                    {
                        this.Operations.Load();
                    }
                }
                ObservableCollection<IOperation> list = new ObservableCollection<IOperation>();
                foreach (var item in this.Operations)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupOperations);
                return list;
            }
            set
            {

            }
        }
        void FixupOperations(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Operation item in e.NewItems)
                {
                    if (!this.Operations.Contains(item))
                    {
                        this.Operations.Add(item);
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (Operation item in e.OldItems)
                {
                    if (this.Operations.Contains(item))
                    {
                        this.Operations.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceStatusPermission", "Permission")]
        public EntityCollection<Permission> Permissions
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Permission>("Cdts.Framework.ResourceStatusPermission", "Permission");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Permission>("Cdts.Framework.ResourceStatusPermission", "Permission", value);
                }
            }
        }

        ICollection<IPermission> IResourceStatus.Permissions
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Permissions.IsLoaded)
                    {
                        this.Permissions.Load();
                    }
                }
                ObservableCollection<IPermission> list = new ObservableCollection<IPermission>();
                foreach (var item in this.Permissions)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupPermissions);
                return list;
            }
            set
            {

            }
        }
        void FixupPermissions(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Permission item in e.NewItems)
                {
                    //item.ResourceStatus = this;
                    this.Permissions.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Permission item in e.OldItems)
                {
                    //if (ReferenceEquals(item.ResourceStatus, this))
                    //{
                    //item.ResourceStatus = null;
                    this.Permissions.Remove(item);
                    //}
                }
            }
        }
    }

    /// <summary>
    /// 资源类别
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "ResourceType")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class ResourceType : EntityObject, IResourceType
    {

        /// <summary>
        /// Create a new ResourceType object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        public static ResourceType CreateResourceType(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.Boolean invalid)
        {
            ResourceType resourceType = new ResourceType();
            resourceType.Id = id;
            resourceType.Code = code;
            resourceType.Name = name;
            resourceType.Description = description;
            resourceType.Invalid = invalid;
            return resourceType;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();


        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeOperation", "Operation")]
        public EntityCollection<Operation> Operations
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Operation>("Cdts.Framework.ResourceTypeOperation", "Operation");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Operation>("Cdts.Framework.ResourceTypeOperation", "Operation", value);
                }
            }
        }

        ICollection<IOperation> IResourceType.Operations
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Operations.IsLoaded)
                    {
                        this.Operations.Load();
                    }
                }
                ObservableCollection<IOperation> list = new ObservableCollection<IOperation>();
                foreach (var item in this.Operations)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupOperations);
                return list;
            }
            set
            {

            }
        }
        void FixupOperations(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Operation item in e.NewItems)
                {
                    //item.ResourceType = this;
                    this.Operations.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Operation item in e.OldItems)
                {
                    //if (ReferenceEquals(item.ResourceType, this))
                    //{
                    //item.ResourceType = null;
                    this.Operations.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// 业务模块 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleResourceType", "BusinessModule")]
        public BusinessModule BusinessModule
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceType", "BusinessModule").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceType", "BusinessModule").Value = value;
            }
        }
        /// <summary>
        /// 业务模块
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> BusinessModuleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceType", "BusinessModule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleResourceType", "BusinessModule", value);
                }
            }
        }

        IBusinessModule IResourceType.BusinessModule
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.BusinessModuleReference.IsLoaded)
                    {
                        this.BusinessModuleReference.Load();
                    }
                }
                return this.BusinessModule;
            }
            set
            {
                this.BusinessModule = (BusinessModule)value;
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeResourceType", "Children")]
        public EntityCollection<ResourceType> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Children");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Children", value);
                }
            }
        }

        ICollection<IResourceType> ITree<IResourceType>.Children
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Children.IsLoaded)
                    {
                        this.Children.Load();
                    }
                }
                ObservableCollection<IResourceType> list = new ObservableCollection<IResourceType>();
                foreach (var item in this.Children)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupChildren);
                return list;
            }
            set
            {

            }
        }
        void FixupChildren(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ResourceType item in e.NewItems)
                {
                    //item.Parent = this;
                    this.Children.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (ResourceType item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Parent, this))
                    //{
                    //item.Parent = null;
                    this.Children.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// 父类 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeResourceType", "Parent")]
        public ResourceType Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Parent").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Parent").Value = value;
            }
        }
        /// <summary>
        /// 父类
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Parent");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType", "Parent", value);
                }
            }
        }

        IResourceType ITree<IResourceType>.Parent
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ParentReference.IsLoaded)
                    {
                        this.ParentReference.Load();
                    }
                }
                return this.Parent;
            }
            set
            {
                this.Parent = (ResourceType)value;
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeResourceType1", "ReferencesTo")]
        public EntityCollection<ResourceType> ReferencesTo
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "ReferencesTo");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "ReferencesTo", value);
                }
            }
        }

        ICollection<IResourceType> IResourceType.ReferencesTo
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ReferencesTo.IsLoaded)
                    {
                        this.ReferencesTo.Load();
                    }
                }
                ObservableCollection<IResourceType> list = new ObservableCollection<IResourceType>();
                foreach (var item in this.ReferencesTo)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupReferencesTo);
                return list;
            }
            set
            {

            }
        }
        void FixupReferencesTo(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (ResourceType item in e.NewItems)
                {
                    //item.Reference = this;
                    this.ReferencesTo.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (ResourceType item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Reference, this))
                    //{
                    //item.Reference = null;
                    this.ReferencesTo.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeResourceType1", "Reference")]
        public ResourceType Reference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "Reference").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "Reference").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ReferenceReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "Reference");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeResourceType1", "Reference", value);
                }
            }
        }

        IResourceType IResourceType.Reference
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ReferenceReference.IsLoaded)
                    {
                        this.ReferenceReference.Load();
                    }
                }
                return this.Reference;
            }
            set
            {
                this.Reference = (ResourceType)value;
            }
        }
    }

    /// <summary>
    /// 角色
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "Role")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class Role : EntityObject, IRole
    {

        /// <summary>
        /// Create a new Role object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        public static Role CreateRole(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.Boolean invalid)
        {
            Role role = new Role();
            role.Id = id;
            role.Code = code;
            role.Name = name;
            role.Description = description;
            role.Invalid = invalid;
            return role;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 编码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();


        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "UserRole", "User")]
        public EntityCollection<User> Users
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<User>("Cdts.Framework.UserRole", "User");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<User>("Cdts.Framework.UserRole", "User", value);
                }
            }
        }

        ICollection<IUser> IRole.Users
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Users.IsLoaded)
                    {
                        this.Users.Load();
                    }
                }
                ObservableCollection<IUser> list = new ObservableCollection<IUser>();
                foreach (var item in this.Users)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupUsers);
                return list;
            }
            set
            {

            }
        }
        void FixupUsers(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (User item in e.NewItems)
                {
                    if (!this.Users.Contains(item))
                    {
                        this.Users.Add(item);
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (User item in e.OldItems)
                {
                    if (this.Users.Contains(item))
                    {
                        this.Users.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "RolePermission1", "Permission")]
        public EntityCollection<Permission> Permissions
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Permission>("Cdts.Framework.RolePermission1", "Permission");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Permission>("Cdts.Framework.RolePermission1", "Permission", value);
                }
            }
        }

        ICollection<IPermission> IRole.Permissions
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Permissions.IsLoaded)
                    {
                        this.Permissions.Load();
                    }
                }
                ObservableCollection<IPermission> list = new ObservableCollection<IPermission>();
                foreach (var item in this.Permissions)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupPermissions);
                return list;
            }
            set
            {

            }
        }
        void FixupPermissions(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Permission item in e.NewItems)
                {
                    if (!this.Permissions.Contains(item))
                    {
                        this.Permissions.Add(item);
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (Permission item in e.OldItems)
                {
                    if (this.Permissions.Contains(item))
                    {
                        this.Permissions.Remove(item);
                    }
                }
            }
        }

        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "RoleRole", "Children")]
        public EntityCollection<Role> Children
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Role>("Cdts.Framework.RoleRole", "Children");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Role>("Cdts.Framework.RoleRole", "Children", value);
                }
            }
        }

        ICollection<IRole> ITree<IRole>.Children
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Children.IsLoaded)
                    {
                        this.Children.Load();
                    }
                }
                ObservableCollection<IRole> list = new ObservableCollection<IRole>();
                foreach (var item in this.Children)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupChildren);
                return list;
            }
            set
            {

            }
        }
        void FixupChildren(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Role item in e.NewItems)
                {
                    //item.Parent = this;
                    this.Children.Add(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (Role item in e.OldItems)
                {
                    //if (ReferenceEquals(item.Parent, this))
                    //{
                    //item.Parent = null;
                    this.Children.Remove(item);
                    //}
                }
            }
        }

        /// <summary>
        /// 上级 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "RoleRole", "Parent")]
        public Role Parent
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Role>("Cdts.Framework.RoleRole", "Parent").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Role>("Cdts.Framework.RoleRole", "Parent").Value = value;
            }
        }
        /// <summary>
        /// 上级
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Role> ParentReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Role>("Cdts.Framework.RoleRole", "Parent");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Role>("Cdts.Framework.RoleRole", "Parent", value);
                }
            }
        }

        IRole ITree<IRole>.Parent
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ParentReference.IsLoaded)
                    {
                        this.ParentReference.Load();
                    }
                }
                return this.Parent;
            }
            set
            {
                this.Parent = (Role)value;
            }
        }
    }

    /// <summary>
    /// 第三方验证
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "ThirdPartyAuthentication")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class ThirdPartyAuthentication : EntityObject, IThirdPartyAuthentication
    {

        /// <summary>
        /// Create a new ThirdPartyAuthentication object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="thirdPartyName">Initial value of the ThirdPartyName property.</param>
        /// <param name="thirdPartyId">Initial value of the ThirdPartyId property.</param>
        /// <param name="thirdPartyUserName">Initial value of the ThirdPartyUserName property.</param>
        /// <param name="verified">Initial value of the Verified property.</param>
        public static ThirdPartyAuthentication CreateThirdPartyAuthentication(global::System.Guid id, global::System.String thirdPartyName, global::System.String thirdPartyId, global::System.String thirdPartyUserName, global::System.Boolean verified)
        {
            ThirdPartyAuthentication thirdPartyAuthentication = new ThirdPartyAuthentication();
            thirdPartyAuthentication.Id = id;
            thirdPartyAuthentication.ThirdPartyName = thirdPartyName;
            thirdPartyAuthentication.ThirdPartyId = thirdPartyId;
            thirdPartyAuthentication.ThirdPartyUserName = thirdPartyUserName;
            thirdPartyAuthentication.Verified = verified;
            return thirdPartyAuthentication;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 第三方名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String ThirdPartyName
        {
            get
            {
                return _ThirdPartyName;
            }
            set
            {
                OnThirdPartyNameChanging(value);
                ReportPropertyChanging("ThirdPartyName");
                _ThirdPartyName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ThirdPartyName");
                OnThirdPartyNameChanged();
            }
        }
        private global::System.String _ThirdPartyName = "";
        partial void OnThirdPartyNameChanging(global::System.String value);
        partial void OnThirdPartyNameChanged();

        /// <summary>
        /// 第三方ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String ThirdPartyId
        {
            get
            {
                return _ThirdPartyId;
            }
            set
            {
                OnThirdPartyIdChanging(value);
                ReportPropertyChanging("ThirdPartyId");
                _ThirdPartyId = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ThirdPartyId");
                OnThirdPartyIdChanged();
            }
        }
        private global::System.String _ThirdPartyId = "";
        partial void OnThirdPartyIdChanging(global::System.String value);
        partial void OnThirdPartyIdChanged();

        /// <summary>
        /// 第三方用户名
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String ThirdPartyUserName
        {
            get
            {
                return _ThirdPartyUserName;
            }
            set
            {
                OnThirdPartyUserNameChanging(value);
                ReportPropertyChanging("ThirdPartyUserName");
                _ThirdPartyUserName = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("ThirdPartyUserName");
                OnThirdPartyUserNameChanged();
            }
        }
        private global::System.String _ThirdPartyUserName = "";
        partial void OnThirdPartyUserNameChanging(global::System.String value);
        partial void OnThirdPartyUserNameChanged();

        /// <summary>
        /// 验证
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Verified
        {
            get
            {
                return _Verified;
            }
            set
            {
                OnVerifiedChanging(value);
                ReportPropertyChanging("Verified");
                _Verified = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Verified");
                OnVerifiedChanged();
            }
        }
        private global::System.Boolean _Verified;
        partial void OnVerifiedChanging(global::System.Boolean value);
        partial void OnVerifiedChanged();


        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "UserThirdPartyAuthentication", "User")]
        public User User
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserThirdPartyAuthentication", "User").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserThirdPartyAuthentication", "User").Value = value;
            }
        }
        /// <summary>
        /// No Metadata Documentation available.
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<User> UserReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserThirdPartyAuthentication", "User");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<User>("Cdts.Framework.UserThirdPartyAuthentication", "User", value);
                }
            }
        }

        IUser IThirdPartyAuthentication.User
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.UserReference.IsLoaded)
                    {
                        this.UserReference.Load();
                    }
                }
                return this.User;
            }
            set
            {
                this.User = (User)value;
            }
        }
    }

    /// <summary>
    /// 用户
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "User")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class User : EntityObject, IUser
    {

        /// <summary>
        /// Create a new User object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="code">Initial value of the Code property.</param>
        /// <param name="name">Initial value of the Name property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="email">Initial value of the Email property.</param>
        /// <param name="emailValidated">Initial value of the EmailValidated property.</param>
        /// <param name="password">Initial value of the Password property.</param>
        /// <param name="mobile">Initial value of the Mobile property.</param>
        /// <param name="mobileValidated">Initial value of the MobileValidated property.</param>
        /// <param name="registerTime">Initial value of the RegisterTime property.</param>
        /// <param name="lastLoginIp">Initial value of the LastLoginIp property.</param>
        /// <param name="lastLoginTime">Initial value of the LastLoginTime property.</param>
        /// <param name="nick">Initial value of the Nick property.</param>
        /// <param name="currentLoginIp">Initial value of the CurrentLoginIp property.</param>
        /// <param name="currentLoginTime">Initial value of the CurrentLoginTime property.</param>
        /// <param name="invalid">Initial value of the Invalid property.</param>
        /// <param name="isThdParty">Initial value of the IsThdParty property.</param>
        public static User CreateUser(global::System.Guid id, global::System.String code, global::System.String name, global::System.String description, global::System.String email, global::System.Boolean emailValidated, global::System.String password, global::System.String mobile, global::System.Boolean mobileValidated, global::System.DateTime registerTime, global::System.String lastLoginIp, global::System.DateTime lastLoginTime, global::System.String nick, global::System.String currentLoginIp, global::System.DateTime currentLoginTime, global::System.Boolean invalid, global::System.Boolean isThdParty)
        {
            User user = new User();
            user.Id = id;
            user.Code = code;
            user.Name = name;
            user.Description = description;
            user.Email = email;
            user.EmailValidated = emailValidated;
            user.Password = password;
            user.Mobile = mobile;
            user.MobileValidated = mobileValidated;
            user.RegisterTime = registerTime;
            user.LastLoginIp = lastLoginIp;
            user.LastLoginTime = lastLoginTime;
            user.Nick = nick;
            user.CurrentLoginIp = currentLoginIp;
            user.CurrentLoginTime = currentLoginTime;
            user.Invalid = invalid;
            user.IsThdParty = isThdParty;
            return user;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 账号
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Code
        {
            get
            {
                return _Code;
            }
            set
            {
                OnCodeChanging(value);
                ReportPropertyChanging("Code");
                _Code = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Code");
                OnCodeChanged();
            }
        }
        private global::System.String _Code = "";
        partial void OnCodeChanging(global::System.String value);
        partial void OnCodeChanged();

        /// <summary>
        /// 名称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                OnNameChanging(value);
                ReportPropertyChanging("Name");
                _Name = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Name");
                OnNameChanged();
            }
        }
        private global::System.String _Name = "";
        partial void OnNameChanging(global::System.String value);
        partial void OnNameChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// 邮箱
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Email
        {
            get
            {
                return _Email;
            }
            set
            {
                OnEmailChanging(value);
                ReportPropertyChanging("Email");
                _Email = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Email");
                OnEmailChanged();
            }
        }
        private global::System.String _Email = "";
        partial void OnEmailChanging(global::System.String value);
        partial void OnEmailChanged();

        /// <summary>
        /// 邮箱验证
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean EmailValidated
        {
            get
            {
                return _EmailValidated;
            }
            set
            {
                OnEmailValidatedChanging(value);
                ReportPropertyChanging("EmailValidated");
                _EmailValidated = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("EmailValidated");
                OnEmailValidatedChanged();
            }
        }
        private global::System.Boolean _EmailValidated;
        partial void OnEmailValidatedChanging(global::System.Boolean value);
        partial void OnEmailValidatedChanged();

        /// <summary>
        /// 密码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Password
        {
            get
            {
                return _Password;
            }
            set
            {
                OnPasswordChanging(value);
                ReportPropertyChanging("Password");
                _Password = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Password");
                OnPasswordChanged();
            }
        }
        private global::System.String _Password = "";
        partial void OnPasswordChanging(global::System.String value);
        partial void OnPasswordChanged();

        /// <summary>
        /// 手机号码
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Mobile
        {
            get
            {
                return _Mobile;
            }
            set
            {
                OnMobileChanging(value);
                ReportPropertyChanging("Mobile");
                _Mobile = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Mobile");
                OnMobileChanged();
            }
        }
        private global::System.String _Mobile = "";
        partial void OnMobileChanging(global::System.String value);
        partial void OnMobileChanged();

        /// <summary>
        /// 手机号码验证
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean MobileValidated
        {
            get
            {
                return _MobileValidated;
            }
            set
            {
                OnMobileValidatedChanging(value);
                ReportPropertyChanging("MobileValidated");
                _MobileValidated = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("MobileValidated");
                OnMobileValidatedChanged();
            }
        }
        private global::System.Boolean _MobileValidated;
        partial void OnMobileValidatedChanging(global::System.Boolean value);
        partial void OnMobileValidatedChanged();

        /// <summary>
        /// 注册时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.DateTime RegisterTime
        {
            get
            {
                return _RegisterTime;
            }
            set
            {
                OnRegisterTimeChanging(value);
                ReportPropertyChanging("RegisterTime");
                _RegisterTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("RegisterTime");
                OnRegisterTimeChanged();
            }
        }
        private global::System.DateTime _RegisterTime = DateTime.Now;
        partial void OnRegisterTimeChanging(global::System.DateTime value);
        partial void OnRegisterTimeChanged();

        /// <summary>
        /// 最后登录IP
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String LastLoginIp
        {
            get
            {
                return _LastLoginIp;
            }
            set
            {
                OnLastLoginIpChanging(value);
                ReportPropertyChanging("LastLoginIp");
                _LastLoginIp = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LastLoginIp");
                OnLastLoginIpChanged();
            }
        }
        private global::System.String _LastLoginIp = "";
        partial void OnLastLoginIpChanging(global::System.String value);
        partial void OnLastLoginIpChanged();

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.DateTime LastLoginTime
        {
            get
            {
                return _LastLoginTime;
            }
            set
            {
                OnLastLoginTimeChanging(value);
                ReportPropertyChanging("LastLoginTime");
                _LastLoginTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LastLoginTime");
                OnLastLoginTimeChanged();
            }
        }
        private global::System.DateTime _LastLoginTime = DateTime.Now;
        partial void OnLastLoginTimeChanging(global::System.DateTime value);
        partial void OnLastLoginTimeChanged();

        /// <summary>
        /// 昵称
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Nick
        {
            get
            {
                return _Nick;
            }
            set
            {
                OnNickChanging(value);
                ReportPropertyChanging("Nick");
                _Nick = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Nick");
                OnNickChanged();
            }
        }
        private global::System.String _Nick = "";
        partial void OnNickChanging(global::System.String value);
        partial void OnNickChanged();

        /// <summary>
        /// 现在登录IP
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String CurrentLoginIp
        {
            get
            {
                return _CurrentLoginIp;
            }
            set
            {
                OnCurrentLoginIpChanging(value);
                ReportPropertyChanging("CurrentLoginIp");
                _CurrentLoginIp = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("CurrentLoginIp");
                OnCurrentLoginIpChanged();
            }
        }
        private global::System.String _CurrentLoginIp = "";
        partial void OnCurrentLoginIpChanging(global::System.String value);
        partial void OnCurrentLoginIpChanged();

        /// <summary>
        /// 现在登录时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.DateTime CurrentLoginTime
        {
            get
            {
                return _CurrentLoginTime;
            }
            set
            {
                OnCurrentLoginTimeChanging(value);
                ReportPropertyChanging("CurrentLoginTime");
                _CurrentLoginTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("CurrentLoginTime");
                OnCurrentLoginTimeChanged();
            }
        }
        private global::System.DateTime _CurrentLoginTime = DateTime.Now;
        partial void OnCurrentLoginTimeChanging(global::System.DateTime value);
        partial void OnCurrentLoginTimeChanged();

        /// <summary>
        /// 有效性
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean Invalid
        {
            get
            {
                return _Invalid;
            }
            set
            {
                OnInvalidChanging(value);
                ReportPropertyChanging("Invalid");
                _Invalid = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("Invalid");
                OnInvalidChanged();
            }
        }
        private global::System.Boolean _Invalid;
        partial void OnInvalidChanging(global::System.Boolean value);
        partial void OnInvalidChanged();

        /// <summary>
        /// 是否是第三方用户
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Boolean IsThdParty
        {
            get
            {
                return _IsThdParty;
            }
            set
            {
                OnIsThdPartyChanging(value);
                ReportPropertyChanging("IsThdParty");
                _IsThdParty = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IsThdParty");
                OnIsThdPartyChanged();
            }
        }
        private global::System.Boolean _IsThdParty;
        partial void OnIsThdPartyChanging(global::System.Boolean value);
        partial void OnIsThdPartyChanged();

        /// <summary>
        /// 推荐人ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public Nullable<global::System.Guid> IntroducerId
        {
            get
            {
                return _IntroducerId;
            }
            set
            {
                OnIntroducerIdChanging(value);
                ReportPropertyChanging("IntroducerId");
                _IntroducerId = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("IntroducerId");
                OnIntroducerIdChanged();
            }
        }
        private Nullable<global::System.Guid> _IntroducerId;
        partial void OnIntroducerIdChanging(Nullable<global::System.Guid> value);
        partial void OnIntroducerIdChanged();


        /// <summary>
        /// No Metadata Documentation available. 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "UserRole", "Role")]
        public EntityCollection<Role> Roles
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedCollection<Role>("Cdts.Framework.UserRole", "Role");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedCollection<Role>("Cdts.Framework.UserRole", "Role", value);
                }
            }
        }

        ICollection<IRole> IUser.Roles
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.Roles.IsLoaded)
                    {
                        this.Roles.Load();
                    }
                }
                ObservableCollection<IRole> list = new ObservableCollection<IRole>();
                foreach (var item in this.Roles)
                {
                    list.Add(item);
                }
                list.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(FixupRoles);
                return list;
            }
            set
            {

            }
        }
        void FixupRoles(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Role item in e.NewItems)
                {
                    if (!this.Roles.Contains(item))
                    {
                        this.Roles.Add(item);
                    }
                }
            }

            if (e.OldItems != null)
            {
                foreach (Role item in e.OldItems)
                {
                    if (this.Roles.Contains(item))
                    {
                        this.Roles.Remove(item);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 用户日志
    /// </summary>
    [EdmEntityTypeAttribute(NamespaceName = "Cdts.Framework", Name = "UserLog")]
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class UserLog : EntityObject, IUserLog
    {

        /// <summary>
        /// Create a new UserLog object.
        /// </summary>
        /// <param name="id">Initial value of the Id property.</param>
        /// <param name="logTime">Initial value of the LogTime property.</param>
        /// <param name="description">Initial value of the Description property.</param>
        /// <param name="logIp">Initial value of the LogIp property.</param>
        public static UserLog CreateUserLog(global::System.Guid id, global::System.DateTime logTime, global::System.String description, global::System.String logIp)
        {
            UserLog userLog = new UserLog();
            userLog.Id = id;
            userLog.LogTime = logTime;
            userLog.Description = description;
            userLog.LogIp = logIp;
            return userLog;
        }

        /// <summary>
        /// ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = true, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.Guid Id
        {
            get
            {
                return _Id;
            }
            set
            {
                if (_Id != value)
                {
                    OnIdChanging(value);
                    ReportPropertyChanging("Id");
                    _Id = StructuralObject.SetValidValue(value);
                    ReportPropertyChanged("Id");
                    OnIdChanged();
                }
            }
        }
        private global::System.Guid _Id;
        partial void OnIdChanging(global::System.Guid value);
        partial void OnIdChanged();

        /// <summary>
        /// 时间
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.DateTime LogTime
        {
            get
            {
                return _LogTime;
            }
            set
            {
                OnLogTimeChanging(value);
                ReportPropertyChanging("LogTime");
                _LogTime = StructuralObject.SetValidValue(value);
                ReportPropertyChanged("LogTime");
                OnLogTimeChanged();
            }
        }
        private global::System.DateTime _LogTime = DateTime.Now;
        partial void OnLogTimeChanging(global::System.DateTime value);
        partial void OnLogTimeChanged();

        /// <summary>
        /// 资源ID
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = true)]
        [DataMemberAttribute()]
        public global::System.String ResourceId
        {
            get
            {
                return _ResourceId;
            }
            set
            {
                OnResourceIdChanging(value);
                ReportPropertyChanging("ResourceId");
                _ResourceId = StructuralObject.SetValidValue(value, true);
                ReportPropertyChanged("ResourceId");
                OnResourceIdChanged();
            }
        }
        private global::System.String _ResourceId = "";
        partial void OnResourceIdChanging(global::System.String value);
        partial void OnResourceIdChanged();

        /// <summary>
        /// 描述
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String Description
        {
            get
            {
                return _Description;
            }
            set
            {
                OnDescriptionChanging(value);
                ReportPropertyChanging("Description");
                _Description = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("Description");
                OnDescriptionChanged();
            }
        }
        private global::System.String _Description = "";
        partial void OnDescriptionChanging(global::System.String value);
        partial void OnDescriptionChanged();

        /// <summary>
        /// IP
        /// </summary>
        [EdmScalarPropertyAttribute(EntityKeyProperty = false, IsNullable = false)]
        [DataMemberAttribute()]
        public global::System.String LogIp
        {
            get
            {
                return _LogIp;
            }
            set
            {
                OnLogIpChanging(value);
                ReportPropertyChanging("LogIp");
                _LogIp = StructuralObject.SetValidValue(value, false);
                ReportPropertyChanged("LogIp");
                OnLogIpChanged();
            }
        }
        private global::System.String _LogIp = "";
        partial void OnLogIpChanging(global::System.String value);
        partial void OnLogIpChanged();


        /// <summary>
        /// 用户 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "UserUserLog", "User")]
        public User User
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserUserLog", "User").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserUserLog", "User").Value = value;
            }
        }
        /// <summary>
        /// 用户
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<User> UserReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<User>("Cdts.Framework.UserUserLog", "User");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<User>("Cdts.Framework.UserUserLog", "User", value);
                }
            }
        }

        IUser IUserLog.User
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.UserReference.IsLoaded)
                    {
                        this.UserReference.Load();
                    }
                }
                return this.User;
            }
            set
            {
                this.User = (User)value;
            }
        }

        /// <summary>
        /// 资源类型 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "ResourceTypeUserLog", "ResourceType")]
        public ResourceType ResourceType
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeUserLog", "ResourceType").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeUserLog", "ResourceType").Value = value;
            }
        }
        /// <summary>
        /// 资源类型
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<ResourceType> ResourceTypeReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeUserLog", "ResourceType");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<ResourceType>("Cdts.Framework.ResourceTypeUserLog", "ResourceType", value);
                }
            }
        }

        IResourceType IUserLog.ResourceType
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.ResourceTypeReference.IsLoaded)
                    {
                        this.ResourceTypeReference.Load();
                    }
                }
                return this.ResourceType;
            }
            set
            {
                this.ResourceType = (ResourceType)value;
            }
        }

        /// <summary>
        /// 业务模块 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "BusinessModuleUserLog", "BusinessModule")]
        public BusinessModule BusinessModule
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleUserLog", "BusinessModule").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleUserLog", "BusinessModule").Value = value;
            }
        }
        /// <summary>
        /// 业务模块
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<BusinessModule> BusinessModuleReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleUserLog", "BusinessModule");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<BusinessModule>("Cdts.Framework.BusinessModuleUserLog", "BusinessModule", value);
                }
            }
        }

        IBusinessModule IUserLog.BusinessModule
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.BusinessModuleReference.IsLoaded)
                    {
                        this.BusinessModuleReference.Load();
                    }
                }
                return this.BusinessModule;
            }
            set
            {
                this.BusinessModule = (BusinessModule)value;
            }
        }

        /// <summary>
        /// 操作 
        /// </summary>
        [XmlIgnoreAttribute()]
        [SoapIgnoreAttribute()]
        [DataMemberAttribute()]
        [EdmRelationshipNavigationPropertyAttribute("Cdts.Framework", "OperationUserLog", "Operation")]
        public Operation Operation
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationUserLog", "Operation").Value;
            }
            set
            {
                ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationUserLog", "Operation").Value = value;
            }
        }
        /// <summary>
        /// 操作
        /// </summary>
        [BrowsableAttribute(false)]
        [DataMemberAttribute()]
        public EntityReference<Operation> OperationReference
        {
            get
            {
                return ((IEntityWithRelationships)this).RelationshipManager.GetRelatedReference<Operation>("Cdts.Framework.OperationUserLog", "Operation");
            }
            set
            {
                if ((value != null))
                {
                    ((IEntityWithRelationships)this).RelationshipManager.InitializeRelatedReference<Operation>("Cdts.Framework.OperationUserLog", "Operation", value);
                }
            }
        }

        IOperation IUserLog.Operation
        {
            get
            {
                if (this.EntityState == System.Data.EntityState.Modified
                    || this.EntityState == System.Data.EntityState.Unchanged)
                {
                    if (!this.OperationReference.IsLoaded)
                    {
                        this.OperationReference.Load();
                    }
                }
                return this.Operation;
            }
            set
            {
                this.Operation = (Operation)value;
            }
        }
    }

}

