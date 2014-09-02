using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 可排序
    /// </summary>
    public interface IOrdered
    {
        /// <summary>
        /// 序号
        /// </summary>
        int Order { get; set; }
    }
}
