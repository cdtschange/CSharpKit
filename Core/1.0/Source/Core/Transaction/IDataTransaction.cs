using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 数据事务接口
    /// </summary>
    public interface IDataTransaction : IDisposable
    {
        /// <summary>
        /// 事务Id
        /// </summary>
        Guid Id { get; }
        /// <summary>
        /// 开始事务
        /// </summary>
        void Begin();
        /// <summary>
        /// 提交事务
        /// </summary>
        void Commit();
        /// <summary>
        /// 回滚事务
        /// </summary>
        void Rollback();
    }
}
