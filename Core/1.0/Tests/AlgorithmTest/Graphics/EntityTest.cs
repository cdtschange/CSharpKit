using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Graphics;

namespace AlgorithmTest.Graphics
{
    /// <summary>
    /// EntityTest 的摘要说明
    /// </summary>
    [TestClass]
    public class EntityTest
    {
        public EntityTest()
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
        public void EntityClassTest()
        {
            Graphic<int, double> g = new Graphic<int, double>();
            g.Vertexes = new List<Vertex<int>>()
            {
                new Vertex<int>(){ Value=1},
                new Vertex<int>(){ Value=2},
                new Vertex<int>(){ Value=3},
                new Vertex<int>(){ Value=4},
                new Vertex<int>(){ Value=5}
            };
            g.Edges = new List<Edge<int, double>>()
            {
                new Edge<int,double>(){ LeftNode=g.Vertexes[0],RightNode=g.Vertexes[1],Weight=1},
                new Edge<int,double>(){ LeftNode=g.Vertexes[0],RightNode=g.Vertexes[3],Weight=1},
                new Edge<int,double>(){ LeftNode=g.Vertexes[1],RightNode=g.Vertexes[2],Weight=1},
                new Edge<int,double>(){ LeftNode=g.Vertexes[1],RightNode=g.Vertexes[4],Weight=1},
                new Edge<int,double>(){ LeftNode=g.Vertexes[2],RightNode=g.Vertexes[3],Weight=1},
                new Edge<int,double>(){ LeftNode=g.Vertexes[2],RightNode=g.Vertexes[4],Weight=1}
            };
            Assert.AreEqual(2, g.GetEdgesByNode(g.Vertexes[0]).Count);
            Assert.AreEqual(3, g.GetEdgesByNode(g.Vertexes[1]).Count);
            Assert.AreEqual(3, g.GetEdgesByNode(g.Vertexes[2]).Count);
            Assert.AreEqual(2, g.GetEdgesByNode(g.Vertexes[3]).Count);
            Assert.AreEqual(2, g.GetEdgesByNode(g.Vertexes[4]).Count);
        }
    }
}
