using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Utility.Mail
{
    /// <summary>
    /// 邮箱模型
    /// </summary>
    public class MailModel
    {
        public int Id { get; set; }
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 运营商
        /// </summary>
        public MailCarrier Carrier { get; set; }

        /// <summary>
        /// 邮箱全名
        /// </summary>
        public string FullAccount
        {
            get { return this.Account + "@" + this.Carrier.MailDomain; }
        }
    }

    /// <summary>
    /// 运营商
    /// </summary>
    public class MailCarrier
    {
        /// <summary>
        /// @163.com,@qq.com ...
        /// </summary>
        public string MailDomain { get; set; }
        public string LoginUrl { get; set; }
        public bool IsPOP3 { get; set; }
        public bool IsNeedActivation { get; set; }

        public string POP3IP { get; set; }
        public short POP3Port { get; set; }
        public int POP3SSL { get; set; }

        public string SMTPIP { get; set; }
        public short SMTPPort { get; set; }
        public int SMTPSSL { get; set; }
    }

}
