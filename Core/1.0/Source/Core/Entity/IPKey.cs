using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 主键
    /// </summary>
    /// <typeparam name="TPKeyType">主键类型</typeparam>
    public interface IPKey<TPKeyType> where TPKeyType : struct
    {
        /// <summary>
        /// 主键
        /// </summary>
        TPKeyType Id { get; set; }
    }
}
