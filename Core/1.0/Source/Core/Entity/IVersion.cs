using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 可分版本
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface IVersion<TEntity> : ILogicalDeleted
    {
        /// <summary>
        /// 版本号
        /// </summary>
        int Version { get; set; }
        /// <summary>
        /// 原始版本
        /// </summary>
        TEntity Original { get; set; }
    }
}
