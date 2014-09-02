using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public sealed class Operations
    {
        private static IOperationManager manager = ManagerFactory.Create<IOperationManager>();

        /// <summary>
        /// 创建用户
        /// </summary>
        public static IOperation CreateUser
        {
            get
            {
                return manager.LoadByCode("1_3.1");
            }
        }
        /// <summary>
        /// 更新用户
        /// </summary>
        public static IOperation UpdateUser
        {
            get
            {
                return manager.LoadByCode("1_3.2");
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        public static IOperation DeleteUser
        {
            get
            {
                return manager.LoadByCode("1_3.3");
            }
        }
        /// <summary>
        /// 查看用户
        /// </summary>
        public static IOperation ViewUser
        {
            get
            {
                return manager.LoadByCode("1_3.4");
            }
        }
        /// <summary>
        /// 给用户分配角色
        /// </summary>
        public static IOperation UserAssignRole
        {
            get
            {
                return manager.LoadByCode("1_3.5");
            }
        }
        /// <summary>
        /// 给用户移除角色
        /// </summary>
        public static IOperation UserRemoveRole
        {
            get
            {
                return manager.LoadByCode("1_3.6");
            }
        }
        /// <summary>
        /// 重设密码
        /// </summary>
        public static IOperation ResetPassword
        {
            get
            {
                return manager.LoadByCode("1_3.7");
            }
        }
        /// <summary>
        /// 用户重设密码
        /// </summary>
        public static IOperation UserResetPassword
        {
            get
            {
                return manager.LoadByCode("1_3.8");
            }
        }
        /// <summary>
        /// 手工验证用户
        /// </summary>
        public static IOperation ManualValidateUser
        {
            get
            {
                return manager.LoadByCode("1_3.9");
            }
        }
        /// <summary>
        /// 创建角色
        /// </summary>
        public static IOperation CreateRole
        {
            get
            {
                return manager.LoadByCode("1_2.1");
            }
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        public static IOperation UpdateRole
        {
            get
            {
                return manager.LoadByCode("1_2.2");
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        public static IOperation DeleteRole
        {
            get
            {
                return manager.LoadByCode("1_2.3");
            }
        }
        /// <summary>
        /// 查看角色
        /// </summary>
        public static IOperation ViewRole
        {
            get
            {
                return manager.LoadByCode("1_2.4");
            }
        }
        /// <summary>
        /// 给角色分配用户
        /// </summary>
        public static IOperation RoleAssignUser
        {
            get
            {
                return manager.LoadByCode("1_2.5");
            }
        }
        /// <summary>
        /// 从角色移除用户
        /// </summary>
        public static IOperation RoleRemoveUser
        {
            get
            {
                return manager.LoadByCode("1_2.6");
            }
        }
        /// <summary>
        /// 给角色分配许可
        /// </summary>
        public static IOperation RoleAssignPermission
        {
            get
            {
                return manager.LoadByCode("1_2.7");
            }
        }
        /// <summary>
        /// 从角色中移除许可
        /// </summary>
        public static IOperation RoleRemovePermission
        {
            get
            {
                return manager.LoadByCode("1_2.8");
            }
        }

        public static Dictionary<string, Dictionary<string, string[]>> OperationFields { get; set; }

        static Operations()
        {
            OperationFields = new Dictionary<string, Dictionary<string, string[]>>();

            OperationFields.Add("1_3.1", //创建用户
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Code","Name","Description","Email","EmailValidated","Password","Mobile","MobileValidated","Nick"}}                
				});

            OperationFields.Add("1_3.2", //修改用户
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Code","Name","Description","Email","EmailValidated","Mobile","MobileValidated","Nick"}}                
				});

            OperationFields.Add("1_3.3", //删除用户
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id"}}                
				});
            OperationFields.Add("1_3.4", //查看用户
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Code","Name","Description","Email","EmailValidated","Mobile","MobileValidated","Nick"}}                
				});
            OperationFields.Add("1_3.5", //用户分配角色
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Roles"}}                
				});
            OperationFields.Add("1_3.6", //用户移除角色
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Roles"}}                
				});
            OperationFields.Add("1_3.7", //重设密码
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Password"}}                
				});
            OperationFields.Add("1_3.8", //用户修改密码
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","Description","Password"}}                
				});
            OperationFields.Add("1_3.9", //手工验证用户
                new Dictionary<string, string[]>() {
                {"User",new string[]{"Id","EmailValidated","MobileValidated"}}                
				});
            OperationFields.Add("1_2.1", //创建角色
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Code","Name","Description","Parent"}}                
				});
            OperationFields.Add("1_2.2", //修改角色
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Code","Name","Description","Parent"}}                
				});
            OperationFields.Add("1_2.3", //删除角色
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id"}}                
				});
            OperationFields.Add("1_2.4", //查看角色
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Code","Name","Description","Parent"}}                
				});
            OperationFields.Add("1_2.5", //角色分配用户
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Users"}}                
				});
            OperationFields.Add("1_2.6", //角色移除用户
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Users"}}                
				});
            OperationFields.Add("1_2.7", //角色分配许可
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Permission"}}                
				});
            OperationFields.Add("1_2.8", //角色移除许可
                new Dictionary<string, string[]>() {
                {"Role",new string[]{"Id","Permission"}}                
				});

        }

    }
}
