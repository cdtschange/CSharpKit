using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Core
{
    public partial class CoreOrderBy
    {
        public CoreOrderBy()
            : this("", true)
        {
        }
        public CoreOrderBy(string name, bool asc)
        {
            Member = new CoreMemberExpression(name);
            Asc = asc;
        }
        public CoreOrderBy(CoreMemberExpression member, bool asc)
        {
            Member = member;
            Asc = asc;
        }
        public CoreMemberExpression Member { get; set; }
        public bool Asc { get; set; }
        public static IList<CoreOrderBy> Create(params CoreOrderBy[] orderBy)
        {
            List<CoreOrderBy> list = new List<CoreOrderBy>();
            list.AddRange(orderBy);
            return list;
        }
    }
}
