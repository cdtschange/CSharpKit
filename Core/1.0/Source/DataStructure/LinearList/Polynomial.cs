using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cdts.DataStructure.Sort;

namespace Cdts.DataStructure.LinearList
{
    /// <summary>
    /// 多项式项
    /// </summary>
    public class PolynomialNode : IComparable
    {
        /// <summary>
        /// 构造器
        /// </summary>
        public PolynomialNode()
            : this(0, 0)
        {
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="coef">系数</param>
        /// <param name="expn">指数</param>
        public PolynomialNode(double coef, int expn)
        {
            this.Coefficient = coef;
            this.Exponent = expn;
        }
        /// <summary>
        /// 系数
        /// </summary>
        public double Coefficient { get; set; }
        /// <summary>
        /// 指数
        /// </summary>
        public int Exponent { get; set; }
        /// <summary>
        /// 相反数项
        /// </summary>
        /// <param name="node">项</param>
        /// <returns>返回相反数项</returns>
        public static PolynomialNode NegativePolynNode(PolynomialNode node)
        {
            return new PolynomialNode(node.Coefficient * -1, node.Exponent);
        }
        /// <summary>
        /// 倒数项
        /// </summary>
        /// <param name="node">项</param>
        /// <returns>返回倒数项</returns>
        public static PolynomialNode ReciprocalPolynNode(PolynomialNode node)
        {
            return new PolynomialNode(1 / node.Coefficient, -node.Exponent);
        }

        /// <summary>
        /// 项相加
        /// </summary>
        /// <param name="left">左项</param>
        /// <param name="right">右项</param>
        /// <returns>返回相加后的项</returns>
        public static PolynomialNode AddPolynNode(PolynomialNode left, PolynomialNode right)
        {
            if (left == null) return right;
            if (right == null) return left;
            if (left.Exponent != right.Exponent)
            {
                throw new InvalidOperationException("指数不同的多项式项无法相加！");
            }
            return new PolynomialNode(left.Coefficient + right.Coefficient, left.Exponent);
        }
        /// <summary>
        /// 项相乘
        /// </summary>
        /// <param name="left">左项</param>
        /// <param name="k">系数</param>
        /// <returns>返回相乘后的项</returns>
        public static PolynomialNode MultiplyPolynNode(PolynomialNode left, double k)
        {
            if (left == null) return null;
            return new PolynomialNode(left.Coefficient * k, left.Exponent);
        }
        /// <summary>
        /// 项相乘
        /// </summary>
        /// <param name="left">左项</param>
        /// <param name="right">右项</param>
        /// <returns>返回相乘后的项</returns>
        public static PolynomialNode MultiplyPolynNode(PolynomialNode left, PolynomialNode right)
        {
            if (left == null) return right;
            if (right == null) return left;
            return new PolynomialNode(left.Coefficient * right.Coefficient, left.Exponent + right.Exponent);
        }
        /// <summary>
        /// 项相除
        /// </summary>
        /// <param name="left">左项</param>
        /// <param name="k">系数</param>
        /// <returns>返回相除后的项</returns>
        public static PolynomialNode DividPolynNode(PolynomialNode left, double k)
        {
            if (left == null) return null;
            if (k == 0)
                throw new InvalidOperationException("除数不能为0！");
            return new PolynomialNode(left.Coefficient / k, left.Exponent);
        }
        /// <summary>
        /// 项相除
        /// </summary>
        /// <param name="left">系数</param>
        /// <param name="k">左项</param>
        /// <returns>返回相除后的项</returns>
        public static PolynomialNode DividPolynNode(double k, PolynomialNode left)
        {
            if (left == null) return null;
            if (left.Coefficient == 0)
                throw new InvalidOperationException("除数不能为0！");
            return new PolynomialNode(k / left.Coefficient, -left.Exponent);
        }
        /// <summary>
        /// 项相除
        /// </summary>
        /// <param name="left">左项</param>
        /// <param name="right">右项</param>
        /// <returns>返回相除后的项</returns>
        public static PolynomialNode DividPolynNode(PolynomialNode left, PolynomialNode right)
        {
            if (left == null) return right;
            if (right == null) return left;
            return new PolynomialNode(left.Coefficient / right.Coefficient, left.Exponent - right.Exponent);
        }

        public static bool operator <(PolynomialNode left, PolynomialNode right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator >(PolynomialNode left, PolynomialNode right)
        {
            return left.CompareTo(right) > 0;
        }
        public static PolynomialNode operator -(PolynomialNode node)
        {
            return PolynomialNode.NegativePolynNode(node);
        }
        public static PolynomialNode operator +(PolynomialNode left, PolynomialNode right)
        {
            return PolynomialNode.AddPolynNode(left, right);
        }
        public static PolynomialNode operator -(PolynomialNode left, PolynomialNode right)
        {
            return PolynomialNode.AddPolynNode(left, -right);
        }
        public static PolynomialNode operator *(PolynomialNode left, PolynomialNode right)
        {
            return PolynomialNode.MultiplyPolynNode(left, right);
        }
        public static PolynomialNode operator *(PolynomialNode left, double k)
        {
            return PolynomialNode.MultiplyPolynNode(left, k);
        }
        public static PolynomialNode operator *(PolynomialNode left, int k)
        {
            return PolynomialNode.MultiplyPolynNode(left, k);
        }
        public static PolynomialNode operator *(double k, PolynomialNode left)
        {
            return PolynomialNode.MultiplyPolynNode(left, k);
        }
        public static PolynomialNode operator *(int k, PolynomialNode left)
        {
            return PolynomialNode.MultiplyPolynNode(left, k);
        }
        public static PolynomialNode operator /(PolynomialNode left, PolynomialNode right)
        {
            return PolynomialNode.DividPolynNode(left, right);
        }
        public static PolynomialNode operator /(PolynomialNode left, double k)
        {
            return PolynomialNode.DividPolynNode(left, k);
        }
        public static PolynomialNode operator /(PolynomialNode left, int k)
        {
            return PolynomialNode.DividPolynNode(left, k);
        }
        public static PolynomialNode operator /(double k, PolynomialNode left)
        {
            return PolynomialNode.DividPolynNode(k, left);
        }
        public static PolynomialNode operator /(int k, PolynomialNode left)
        {
            return PolynomialNode.DividPolynNode(k, left);
        }

        public int CompareTo(object obj)
        {
            if (obj != null)
            {
                PolynomialNode right = obj as PolynomialNode;
                if (this.Exponent < right.Exponent) return -1;
                else if (this.Exponent == right.Exponent) return 0;
                else return 1;
            }
            return 1;
        }
        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                PolynomialNode right = obj as PolynomialNode;
                return this.Exponent == right.Exponent && this.Coefficient == right.Coefficient;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    /// <summary>
    /// 多项式
    /// </summary>
    public class Polynomial
    {
        private LinkedList<PolynomialNode> list;
        private ISortHelper<PolynomialNode> sort = new SortHelper<PolynomialNode>();
        /// <summary>
        /// 构造器
        /// </summary>
        public Polynomial()
        {
            list = new LinkedList<PolynomialNode>();
        }
        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="plns">多项式项集</param>
        public Polynomial(List<PolynomialNode> plns)
        {
            CreatePolyn(plns);
        }

        /// <summary>
        /// 创建多项式
        /// </summary>
        /// <param name="plns">多项式项集</param>
        public void CreatePolyn(List<PolynomialNode> plns)
        {
            list = new LinkedList<PolynomialNode>();
            plns.ForEach(n => AddPolynNode(n));
        }
        /// <summary>
        /// 添加多项式项
        /// </summary>
        /// <param name="node">多项式项</param>
        public void AddPolynNode(PolynomialNode node)
        {
            if (node.Coefficient == 0)
                return;
            if (list == null)
                list = new LinkedList<PolynomialNode>();
            PolynomialNode en = FindPolynNode(node.Exponent);
            if (en == null)
            {
                list.AddLast(node);
            }
            else
            {
                LinkedListNode<PolynomialNode> lNode = list.Find(en);
                lNode.Value += node;
                if (lNode.Value.Coefficient == 0)
                {
                    list.Remove(lNode);
                }
            }
        }
        /// <summary>
        /// 寻找项
        /// </summary>
        /// <param name="expn">指数</param>
        /// <returns>返回项，没有返回Null</returns>
        public PolynomialNode FindPolynNode(int expn)
        {
            PolynomialNode result = null;
            var head = list.First;
            while (head != null)
            {
                if (head.Value != null && expn == head.Value.Exponent)
                {
                    result = head.Value;
                    break;
                }
                head = head.Next;
            }
            return result;
        }
        /// <summary>
        /// 销毁多项式
        /// </summary>
        public void DestoryPolyn()
        {
            list.Clear();
        }
        /// <summary>
        /// 多项式排序
        /// </summary>
        public void SortPolyn()
        {
            PolynomialNode[] nodes = list.ToArray();
            sort.Sort(nodes, 0, list.Count - 1, null);
            CreatePolyn(nodes.ToList());
        }
        /// <summary>
        /// 相反数项
        /// </summary>
        /// <param name="node">项</param>
        /// <returns>返回相反数项</returns>
        public static Polynomial NegativePolyn(Polynomial p)
        {
            Polynomial result = new Polynomial();
            var head = p.list.First;
            while (head != null)
            {
                result.AddPolynNode(-head.Value);
                head = head.Next;
            }
            return result;
        }
        /// <summary>
        /// 多项式相加
        /// </summary>
        /// <param name="left">左多项式</param>
        /// <param name="right">右多项式</param>
        /// <returns>返回相加后的多项式</returns>
        public static Polynomial AddPolyn(Polynomial left, Polynomial right)
        {
            if (left == null) return right;
            if (right == null) return left;
            Polynomial result = new Polynomial();
            var head = left.list.First;
            while (head != null)
            {
                result.AddPolynNode(head.Value);
                head = head.Next;
            }
            head = right.list.First;
            while (head != null)
            {
                result.AddPolynNode(head.Value);
                head = head.Next;
            }
            return result;
        }
        /// <summary>
        /// 多项式相乘
        /// </summary>
        /// <param name="left">左多项式</param>
        /// <param name="right">倍数</param>
        /// <returns>返回相乘后的多项式</returns>
        public static Polynomial MultiplyPolyn(Polynomial left, double k)
        {
            if (left == null) return null;
            Polynomial result = new Polynomial();
            var head = left.list.First;
            while (head != null)
            {
                result.AddPolynNode(head.Value * k);
                head = head.Next;
            }
            return result;
        }
        /// <summary>
        /// 多项式相乘
        /// </summary>
        /// <param name="left">左多项式</param>
        /// <param name="right">多项式项</param>
        /// <returns>返回相乘后的多项式</returns>
        public static Polynomial MultiplyPolyn(Polynomial left, PolynomialNode node)
        {
            if (left == null) return null;
            Polynomial result = new Polynomial();
            var head = left.list.First;
            while (head != null)
            {
                result.AddPolynNode(head.Value * node);
                head = head.Next;
            }
            return result;
        }
        /// <summary>
        /// 多项式相乘
        /// </summary>
        /// <param name="left">左多项式</param>
        /// <param name="right">右多项式</param>
        /// <returns>返回相乘后的多项式</returns>
        public static Polynomial MultiplyPolyn(Polynomial left, Polynomial right)
        {
            Polynomial result = null;
            var head = right.list.First;
            while (head != null)
            {
                result += left * head.Value;
                head = head.Next;
            }
            return result;
        }

        public static Polynomial operator -(Polynomial p)
        {
            return Polynomial.NegativePolyn(p);
        }
        public static Polynomial operator +(Polynomial left, Polynomial right)
        {
            return Polynomial.AddPolyn(left, right);
        }
        public static Polynomial operator -(Polynomial left, Polynomial right)
        {
            return Polynomial.AddPolyn(left, -right);
        }
        public static Polynomial operator *(Polynomial left, Polynomial right)
        {
            return Polynomial.MultiplyPolyn(left, right);
        }
        public static Polynomial operator *(Polynomial left, PolynomialNode node)
        {
            return Polynomial.MultiplyPolyn(left, node);
        }
        public static Polynomial operator *(Polynomial left, double k)
        {
            return Polynomial.MultiplyPolyn(left, k);
        }
        public static Polynomial operator *(Polynomial left, int k)
        {
            return Polynomial.MultiplyPolyn(left, k);
        }
        public static Polynomial operator *(double k, Polynomial left)
        {
            return Polynomial.MultiplyPolyn(left, k);
        }
        public static Polynomial operator *(int k, Polynomial left)
        {
            return Polynomial.MultiplyPolyn(left, k);
        }

        /// <summary>
        /// 项数
        /// </summary>
        public int Count { get { return list.Count; } }
    }
}
