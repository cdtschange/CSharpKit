using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Facet;
using Cdts.Algorithm.Graphics;

namespace AlgorithmTest.Facet
{
    /// <summary>
    /// ConvexHullTest 的摘要说明
    /// </summary>
    [TestClass]
    public class ConvexHullTest
    {
        public ConvexHullTest()
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
        private List<Vector2> points;
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        [TestInitialize()]
        public void MyTestInitialize()
        {
            points = new List<Vector2>()
            {
                new Vector2(0,0),
                new Vector2(-5,-2),
                new Vector2(-2,-1),
                new Vector2(-6,0),
                new Vector2(-3.5,1),
                new Vector2(-4.5,1.5),
                new Vector2(-2.5,-5),
                new Vector2(1,-2.5),
                new Vector2(2.5,0.5),
                new Vector2(-2.2,2.2),
                new Vector2(2,1),
                new Vector2(1,2),
            };
        }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void GrahamScanTest()
        {
            Graphic<Vector2, double> graphic = ConvexHull.GrahamScan(points);

            Assert.AreEqual(8, graphic.Vertexes.Count);
            Assert.AreEqual(8, graphic.Edges.Count);
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[6]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[6]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[7]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[8]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[11]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[9]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[5]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[3]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[1]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[6]));


        }

        [TestMethod]
        public void GiftWrappingScanTest()
        {
            Graphic<Vector2, double> graphic = ConvexHull.GiftWrappingScan(points);

            Assert.AreEqual(8, graphic.Vertexes.Count);
            Assert.AreEqual(8, graphic.Edges.Count);
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[6]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[6]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[7]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[8]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[11]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[9]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[5]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[3]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[1]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[6]));
        }

        [TestMethod]
        public void QuickHullScanTest()
        {
            Graphic<Vector2, double> graphic = ConvexHull.QuickHullScan(points);

            Assert.AreEqual(8, graphic.Vertexes.Count);
            Assert.AreEqual(8, graphic.Edges.Count);
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[6]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Vertexes.Select(v => v.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[6]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[7]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[7]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[8]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[8]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[11]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[11]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[9]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[9]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[5]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[5]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[3]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[3]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[1]));
            Assert.AreEqual(true, graphic.Edges.Select(e => e.LeftNode.Value).Contains(points[1]) && graphic.Edges.Select(e => e.RightNode.Value).Contains(points[6]));
        }

    }
}
