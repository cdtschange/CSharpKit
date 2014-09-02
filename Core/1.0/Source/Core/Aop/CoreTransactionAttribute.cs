using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostSharp.Aspects;

namespace Cdts.Core
{
    /// <summary>
    /// 事务
    /// </summary>
    [Serializable]
    public class CoreTransactionAttribute : OnMethodBoundaryAspect
    {
        public CoreTransactionAttribute()
        {
        }

        public override void OnEntry(MethodExecutionArgs eventArgs)
        {
            if (!Configuration.EnableTransaction)
            {
                return;
            }
            ITransactionable instance = eventArgs.Instance as ITransactionable;
            IDataTransaction tran = null;
            if (instance != null)
            {
                tran = instance.BeginTransaction();

            }
            eventArgs.MethodExecutionTag = tran;

        }

        public override void OnSuccess(MethodExecutionArgs eventArgs)
        {
            if (!Configuration.EnableTransaction)
            {
                return;
            }
            ITransactionable instance = eventArgs.Instance as ITransactionable;
            if (instance != null)
            {
                IDataTransaction tran = eventArgs.MethodExecutionTag as IDataTransaction;
                if (tran != null)
                {
                    tran.Commit();
                }
            }
        }

        public override void OnException(MethodExecutionArgs eventArgs)
        {
            if (!Configuration.EnableTransaction)
            {
                eventArgs.FlowBehavior = FlowBehavior.RethrowException;
                return;
            }
            ITransactionable instance = eventArgs.Instance as ITransactionable;
            if (instance != null)
            {
                IDataTransaction tran = eventArgs.MethodExecutionTag as IDataTransaction;
                if (tran != null)
                {
                    tran.Rollback();
                }
            }
            throw eventArgs.Exception;
        }

        public override void OnExit(MethodExecutionArgs eventArgs)
        {
            if (!Configuration.EnableTransaction)
            {
                return;
            }
            ITransactionable instance = eventArgs.Instance as ITransactionable;
            if (instance != null)
            {
                IDataTransaction tran = eventArgs.MethodExecutionTag as IDataTransaction;
                if (tran != null)
                {
                    tran.Dispose();
                }
            }
        }
    }
}
