using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Framework
{
    public partial interface IUserManager
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <param name="ipAddress">IP地址</param>
        /// <returns>返回登录结果</returns>
        IUser Login(string account, string password, string ipAddress);
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">用户</param>
        void Register(IUser user);
        /// <summary>
        /// 判断用户账号是否已存在
        /// </summary>
        bool IsAccountExisted(string account);
        /// <summary>
        /// 通过邮箱获取用户
        /// </summary>
        /// <param name="email">邮件地址</param>
        /// <returns>返回用户</returns>
        IUser LoadByEmail(string email);
        /// <summary>
        /// 通过昵称获取用户
        /// </summary>
        /// <param name="nick">昵称</param>
        /// <returns>返回用户</returns>
        IUser LoadByNick(string nick);
        /// <summary>
        /// 创建用户注册验证并发送邮件
        /// </summary>
        /// <param name="device">设备</param>
        void CreateRegisterValidationAndSendEmail(string device);
        /// <summary>
        /// 创建重设密码验证并发送邮件
        /// </summary>
        /// <param name="device">设备</param>
        void CreateRetrievePasswordValidationAndSendEmail(string device);
        /// <summary>
        /// 注册用户验证
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="validateCode">验证码</param>
        /// <returns>验证结果</returns>
        bool RegisterDeviceValidate(string device, string validateCode);
        /// <summary>
        /// 用户找回密码验证
        /// </summary>
        /// <param name="device">设备</param>
        /// <param name="validateCode">验证码</param>
        /// <param name="isDelete">是否删除验证</param>
        /// <returns>验证结果</returns>
        IUser RetrievePasswordDeviceValidate(string device, string validateCode, bool isDelete);
        /// <summary>
        /// 修改当前用户密码
        /// </summary>
        /// <param name="oldpwd">旧密码</param>
        /// <param name="newpwd">新密码</param>
        void ChangePassword(string oldpwd, string newpwd);
        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <param name="newpwd">新密码</param>
        void ResetPassword(Guid id, string newpwd);
        /// <summary>
        /// 分配角色
        /// </summary>
        void AssignRole(IUser entity, List<IRole> roles);
        /// <summary>
        /// 移除角色
        /// </summary>
        void RemoveRole(IUser entity, List<IRole> roles);
    }
}
