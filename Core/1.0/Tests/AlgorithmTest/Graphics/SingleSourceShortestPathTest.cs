using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Graphics;
using Cdts.Algorithm.Graphics.Tree;

namespace AlgorithmTest.Graphics.Tree
{
    /// <summary>
    /// SingleSourceShortestPathTest 的摘要说明
    /// </summary>
    [TestClass]
    public class SingleSourceShortestPathTest
    {
        public SingleSourceShortestPathTest()
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

        private Graphic<int, double> graphic;

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
            graphic = new Graphic<int, double>();
            graphic.Vertexes = new List<Vertex<int>>()
            {
                new Vertex<int>(){ Value=1},
                new Vertex<int>(){ Value=2},
                new Vertex<int>(){ Value=3},
                new Vertex<int>(){ Value=4},
                new Vertex<int>(){ Value=5},
                new Vertex<int>(){ Value=6},
                new Vertex<int>(){ Value=7},
                new Vertex<int>(){ Value=8},
                new Vertex<int>(){ Value=9},
                new Vertex<int>(){ Value=10}
            };
            graphic.Edges = new List<Edge<int, double>>()
            {
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[0],RightNode=graphic.Vertexes[1],Weight=3},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[0],RightNode=graphic.Vertexes[3],Weight=6},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[0],RightNode=graphic.Vertexes[9],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[1],RightNode=graphic.Vertexes[2],Weight=2},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[1],RightNode=graphic.Vertexes[3],Weight=4},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[1],RightNode=graphic.Vertexes[8],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[1],RightNode=graphic.Vertexes[9],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[2],RightNode=graphic.Vertexes[3],Weight=2},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[2],RightNode=graphic.Vertexes[4],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[2],RightNode=graphic.Vertexes[8],Weight=8},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[3],RightNode=graphic.Vertexes[4],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[4],RightNode=graphic.Vertexes[5],Weight=4},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[4],RightNode=graphic.Vertexes[7],Weight=5},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[4],RightNode=graphic.Vertexes[8],Weight=7},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[5],RightNode=graphic.Vertexes[6],Weight=4},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[5],RightNode=graphic.Vertexes[7],Weight=1},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[6],RightNode=graphic.Vertexes[7],Weight=3},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[6],RightNode=graphic.Vertexes[8],Weight=18},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[6],RightNode=graphic.Vertexes[9],Weight=10},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[7],RightNode=graphic.Vertexes[8],Weight=9},
                new Edge<int,double>(){ LeftNode=graphic.Vertexes[8],RightNode=graphic.Vertexes[9],Weight=8}
            };
        }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void DijkstraTest()
        {
            Dictionary<Vertex<int>, List<Edge<int, double>>> paths = new Dictionary<Vertex<int>, List<Edge<int, double>>>();
            Graphic<int, double> newG = SingleSourceShortestPath<int, double>.Dijkstra(graphic, graphic.Vertexes[0], out paths);
            Assert.AreEqual(9, newG.Edges.Count);
            Assert.AreEqual(10, newG.Vertexes.Count);
            Assert.AreEqual(3, paths[graphic.Vertexes[1]].Sum(e=>e.Weight));
            Assert.AreEqual(5, paths[graphic.Vertexes[2]].Sum(e => e.Weight));
            Assert.AreEqual(6, paths[graphic.Vertexes[3]].Sum(e => e.Weight));
            Assert.AreEqual(14, paths[graphic.Vertexes[4]].Sum(e => e.Weight));
            Assert.AreEqual(18, paths[graphic.Vertexes[5]].Sum(e => e.Weight));
            Assert.AreEqual(19, paths[graphic.Vertexes[6]].Sum(e => e.Weight));
            Assert.AreEqual(19, paths[graphic.Vertexes[7]].Sum(e => e.Weight));
            Assert.AreEqual(12, paths[graphic.Vertexes[8]].Sum(e => e.Weight));
            Assert.AreEqual(9, paths[graphic.Vertexes[9]].Sum(e => e.Weight));
        }
    }
}
