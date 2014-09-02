using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Web.Mvc
{
    /// <summary>
    /// 模型基类
    /// </summary>
    public class ModelBase
    {
        public ModelBase()
        {
            HasErrors = false;
            UserName = "游客";
            IsAjaxRequest = false;
        }
        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsAuthenticated { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 客户端IP
        /// </summary>
        public string ClientIp { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 是否有错误
        /// </summary>
        public bool HasErrors { get; set; }
        /// <summary>
        /// 用户是否验证
        /// </summary>
        public bool Validated { get; set; }
        /// <summary>
        /// 语言
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// 请求开始时间
        /// </summary>
        public DateTime RequestBeginTime { get; set; }
        /// <summary>
        /// 请求结束时间
        /// </summary>
        public DateTime RequestEndTime { get; set; }
        /// <summary>
        /// 是否是Ajax请求
        /// </summary>
        public bool IsAjaxRequest { get; set; }
        /// <summary>
        /// 匿名Id
        /// </summary>
        public Guid? AnonymousId { get; set; }
    }
}
