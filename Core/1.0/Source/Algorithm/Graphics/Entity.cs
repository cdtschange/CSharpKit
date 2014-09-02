using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cdts.Algorithm.Graphics
{
    /// <summary>
    /// 邊
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Edge<T, K> : IComparable
        where T : IComparable
        where K : IConvertible, IComparable
    {
        public Vertex<T> LeftNode { get; set; }
        public Vertex<T> RightNode { get; set; }
        public K Weight { get; set; }

        public int CompareTo(object obj)
        {
            Edge<T, K> ed = obj as Edge<T, K>;
            if (ed == null)
            {
                throw new InvalidOperationException("Can not compare Edge and other type of class.");
            }
            return this.Weight.CompareTo(ed.Weight);
        }
        public override bool Equals(object other)
        {
            // Check object other is a Vector3 object
            if (other is Edge<T, K>)
            {
                // Convert object to Vector3
                Edge<T, K> otherVector = (Edge<T, K>)other;

                // Check for equality
                return otherVector == this;
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator >(Edge<T, K> left, Edge<T, K> right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <(Edge<T, K> left, Edge<T, K> right)
        {
            return left.CompareTo(right) < 0;
        }
        public static bool operator ==(Edge<T, K> left, Edge<T, K> right)
        {
            if (right is Edge<T, K> && left is Edge<T, K>)
            {
                return left.LeftNode == right.LeftNode && left.RightNode == right.RightNode;
            }
            return false;
        }
        public static bool operator !=(Edge<T, K> left, Edge<T, K> right)
        {
            return !(left == right);
        }

    }

    /// <summary>
    /// 頂點
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Vertex<T> : IComparable where T : IComparable
    {
        public T Value { get; set; }

        public int CompareTo(object obj)
        {
            Vertex<T> nd = obj as Vertex<T>;
            if (nd == null)
            {
                throw new InvalidOperationException("Can not compare Node and other type of class.");
            }
            return this.Value.CompareTo(nd.Value);
        }

        public static bool operator >(Vertex<T> left, Vertex<T> right)
        {
            return left.CompareTo(right) > 0;
        }
        public static bool operator <(Vertex<T> left, Vertex<T> right)
        {
            return left.CompareTo(right) < 0;
        }

    }

    public class Graphic<T, K>
        where T : IComparable
        where K : IConvertible, IComparable
    {
        public List<Edge<T, K>> Edges { get; set; }
        public List<Vertex<T>> Vertexes { get; set; }


        public Graphic()
        {
            Edges = new List<Edge<T, K>>();
            Vertexes = new List<Vertex<T>>();
        }

        public List<Edge<T, K>> GetEdgesByNode(Vertex<T> node)
        {
            List<Edge<T, K>> result = this.Edges.Where(e => e.LeftNode == node || e.RightNode == node).ToList();
            return result;
        }
    }
}
