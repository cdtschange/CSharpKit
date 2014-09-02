using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.Facet;

namespace AlgorithmTest.Facet
{
    /// <summary>
    /// VectorTest 的摘要说明
    /// </summary>
    [TestClass]
    public class VectorTest
    {
        public VectorTest()
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
            Vector v1 = new Vector(new double[] { 1, 0, 2, 2 });
            Vector v2 = new Vector(new double[] { 3, -1, 2, 4 });
            Assert.AreEqual(1, v1[0]);
            Assert.AreEqual(0, v1[1]);
            Assert.AreEqual(4, v1.Dimension);
            Assert.AreEqual(9, v1.SqrMagnitude);
            Assert.AreEqual(3, v1.Magnitude);
            Assert.AreEqual(new Vector(new double[] { 1 / 3.0, 0, 2 / 3.0, 2 / 3.0 }), Vector.Normalize(v1));
            Assert.AreEqual(new Vector(new double[] { 1, 0, 4, 4 }), Vector.Power(v1, 2));
            Assert.AreEqual(15, v1.DotProduct(v2));
            Assert.AreEqual(3, v1.Distance(v2));
            Assert.AreEqual(Math.Round(Math.PI / 4, 6), Math.Round(new Vector(new double[] { 1, 0 }).Angle(new Vector(new double[] { 1, 1 })), 6));
            Assert.AreEqual(Math.Round(Math.PI / 4, 6), Math.Round(new Vector(new double[] { 1, 0 }).Angle(new Vector(new double[] { 1, -1 })), 6));
            Assert.AreEqual(0, v1.Angle(v1));
            Assert.AreEqual(Math.PI, new Vector(new double[] { 1, 0 }).Angle(new Vector(new double[] { -1, 0 })));
            Assert.AreEqual(v2, Vector.Max(v1, v2));
            Assert.AreEqual(v1, Vector.Min(v1, v2));
            Assert.AreEqual(new Vector(new double[] { 2, -0.5, 2, 3 }), v1.Interpolate(v2, 0.5));
            Assert.AreEqual("(1,0,2,2)", v1.ToString());
            Assert.AreEqual(true, v1.Equals(new Vector(new double[] { 1, 0, 2, 2 })));
            Assert.AreEqual(false, v1.IsUnitVector());
            Assert.AreEqual(true, new Vector(new double[] { 1, 0 }).IsUnitVector());
            Assert.AreEqual(false, new Vector(new double[] { 1, 0 }).IsBackFace(new Vector(new double[] { 1, 1 })));
            Assert.AreEqual(true, new Vector(new double[] { 1, 0 }).IsBackFace(new Vector(new double[] { -1, 1 })));
            Assert.AreEqual(false, v1.IsPerpendicular(v2));
            Assert.AreEqual(true, new Vector(new double[] { 1, 0 }).IsPerpendicular(new Vector(new double[] { 0, 1 })));
            Assert.AreEqual(new Vector(new double[] { 1, 0 }), Vector.GetAxis(2, 0));
            Assert.AreEqual(new Vector(new double[] { 0, 1 }), Vector.GetAxis(2, 1));
            //Operator
            Assert.AreEqual(new Vector(new double[] { 4, -1, 4, 6 }), v1 + v2);
            Assert.AreEqual(new Vector(new double[] { -2, 1, 0, -2 }), v1 - v2);
            Assert.AreEqual(new Vector(new double[] { 2, 0, 4, 4 }), v1 * 2);
            Assert.AreEqual(new Vector(new double[] { 0.5, 0, 1, 1 }), v1 / 2);
            Assert.AreEqual(new Vector(new double[] { -1, 0, -2, -2 }), -v1);
            Assert.AreEqual(true, v1 < v2);
            Assert.AreEqual(false, v1 > v2);
            Assert.AreEqual(true, v1 <= v2);
            Assert.AreEqual(false, v1 >= v2);
            Assert.AreEqual(true, v1 != v2);
            Assert.AreEqual(false, v1 == v2);

        }
    }
}
