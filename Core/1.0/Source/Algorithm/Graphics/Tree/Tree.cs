using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Cdts.Algorithm.Graphics.Tree
{
    /// <summary>
    /// 树
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Tree<T> : ICollection<T> where T : IComparable, new()
    {
        /// <summary>
        /// 树遍历模式
        /// </summary>
        public enum TraversalMode
        {
            /// <summary>
            /// 深度优先搜索
            /// </summary>
            DepthFirstSearchOrder,
            /// <summary>
            /// 广度优先搜索
            /// </summary>
            BreadthFirstSearchOrder
        }
        /// <summary>
        /// 构造树
        /// </summary>
        public Tree()
        {
            this.Root = null;
            this.Size = 0;
            this.TraversalOrder = TraversalMode.BreadthFirstSearchOrder;
        }

        #region Public Properties
        /// <summary>
        /// 根节点
        /// </summary>
        public virtual TreeNode<T> Root { get; set; }
        /// <summary>
        /// 节点数
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 树是否只读
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 树的节点数
        /// </summary>
        public virtual int Count
        {
            get { return this.Size; }
        }
        /// <summary>
        /// 树的遍历模式
        /// </summary>
        public virtual TraversalMode TraversalOrder { get; set; }
        #endregion

        #region Public Methods
        /// <summary>
        /// 找寻值等于value的节点
        /// </summary>
        /// <param name="Value">节点值</param>
        /// <returns>返回找到节点</returns>
        public virtual TreeNode<T> Find(T value)
        {
            return Find(value, this.Root);
        }

        private TreeNode<T> Find(T value, TreeNode<T> node)
        {
            TreeNode<T> result = null;
            if (node != null)
            {
                int compare = value.CompareTo(node.Value);
                if (compare == 0) return node; //找到节点
                if (node.Children != null)
                {
                    node.Children.ForEach(i =>
                    {
                        var f = Find(value, i);
                        if (f != null)
                        {
                            result = f;
                            return;
                        }
                    });
                }
            }
            return result;
        }

        public virtual List<TreeNode<T>> FindAllChildren(T value)
        {
            TreeNode<T> node = Find(value);
            return FindAllChildren(node);
        }
        private List<TreeNode<T>> FindAllChildren(TreeNode<T> node)
        {
            if (node == null)
            {
                throw new InvalidOperationException("Can not find the node!");
            }
            List<TreeNode<T>> result = new List<TreeNode<T>>();
            if (node.Children != null)
            {
                result.AddRange(node.Children);
                node.Children.ForEach(i =>
                {
                    result.AddRange(FindAllChildren(i));
                });
            }
            return result;
        }

        /// <summary>
        /// 树中是否包含节点值
        /// </summary>
        public virtual bool Contains(T value)
        {
            return (this.Find(value) != null);
        }
        /// <summary>
        /// 获取树高度
        /// </summary>
        public virtual int GetHeight()
        {
            return this.GetHeight(this.Root);
        }
        /// <summary>
        /// 获取节点为根的子树的高度
        /// </summary>
        public virtual int GetHeight(T value)
        {
            //找到值所对应的树节点
            TreeNode<T> valueNode = this.Find(value);
            if (value != null)
                return this.GetHeight(valueNode);
            else
                return 0;
        }
        /// <summary>
        /// 获取节点为根的子树的高度
        /// </summary>
        public virtual int GetHeight(TreeNode<T> startNode)
        {
            if (startNode == null)
                return 0;
            else //递归
            {
                int h = 0;
                if (startNode.Children == null)
                {
                    return 1;
                }
                if (startNode.Children.Count(c => c != null) == 0)
                {
                    return 1;
                }
                startNode.Children.ForEach(n =>
                {
                    h = Math.Max(h, GetHeight(n));
                });
                return 1 + h;
            }
        }
        /// <summary>
        /// 获取节点的深度
        /// </summary>
        public virtual int GetDepth(T value)
        {
            TreeNode<T> node = this.Find(value);
            return this.GetDepth(node);
        }
        /// <summary>
        /// 获取节点的深度
        /// </summary>
        public virtual int GetDepth(TreeNode<T> startNode)
        {
            int depth = 0;

            if (startNode == null)
                return depth;

            TreeNode<T> parentNode = startNode.Parent;
            while (parentNode != null)
            {
                depth++;
                parentNode = parentNode.Parent; //向上搜索父节点
            }

            return depth;
        }

        /// <summary>
        /// 获取枚举遍历模式
        /// </summary>
        public virtual IEnumerator<T> GetEnumerator()
        {
            switch (this.TraversalOrder)
            {
                case TraversalMode.DepthFirstSearchOrder:
                    return GetDepthFirstSearchOrderEnumerator();
                case TraversalMode.BreadthFirstSearchOrder:
                    return GetBreadthFirstSearchOrderEnumerator();
                default:
                    return GetDepthFirstSearchOrderEnumerator();
            }
        }
        /// <summary>
        /// 枚举遍历模式
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        /// <summary>
        /// 深度优先搜索模式
        /// </summary>
        public virtual IEnumerator<T> GetDepthFirstSearchOrderEnumerator()
        {
            return new DepthFirstSearchOrderEnumerator(this);
        }
        /// <summary>
        /// 广度优先搜索模式
        /// </summary>
        public virtual IEnumerator<T> GetBreadthFirstSearchOrderEnumerator()
        {
            return new BreadthFirstSearchOrderEnumerator(this);
        }
        /// <summary>
        /// 深度优先搜索
        /// </summary>
        internal class DepthFirstSearchOrderEnumerator : IEnumerator<T>
        {
            private TreeNode<T> current;
            private Tree<T> tree;
            internal Queue<TreeNode<T>> traverseQueue;

            public DepthFirstSearchOrderEnumerator(Tree<T> tree)
            {
                this.tree = tree;
                //建立队列
                this.traverseQueue = new Queue<TreeNode<T>>();
                this.DepthFirstSearchOrderTraverse(this.tree.Root);
            }
            /// <summary>
            /// 深度优先搜索递归法
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchOrderTraverse(TreeNode<T> node)
            {
                if (node != null)
                {
                    this.traverseQueue.Enqueue(node);
                    if (node.Children != null)
                    {
                        node.Children.ForEach(n =>
                        {
                            DepthFirstSearchOrderTraverse(n);
                        });
                    }
                }
            }

            public T Current
            {
                get { return this.current.Value; }
            }
            object IEnumerator.Current
            {
                get { return Current; }
            }
            public void Dispose()
            {
                this.current = null;
                this.tree = null;
            }
            public void Reset()
            {
                this.current = null;
            }
            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    this.current = this.traverseQueue.Dequeue();
                else
                    this.current = null;

                return (this.current != null);
            }
        }
        /// <summary>
        /// 广度优先搜索
        /// </summary>
        internal class BreadthFirstSearchOrderEnumerator : IEnumerator<T>
        {
            private TreeNode<T> current;
            private Tree<T> tree;
            internal Queue<TreeNode<T>> traverseQueue;

            public BreadthFirstSearchOrderEnumerator(Tree<T> tree)
            {
                this.tree = tree;
                //建立队列
                this.traverseQueue = new Queue<TreeNode<T>>();
                this.BreadthFirstSearchOrderTraverse(this.tree.Root);
            }
            /// <summary>
            /// 广度优先搜索递归法
            /// </summary>
            /// <param name="node">节点</param>
            private void BreadthFirstSearchOrderTraverse(TreeNode<T> node)
            {
                Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
                if (node != null)
                {
                    queue.Enqueue(node);
                    while (queue.Count > 0)
                    {
                        node = queue.Dequeue();
                        this.traverseQueue.Enqueue(node);
                        if (node.Children != null)
                        {
                            node.Children.ForEach(n =>
                            {
                                if (n != null)
                                {
                                    queue.Enqueue(n);
                                }
                            });
                        }
                    }
                }
            }

            public T Current
            {
                get { return this.current.Value; }
            }
            object IEnumerator.Current
            {
                get { return Current; }
            }
            public void Dispose()
            {
                this.current = null;
                this.tree = null;
            }
            public void Reset()
            {
                this.current = null;
            }
            public bool MoveNext()
            {
                if (traverseQueue.Count > 0)
                    this.current = this.traverseQueue.Dequeue();
                else
                    this.current = null;

                return (this.current != null);
            }
        }

        /// <summary>
        /// 用遍历模式拷贝树中的节点到数组
        /// </summary>
        public virtual void CopyTo(T[] array)
        {
            this.CopyTo(array, 0);
        }
        /// <summary>
        /// 用遍历模式拷贝树中的节点到数组
        /// </summary>
        public virtual void CopyTo(T[] array, int startIndex)
        {
            IEnumerator<T> enumerator = this.GetEnumerator();

            for (int i = startIndex; i < array.Length; i++)
            {
                if (enumerator.MoveNext())
                    array[i] = enumerator.Current;
                else
                    break;
            }
        }

        #endregion


        public virtual void Add(T item)
        {
            if (this.Root == null)
            {
                this.Root = new TreeNode<T>(item);
                this.Size++;
            }
            else
            {
                Add(item, this.Root.Value);
            }
        }
        public virtual void Add(T item, T parent)
        {
            TreeNode<T> parentNode = Find(parent);
            if (parentNode == null)
            {
                throw new InvalidOperationException("Parent node is null!");
            }
            TreeNode<T> node = new TreeNode<T>(item);
            if (parentNode.Children == null)
            {
                parentNode.Children = new List<TreeNode<T>>();
            }
            if (parentNode.Children.Contains(node))
            {
                throw new InvalidOperationException("Parent node already has the child noe!");
            }
            parentNode.Children.Add(node);
            node.Parent = parentNode;
            this.Size++;
        }

        public virtual void Clear()
        {
            List<T> list = new List<T>();
            foreach (T item in this)
            {
                list.Add(item);
            }
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Remove(list[i]);
            }
        }

        public virtual bool Remove(T item)
        {
            TreeNode<T> node = Find(item);
            return Remove(node);
        }
        private bool Remove(TreeNode<T> node)
        {
            if (node == null)
            {
                return false;
            }
            if (node.Parent == null && this.Root == node)
            {
                if (this.Size > 1)
                {
                    Clear();
                    return true;
                }
                else
                {
                    this.Root = null;
                    this.Size--;
                    return true;
                }
            }
            var list = FindAllChildren(node);
            for (int i = list.Count - 1; i >= 0; i--)
            {
                Remove(list[i]);
            }
            node.Parent.Children.Remove(node);
            node.Parent = null;
            this.Size--;
            return true;
        }
    }

    /// <summary>
    /// 树结点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeNode<T> : IComparable where T : IComparable, new()
    {
        /// <summary>
        /// 节点值
        /// </summary>
        public virtual T Value { get; set; }
        /// <summary>
        /// 孩子节点
        /// </summary>
        public virtual List<TreeNode<T>> Children { get; set; }
        /// <summary>
        /// 父节点
        /// </summary>
        public virtual TreeNode<T> Parent { get; set; }
        /// <summary>
        /// 所属树
        /// </summary>
        public virtual Tree<T> Tree { get; set; }

        #region Constructors
        public TreeNode() { }
        public TreeNode(T value) : this(value, null, null) { }
        public TreeNode(T value, List<TreeNode<T>> children, TreeNode<T> parent)
        {
            this.Value = value;
            this.Children = children;
            this.Parent = parent;
        }

        #endregion
        /// <summary>
        /// 是否是叶子节点
        /// </summary>
        public virtual bool IsLeaf
        {
            get { return this.ChildCount == 0; }
        }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public virtual int ChildCount
        {
            get
            {
                if (this.Children == null)
                {
                    return 0;
                }
                return this.Children.Count(n => n != null);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is TreeNode<T>)
            {
                // Convert object to Vector3
                TreeNode<T> other = (TreeNode<T>)obj;

                // Check for equality
                return this.Value.CompareTo(other.Value) == 0;
            }
            else
            {
                return false;
            }
        }
        public int CompareTo(object obj)
        {
            if (obj is TreeNode<T>)
            {
                if (this < (TreeNode<T>)obj)
                {
                    return -1;
                }
                else if (this > (TreeNode<T>)obj)
                {
                    return 1;
                }
                return 0;
            }
            else
            {
                throw new ArgumentException("Compare type is not tree node");
            }
        }

        public static bool operator <(TreeNode<T> v1, TreeNode<T> v2)
        {
            return v1.Value.CompareTo(v2.Value) < 0;
        }
        public static bool operator <=(TreeNode<T> v1, TreeNode<T> v2)
        {
            return v1.Value.CompareTo(v2.Value) <= 0;
        }
        public static bool operator >(TreeNode<T> v1, TreeNode<T> v2)
        {
            return v1.Value.CompareTo(v2.Value) > 0;
        }
        public static bool operator >=(TreeNode<T> v1, TreeNode<T> v2)
        {
            return v1.Value.CompareTo(v2.Value) >= 0;
        }
    }
}
