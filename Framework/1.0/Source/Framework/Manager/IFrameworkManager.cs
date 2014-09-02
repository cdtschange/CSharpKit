using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.Core;

namespace Cdts.Framework
{
    public interface IFrameworkManager<TEntity> : IManager<TEntity, Guid>
       where TEntity : IPKey<Guid>
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        IUser CurrentUser { get; set; }
    }
}
