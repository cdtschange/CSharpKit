using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Linq.Expressions;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Collections;
using Newtonsoft.Json;
using FastReflectionLib;

namespace Cdts.Core
{
    public static class TypeExtentions
    {
        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="type">实体类型</param>
        /// <param name="name">属性名称</param>
        /// <returns>返回属性信息</returns>
        public static PropertyInfo GetPropertyEx(this Type type, string name)
        {
            PropertyInfo pi = type.GetProperty(name);
            if (pi == null)
            {
                if (type.IsInterface)
                {
                    pi = type.GetPropertyFromInterface(name);
                }
            }
            return pi;
        }
        /// <summary>
        /// 获取属性信息
        /// </summary>
        /// <param name="interfaceType">接口类型</param>
        /// <param name="name">属性名称</param>
        /// <returns>返回属性信息</returns>
        public static PropertyInfo GetPropertyFromInterface(this Type interfaceType, string name)
        {
            PropertyInfo pi = interfaceType.GetProperty(name);
            if (pi != null)
            {
                return pi;
            }
            foreach (Type parent in interfaceType.GetInterfaces())
            {
                pi = GetPropertyFromInterface(parent, name);
                if (pi != null)
                {
                    break;
                }
            }
            return pi;
        }
        /// <summary>
        /// 类型是否可以为空
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns>返回结果</returns>
        public static bool IsNullableType(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        /// <summary>
        /// 获取不为空的类型
        /// </summary>
        /// <param name="type">类型（类型可以是可空类型）</param>
        /// <returns>返回不为空的类型</returns>
        public static Type GetNonNullableType(this Type type)
        {
            return IsNullableType(type) ? type.GetGenericArguments()[0] : type;
        }

    }

    [CoreLogging]
    public static partial class Extends
    {
        public static IQueryable<T> ExecuteLinqStatement<T>(this IQueryable<T> source, string linqStatement, List<string> assemblies, List<string> imports, params object[] values)
        {
            CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

            //ICodeCompiler compiler =  CodeDomProvider.CompileAssemblyFromSource( csharpCodeProvider.CreateCompiler();
            // var c = CodeDomProvider.CreateProvider("c#");

            CompilerParameters cp = new CompilerParameters();

            cp.ReferencedAssemblies.Add("system.dll");
            cp.ReferencedAssemblies.Add("System.Core.dll");
            cp.ReferencedAssemblies.Add("System.Data.dll");
            cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
            cp.ReferencedAssemblies.Add("System.Data.Entity.dll");
            cp.ReferencedAssemblies.Add("System.Xml.dll");
            cp.ReferencedAssemblies.Add("System.Xml.Linq.dll");
            if (assemblies != null && assemblies.Count > 0)
            {
                cp.ReferencedAssemblies.AddRange(assemblies.ToArray());
            }
            cp.CompilerOptions = "/t:library";

            cp.GenerateInMemory = true;

            StringBuilder myCode = new StringBuilder();

            myCode.AppendLine("using System;");
            myCode.AppendLine("using System.Collections.Generic;");
            myCode.AppendLine("using System.Linq;");
            myCode.AppendLine("using System.Linq.Expressions;");
            myCode.AppendLine("using System.Text;");
            myCode.AppendLine("using System.Data;");
            myCode.AppendLine("using System.Data.Objects;");
            string semicolon = ";";
            if (imports != null && imports.Count > 0)
            {
                foreach (string import in imports)
                {
                    semicolon = ";";
                    if (import.EndsWith(";"))
                    {
                        semicolon = "";
                    }
                    myCode.AppendLine(string.Format("using {0}{1}", import, semicolon));
                }
            }
            myCode.AppendLine("namespace LinqStatement___1{");//begin namespace
            myCode.AppendLine("public class LinqStatement___2{");//begin class
            myCode.AppendLine(string.Format("public IQueryable<{0}> ExecuteLinqStatement(IQueryable<{0}> source, params object[] values){{", typeof(T).FullName));//begin ExecuteLinqStatement
            semicolon = ";";
            if (linqStatement.EndsWith(";"))
            {
                semicolon = "";

            }
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] != null)
                {
                    myCode.AppendLine(string.Format("var p{0} = ({1})values[{0}];", i, values[i].GetType().FullName));
                }
                else
                {
                    string pi = string.Format("@p{0}", i);
                    linqStatement = linqStatement.Replace(pi, "null");
                    //myCode.AppendLine(string.Format("var p{0} = null;", i));

                }
            }

