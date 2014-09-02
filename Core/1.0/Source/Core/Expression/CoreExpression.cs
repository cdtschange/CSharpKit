using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Cdts.Core
{
    /// <summary>
    /// 表达式
    /// </summary>
    public abstract partial class CoreExpression
    {
        /// <summary>
        /// 节点类型
        /// </summary>
        public virtual CoreExpressionType NodeType { get; set; }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="value">对象</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>返回转换后的对象</returns>
        protected object ConvertTo(object value, Type targetType)
        {
            if (value == null)
                return null;

            string v = value.ToString();
            switch (Type.GetTypeCode(targetType.GetNonNullableType()))
            {
                case TypeCode.Boolean:
                    bool b;
                    if (bool.TryParse(v, out b)) return b;
                    break;
                case TypeCode.SByte:
                    sbyte sb;
                    if (sbyte.TryParse(v, out sb)) return sb;
                    break;
                case TypeCode.Byte:
                    byte bt;
                    if (byte.TryParse(v, out bt)) return bt;
                    break;
                case TypeCode.Int16:
                    short s;
                    if (short.TryParse(v, out s)) return s;
                    break;
                case TypeCode.UInt16:
                    ushort us;
                    if (ushort.TryParse(v, out us)) return us;
                    break;
                case TypeCode.Int32:
                    int i;
                    if (int.TryParse(v, out i)) return i;
                    break;
                case TypeCode.UInt32:
                    uint ui;
                    if (uint.TryParse(v, out ui)) return ui;
                    break;
                case TypeCode.Int64:
                    long l;
                    if (long.TryParse(v, out l)) return l;
                    break;
                case TypeCode.UInt64:
                    ulong ul;
                    if (ulong.TryParse(v, out ul)) return ul;
                    break;
                case TypeCode.Single:
                    float f;
                    if (float.TryParse(v, out f)) return f;
                    break;
                case TypeCode.Double:
                    double d;
                    if (double.TryParse(v, out d)) return d;
                    break;
                case TypeCode.Decimal:
                    decimal e;
                    if (decimal.TryParse(v, out e)) return e;
                    break;
                case TypeCode.Char:
                    char c;
                    if (char.TryParse(v, out c)) return c;
                    break;
                case TypeCode.DateTime:
                    DateTime dt;
                    if (DateTime.TryParse(v, out dt)) return dt;
                    break;
                case TypeCode.String:
                    return v;
            }
            if (targetType == typeof(Guid))
            {
                Guid guid = new Guid(v);
                return guid;
            }
            return value;
        }

        public virtual string ToLinqStatement(Type sourceType, List<string> setMembers, List<object> values)
        {
            return "";
        }

        public static CoreMemberExpression Member(string name)
        {
            CoreMemberExpression cme = new CoreMemberExpression(name);
            return cme;
        }
        public static CoreConstantExpression Constant(object value)
        {
            CoreConstantExpression cce = new CoreConstantExpression(value);
            return cce;
        }
        public static CoreBinaryExpression And(CoreExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.And;
            return cbe;
        }
        public static CoreBinaryExpression Or(CoreExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.Or;
            return cbe;
        }
        public static CoreBinaryExpression In(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.In;
            return cbe;
        }
        public static CoreBinaryExpression In(string left, string right)
        {
            return In(Member(left), Constant(right));
        }
        public static CoreBinaryExpression InLike(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.InLike;
            return cbe;
        }
        public static CoreBinaryExpression InLike(string left, string right)
        {
            return InLike(Member(left), Constant(right));
        }
        public static CoreBinaryExpression Equal(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.Equal;
            return cbe;
        }
        public static CoreBinaryExpression Equal(string left, object right)
        {
            return Equal(Member(left), Constant(right));
        }
        public static CoreBinaryExpression GreaterThan(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.GreaterThan;
            return cbe;
        }
        public static CoreBinaryExpression GreaterThan(string left, object right)
        {
            return GreaterThan(Member(left), Constant(right));
        }
        public static CoreBinaryExpression GreaterThanOrEqual(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.GreaterThanOrEqual;
            return cbe;
        }
        public static CoreBinaryExpression GreaterThanOrEqual(string left, object right)
        {
            return GreaterThanOrEqual(Member(left), Constant(right));
        }
        public static CoreBinaryExpression LessThan(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.LessThan;
            return cbe;
        }
        public static CoreBinaryExpression LessThan(string left, object right)
        {
            return LessThan(Member(left), Constant(right));
        }
        public static CoreBinaryExpression LessThanOrEqual(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.LessThanOrEqual;
            return cbe;
        }
        public static CoreBinaryExpression LessThanOrEqual(string left, object right)
        {
            return LessThanOrEqual(Member(left), Constant(right));
        }
        public static CoreBinaryExpression NotEqual(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.NotEqual;
            return cbe;
        }
        public static CoreBinaryExpression NotEqual(string left, object right)
        {
            return NotEqual(Member(left), Constant(right));
        }
        public static CoreBinaryExpression Like(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.Like;
            return cbe;
        }
        public static CoreBinaryExpression Like(string left, object right)
        {
            return Like(Member(left), Constant(right));
        }
        public static CoreBinaryExpression NotLike(CoreMemberExpression left, CoreExpression right)
        {
            CoreBinaryExpression cbe = new CoreBinaryExpression();
            cbe.Left = left;
            cbe.Right = right;
            cbe.NodeType = CoreExpressionType.NotLike;
            return cbe;
        }
        public static CoreBinaryExpression NotLike(string left, object right)
        {
            return NotLike(Member(left), Constant(right));
        }
    }

    public partial class CoreBinaryExpression : CoreExpression
    {       
        public CoreExpression Left { get; set; }
        public CoreExpression Right { get; set; }

        public override string ToLinqStatement(Type sourceType, List<string> setMembers, List<object> values)
        {
            string left = "";
            string right = "";
            string typeName = sourceType.Name.Substring(0, 1).ToLower() + sourceType.Name.Substring(1);
            if (Left.NodeType == CoreExpressionType.Member && Right.NodeType == CoreExpressionType.Constant)
            {
                CoreMemberExpression cme = Left as CoreMemberExpression;
                left = cme.Name;
                string[] ms = cme.Name.Split(".".ToCharArray());
                Type declareType = sourceType;
                bool isSetMember = false;
                string setMemberName = "";
                string errorMsg = typeName;
                for (int i = 0; i < ms.Length; i++)
                {
                    errorMsg += "." + ms[i];
                    PropertyInfo pi = declareType.GetPropertyEx(ms[i]);
                    if (pi != null)
                    {
                        declareType = pi.PropertyType;
                        if (declareType != typeof(string))
                        {
                            bool isSet = false;
                            isSet |= declareType.GetInterface("IEnumerable", true) != null;
                            isSet |= declareType.GetInterface("IEnumerable`1", true) != null;
                            if (isSet)
                            {
                                declareType = declareType.GetGenericArguments()[0];
                                if (!isSetMember)
                                {
                                    isSetMember = true;
                                    setMemberName = ms[i];
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException(Resources.Resource("PropertyIsNotExisted", errorMsg));
                    }
                }
                if (isSetMember)
                {
                    if (!cme.Name.StartsWith(setMemberName + "."))
                    {
                        throw new InvalidOperationException(Resources.Resource("InvalidProperty", setMemberName));
                    }
                    left = setMemberName.Substring(0, 1).ToLower() + setMemberName.Substring(1) + left.Substring(left.IndexOf("."));
                    if (!setMembers.Contains(setMemberName))
                    {
                        setMembers.Add(setMemberName);
                    }
                }
                else
                {
                    left = typeName + "." + left;
                }

                CoreConstantExpression cce = Right as CoreConstantExpression;
                string v;
                string[] vs;
                string methodName;
                switch (NodeType)
                {
                    case CoreExpressionType.In:
                    case CoreExpressionType.InLike:
                        v = cce.Value as string;
                        if (string.IsNullOrEmpty(v))
                        {
                            throw new InvalidOperationException(Resources.Resource("InOrInLikeTargetIsNull"));
                        }
                        vs = v.Split(',');
                        string linq = "";
                        if (NodeType == CoreExpressionType.In)
                        {
                            for (int i = 0; i < vs.Length; i++)
                            {
                                string exp = left;
                                exp = exp + " == {0} ";
                                values.Add(ConvertTo(vs[i], declareType));
                                right = string.Format("@p{0}", values.Count - 1);
                                if (i > 0)
                                {
                                    linq = linq + " || ";
                                }
                                linq = linq + string.Format(exp, right);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < vs.Length; i++)
                            {
                                string exp = left;
                                v = vs[i];
                                methodName = ".Contains({0})";
                                if (v.StartsWith("%") && v.EndsWith("%"))
                                {
                                    methodName = ".Contains({0})";
                                }
                                else if (v.EndsWith("%"))
                                {
                                    methodName = ".StartsWith({0})";
                                }
                                else if (v.StartsWith("%"))
                                {
                                    methodName = ".EndsWith({0})";
                                }
                                exp = exp + methodName;
                                values.Add(v.Replace("%", ""));

                                right = string.Format("@p{0}", values.Count - 1);
                                if (i > 0)
                                {
                                    linq = linq + " || ";
                                }
                                linq = linq + string.Format(exp, right);
                            }
                        }
                        return "( " + linq + " )";
                    case CoreExpressionType.Like:
                    case CoreExpressionType.NotLike:
                        if (declareType != typeof(string))
                        {
                            throw new InvalidOperationException(Resources.Resource("LikeOrNotLikeBodyIsNotString"));
                        }
                        v = cce.Value as string;
                        if (string.IsNullOrEmpty(v))
                        {
                            throw new ArgumentNullException(Resources.Resource("LikeOrNotLikeBodyIsEmpty"));
                        }
                        methodName = ".Contains({0})";
                        if (v.StartsWith("%") && v.EndsWith("%"))
                        {
                            methodName = ".Contains({0})";
                        }
                        else if (v.EndsWith("%"))
                        {
                            methodName = ".StartsWith({0})";
                        }
                        else if (v.StartsWith("%"))
                        {
                            methodName = ".EndsWith({0})";
                        }
                        values.Add(v.Replace("%", ""));
                        right = string.Format("@p{0}", values.Count - 1);
                        left = left + string.Format(methodName, right);
                        if (NodeType == CoreExpressionType.NotLike)
                        {
                            left = "!" + left;
                        }
                        return string.Format(" ( {0} ) ", left);
                    default:
                        Type resultType = declareType;
                        if (declareType.IsGenericType && declareType.GetGenericTypeDefinition() == typeof(Nullable<>))
                        {
                            resultType = declareType.GetGenericArguments()[0];
                        }
                        values.Add(ConvertTo(cce.Value, resultType));
                        right = string.Format("@p{0}", values.Count - 1);
                        break;
                }
            }
            if (string.IsNullOrEmpty(left))
            {
                left = Left.ToLinqStatement(sourceType, setMembers, values);
            }
            if (string.IsNullOrEmpty(right))
            {
                right = Right.ToLinqStatement(sourceType, setMembers, values);
            }
            string op = "";
            switch (NodeType)
            {
                case CoreExpressionType.And:
                    op = "&&";
                    break;
                case CoreExpressionType.Equal:
                    op = "==";
                    break;
                case CoreExpressionType.GreaterThan:
                    op = ">";
                    break;
                case CoreExpressionType.GreaterThanOrEqual:
                    op = ">=";
                    break;
                case CoreExpressionType.LessThan:
                    op = "<";
                    break;
                case CoreExpressionType.LessThanOrEqual:
                    op = "<=";
                    break;
                case CoreExpressionType.NotEqual:
                    op = "!=";
                    break;
                case CoreExpressionType.Or:
                    op = "||";
                    break;
                default:
                    throw new InvalidOperationException(Resources.Resource("NonSupportOperation"));
            }
            return string.Format("({0} {1} {2})", left, op, right);
        }
    }

    public partial class CoreConstantExpression : CoreExpression
    {
        public CoreConstantExpression()
            : this(null)
        {
        }
        public CoreConstantExpression(object value)
        {
            NodeType = CoreExpressionType.Constant;
            Value = value;
        }
        public object Value { get; set; }

    }

    public partial class CoreMemberExpression : CoreExpression
    {
        public CoreMemberExpression()
            : this("")
        {
        }
        public CoreMemberExpression(string name)
        {
            NodeType = CoreExpressionType.Member;
            Name = name;
        }
        public string Name { get; set; }
    }

    public class CoreSearchParemeter
    {
        public CoreExpression Expression { get; set; }
        public string Selector { get; set; }
        private List<string> operationCodes = new List<string>();
        public List<string> OperationCodes
        {
            get
            {
                if (operationCodes == null)
                {
                    operationCodes = new List<string>();
                }
                return operationCodes;
            }
            set
            {
                operationCodes = value;
            }
        }
        private List<CoreOrderBy> orderBy = new List<CoreOrderBy>();
        public List<CoreOrderBy> OrderBy
        {
            get
            {
                if (orderBy == null)
                {
                    orderBy = new List<CoreOrderBy>();
                }
                return orderBy;
            }
            set
            {
                orderBy = value;
            }
        }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    /// <summary>
    /// 表达式类型
    /// </summary>
    public enum CoreExpressionType
    {
        Member = 0,
        Constant,
        And,
        Equal,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
        NotEqual,
        Or,
        Like,
        NotLike,
        In,
        InLike
    }
}
