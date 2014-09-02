using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Graphics.Tree;

namespace AlgorithmTest.Graphics.Tree
{
    /// <summary>
    /// TreeTest 的摘要说明
    /// </summary>
    [TestClass]
    public class TreeTest
    {
        public TreeTest()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        private Tree<int> tree;

        #region 附加测试特性
        //
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            tree = new Tree<int>();
        }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void EntityTest()
        {
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);
            tree.Add(5, 2);
            tree.Add(6, 2);
            tree.Add(7, 2);
            tree.Add(8, 3);
            tree.Add(9, 3);
            tree.Add(10, 4);

            Assert.AreEqual(10, tree.Size);
            Assert.AreEqual(3, tree.GetHeight());
            Assert.AreEqual(2, tree.GetHeight(2));
            Assert.AreEqual(2, tree.GetDepth(5));
            Assert.AreEqual(9, tree.Find(9).Value);
            Assert.AreEqual(true, tree.Contains(9));
            Assert.AreEqual(3, tree.Find(2).ChildCount);
            Assert.AreEqual(2, tree.Find(3).ChildCount);
            Assert.AreEqual(1, tree.Find(4).ChildCount);
            Assert.AreEqual(false, tree.Find(2).IsLeaf);
            Assert.AreEqual(true, tree.Find(9).IsLeaf);

            tree.Remove(10);
            Assert.AreEqual(9, tree.Size);
            Assert.AreEqual(true, tree.Find(4).IsLeaf);
            tree.Remove(3);
            Assert.AreEqual(6, tree.Size);
            Assert.AreEqual(2, tree.Find(1).ChildCount);
            tree.Clear();
            Assert.AreEqual(0, tree.Size);
            Assert.IsNull(tree.Root);

        }
    }
}
