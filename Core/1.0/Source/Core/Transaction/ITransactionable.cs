using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 可支持事务
    /// </summary>
    public interface ITransactionable
    {
        /// <summary>
        /// 开始事务
        /// </summary>
        /// <returns>
        /// 返回<seealso cref="IDataTransaction"/>的实例。
        /// </returns>
        IDataTransaction BeginTransaction();
    }
}
