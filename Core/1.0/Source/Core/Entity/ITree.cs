using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 树结构
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public interface ITree<TEntity>
    {
        /// <summary>
        /// 编码
        /// </summary>
        string Code { get; set; }
        /// <summary>
        /// 父类
        /// </summary>
        TEntity Parent { get; set; }
        /// <summary>
        /// 子类集
        /// </summary>
        ICollection<TEntity> Children { get; set; }
    }
}
