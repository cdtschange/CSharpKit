using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Web.Mvc;

namespace Cdts.Framework.Web.Mvc
{
    public class FrameworkModel : ModelBase
    {
        /// <summary>
        /// 用户邮件
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户注册时间
        /// </summary>
        public DateTime? RegisterTime { get; set; }
    }
}
