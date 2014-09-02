using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 获取对象
    /// </summary>
    public interface IGetObject
    {
        /// <summary>
        /// 映射类型字典
        /// </summary>
        Dictionary<Type, Type> MappingTypes { get; }
        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="id"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        object GetObject(string keyName, object id, Type objectType);
        /// <summary>
        /// 创建对象
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns></returns>
        object CreateObject(Type objectType);
    }
}
