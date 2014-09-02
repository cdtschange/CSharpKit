using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 数据事务
    /// </summary>
    [CoreLogging]
    public class DataContextTransaction<TPKeyType> : IDataTransaction
        where TPKeyType : struct
    {
        private IDataContext<TPKeyType> dataContext;
        private Guid id;
        private int beginCount = 0, commitCount = 0, rollbackCount = 0, disposeCount = 0;
        private bool inTransaction;
        /// <summary>
        /// 构造事务
        /// </summary>
        /// <param name="IDataContext">上下文</param>
        public DataContextTransaction(IDataContext<TPKeyType> dataContext)
        {
            this.dataContext = dataContext;
            id = Guid.NewGuid();
            beginCount = 0;
            commitCount = 0;
            rollbackCount = 0;
            disposeCount = 0;
            inTransaction = false;
        }
        #region IDataTransaction 成员
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
            if (!inTransaction)
            {
                dataContext.BeginTransaction(id);
            }
            inTransaction = true;
            beginCount++;
        }
        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            commitCount++;
            Deal();
        }
        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            rollbackCount++;
            Deal();
        }
        /// <summary>
        /// 销毁事务
        /// </summary>
        public void Dispose()
        {
            disposeCount++;

            if (disposeCount == beginCount)
            {
                dataContext.Dispose(id);

                beginCount = 0; disposeCount = 0;
                inTransaction = false;
            }
        }

        #endregion

        /// <summary>
        /// 处理事务
        /// </summary>
        private void Deal()
        {
            if (commitCount + rollbackCount < beginCount)
            {
                return;
            }
            else if (commitCount + rollbackCount == beginCount) // 执行事务
            {
                if (rollbackCount == 0)
                {
                    dataContext.Commit(id);
                }
                else
                {
                    dataContext.Rollback(id);
                }
                commitCount = 0; rollbackCount = 0;
            }
        }
    }
}