            myCode.AppendLine(string.Format("IQueryable<{2}> obj ={0}{1}", linqStatement.Replace("@", ""), semicolon, typeof(T).FullName));
            myCode.AppendLine("return obj;");
            myCode.AppendLine("}");//end ExecuteLinqStatement
            myCode.AppendLine("}");//end class
            myCode.AppendLine("}");//end namespace

            CompilerResults cr = csharpCodeProvider.CompileAssemblyFromSource(cp, myCode.ToString());//compiler.CompileAssemblyFromSource(cp, myCode.ToString());

            if (cr.Errors.HasErrors)
            {
                StringBuilder strb = new StringBuilder();
                foreach (CompilerError error in cr.Errors)
                {
                    strb.AppendLine(error.ErrorText);
                }
                throw new InvalidProgramException(strb.ToString());
            }

            Assembly assembly = cr.CompiledAssembly;

            object tmp = assembly.CreateInstance("LinqStatement___1.LinqStatement___2");

            Type type = tmp.GetType();

            MethodInfo mi = type.GetMethod("ExecuteLinqStatement");
            //mi= mi.MakeGenericMethod(typeof(T));
            IQueryable<T> result = (IQueryable<T>)mi.Invoke(tmp, new object[] { source, values });

            return result;

        }

        //public static IQueryable<T> ExecuteLinqStatement<T>(this IQueryable<T> source, string linqStatement, List<string> assemblies, List<string> imports, params object[] values)
        //{
        //    CSharpCodeProvider csharpCodeProvider = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

        //    ICodeCompiler compiler = csharpCodeProvider.CreateCompiler();

        //    CompilerParameters cp = new CompilerParameters();

        //    cp.ReferencedAssemblies.Add("system.dll");
        //    cp.ReferencedAssemblies.Add("System.Core.dll");
        //    cp.ReferencedAssemblies.Add("System.Data.dll");
        //    cp.ReferencedAssemblies.Add("System.Data.DataSetExtensions.dll");
        //    cp.ReferencedAssemblies.Add("System.Data.Entity.dll");
        //    cp.ReferencedAssemblies.Add("System.Xml.dll");
        //    cp.ReferencedAssemblies.Add("System.Xml.Linq.dll");
        //    if (assemblies != null && assemblies.Count > 0)
        //    {
        //        cp.ReferencedAssemblies.AddRange(assemblies.ToArray());
        //    }
        //    cp.CompilerOptions = "/t:library";

        //    cp.GenerateInMemory = true;

        //    StringBuilder myCode = new StringBuilder();

        //    myCode.AppendLine("using System;");
        //    myCode.AppendLine("using System.Collections.Generic;");
        //    myCode.AppendLine("using System.Linq;");
        //    myCode.AppendLine("using System.Linq.Expressions;");
        //    myCode.AppendLine("using System.Text;");
        //    myCode.AppendLine("using System.Data;");
        //    myCode.AppendLine("using System.Data.Objects;");
        //    string semicolon = ";";
        //    if (imports != null && imports.Count > 0)
        //    {
        //        foreach (string import in imports)
        //        {
        //            semicolon = ";";
        //            if (import.EndsWith(";"))
        //            {
        //                semicolon = "";
        //            }
        //            myCode.AppendLine(string.Format("using {0}{1}", import, semicolon));
        //        }
        //    }
        //    myCode.AppendLine("namespace LinqStatement___1{");//begin namespace
        //    myCode.AppendLine("public class LinqStatement___2{");//begin class
        //    myCode.AppendLine(string.Format("public IQueryable<{0}> ExecuteLinqStatement(IQueryable<{0}> source, params object[] values){{", typeof(T).FullName));//begin ExecuteLinqStatement
        //    semicolon = ";";
        //    if (linqStatement.EndsWith(";"))
        //    {
        //        semicolon = "";

        //    }
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        myCode.AppendLine(string.Format("var p{0} = ({1})values[{0}];", i, values[i].GetType().FullName));
        //    }

        //    myCode.AppendLine(string.Format("IQueryable<{2}> obj ={0}{1}", linqStatement.Replace("@", ""), semicolon, typeof(T).FullName));
        //    myCode.AppendLine("return obj;");
        //    myCode.AppendLine("}");//end ExecuteLinqStatement
        //    myCode.AppendLine("}");//end class
        //    myCode.AppendLine("}");//end namespace

        //    CompilerResults cr = compiler.CompileAssemblyFromSource(cp, myCode.ToString());

        //    Assembly assembly = cr.CompiledAssembly;

        //    object tmp = assembly.CreateInstance("LinqStatement___1.LinqStatement___2");

        //    Type type = tmp.GetType();

        //    MethodInfo mi = type.GetMethod("ExecuteLinqStatement");
        //    //mi= mi.MakeGenericMethod(typeof(T));
        //    IQueryable<T> result = (IQueryable<T>)mi.FastInvoke(tmp, new object[] { source, values });

        //    return result;

        //}

        public static IQueryable<T> Where<T>(this IQueryable<T> source, CoreExpression expression)
        {
            if (expression == null)
            {
                return source;
            }
            List<string> setMemberNames = new List<string>();
            List<object> values = new List<object>();
            string typeName = source.ElementType.Name.Substring(0, 1).ToLower() + source.ElementType.Name.Substring(1);
            string where = expression.ToLinqStatement(source.ElementType, setMemberNames, values);
            string linq = string.Format("from {0} in source ", typeName);
            if (setMemberNames.Count > 0)
            {
                linq = linq + string.Format("from {0} in {1}.{2} ", setMemberNames[0].Substring(0, 1).ToLower() + setMemberNames[0].Substring(1), typeName, setMemberNames[0]);
                linq = linq + " where " + where + string.Format(" select {0}", typeName);
                List<string> assemblies = new List<string>();
                assemblies.Add(System.Reflection.Assembly.GetExecutingAssembly().Location);
                assemblies.Add(source.ElementType.Assembly.Location);
                foreach (Type type in source.ElementType.GetInterfaces())
                {
                    if (type.Assembly.FullName.StartsWith("System.") || type.Assembly.FullName.StartsWith("System,"))
                        continue;
                    if (!assemblies.Contains(type.Assembly.Location))
                    {
                        assemblies.Add(type.Assembly.Location);
                    }
                }
                return source.ExecuteLinqStatement(linq, assemblies, null, values.ToArray());
            }
            ParameterExpression pe = Expression.Parameter(source.ElementType, "it");
            Expression exp = expression.ToExpression(source.ElementType, pe);
            return (IQueryable<T>)source.Provider.CreateQuery(
                Expression.Call(
                    typeof(Queryable), "Where",
                    new Type[] { source.ElementType },
                    source.Expression, Expression.Quote(Expression.Lambda<Func<T, bool>>(exp, pe))));
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, IList<CoreOrderBy> orderBy)
        {
            if (orderBy == null)
            {
                return source;
            }
            if (orderBy.Count <= 0)
            {
                return source;
            }
            string methodAsc = "OrderBy";
            string methodDesc = "OrderByDescending";
            Expression queryExpr = source.Expression;
            ParameterExpression pe = Expression.Parameter(source.ElementType, "it");
            foreach (CoreOrderBy by in orderBy)
            {
                Expression selector = by.Member.ToExpression(source.ElementType, pe);
                if (selector.Type.IsValueType || selector.Type == typeof(string))
                {
                    queryExpr = Expression.Call(
                    typeof(Queryable), by.Asc ? methodAsc : methodDesc,
                    new Type[] { source.ElementType, selector.Type },
                    queryExpr, Expression.Quote(Expression.Lambda(selector, pe)));
                    methodAsc = "ThenBy";
                    methodDesc = "ThenByDescending";
                }
                else
                {
                    throw new InvalidOperationException(Resources.Resource("NonSupportOrderByType", selector.Type.FullName));
                }
            }
            return source.Provider.CreateQuery<T>(queryExpr);
        }

        /// <summary>
        /// 获取给定的MemberExpression的类型
        /// </summary>
        /// <param name="member">MemberExpression实例</param>
        /// <returns>member的类型</returns>
        public static Type MemberType(this MemberExpression member)
        {
            if (member == null)
            {
                throw new ArgumentNullException("member");
            }
            if (!member.Type.IsNullableType())
            {
                return member.Type;
            }
            return member.Type.GetNonNullableType();
        }
        #region CoreExpression Extends
        public static Expression ToExpression(this CoreExpression expression, Type sourceType, ParameterExpression pe)
        {
            if (expression == null)
            {
                return null;
            }
            Expression exp = ToExpression(expression, sourceType, pe, null);
            return exp;
        }

        private static Expression ToExpression(CoreExpression expression, Type sourceType, ParameterExpression pe, Expression member)
        {
            if (expression == null)
            {
                return null;
            }
            Expression exp = null;
            Type memberType = null;
            if (member != null)
            {
                memberType = member.Type;
            }
            if (memberType == null)
            {
                memberType = sourceType;
            }
            CoreMemberExpression sme = null;
            CoreBinaryExpression sbe = expression as CoreBinaryExpression;

            Expression left = null;
            Expression right = null;

            if (sbe != null)
            {
                if ((sbe.Left.NodeType == CoreExpressionType.Member && sbe.Right.NodeType == CoreExpressionType.Constant)
                    || (sbe.Left.NodeType == CoreExpressionType.Constant && sbe.Right.NodeType == CoreExpressionType.Member))
                {
                    if (sbe.Left.NodeType == CoreExpressionType.Member)
                    {
                        left = ToExpression(sbe.Left, sourceType, pe);
                        if (sbe.NodeType != CoreExpressionType.In && sbe.NodeType != CoreExpressionType.InLike)
                        {
                            right = ToExpression(sbe.Right, sourceType, pe, left);
                        }
                    }
                    else
                    {
                        right = ToExpression(sbe.Left, sourceType, pe);
                        if (sbe.NodeType != CoreExpressionType.In && sbe.NodeType != CoreExpressionType.InLike)
                        {
                            left = ToExpression(sbe.Right, sourceType, pe, right);
                        }
                    }
                }
                else
                {
                    left = ToExpression(sbe.Left, sourceType, pe);
                    right = ToExpression(sbe.Right, sourceType, pe);
                }
            }
            string str;
            string typeName;
            string[] values;
            //BinaryExpression m;
            CoreConstantExpression sce1;
            switch (expression.NodeType)
            {
                case CoreExpressionType.And:
                    exp = Expression.AndAlso(left, right);
                    break;
                case CoreExpressionType.Or:
                    exp = Expression.OrElse(left, right);
                    break;
                case CoreExpressionType.GreaterThan:
                    exp = Expression.GreaterThan(left, right);
                    break;
                case CoreExpressionType.GreaterThanOrEqual:
                    exp = Expression.GreaterThanOrEqual(left, right);
                    break;
                case CoreExpressionType.LessThan:
                    exp = Expression.LessThan(left, right);
                    break;
                case CoreExpressionType.LessThanOrEqual:
                    exp = Expression.LessThanOrEqual(left, right);
                    break;
                case CoreExpressionType.Equal:
                    exp = Expression.Equal(left, right);
                    break;
                case CoreExpressionType.NotEqual:
                    exp = Expression.NotEqual(left, right);
                    break;
                case CoreExpressionType.Like:
                case CoreExpressionType.NotLike:
                    exp = ConvertLike(right, sbe.NodeType, left);
                    break;
                case CoreExpressionType.In:
                    sce1 = sbe.Right as CoreConstantExpression;
                    if (sce1 == null)
                    {
                        throw new InvalidOperationException(Resources.Resource("InNeedConstantExpressionOnRight"));
                    }
                    str = sce1.Value as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        throw new InvalidOperationException(Resources.Resource("InOnRightStringIsNull"));
                    }
                    values = str.Split(",".ToCharArray());
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (string.IsNullOrEmpty(values[i]))
                        {
                            continue;
                        }
                        CoreConstantExpression valueEx = CoreExpression.Constant(values[i]);
                        if (i == 0)
                        {
                            exp = Expression.Equal(left, ToExpression(valueEx, sourceType, pe, left));
                        }
                        else
                        {
                            exp = Expression.OrElse(exp, Expression.Equal(left, ToExpression(valueEx, sourceType, pe, left)));
                        }
                    }
                    break;
                case CoreExpressionType.InLike:
                    sce1 = sbe.Right as CoreConstantExpression;
                    if (sce1 == null)
                    {
                        throw new InvalidOperationException(Resources.Resource("InLikeNeedConstantExpressionOnRight"));
                    }
                    str = sce1.Value as string;
                    if (string.IsNullOrEmpty(str))
                    {
                        throw new InvalidOperationException(Resources.Resource("InLikeOnRightStringIsNull"));
                    }
                    values = str.Split(",".ToCharArray());
                    for (int i = 0; i < values.Length; i++)
                    {
                        if (string.IsNullOrEmpty(values[i]))
                        {
                            continue;
                        }
                        CoreConstantExpression valueEx = CoreExpression.Constant(values[i]);
                        if (i == 0)
                        {
                            exp = ConvertLike(ToExpression(valueEx, sourceType, pe, (MemberExpression)left), CoreExpressionType.Like, left);
                        }
                        else
                        {
                            exp = Expression.OrElse(exp, ConvertLike(ToExpression(valueEx, sourceType, pe, (MemberExpression)left), CoreExpressionType.Like, left));
                        }
                    }
                    break;
                case CoreExpressionType.Member:
                    sme = expression as CoreMemberExpression;
                    if (exp == null)
                    {
                        exp = pe;
                    }
                    string[] members = sme.Name.Split(".".ToCharArray());
                    for (int i = 0; i < members.Length; i++)
                    {
                        if (i == 0)
                        {
                            PropertyInfo pi = exp.Type.GetProperty(members[i]);
                            if (exp.Type.IsInterface)
                            {
                                pi = exp.Type.GetPropertyFromInterface(members[i]);
                            }
                            exp = Expression.Property(exp, pi);
                        }
                        else
                        {
                            Type type1 = exp.Type;
                            PropertyInfo pi = type1.GetProperty(members[i]);
                            if (type1.IsInterface)
                            {
                                pi = type1.GetPropertyFromInterface(members[i]);
                            }
                            if (pi != null)
                            {
                                exp = Expression.Property(exp, pi);
                            }
                            else
                            {
                                throw new InvalidOperationException(Resources.Resource("PropertyIsNotExisted", sme.Name));
                            }
                        }
                    }
                    break;
                case CoreExpressionType.Constant:
                    CoreConstantExpression sce = expression as CoreConstantExpression;
                    if (sce.Value == null)
                    {
                        exp = Expression.Constant(null);
                        break;
                    }
                    if (memberType == null)
                    {
                        exp = Expression.Constant(sce.Value);
                        break;
                    }
                    //typeName = memberType.Name.Substring(memberType.Name.LastIndexOf(".") + 1);
                    typeName = memberType.Name.Substring(memberType.Name.LastIndexOf(".") + 1);
                    Type[] types = memberType.GetGenericArguments();
                    if (types.Length > 0)
                    {
                        typeName = types[0].Name.Substring(types[0].Name.LastIndexOf(".") + 1);
                    }
                    switch (typeName)
                    {
                        case "Boolean":
                            exp = Expression.Constant(Convert.ToBoolean(sce.Value), memberType);
                            break;
                        case "Byte":
                            exp = Expression.Constant(Convert.ToByte(sce.Value), memberType);
                            break;
                        case "DateTime":
                            exp = Expression.Constant(Convert.ToDateTime(sce.Value), memberType);
                            break;
                        case "Decimal":
                            exp = Expression.Constant(Convert.ToDecimal(sce.Value), memberType);
                            break;
                        case "Double":
                            exp = Expression.Constant(Convert.ToDouble(sce.Value), memberType);
                            break;
                        case "Guid":
                            Guid guid;
                            if (sce.Value.GetType() == typeof(Guid))
                            {
                                guid = (Guid)sce.Value;
                            }
                            else
                            {
                                guid = new Guid(Convert.ToString(sce.Value));
                            }
                            exp = Expression.Constant(guid, memberType);
                            break;
                        case "Int16":
                            exp = Expression.Constant(Convert.ToInt16(sce.Value), memberType);
                            break;
                        case "Int32":
                            exp = Expression.Constant(Convert.ToInt32(sce.Value), memberType);
                            break;
                        case "Int64":
                            exp = Expression.Constant(Convert.ToInt64(sce.Value), memberType);
                            break;
                        case "SByte":
                            exp = Expression.Constant(Convert.ToSByte(sce.Value), memberType);
                            break;
                        case "Single":
                            exp = Expression.Constant(Convert.ToSingle(sce.Value), memberType);
                            break;
                        case "String":
                            exp = Expression.Constant(Convert.ToString(sce.Value), memberType);
                            break;
                        default:
                            exp = Expression.Constant(sce.Value);
                            break;
                    }
                    break;
            }
            return exp;
        }

        private static Expression ConvertLike(Expression exp, CoreExpressionType nodeType, Expression left)
        {
            ConstantExpression ce = exp as ConstantExpression;
            Expression expression = null;
            if (ce != null)
            {
                string value = ce.Value as string;
                if (!string.IsNullOrEmpty(value))
                {
                    string methodName = "Contains";
                    if (value.StartsWith("%") && value.EndsWith("%"))
                    {
                        methodName = "Contains";
                    }
                    else if (value.EndsWith("%"))
                    {
                        methodName = "StartsWith";
                    }
                    else if (value.StartsWith("%"))
                    {
                        methodName = "EndsWith";
                    }
                    ConstantExpression ce1 = Expression.Constant(value.Replace("%", ""), typeof(string));
                    expression = Expression.Call(left, typeof(string).GetMethod(methodName, new Type[] { typeof(string) }), ce1);
                    if (nodeType == CoreExpressionType.NotLike)
                    {
                        expression = Expression.Not(expression);
                    }
                }
            }
            return expression;
        }
        #endregion

        //public static string ToJson(this IQueryable query)
        //{
        //    return ToJson(query, -1);
        //}
        //public static string ToJson(this IQueryable query, int record)
        //{
        //    List<object> list = new List<object>();
        //    foreach (var item in query)
        //    {
        //        list.Add(item);
        //    }
        //    record = record == -1 ? list.Count : record;
        //    return string.Format("{{\"TotalRecords\":{0},\"Rows\":{1}}}", record, ToJson(list));
        //}
        public static string ToJson(this object obj)
        {
            JsonSerializer ser = new JsonSerializer();
            ser.ObjectCreationHandling = ObjectCreationHandling.Auto;
            ser.NullValueHandling = NullValueHandling.Ignore;
            ser.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            //ser.Depth = depth;
            string json = "";
            using (StringWriter sw = new StringWriter())
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    ser.Serialize(writer, obj);
                    json = sw.ToString();
                    writer.Close();
                    sw.Close();
                }
            }
            return json;
        }

        public static object ToObject(this string json, Type type, params List<Type>[] knownTypes)
        {
            List<Type> types = new List<Type>();
            foreach (List<Type> typeList in knownTypes)
            {
                types.AddRange(typeList);
            }
            System.IO.Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(type, types);
            object output = ser.ReadObject(stream);
            stream.Dispose();
            return output;
        }
        public static object ToObject(this string json, IGetObject getObject, Type objectType, string keyName)
        {
            return ToObject(json, getObject, objectType, keyName, getObject.MappingTypes);
        }
        public static object ToObject(this JObject jo, IGetObject getObject, Type objectType, string keyName)
        {
            return ToObject(jo, getObject, objectType, keyName, getObject.MappingTypes);
        }
        /// <summary>
        /// Json -> Object
        /// </summary>
        /// <param name="json">Json</param>
        /// <param name="getObject">GetObject</param>
        /// <param name="objectType">目标对象的类型</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="mappingTypes">接口->实例的映射字典</param>
        /// <returns>返回对象</returns>
        public static object ToObject(this string json, IGetObject getObject, Type objectType, string keyName, params Dictionary<Type, Type>[] mappingTypes)
        {
            return ToObject(json, getObject, objectType, keyName, true, mappingTypes);
        }
        //TODO: collectionAdd is null
        public static object ToObjectCollection(this string json, IGetObject getObject, Type objectType, bool? collectionAdd, string keyName, params Dictionary<Type, Type>[] mappingTypes)
        {
            Dictionary<Type, Type> types = new Dictionary<Type, Type>();
            foreach (var map in mappingTypes) //拼接映射字典
            {
                foreach (var keyValue in map)
                {
                    if (!types.ContainsKey(keyValue.Key))
                    {
                        types.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }
            JObject jobject = JObject.Parse(json);
            object oldEntity;
            return JObjectToObject(jobject, getObject, objectType, keyName, true, collectionAdd, types, false, out oldEntity);
        }

        public static object ToObject(this string json, IGetObject getObject, Type objectType, string keyName, bool collectionAdd, params Dictionary<Type, Type>[] mappingTypes)
        {
            Dictionary<Type, Type> types = new Dictionary<Type, Type>();
            foreach (var map in mappingTypes) //拼接映射字典
            {
                foreach (var keyValue in map)
                {
                    if (!types.ContainsKey(keyValue.Key))
                    {
                        types.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }
            JObject jobject = JObject.Parse(json);
            object oldEntity;
            return JObjectToObject(jobject, getObject, objectType, keyName, true, collectionAdd, types, false, out oldEntity);
        }
        public static object ToObject(this JObject jobject, IGetObject getObject, Type objectType, string keyName, params Dictionary<Type, Type>[] mappingTypes)
        {
            return ToObject(jobject, getObject, objectType, keyName, true, mappingTypes);
        }
        public static object ToObject(this JObject jobject, IGetObject getObject, Type objectType, string keyName, bool collectionAdd, params Dictionary<Type, Type>[] mappingTypes)
        {
            return ToObject(jobject, getObject, objectType, keyName, true, collectionAdd, mappingTypes);
        }

        //TODO: collectionAdd is null
        public static object ToObjectCollection(this JObject jobject, IGetObject getObject, Type objectType, string keyName, bool? collectionAdd, bool ignoreNavigationProperty, params Dictionary<Type, Type>[] mappingTypes)
        {
            Dictionary<Type, Type> types = new Dictionary<Type, Type>();
            foreach (var map in mappingTypes) //拼接映射字典
            {
                foreach (var keyValue in map)
                {
                    if (!types.ContainsKey(keyValue.Key))
                    {
                        types.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }
            object oldEntity;
            return JObjectToObject(jobject, getObject, objectType, keyName, ignoreNavigationProperty, collectionAdd, types, false, out oldEntity);
        }

        public static object ToObject(this JObject jobject, IGetObject getObject, Type objectType, string keyName, bool ignoreNavigationProperty, bool collectionAdd, params Dictionary<Type, Type>[] mappingTypes)
        {
            Dictionary<Type, Type> types = new Dictionary<Type, Type>();
            foreach (var map in mappingTypes) //拼接映射字典
            {
                foreach (var keyValue in map)
                {
                    if (!types.ContainsKey(keyValue.Key))
                    {
                        types.Add(keyValue.Key, keyValue.Value);
                    }
                }
            }
            object oldEntity;
            return JObjectToObject(jobject, getObject, objectType, keyName, ignoreNavigationProperty, collectionAdd, types, false, out oldEntity);
        }

        /// <summary>
        /// Json -> Object
        /// </summary>
        /// <param name="jobject">JObject</param>
        /// <param name="getObject">GetObject</param>
        /// <param name="objectType">目标对象的类型</param>
        /// <param name="keyName">主键名称</param>
        /// <param name="mappingTypes">接口->实例的映射字典</param>
        /// <returns></returns>
        private static object JObjectToObject(JObject jobject, IGetObject getObject, Type objectType, string keyName, bool ignoreNavigationProperty, bool? collectionAdd, Dictionary<Type, Type> mappingTypes, bool getOldEntity, out object oldEntity)
        {
            oldEntity = null;
            if (jobject.Property(keyName) == null)
            {
                throw new InvalidOperationException(string.Format("JSON中没有主键是{0}的属性。", keyName));
            }
            Type mappedType = objectType;
            if (mappingTypes.ContainsKey(objectType))
            {
                mappedType = mappingTypes[objectType];
            }
            object id = ConvertTo(((JValue)jobject[keyName]).Value, GetKeyType(mappedType, keyName));
            object obj = getObject.GetObject(keyName, id, mappedType); //获取对象
            if (obj == null) //如果对象为空，则创建对象实例并对主键赋值
            {
                obj = getObject.CreateObject(mappedType);
                SetValue(obj, keyName, id);
            }
            else
            {
                if (getOldEntity)
                {
                    oldEntity = getObject.CreateObject(mappedType);
                }
            }
            foreach (var p in jobject.Properties()) //遍历Json对象中的属性
            {
                if (!jobject[p.Name].HasValues && p.Value.Type != JTokenType.Array)
                {
                    if (p.Name != keyName) //为对象的属性赋值
                    {
                        SetValue(obj, p.Name, ((JValue)jobject[p.Name]).Value);
                    }

                }
                else //如果属性是关联属性
                {
                    Type navType = GetKeyType(mappedType, p.Name);
                    if (p.Value.Type != JTokenType.Array)// || (p.Value.Type != JsonTokenType.Array && !navType.IsGenericType))
                    {
                        JObject jo = (JObject)(jobject[p.Name]);
                        object old;
                        object nav = null;
                        if (ignoreNavigationProperty)
                        {
                            object navId = ConvertTo(((JValue)jo[keyName]).Value, GetKeyType(navType, keyName));
                            nav = getObject.GetObject(keyName, navId, navType); //获取对象
                        }
                        else
                        {
                            //ToDo:需要进一步测试关联属性的集合属性。
                            nav = JObjectToObject(jo, getObject, navType, keyName, ignoreNavigationProperty, true, mappingTypes, false, out old);
                        }
                        SetValue(obj, p.Name, nav);
                    }
                    else
                    {
                        if (!navType.IsGenericType)
                        {
                            SetValue(obj, p.Name, p.Value);
                        }
                        else
                        {
                            //if (typeof(ICollection<>).IsAssignableFrom(navType))
                            //{
                            //}
                            JArray ja = p.Value as JArray;
                            Type elementType = navType.GetGenericArguments()[0];
                            object navObj = GetValue(obj, p.Name);
                            if (navObj != null)
                            {
                                MethodInfo mi;
                                string methodName;
                                if (collectionAdd == null)
                                {
                                    mi = navObj.GetType().GetMethod("Clear", BindingFlags.Instance | BindingFlags.Public);
                                    if (mi != null)
                                    {
                                        mi.FastInvoke(navObj, new object[] { });
                                    }
                                    methodName = "Add";
                                }
                                else
                                {
                                    methodName = (bool)collectionAdd ? "Add" : "Remove";
                                }
                                mi = navObj.GetType().GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public);
                                if (mi != null)
                                {
                                    for (int i = 0; i < ja.Count; i++)
                                    {
                                        JObject jo1 = ja[i] as JObject;
                                        object elementId = null;
                                        object elementObj = null;
                                        if (jo1 != null)
                                        {
                                            elementId = ConvertTo(((JValue)jo1[keyName]).Value, GetKeyType(elementType, keyName));
                                            elementObj = getObject.GetObject(keyName, elementId, elementType); //获取对象
                                        }
                                        else
                                        {
                                            JValue jv = ja[i] as JValue;
                                            if (jv != null)
                                            {
                                                elementObj = ConvertTo(jv.Value, elementType);
                                            }
                                        }
                                        if (elementObj == null && methodName == "Add")
                                        {
                                            object old;
                                            elementObj = JObjectToObject(jo1, getObject, elementType, keyName, true, true, mappingTypes, false, out old);
                                        }
                                        if (elementObj != null)
                                        {
                                            mi.FastInvoke(navObj, new object[] { elementObj });
                                        }


                                    }
                                }
                            }
                        }
                    }
                }
            }
            return obj;

        }
        /// <summary>
        /// 获取主键类型
        /// </summary>
        /// <param name="objectType">对象</param>
        /// <param name="keyName">主键名称</param>
        /// <returns>返回主键类型</returns>
        private static Type GetKeyType(Type objectType, string keyName)
        {
            PropertyInfo pi = objectType.GetProperty(keyName);
            if (pi == null)
            {
                if (objectType.IsInterface) //如果是接口，需要继续搜索接口
                {
                    pi = objectType.GetPropertyFromInterface(keyName);
                }
                if (pi == null)
                    throw new InvalidOperationException(string.Format("属性{0}不存在！", keyName));
            }
            return pi.PropertyType;
        }
        /// <summary>
        /// 为对象的属性赋值
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="member">属性名</param>
        /// <param name="value">属性值</param>
        private static void SetValue(object obj, string member, object value)
        {
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(member);
            if (pi == null)
            {
                return;
            }
            pi.SetValue(obj, ConvertTo(value, pi.PropertyType), null);
        }
        private static object GetValue(object obj, string member)
        {
            Type type = obj.GetType();
            PropertyInfo pi = type.GetProperty(member);
            if (pi == null)
            {
                return null;
            }
            return pi.GetValue(obj, null);
        }
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="targetType">目标类型</param>
        /// <returns>返回对象</returns>
        private static object ConvertTo(object value, Type targetType)
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
            if (targetType.GetNonNullableType() == typeof(Guid)) //Guid单独处理
            {
                Guid guid = new Guid(v);
                return guid;
            }
            return value;
        }

        public static List<T> Split<T>(this string values, string splitor)
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            if (string.IsNullOrEmpty(values))
            {
                return list;
            }
            string[] vs = values.Split(splitor.ToCharArray());
            for (int i = 0, il = vs.Length; i < il; i++)
            {
                if (string.IsNullOrEmpty(vs[i]))
                {
                    continue;
                }
                object value = ConvertTo(vs[i], type);
                if (!(value is T))
                {
                    throw new InvalidOperationException(string.Format("无效的值:{0}", vs[i]));
                }
                list.Add((T)value);
            }
            return list;
        }

        public static string Connect(this IEnumerable list, string connector)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.Append(connector + item.ToString());
            }
            string values = sb.ToString();
            if (values.StartsWith(connector))
            {
                values = values.Substring(connector.Length);
            }
            return values;
        }
    }
}
