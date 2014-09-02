using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Facet;

namespace AlgorithmTest.Facet
{
    /// <summary>
    /// Vector2Test 的摘要说明
    /// </summary>
    [TestClass]
    public class Vector2Test
    {
        public Vector2Test()
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
        public void PropertyTest()
        {
            Vector2 v1 = new Vector2(1, 2);
            Vector2 v2 = new Vector2(-2, 5);
            Assert.AreEqual(1, v1.X);
            Assert.AreEqual(2, v1.Y);
            Assert.AreEqual(9, v1.CrossProduct(v2));
            Assert.AreEqual(-1, new Vector2(-1, 1).CrossProduct(new Vector2(1, 0)));
            Assert.AreEqual(0, Math.Round(new Vector2(1, 0).Rotate(Math.PI / 2).X, 10));
            Assert.AreEqual(1, Math.Round(new Vector2(1, 0).Rotate(Math.PI / 2).Y, 10));
            Assert.AreEqual(3 * Math.PI / 2, new Vector2(0, 1).RotateAngle(new Vector2(1, 0)));
            Assert.AreEqual(1 * Math.PI / 2, new Vector2(1, 0).RotateAngle(new Vector2(0, 1)));
            Assert.AreEqual(v1, new Vector(new double[] { 1, 2 }).ToVector2());

        }
    }
}
