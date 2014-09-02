using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 基于<seealso cref="IDataContext"/>的元数据管理基类
    /// </summary>
    public abstract class BasedDataContextMetadataManager<TPKeyType> : IMetadataManager
        where TPKeyType : struct
    {
        /// <summary>
        /// 上下文
        /// </summary>
        protected abstract IDataContext<TPKeyType> DataContext { get; }

        #region IMetadataManager 成员

        /// <summary>
        /// 根据类名获取类的元数据。
        /// </summary>
        /// <param name="typeName">类名</param>
        /// <returns>返回对应的元数据。<seealso cref="TypeMetadata"/></returns>
        public virtual TypeMetadata GetMetadata(string typeName)
        {
            return DataContext.GetMetadata(typeName);
        }
        /// <summary>
        /// 填充类元数据
        /// </summary>
        /// <remarks>
        /// 主要功能就是填充类元数据的属性。
        /// </remarks>
        /// <param name="metadata">类的元数据。<seealso cref="TypeMetadata"/></param>
        public virtual void FillMetadata(TypeMetadata metadata)
        {
            DataContext.FillMetadata(metadata);
        }

        #endregion
    }
}
