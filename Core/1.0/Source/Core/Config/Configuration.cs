using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace Cdts.Core
{
    /// <summary>
    /// 配置
    /// </summary>
    public static class Configuration
    {
        private static bool methodEnableLogging = false;
        private static bool exceptionEnableLogging = false;
        private static bool enableTransaction = true;
        static Configuration()
        {
            string enable = ConfigurationManager.AppSettings["MethodEnableLogging"];
            if (string.IsNullOrEmpty(enable) || enable.ToLower() != "true")
            {
                methodEnableLogging = false;
            }
            else
            {
                methodEnableLogging = true;
            }
            enable = ConfigurationManager.AppSettings["ExceptionEnableLogging"];
            if (string.IsNullOrEmpty(enable) || enable.ToLower() != "true")
            {
                exceptionEnableLogging = false;
            }
            else
            {
                exceptionEnableLogging = true;
            }
            enable = System.Configuration.ConfigurationManager.AppSettings["EnableTransaction"];
            if (string.IsNullOrEmpty(enable) || enable.ToLower() != "false")
            {
                enableTransaction = true;
            }
            else
            {
                enableTransaction = false;
            }
        }
        /// <summary>
        /// 方法日志记录
        /// </summary>
        public static bool MethodEnableLogging
        {
            get
            {
                return methodEnableLogging;
            }
        }
        /// <summary>
        /// 异常日志记录
        /// </summary>
        public static bool ExceptionEnableLogging
        {
            get
            {
                return exceptionEnableLogging;
            }
        }
        /// <summary>
        /// 事务启动
        /// </summary>
        public static bool EnableTransaction
        {
            get
            {
                return enableTransaction;
            }
        }
    }
}
