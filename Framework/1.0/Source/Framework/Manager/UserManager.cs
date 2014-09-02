using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;
using Cdts.Utility;
using System.Configuration;
using System.Web;

namespace Cdts.Framework
{
    [CoreLogging]
    public partial class UserManager : IUserManager
    {
        IUserLogManager userLogManager;
        internal protected virtual IUserLogManager UserLogManager
        {
            get
            {
                if (userLogManager == null)
                    userLogManager = ManagerFactory.Create<IUserLogManager>();
                return userLogManager;
            }
        }
        IDeviceValidationManager deviceValidationManager;
        internal protected virtual IDeviceValidationManager DeviceValidationManager
        {
            get
            {
                if (deviceValidationManager == null)
                    deviceValidationManager = ManagerFactory.Create<IDeviceValidationManager>();
                return deviceValidationManager;
            }
        }
        IRoleManager roleManager;
        internal protected virtual IRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = ManagerFactory.Create<IRoleManager>();
                return roleManager;
            }
        }


        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>返回登录用户</returns>
        [CoreTransaction]
        public IUser Login(string account, string password, string ipAddress)
        {
            IUser user = LoadByEmail(account);
            if (user != null)
            {
                IUserLog log = UserLogManager.NewEntity();
                log.Id = Guid.NewGuid();
                log.LogTime = DateTime.Now;
                log.ResourceId = user.Id.ToString();
                log.LogIp = ipAddress;
                // 密码加密 
                password = password.SHA256();
                if (user.Password.Equals(password)) // 登录成功
                {
                    user.LastLoginTime = user.CurrentLoginTime;
                    user.LastLoginIp = user.CurrentLoginIp;
                    user.CurrentLoginIp = ipAddress;
                    user.CurrentLoginTime = DateTime.Now;
                    Update(user);
                    log.Description = Resources.Resource("UserLoginSuccess", user.Email);
                    log.User = user;
                    CurrentUser = user;
                }
                else
                {
                    log.Description = Resources.Resource("UserLoginFailure", user.Email);
                    user = null;
                }
                UserLogManager.Create(log);
                SaveChanges();
            }
            return user;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">用户</param>
        [CoreTransaction]
        public void Register(IUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("User", Resources.Resource("UserIsNull"));
            }
            if (IsAccountExisted(user.Email)) // 用户Email已存在
            {
                throw new InvalidOperationException(Resources.Resource("UserEmailIsExisted", user.Email));
            }
            else
            {
                user.RegisterTime = DateTime.Now;
                Create(user);// 创建用户
                SaveChanges();
            }
        }

        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        [CoreTransaction]
        public void ChangePassword(string oldpwd, string newpwd)
        {
            oldpwd = oldpwd.SHA256();
            newpwd = newpwd.SHA256();

            IUser user = CurrentUser;
            if (user == null)
            {
                throw new ArgumentNullException("User", Resources.Resource("UserIsNull"));
            }
            if (user.Password != oldpwd)
            {
                throw new ArgumentException("OldPassword", Resources.Resource("OldPasswordError"));
            }
            if (oldpwd == newpwd)
            {
                return;
            }
            user.Password = newpwd;
            Update(user);
            SaveChanges();
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="newpwd">新密码</param>
        [CoreTransaction]
        public void ResetPassword(Guid id, string newpwd)
        {
            newpwd = newpwd.SHA256();
            IUser user = LoadById(id);
            if (user == null)
            {
                throw new ArgumentNullException("User", Resources.Resource("UserIsNull"));
            }
            user.Password = newpwd;
            Update(user);
            SaveChanges();
        }

        /// <summary>
        /// 检测账号是否已存在
        /// </summary>
        /// <param name="account">账号</param>
        /// <returns>检测结果</returns>
        public bool IsAccountExisted(string account)
        {
            return LoadByEmail(account) != null;
        }
        /// <summary>
        /// 通过邮箱获取用户
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <returns>返回用户</returns>
        public IUser LoadByEmail(string email)
        {
            var query = CreateQuery();
            return query.Where(u => u.Email == email).FirstOrDefault();
        }
        /// <summary>
        /// 通过昵称获取用户
        /// </summary>
        /// <param name="nick">昵称</param>
        /// <returns>返回用户</returns>
        public IUser LoadByNick(string nick)
        {
            var query = CreateQuery();
            return query.Where(u => u.Nick == nick).FirstOrDefault();
        }

        #region 用户验证

        /// <summary>
        /// 创建用户注册验证并发送邮件
        /// </summary>
        /// <param name="device">设备</param>
        [CoreTransaction]
        public void CreateRegisterValidationAndSendEmail(string device)
        {
            IUser user = LoadByEmail(device);
            if (user == null)
            {
                throw new InvalidOperationException(Resources.Resource("ValidateEmailError"));
            }
            IDeviceValidation validation = DeviceValidationManager.NewEntity();
            validation.Id = Guid.NewGuid();
            validation.Device = device;
            validation.DeviceName = "Email";
            validation.ValidateTime = DateTime.Now;
            int days;
            if (!int.TryParse(ConfigurationManager.AppSettings["RegisterValidationExpireDay"], out days))
            {
                days = 5;
            }
            validation.ExpireTime = DateTime.Now.AddDays(days);
            validation.ValidationType = (int)DeviceValidationType.Register;
            validation.User = user;
            validation.ValidationCode = Guid.NewGuid().ToString().Replace("-", "");

            this.DeviceValidationManager.Create(validation);
            this.DeviceValidationManager.SaveChanges();

            string subject = Resources.Resource("ValidateRegisterEmailSubject");
            string body = Resources.Resource("ValidateRegisterEmailBody",
                HttpUtility.UrlEncode(validation.Device), validation.ValidationCode);

            SendEmail(validation.Device, subject, body);

        }

        /// <summary>
        /// 创建重设密码验证并发送邮件
        /// </summary>
        /// <param name="device">设备</param>
        [CoreTransaction]
        public void CreateRetrievePasswordValidationAndSendEmail(string device)
        {
            IUser user = LoadByEmail(device);
            if (user == null)
            {
                throw new InvalidOperationException(Resources.Resource("EmailIsNotRegistered"));
            }
            IDeviceValidation validation = DeviceValidationManager.NewEntity();
            validation.Id = Guid.NewGuid();
            validation.Device = device;
            validation.DeviceName = "Email";
            validation.ValidateTime = DateTime.Now;
            int days;
            if (!int.TryParse(ConfigurationManager.AppSettings["RetrievePasswordExpireDay"], out days))
            {
                days = 5;
            }
            validation.ExpireTime = DateTime.Now.AddDays(days);
            validation.ValidationType = (int)DeviceValidationType.RetrievePassword;
            validation.User = user;
            validation.ValidationCode = Guid.NewGuid().ToString().Replace("-", "");

            this.DeviceValidationManager.Create(validation);
            this.DeviceValidationManager.SaveChanges();

            string subject = Resources.Resource("ValidateRetrievePasswordSubject");
            string body = Resources.Resource("ValidateRetrievePasswordBody",
                HttpUtility.UrlEncode(validation.Device), validation.ValidationCode);

            SendEmail(validation.Device, subject, body);

        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="emailTo">邮件地址</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        [CoreTransaction]
        public virtual void SendEmail(string emailTo, string subject, string body)
        {
            Utility.SendEMail(emailTo, subject, body);
        }


        /// <summary>
        /// 注册用户验证
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="validateCode">验证码</param>
        /// <returns>验证结果</returns>
        [CoreTransaction]
        public bool RegisterDeviceValidate(string device, string validateCode)
        {
            IList<IDeviceValidation> validationList = LoadDeviceValidate(device, "Email", validateCode, DeviceValidationType.Register);
            if (validationList != null && validationList.Count > 0)
            {
                validationList.Where(v => v.User != null).ToList()
                    .ForEach(v =>
                    {
                        v.User.EmailValidated = true;
                        Update(v.User);
                    });

                DeviceValidationManager.Delete(validationList);
                DeviceValidationManager.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 用户找回密码验证
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="validateCode">验证码</param>
        /// <param name="isDelete">是否删除验证</param>
        /// <returns>验证结果</returns>
        [CoreTransaction]
        public IUser RetrievePasswordDeviceValidate(string device, string validateCode, bool isDelete)
        {
            IList<IDeviceValidation> validationList = LoadDeviceValidate(device, "Email", validateCode, DeviceValidationType.RetrievePassword);
            if (!isDelete)
            {
                return validationList != null && validationList.Count > 0 ? validationList.First().User : null;
            }
            else
            {
                if (validationList != null && validationList.Count > 0)
                {
                    IUser user = validationList.First().User;
                    DeviceValidationManager.Delete(validationList);
                    DeviceValidationManager.SaveChanges();
                    return user;
                }
            }
            return null;
        }
        /// <summary>
        /// 获取用户验证
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="deviceName">设备名称</param>
        /// <param name="validateCode">验证码</param>
        /// <param name="deviceType">验证类型</param>
        /// <returns>返回所有匹配验证</returns>
        [CoreTransaction]
        private IList<IDeviceValidation> LoadDeviceValidate(string device, string deviceName, string validateCode, DeviceValidationType deviceType)
        {
            ClearExpiredValidation();// 清除过期验证
            //验证
            var query = DeviceValidationManager.CreateQuery();
            return query.Where(
                d => d.Device == device &&
                    d.DeviceName == deviceName &&
                    d.ValidationCode == validateCode &&
                    d.ValidationType == (int)deviceType).ToList();
        }

        /// <summary>
        /// 清除过期验证
        /// </summary>
        [CoreTransaction]
        private void ClearExpiredValidation()
        {
            var query = DeviceValidationManager.CreateQuery();
            IList<IDeviceValidation> clearList = query.Where(d => d.ExpireTime < DateTime.Now).ToList();
            if (clearList != null && clearList.Count > 0)
            {
                DeviceValidationManager.Delete(clearList);
            }
        }

        #endregion
        protected override void ValidateEntityForCreate(IUser entity)
        {
            base.ValidateEntityForCreate(entity);
            IUser user = LoadByNick(entity.Nick);
            if (user != null)
            {
                //ToDo:多语言
                throw new InvalidOperationException(string.Format("昵称为{0}的用户已经存在", entity.Nick));
            }
        }
        protected override void CreateEntity(IUser entity)
        {
            IRole everyoneRole = RoleManager.CreateQuery().Where(r => r.Code == FrameworkSetting.EveryoneRoleCode).FirstOrDefault();
            if (everyoneRole != null)
            {
                entity.Roles.Add(everyoneRole);
            }
            base.CreateEntity(entity);
        }
        protected override void ValidateEntityForUpdate(IUser entity)
        {
            base.ValidateEntityForUpdate(entity);
            IUser oldUser = LoadFromDatabase(entity.Id);
            if (oldUser.Nick != entity.Nick)
            {
                IUser user = LoadByNick(entity.Nick);
                if (user != null)
                {
                    //ToDo:多语言
                    throw new InvalidOperationException(string.Format("昵称为{0}的用户已经存在", entity.Nick));
                }
            }
        }
        /// <summary>
        /// 分配角色
        /// </summary>
        [CoreTransaction]
        public void AssignRole(IUser entity, List<IRole> roles)
        {
            roles.ForEach(r => entity.Roles.Add(r));
            Update(entity);
            SaveChanges();
        }
        /// <summary>
        /// 移除角色
        /// </summary>
        [CoreTransaction]
        public void RemoveRole(IUser entity, List<IRole> roles)
        {
            if (entity.Code == FrameworkSetting.AdministratorCode)
            {
                IRole adminRole = roles.Where(r => r.Code == FrameworkSetting.AdministratorsRoleCode).FirstOrDefault();
                if (adminRole == null)
                {//TODO:多语言
                    throw new InvalidOperationException("管理员用户必须要有管理员角色");
                }
                roles.Remove(adminRole);
            }
            IRole everyoneRole = entity.Roles.Where(r => r.Code == FrameworkSetting.EveryoneRoleCode).FirstOrDefault();
            if (everyoneRole == null)
            {//TODO:多语言
                throw new InvalidOperationException("用户必须要有Everyone角色");
            }
            roles.Remove(everyoneRole);
            roles.ForEach(r => entity.Roles.Remove(r));
            Update(entity);
            SaveChanges();
        }
    }
}
