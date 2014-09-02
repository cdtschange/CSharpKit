using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Cdts.Algorithm.Graphics.Tree
{
    public class BinaryTree<T> : Tree<T>, ICollection<T> where T : IComparable, new()
    {
        /// <summary>
        /// 树遍历模式
        /// </summary>
        public enum BinaryTraversalMode
        {
            /// <summary>
            /// 先序深度优先搜索
            /// </summary>
            DepthFirstSearchPreOrder,
            /// <summary>
            /// 中序深度优先搜索
            /// </summary>
            DepthFirstSearchInOrder,
            /// <summary>
            /// 后序深度优先搜索
            /// </summary>
            DepthFirstSearchPostOrder,
            /// <summary>
            /// 广度优先搜索
            /// </summary>
            BreadthFirstSearchOrder
        }
        /// <summary>
        /// 构造二叉树
        /// </summary>
        public BinaryTree()
            : base()
        {
            this.TraversalOrder = BinaryTraversalMode.DepthFirstSearchInOrder;
        }

        /// <summary>
        /// 树的遍历模式
        /// </summary>
        public new BinaryTraversalMode TraversalOrder { get; set; }

        #region Public Methods
        /// <summary>
        /// 清空树
        /// </summary>
        public override void Clear()
        {
            //先移除孩子节点，再移除父节点
            //后序遍历模式
            IEnumerator<T> enumerator = GetDepthFirstSearchPostOrderEnumerator();
            while (enumerator.MoveNext())
            {
                this.Remove(enumerator.Current);
            }
            enumerator.Dispose();
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        public override void Add(T value)
        {
            BinaryTreeNode<T> node = new BinaryTreeNode<T>(value);
            this.Add(node);
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        public virtual void Add(BinaryTreeNode<T> node)
        {
            if (this.Root == null) //空树
            {
                this.Root = node;
                node.Tree = this;
                this.Size++;
            }
            else
            {
                if (node.Parent == null)
                    node.Parent = this.Root; //从根节点开始

                while (true)
                {
                    //如果节点值小于父节点，插入左边
                    bool insertLeftSide = node.Value.CompareTo(node.Parent.Value) < 0;

                    if (insertLeftSide) //插入左边
                    {
                        if (((BinaryTreeNode<T>)node.Parent).LeftChild != null)
                        {
                            // 转向左孩子节点
                            node.Parent = ((BinaryTreeNode<T>)node.Parent).LeftChild;
                            continue;
                        }
                        else
                        {
                            ((BinaryTreeNode<T>)node.Parent).LeftChild = node; //插入左边
                            this.Size++;
                            node.Tree = this;
                            break;
                        }
                    }
                    else //插入右边
                    {
                        if (((BinaryTreeNode<T>)node.Parent).RightChild != null)
                        {
                            // 转向右孩子节点
                            node.Parent = ((BinaryTreeNode<T>)node.Parent).RightChild;
                            continue;
                        }
                        else
                        {
                            ((BinaryTreeNode<T>)node.Parent).RightChild = node; //插入右边
                            this.Size++;
                            node.Tree = this;
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 找寻值等于value的节点
        /// </summary>
        /// <param name="Value">节点值</param>
        /// <returns>返回找到节点</returns>
        public new BinaryTreeNode<T> Find(T value)
        {
            BinaryTreeNode<T> iterator = (BinaryTreeNode<T>)this.Root; //从根节点开始
            while (iterator != null)
            {
                int compare = value.CompareTo(iterator.Value);
                if (compare == 0) return iterator; //找到节点
                iterator = compare < 0 ? iterator.LeftChild : iterator.RightChild;
            }
            return null; //没找到节点
        }
        /// <summary>
        /// 树中是否包含节点值
        /// </summary>
        public override bool Contains(T value)
        {
            return (this.Find(value) != null);
        }
        /// <summary>
        /// 移除节点
        /// </summary>
        public override bool Remove(T value)
        {
            BinaryTreeNode<T> removeNode = Find(value);

            return this.Remove(removeNode);
        }
        /// <summary>
        /// 移除节点
        /// </summary>>
        public virtual bool Remove(BinaryTreeNode<T> removeNode)
        {
            if (removeNode == null || removeNode.Tree != this)
                return false; //值不为空且是该树上的节点

            //是否是根节点
            bool wasRoot = (removeNode == this.Root);

            if (this.Count == 1)
            {
                this.Root = null; //如果树只有根节点
                removeNode.Tree = null;

                this.Size--; //节点数-1
            }
            else if (removeNode.IsLeaf) //Case 1: 叶子结点
            {
                if (removeNode.IsLeftChild)
                    ((BinaryTreeNode<T>)removeNode.Parent).LeftChild = null;
                else
                    ((BinaryTreeNode<T>)removeNode.Parent).RightChild = null;

                removeNode.Tree = null;
                removeNode.Parent = null;

                Size--; //节点数-1
            }
            else if (removeNode.ChildCount == 1) //Case 2: 有一个孩子节点
            {
                if (removeNode.HasLeftChild) //有左孩子节点
                {
                    //更新孩子节点
                    removeNode.LeftChild.Parent = removeNode.Parent;

                    if (wasRoot)
                        this.Root = removeNode.LeftChild; //如果是移除根节点，更新根节点
                    else
                    {
                        if (removeNode.IsLeftChild) //更新父节点
                            ((BinaryTreeNode<T>)removeNode.Parent).LeftChild = removeNode.LeftChild;
                        else
                            ((BinaryTreeNode<T>)removeNode.Parent).RightChild = removeNode.LeftChild;
                    }
                }
                else //有右孩子节点
                {
                    //更新孩子节点
                    removeNode.RightChild.Parent = removeNode.Parent;

                    if (wasRoot)
                        this.Root = removeNode.RightChild; //如果是移除根节点，更新根节点
                    else
                    {
                        if (removeNode.IsLeftChild) //更新父节点
                            ((BinaryTreeNode<T>)removeNode.Parent).LeftChild = removeNode.RightChild;
                        else
                            ((BinaryTreeNode<T>)removeNode.Parent).RightChild = removeNode.RightChild;
                    }
                }

                removeNode.Tree = null;
                removeNode.Parent = null;
                removeNode.LeftChild = null;
                removeNode.RightChild = null;

                Size--; //节点数-1
            }
            else //Case 3: 有两个孩子结点
            {
                //找到左子树的最右子孙节点
                BinaryTreeNode<T> successorNode = removeNode.LeftChild;
                while (successorNode.RightChild != null)
                {
                    successorNode = successorNode.RightChild;
                }

                removeNode.Value = successorNode.Value; //替换节点值

                this.Remove(successorNode); //删除被替换的节点
            }

            return true;
        }

        /// <summary>
        /// 获取枚举遍历模式
        /// </summary>
        public override IEnumerator<T> GetEnumerator()
        {
            switch (this.TraversalOrder)
            {
                case BinaryTraversalMode.DepthFirstSearchPreOrder:
                    return GetDepthFirstSearchPreOrderEnumerator();
                case BinaryTraversalMode.DepthFirstSearchInOrder:
                    return GetDepthFirstSearchInOrderEnumerator();
                case BinaryTraversalMode.DepthFirstSearchPostOrder:
                    return GetDepthFirstSearchPostOrderEnumerator();
                case BinaryTraversalMode.BreadthFirstSearchOrder:
                    return GetBreadthFirstSearchOrderEnumerator();
                default:
                    return GetDepthFirstSearchInOrderEnumerator();
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
        /// VLR先序深度优先搜索模式
        /// </summary>
        public virtual IEnumerator<T> GetDepthFirstSearchPreOrderEnumerator()
        {
            return new DepthFirstSearchPreOrderEnumerator(this);
        }
        /// <summary>
        /// LVR中序深度优先搜索模式
        /// </summary>
        public virtual IEnumerator<T> GetDepthFirstSearchInOrderEnumerator()
        {
            return new DepthFirstSearchInOrderEnumerator(this);
        }
        /// <summary>
        /// LRV后序深度优先搜索模式
        /// </summary>
        public virtual IEnumerator<T> GetDepthFirstSearchPostOrderEnumerator()
        {
            return new DepthFirstSearchPostOrderEnumerator(this);
        }
        /// <summary>
        /// 先序深度优先搜索二叉树
        /// </summary>
        internal class DepthFirstSearchPreOrderEnumerator : IEnumerator<T>
        {
            private BinaryTreeNode<T> current;
            private BinaryTree<T> tree;
            internal Queue<BinaryTreeNode<T>> traverseQueue;

            public DepthFirstSearchPreOrderEnumerator(BinaryTree<T> tree)
            {
                this.tree = tree;
                //建立队列
                this.traverseQueue = new Queue<BinaryTreeNode<T>>();
                this.DepthFirstSearchPreStackOrderTraverse((BinaryTreeNode<T>)this.tree.Root);
            }
            /// <summary>
            /// 先序深度优先搜索二叉树递归法
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchPreOrderTraverse(BinaryTreeNode<T> node)
            {
                if (node != null)
                {
                    this.traverseQueue.Enqueue(node);
                    DepthFirstSearchPreOrderTraverse(node.LeftChild);
                    DepthFirstSearchPreOrderTraverse(node.RightChild);
                }
            }
            /// <summary>
            /// 先序深度优先搜索二叉树堆栈法
            /// 1.先访问完当前节点（node）的所有左孩子，
            /// 在访问某个节点操作完成后立即将该节点进栈。
            /// 2.当访问完一个节点的所有左孩子后，进行出栈操作，
            /// 如果出栈节点没有右孩子，则继续出栈操作，否则按
            /// 第1点访问该出栈节点的右孩子节点
            /// </summary>
            /// <param name="node">节点</param>
            public void DepthFirstSearchPreStackOrderTraverse(BinaryTreeNode<T> node)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                while (node != null || stack.Count > 0)
                {
                    while (node != null)
                    {
                        this.traverseQueue.Enqueue(node);
                        stack.Push(node);
                        node = node.LeftChild;
                    }
                    if (stack.Count > 0)
                    {
                        node = stack.Pop();
                        node = node.RightChild;
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
        /// 中序深度优先搜索二叉树
        /// </summary>
        internal class DepthFirstSearchInOrderEnumerator : IEnumerator<T>
        {
            private BinaryTreeNode<T> current;
            private BinaryTree<T> tree;
            internal Queue<BinaryTreeNode<T>> traverseQueue;

            public DepthFirstSearchInOrderEnumerator(BinaryTree<T> tree)
            {
                this.tree = tree;
                //建立队列
                this.traverseQueue = new Queue<BinaryTreeNode<T>>();
                this.DepthFirstSearchInStackOrderTraverse((BinaryTreeNode<T>)this.tree.Root);
            }
            /// <summary>
            /// 中序深度优先搜索二叉树递归法
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchInOrderTraverse(BinaryTreeNode<T> node)
            {
                if (node != null)
                {
                    DepthFirstSearchInOrderTraverse(node.LeftChild);
                    this.traverseQueue.Enqueue(node);
                    DepthFirstSearchInOrderTraverse(node.RightChild);
                }
            }
            /// <summary>
            /// 中序深度优先搜索二叉树堆栈法
            /// 1.先访问完当前节点（node）的所有左孩子，
            /// 在访问某个节点操作完成后立即将该节点进栈。
            /// 2.当访问完一个节点的所有左孩子后，进行出栈操作，
            /// 如果出栈节点没有右孩子，则继续出栈操作，否则按
            /// 第1点访问该出栈节点的右孩子节点
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchInStackOrderTraverse(BinaryTreeNode<T> node)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                while (node != null || stack.Count > 0)
                {
                    while (node != null)
                    {
                        stack.Push(node);
                        node = node.LeftChild;
                    }
                    if (stack.Count > 0)
                    {
                        node = stack.Pop();
                        this.traverseQueue.Enqueue(node);
                        node = node.RightChild;
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
        /// 后序深度优先搜索二叉树
        /// </summary>
        internal class DepthFirstSearchPostOrderEnumerator : IEnumerator<T>
        {
            private BinaryTreeNode<T> current;
            private BinaryTree<T> tree;
            internal Queue<BinaryTreeNode<T>> traverseQueue;
            internal Stack<BinaryTreeNode<T>> traverseStack;

            public DepthFirstSearchPostOrderEnumerator(BinaryTree<T> tree)
            {
                this.tree = tree;
                //建立队列
                this.traverseStack = new Stack<BinaryTreeNode<T>>();
                this.traverseQueue = new Queue<BinaryTreeNode<T>>();
                this.DepthFirstSearchPostMirrorStackOrderTraverse((BinaryTreeNode<T>)this.tree.Root);
            }
            /// <summary>
            /// 后序深度优先搜索二叉树递归法
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchPostOrderTraverse(BinaryTreeNode<T> node)
            {
                if (node != null)
                {
                    DepthFirstSearchPostOrderTraverse(node.LeftChild);
                    DepthFirstSearchPostOrderTraverse(node.RightChild);
                    this.traverseQueue.Enqueue(node);
                }
            }
            /// <summary>
            /// 后序深度优先搜索二叉树双堆栈法（T(n)最差，O(n)持平）
            /// 1.先把当前节点（node）进左栈，右孩子进右栈，
            /// 然后以同样方法继续访问该节点的左节点。
            /// 2.当访问完一个节点的所有左孩子后，进行左、右出栈操作，
            /// 如果右出栈节点不为空，把刚从右出栈的节点进左栈。把一个空值进右栈，
            /// 然后按第1点访问刚从右出栈的右孩子节点
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchPostDoubleStackOrderTraverse(BinaryTreeNode<T> node)
            {
                Stack<BinaryTreeNode<T>> lStack = new Stack<BinaryTreeNode<T>>();
                Stack<BinaryTreeNode<T>> rStack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> right;
                do
                {
                    while (node != null)
                    {
                        right = node.RightChild;
                        lStack.Push(node);
                        rStack.Push(right);
                        node = node.LeftChild;
                    }
                    node = lStack.Pop();
                    right = rStack.Pop();
                    if (right == null)
                    {
                        this.traverseQueue.Enqueue(node);
                    }
                    else
                    {
                        lStack.Push(node);
                        rStack.Push(null);
                    }
                    node = right;
                }
                while (lStack.Count > 0 || rStack.Count > 0);
            }
            /// <summary>
            /// 后序深度优先搜索二叉树堆栈法（T(n)持平，O(n)最好）
            /// 1.先访问完当前节点（node）的所有左孩子，
            /// 在访问某个节点操作完成后立即将该节点进栈。
            /// 2.当访问完一个节点的所有左孩子后，进行出栈操作，
            /// 3.如果之前出栈节点是栈顶元素的右孩子，
            /// 或栈顶元素的右孩子为空，则出栈。
            /// 4.不满足以上条件则按第1点访问该栈顶元素的右孩子。
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchPostStackOrderTraverse(BinaryTreeNode<T> node)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                BinaryTreeNode<T> prevNode = node;
                while (node != null || stack.Count > 0)
                {
                    while (node != null)
                    {
                        stack.Push(node);
                        node = node.LeftChild;
                    }
                    if (stack.Count > 0)
                    {
                        BinaryTreeNode<T> temp = stack.Peek().RightChild;
                        if (temp == null || temp == prevNode)
                        {
                            node = stack.Pop();
                            this.traverseQueue.Enqueue(node);
                            prevNode = node;
                            node = null;
                        }
                        else
                        {
                            node = temp;
                        }
                    }
                }
            }
            /// <summary>
            /// 后序深度优先搜索二叉树镜像堆栈法（T(n)持平，O(n)最差）
            /// 1.同先序遍历堆栈法一样，只不过把左右孩子交换，
            /// 2.输出的序列需要做镜像。
            /// 如果是返回数列而不是直接输出，此方法最好！
            /// </summary>
            /// <param name="node">节点</param>
            private void DepthFirstSearchPostMirrorStackOrderTraverse(BinaryTreeNode<T> node)
            {
                Stack<BinaryTreeNode<T>> stack = new Stack<BinaryTreeNode<T>>();
                while (node != null || stack.Count > 0)
                {
                    while (node != null)
                    {
                        this.traverseStack.Push(node);
                        stack.Push(node);
                        node = node.RightChild;
                    }
                    if (stack.Count > 0)
                    {
                        node = stack.Pop();
                        node = node.LeftChild;
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
                if (traverseStack.Count > 0)//traverseQueue
                    this.current = this.traverseStack.Pop();//traverseQueue
                else
                    this.current = null;

                return (this.current != null);
            }
        }

        #endregion
    }

    /// <summary>
    /// 二叉树节点
    /// </summary>
    public class BinaryTreeNode<T> : TreeNode<T> where T : IComparable, new()
    {
        #region Constructors
        public BinaryTreeNode() { }
        public BinaryTreeNode(T value) : this(value, null, null, null) { }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right, BinaryTreeNode<T> parent)
        {
            this.Value = value;
            this.Children = new List<TreeNode<T>>();
            this.LeftChild = left;
            this.RightChild = right;
            this.Parent = parent;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 左孩子节点
        /// </summary>
        public BinaryTreeNode<T> LeftChild
        {
            get
            {
                return this.Children[0] as BinaryTreeNode<T>;
            }
            set
            {
                if (this.Children.Count > 0)
                {
                    this.Children[0] = value;
                }
                else
                {
                    this.Children.Add(value);
                }
            }
        }
        /// <summary>
        /// 右孩子节点
        /// </summary>
        public BinaryTreeNode<T> RightChild
        {
            get
            {
                return this.Children[1] as BinaryTreeNode<T>;
            }
            set
            {
                if (this.Children.Count > 1)
                {
                    this.Children[1] = value;
                }
                else if (this.Children.Count == 1)
                {
                    this.Children.Add(value);
                }
                else
                {
                    this.Children.Add(null);
                    this.Children.Add(value);
                }
            }
        }

        /// <summary>
        /// 是否是左孩子
        /// </summary>
        public virtual bool IsLeftChild
        {
            get { return this.Parent != null && ((BinaryTreeNode<T>)this.Parent).LeftChild == this; }
        }
        /// <summary>
        /// 是否是右孩子
        /// </summary>
        public virtual bool IsRightChild
        {
            get { return this.Parent != null && ((BinaryTreeNode<T>)this.Parent).RightChild == this; }
        }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public override int ChildCount
        {
            get
            {
                int count = 0;

                if (this.LeftChild != null)
                    count++;

                if (this.RightChild != null)
                    count++;

                return count;
            }
        }
        /// <summary>
        /// 是否有左孩子
        /// </summary>
        public virtual bool HasLeftChild
        {
            get { return (this.LeftChild != null); }
        }
        /// <summary>
        /// 是否有右孩子
        /// </summary>
        public virtual bool HasRightChild
        {
            get { return (this.RightChild != null); }
        }
        #endregion
    }

}
