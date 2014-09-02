using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 元数据管理接口
    /// </summary>
    public interface IMetadataManager
    {
        /// <summary>
        /// 根据类名获取类的元数据。
        /// </summary>
        /// <param name="typeName">类名</param>
        /// <returns>返回对应的元数据。<seealso cref="TypeMetadata"/></returns>
        TypeMetadata GetMetadata(string typeName);
        /// <summary>
        /// 填充类元数据
        /// </summary>
        /// <remarks>
        /// 主要功能就是填充类元数据的属性。
        /// </remarks>
        /// <param name="metadata">类的元数据。<seealso cref="TypeMetadata"/></param>
        void FillMetadata(TypeMetadata metadata);
    }
}
