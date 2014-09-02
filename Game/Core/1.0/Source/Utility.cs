using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CdtsGame.Core
{
    public static class Utility
    {
        /// <summary>
        /// 获取枚举类型的所有枚举值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns>枚举值列表</returns>
        public static object[] GetValues(Type enumType)
        {
            if (enumType.IsEnum == false)
            {
                throw new ArgumentException("Type " + enumType.Name + " is not an enum!");
            }

            List<Object> values = new List<object>();

            var fields = from n in enumType.GetFields()
                         where n.IsLiteral
                         select n;

            foreach (FieldInfo fi in fields)
                values.Add(fi.GetValue(enumType));

            return values.ToArray();

        }
    }
}
