using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.DataStructure.LinearList;

namespace DataStructureTest.LinearList
{
    /// <summary>
    /// Summary description for PolynomialTest
    /// </summary>
    [TestClass]
    public class PolynomialTest
    {
        public PolynomialTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
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

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void PolynomialNodeEqualTest()
        {
            Polynomial p = new Polynomial();
            PolynomialNode n1 = new PolynomialNode(1, 2);
            PolynomialNode n2 = new PolynomialNode(2, 3);
            PolynomialNode n3 = new PolynomialNode(3, 3);
            p.CreatePolyn(new List<PolynomialNode>() { n1, n2, n3 });
            Assert.AreEqual(2, p.Count);
        }

        [TestMethod]
        public void PolynomialNodeOperationTest()
        {
            PolynomialNode n1 = new PolynomialNode(1, 2);
            PolynomialNode n2 = new PolynomialNode(2, 2);
            PolynomialNode n3 = new PolynomialNode(2, 4);
            Assert.AreEqual(new PolynomialNode(3, 2), n1 + n2);
            Assert.AreEqual(new PolynomialNode(-1, 2), n1 - n2);
            Assert.AreEqual(new PolynomialNode(2, 4), n1 * n2);
            Assert.AreEqual(new PolynomialNode(4, 6), n2 * n3);
            Assert.AreEqual(new PolynomialNode(-1, 2), -n1);
            Assert.AreEqual(new PolynomialNode(10, 2), n2 * 5);
            Assert.AreEqual(new PolynomialNode(-10, 2), n2 * -5);
            Assert.AreEqual(new PolynomialNode(1, 2), n2 / 2);
            Assert.AreEqual(new PolynomialNode(1, -2), 2 / n2);
            Assert.AreEqual(new PolynomialNode(0.5, -2), PolynomialNode.ReciprocalPolynNode(n2));
        }

        [TestMethod]
        public void PolynomialCreateTest()
        {
            Polynomial p = new Polynomial();
            PolynomialNode n1 = new PolynomialNode(2, 3);
            PolynomialNode n2 = new PolynomialNode(1, 1);
            PolynomialNode n3 = new PolynomialNode(3, 5);
            PolynomialNode n4 = new PolynomialNode(5, 5);
            PolynomialNode n5 = new PolynomialNode(-5, 1);
            p.CreatePolyn(new List<PolynomialNode>() { n1, n2, n3, n4, n5 });
            Assert.AreEqual(new PolynomialNode(-4, 1), p.FindPolynNode(1));
            Assert.AreEqual(new PolynomialNode(2, 3), p.FindPolynNode(3));
            Assert.AreEqual(new PolynomialNode(8, 5), p.FindPolynNode(5));
        }

        [TestMethod]
        public void PolynomialAddNodeTest()
        {
            Polynomial p = new Polynomial();
            PolynomialNode n1 = new PolynomialNode(1, 7);
            PolynomialNode n2 = new PolynomialNode(5, 2);
            p.CreatePolyn(new List<PolynomialNode>() { n1, n2 });
            p.AddPolynNode(new PolynomialNode(3, 2));
            Assert.AreEqual(new PolynomialNode(8, 2), p.FindPolynNode(2));
            p.AddPolynNode(new PolynomialNode(-1, 7));
            Assert.AreEqual(null, p.FindPolynNode(7));
        }
        [TestMethod]
        public void PolynomialOperation()
        {
            Polynomial p1 = new Polynomial();
            PolynomialNode n1 = new PolynomialNode(2, 3);
            PolynomialNode n2 = new PolynomialNode(1, 1);
            PolynomialNode n3 = new PolynomialNode(3, 5);
            PolynomialNode n5 = new PolynomialNode(-5, 7);
            PolynomialNode n4 = new PolynomialNode(5, 6);
            p1.CreatePolyn(new List<PolynomialNode>() { n1, n2, n3, n4, n5 });
            Polynomial p2 = new Polynomial();
            PolynomialNode n6 = new PolynomialNode(1, 7);
            PolynomialNode n7 = new PolynomialNode(5, 2);
            p2.CreatePolyn(new List<PolynomialNode>() { n6, n7 });
            Polynomial p3 = p1 * 2;
            Assert.AreEqual(new PolynomialNode(4, 3), p3.FindPolynNode(3));
            p3 = p3 + p2;
            Assert.AreEqual(new PolynomialNode(-9, 7), p3.FindPolynNode(7));
            p3 = p1 * p2;
            Assert.AreEqual(new PolynomialNode(5, 3), p3.FindPolynNode(3));
            Assert.AreEqual(new PolynomialNode(10, 5), p3.FindPolynNode(5));
            Assert.AreEqual(new PolynomialNode(15, 7), p3.FindPolynNode(7));
            Assert.AreEqual(new PolynomialNode(26, 8), p3.FindPolynNode(8));
            Assert.AreEqual(new PolynomialNode(-25, 9), p3.FindPolynNode(9));
            Assert.AreEqual(new PolynomialNode(2, 10), p3.FindPolynNode(10));
            Assert.AreEqual(new PolynomialNode(3, 12), p3.FindPolynNode(12));
            Assert.AreEqual(new PolynomialNode(5, 13), p3.FindPolynNode(13));
            Assert.AreEqual(new PolynomialNode(-5, 14), p3.FindPolynNode(14));
            p3 = -p1;
            Assert.AreEqual(new PolynomialNode(-2, 3), p3.FindPolynNode(3));
            p3 = p2 - p1;
            Assert.AreEqual(new PolynomialNode(6, 7), p3.FindPolynNode(7));
        }
    }
}
