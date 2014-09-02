using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm;

namespace AlgorithmTest
{
    /// <summary>
    /// SearchTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SearchTest
    {
        public SearchTest()
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
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void BinarySearchTest()
        {
            int[] arr = new int[] { 1, 2, 3, 4, 7, 9, 17 };
            Assert.AreEqual(1, Search<int>.BinarySearch(arr, 1));
            Assert.AreEqual(2, Search<int>.BinarySearch(arr, 2));
            Assert.AreEqual(3, Search<int>.BinarySearch(arr, 3));
            Assert.AreEqual(4, Search<int>.BinarySearch(arr, 4));
            Assert.AreEqual(5, Search<int>.BinarySearch(arr, 7));
            Assert.AreEqual(6, Search<int>.BinarySearch(arr, 9));
            Assert.AreEqual(7, Search<int>.BinarySearch(arr, 17));
            Assert.AreEqual(0, Search<int>.BinarySearch(arr, 8));
            Assert.AreEqual(0, Search<int>.BinarySearch(arr, -1));
            Assert.AreEqual(0, Search<int>.BinarySearch(arr, 99));
        }
    }
}
