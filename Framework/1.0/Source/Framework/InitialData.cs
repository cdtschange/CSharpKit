using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial class InitialData
    {
        private IBusinessModuleManager businessModuleManager = ManagerFactory.Create<IBusinessModuleManager>();
        private IResourceTypeManager resourceTypeManager = ManagerFactory.Create<IResourceTypeManager>();
        private IOperationManager operationManager = ManagerFactory.Create<IOperationManager>();

        public void Init()
        {
            #region 初始化配置
            
            IFrameworkSettingManager frameworkSettingManager = ManagerFactory.Create<IFrameworkSettingManager>();
            IFrameworkSetting frameworkSetting = ModelFactory.Create<IFrameworkSetting>();
            frameworkSetting.Id = new Guid("E9D3BAC7-C5E4-43C1-A5FA-543C2201C660");
            frameworkSetting.BusinessModuleCode = "1";
            frameworkSetting.AdministratorCode = "admin";
            frameworkSetting.AdministratorsRoleCode = "1";
            frameworkSetting.EveryoneRoleCode = "2";
            frameworkSetting.Version = "1.0";
            frameworkSettingManager.Create(frameworkSetting);
            #endregion
            #region 初始化模块
            IBusinessModule module0 = ModelFactory.Create<IBusinessModule>();
            module0.Code = "1";
            module0.Name = "业务框架";
            module0.Description = "业务框架";
            module0.Id = new Guid("f4423556-ec25-49ba-9e06-29d2d85e7bb5");
            businessModuleManager.Create(module0);
            #endregion
            #region 初始化资源类别
            IResourceType resourceType0 = ModelFactory.Create<IResourceType>();
            resourceType0.BusinessModule = module0;
            resourceType0.Id = new Guid("3cef1546-9061-4413-a306-5afb4fc1e812");
            resourceType0.Name = "业务框架基本资源类别";
            resourceType0.Parent = null;
            resourceType0.Code = "1_1";
            resourceType0.Description = "业务框架基本资源类别";
            resourceTypeManager.Create(resourceType0);

            IResourceType resourceType1 = ModelFactory.Create<IResourceType>();
            resourceType1.BusinessModule = module0;
            resourceType1.Id = new Guid("bd1285c0-d56e-4720-ba61-487b85375625");
            resourceType1.Name = "角色资源类别";
            resourceType1.Parent = null;
            resourceType1.Code = "1_2";
            resourceType1.Description = "角色资源类别";
            resourceTypeManager.Create(resourceType1);

            IResourceType resourceType2 = ModelFactory.Create<IResourceType>();
            resourceType2.BusinessModule = module0;
            resourceType2.Id = new Guid("98305faf-6264-453f-99e0-b8d5db930c4a");
            resourceType2.Name = "用户资源类别";
            resourceType2.Parent = null;
            resourceType2.Code = "1_3";
            resourceType2.Description = "用户资源类别";
            resourceTypeManager.Create(resourceType2);

            #endregion
            #region 初始化操作
            IOperation operation0 = ModelFactory.Create<IOperation>();
            operation0.ResourceType = resourceType2;
            operation0.BusinessModule = module0;
            operation0.Id = new Guid("916bc44c-00fe-4942-88bf-2b2ed30bf418");
            operation0.Name = "创建用户";
            operation0.Description = "创建用户";
            operation0.Code = "1_3.1";
            operationManager.Create(operation0);
            IOperation operation1 = ModelFactory.Create<IOperation>();
            operation1.ResourceType = resourceType2;
            operation1.BusinessModule = module0;
            operation1.Id = new Guid("56eec5f4-65ab-4c7c-999d-d028368ce813");
            operation1.Name = "修改用户";
            operation1.Description = "修改用户";
            operation1.Code = "1_3.2";
            operationManager.Create(operation1);
            IOperation operation2 = ModelFactory.Create<IOperation>();
            operation2.ResourceType = resourceType2;
            operation2.BusinessModule = module0;
            operation2.Id = new Guid("8220d3bd-51dc-4515-961d-f1772712cb2c");
            operation2.Name = "删除用户";
            operation2.Description = "删除用户";
            operation2.Code = "1_3.3";
            operationManager.Create(operation2);
            IOperation operation3 = ModelFactory.Create<IOperation>();
            operation3.ResourceType = resourceType2;
            operation3.BusinessModule = module0;
            operation3.Id = new Guid("6f35ee3e-2e22-4f62-a048-4eb5ce403cfe");
            operation3.Name = "查看用户";
            operation3.Description = "查看用户";
            operation3.Code = "1_3.4";
            operationManager.Create(operation3);
            IOperation operation4 = ModelFactory.Create<IOperation>();
            operation4.ResourceType = resourceType2;
            operation4.BusinessModule = module0;
            operation4.Id = new Guid("75184fc4-1bed-44ad-877c-447d7a57d3dd");
            operation4.Name = "用户分配角色";
            operation4.Description = "用户分配角色";
            operation4.Code = "1_3.5";
            operationManager.Create(operation4);
            IOperation operation5 = ModelFactory.Create<IOperation>();
            operation5.ResourceType = resourceType2;
            operation5.BusinessModule = module0;
            operation5.Id = new Guid("6bac5ff6-faa4-4063-929a-168634d207e1");
            operation5.Name = "用户移除角色";
            operation5.Description = "用户移除角色";
            operation5.Code = "1_3.6";
            operationManager.Create(operation5);
            IOperation operation6 = ModelFactory.Create<IOperation>();
            operation6.ResourceType = resourceType2;
            operation6.BusinessModule = module0;
            operation6.Id = new Guid("56684375-80ce-4025-b3a9-ec0a9c99b9a7");
            operation6.Name = "重设密码";
            operation6.Description = "重设密码";
            operation6.Code = "1_3.7";
            operationManager.Create(operation6);
            IOperation operation7 = ModelFactory.Create<IOperation>();
            operation7.ResourceType = resourceType2;
            operation7.BusinessModule = module0;
            operation7.Id = new Guid("46751003-5d00-4f90-9ece-a83f03f92491");
            operation7.Name = "用户修改密码";
            operation7.Description = "用户修改密码";
            operation7.Code = "1_3.8";
            operationManager.Create(operation7);
            IOperation operation8 = ModelFactory.Create<IOperation>();
            operation8.ResourceType = resourceType2;
            operation8.BusinessModule = module0;
            operation8.Id = new Guid("ef82a3f4-83bc-47c6-a89b-4c9ebf86b0c9");
            operation8.Name = "手工验证用户";
            operation8.Description = "手工验证用户";
            operation8.Code = "1_3.9";
            operationManager.Create(operation8);
            IOperation operation9 = ModelFactory.Create<IOperation>();
            operation9.ResourceType = resourceType1;
            operation9.BusinessModule = module0;
            operation9.Id = new Guid("20e8ea00-36ce-420a-a955-24c1e023290d");
            operation9.Name = "创建角色";
            operation9.Description = "创建角色";
            operation9.Code = "1_2.1";
            operationManager.Create(operation9);
            IOperation operation10 = ModelFactory.Create<IOperation>();
            operation10.ResourceType = resourceType1;
            operation10.BusinessModule = module0;
            operation10.Id = new Guid("cd6a9e61-361c-4547-a204-90bca22f7cb7");
            operation10.Name = "修改角色";
            operation10.Description = "修改角色";
            operation10.Code = "1_2.2";
            operationManager.Create(operation10);
            IOperation operation11 = ModelFactory.Create<IOperation>();
            operation11.ResourceType = resourceType1;
            operation11.BusinessModule = module0;
            operation11.Id = new Guid("2fca4686-97e6-4210-9fa2-a454f9619509");
            operation11.Name = "删除角色";
            operation11.Description = "删除角色";
            operation11.Code = "1_2.3";
            operationManager.Create(operation11);
            IOperation operation12 = ModelFactory.Create<IOperation>();
            operation12.ResourceType = resourceType1;
            operation12.BusinessModule = module0;
            operation12.Id = new Guid("d4509179-617c-48b4-96b5-eb4374f3efa7");
            operation12.Name = "查看角色";
            operation12.Description = "查看角色";
            operation12.Code = "1_2.4";
            operationManager.Create(operation12);
            IOperation operation13 = ModelFactory.Create<IOperation>();
            operation13.ResourceType = resourceType1;
            operation13.BusinessModule = module0;
            operation13.Id = new Guid("978379ee-7b61-49f7-b1a9-230cab4c9e80");
            operation13.Name = "角色分配用户";
            operation13.Description = "角色分配用户";
            operation13.Code = "1_2.5";
            operationManager.Create(operation13);
            IOperation operation14 = ModelFactory.Create<IOperation>();
            operation14.ResourceType = resourceType1;
            operation14.BusinessModule = module0;
            operation14.Id = new Guid("bb6b2b9d-e936-402a-a819-8b6fddb96aa7");
            operation14.Name = "角色移除用户";
            operation14.Description = "角色移除用户";
            operation14.Code = "1_2.6";
            operationManager.Create(operation14);
            IOperation operation15 = ModelFactory.Create<IOperation>();
            operation15.ResourceType = resourceType1;
            operation15.BusinessModule = module0;
            operation15.Id = new Guid("e95b8f67-ee57-483b-8c37-1e8787ee8654");
            operation15.Name = "角色分配许可";
            operation15.Description = "角色分配许可";
            operation15.Code = "1_2.7";
            operationManager.Create(operation15);
            IOperation operation16 = ModelFactory.Create<IOperation>();
            operation16.ResourceType = resourceType1;
            operation16.BusinessModule = module0;
            operation16.Id = new Guid("dcff5d1d-25c5-49ca-b84e-202518db3689");
            operation16.Name = "角色移除许可";
            operation16.Description = "角色移除许可";
            operation16.Code = "1_2.8";
            operationManager.Create(operation16);
            operationManager.SaveChanges();
            #endregion
        }
    }
}
