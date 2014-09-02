using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace Cdts.Core
{
    /// <summary>
    /// TransactionScope数据事务
    /// </summary>
    public class DataTransactionScope : IDataTransaction
    {
        Guid id = Guid.NewGuid();
        TransactionScope scope = null;
        /// <summary>
        /// 事务Id
        /// </summary>
        public Guid Id
        {
            get { return id; }
        }
        /// <summary>
        /// 开始事务
        /// </summary>
        public void Begin()
        {
            scope = new TransactionScope();
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (scope != null)
            {
                scope.Complete();
            }
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {

        }
        /// <summary>
        /// 销毁事务
        /// </summary>
        public void Dispose()
        {
            if (scope != null)
            {
                scope.Dispose();
            }
        }
    }
}
