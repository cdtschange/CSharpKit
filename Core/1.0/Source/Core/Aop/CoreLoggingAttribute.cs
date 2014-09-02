using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using PostSharp.Aspects;

namespace Cdts.Core
{
    /// <summary>
    /// 日志记录
    /// </summary>
    [Serializable]
    public class CoreLoggingAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            if (!Configuration.MethodEnableLogging)
            {
                return;
            }
            try
            {
                Environment.SetEnvironmentVariable("MYDATE", DateTime.Now.ToString("yyyyMMdd"));
                LogEntry log = new LogEntry();
                log.EventId = 1;
                log.Priority = 1;
                log.Title = string.Format("进入方法：{0}", args.Method);
                StringBuilder arguments = new StringBuilder();
                if (args != null && args.Arguments.Count > 0)
                {
                    arguments.AppendLine("调用参数：");
                    for (int i = 0; i < args.Arguments.Count; i++)
                    {
                        arguments.AppendLine(string.Format("参数{0}:{1}", i + 1, args.Arguments[i]));
                    }
                }
                log.Message = arguments.ToString();
                Logger.Writer.Write(log, "General");
            }
            catch
            {
            }
            args.MethodExecutionTag = DateTime.Now;
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            if (!Configuration.MethodEnableLogging)
            {
                return;
            }
            DateTime begin = (DateTime)args.MethodExecutionTag;
            TimeSpan timeSpan = DateTime.Now - begin;
            double t = timeSpan.TotalMilliseconds;
            try
            {
                LogEntry log = new LogEntry();
                log.EventId = 1;
                log.Priority = 1;
                log.Title = string.Format("离开方法：{0}\r\n耗时：{1}\r\n", args.Method, t);
                StringBuilder message = new StringBuilder();
                if (args.ReturnValue != null)
                {
                    message.AppendLine(string.Format("返回值：{0}", args.ReturnValue));
                }
                if (args.Exception != null)
                {
                    GetExceptionMessage(args.Exception, ref message);
                }
                log.Message = message.ToString();
                Logger.Writer.Write(log, "General");
            }
            catch
            {
            }
        }
        private void GetExceptionMessage(Exception ex, ref StringBuilder message)
        {
            if (message == null)
                return;
            message.AppendLine("错误信息：");
            message.AppendLine(ex.Message);
            message.AppendLine("堆栈跟踪：");
            message.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                GetExceptionMessage(ex.InnerException, ref message);
            }

        }
        public override void OnException(MethodExecutionArgs args)
        {
            if (!Configuration.ExceptionEnableLogging)
            {
                args.FlowBehavior = FlowBehavior.RethrowException;
                return;
            }
            try
            {
                Environment.SetEnvironmentVariable("MYDATE", DateTime.Now.ToString("yyyyMMdd"));
                LogEntry log = new LogEntry();
                log.EventId = 1;
                log.Priority = 1;
                log.Title = string.Format("错误异常：{0}", args.Exception);
                StringBuilder message = new StringBuilder();
                GetExceptionMessage(args.Exception, ref message);
                log.Message = message.ToString();
                Logger.Writer.Write(log);
            }
            catch
            {
            }
            args.FlowBehavior = FlowBehavior.RethrowException;
        }
    }
}
