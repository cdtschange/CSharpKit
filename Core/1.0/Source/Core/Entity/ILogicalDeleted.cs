using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 可逻辑删除
    /// </summary>
    public interface ILogicalDeleted
    {
        /// <summary>
        /// 无效性
        /// </summary>
        bool Invalid { get; set; }
    }
}
