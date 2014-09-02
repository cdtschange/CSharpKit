using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    /// <summary>
    /// 表单控件类型
    /// </summary>
    public enum ControlType
    {

    }
    /// <summary>
    /// 属性类型
    /// </summary>
    public enum PropertyType
    {
        /// <summary>
        /// 标量属性
        /// </summary>
        Primitive,
        /// <summary>
        /// 导航属性
        /// </summary>
        Navigation,
        /// <summary>
        /// 复杂属性
        /// </summary>
        Complex,
        /// <summary>
        /// 类
        /// </summary>
        Type
    }
    public class TypeMetadata
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 数据表中对应的字段名称，如果属性类型是Type，则对应的数据表名
        /// </summary>
        public string MappedName { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }
        /// <summary>
        /// 数据表中对应的类型
        /// </summary>
        public string MappedType { get; set; }
        /// <summary>
        /// 所有者
        /// </summary>
        public TypeMetadata Owner { get; set; }
        /// <summary>
        /// 属性类型
        /// </summary>
        public PropertyType PropertyType { get; set; }
        /// <summary>
        /// 控件类型
        /// </summary>
        public ControlType ControlType { get; set; }

        List<TypeMetadata> properties = new List<TypeMetadata>();
        /// <summary>
        /// 属性列表
        /// </summary>
        public IEnumerable<TypeMetadata> Properties
        {
            get
            {
                return properties;
            }
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="property"></param>
        public void AddProperty(TypeMetadata property)
        {
            if (property != null && !properties.Contains(property))
            {
                property.Owner = this;
                properties.Add(property);
            }
        }
    }
}
