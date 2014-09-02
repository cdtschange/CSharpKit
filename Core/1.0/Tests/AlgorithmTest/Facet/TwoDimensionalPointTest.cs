using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Facet;

namespace AlgorithmTest.Facet
{
    /// <summary>
    /// TwoDimensionalPointTest 的摘要说明
    /// </summary>
    [TestClass]
    public class TwoDimensionalPointTest
    {
        public TwoDimensionalPointTest()
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
        public void FindMaximumPointsTest()
        {
            List<Vector2> vectors = new List<Vector2>()
            {
                new Vector2(1,4),
                new Vector2(1,3),
                new Vector2(1,1),
                new Vector2(2,3),
                new Vector2(2,0),
                new Vector2(3,2),
                new Vector2(3,1),
                new Vector2(4,0),
                new Vector2(5,1)
            };

            var list = TwoDimensionalPoint.FindMaximumPoints(vectors);
            Assert.AreEqual(4, list.Count);
            Assert.AreEqual(true, list.Contains(new Vector2(1, 4)));
            Assert.AreEqual(true, list.Contains(new Vector2(2, 3)));
            Assert.AreEqual(true, list.Contains(new Vector2(3, 2)));
            Assert.AreEqual(true, list.Contains(new Vector2(5, 1)));
        }

        [TestMethod]
        public void FindClosestPointsTest()
        {
            List<Vector2> vectors = new List<Vector2>()
            {
                new Vector2(1.5,4),
                new Vector2(1,3.5),
                new Vector2(1,1),
                new Vector2(2,3),
                new Vector2(2,0),
                new Vector2(3,2),
                new Vector2(3,1),
                new Vector2(4,0),
                new Vector2(5,1)
            };

            var list = TwoDimensionalPoint.FindClosestPoints(vectors);
            Assert.AreEqual(2, list.Count);
            Assert.AreEqual(true, list.Contains(new Vector2(1.5, 4)));
            Assert.AreEqual(true, list.Contains(new Vector2(1, 3.5)));
        }
    }
}
