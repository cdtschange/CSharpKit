using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    public class KnownTypeList
    {
        static List<Type> knownTypes = new List<Type>();
        public static List<Type> KnownTypes
        {
            get
            {
                if (knownTypes == null)
                {
                    knownTypes = new List<Type>();
                }
                if (knownTypes.Count < 1)
                {
                    knownTypes.Add(typeof(CoreBinaryExpression));
                    knownTypes.Add(typeof(CoreMemberExpression));
                    knownTypes.Add(typeof(CoreConstantExpression));
                }
                return knownTypes;
            }
        }
        static List<Type> allTypes = new List<Type>();
        public static List<Type> AllTypes
        {
            get
            {
                return allTypes;
            }
        }
    }
}
