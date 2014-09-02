using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Utility;

namespace UtilityTest
{
    /// <summary>
    /// Summary description for CounterTest
    /// </summary>
    [TestClass]
    public class FunctoinsTest
    {
        public FunctoinsTest()
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
        public void FSumcountTest()
        {
            Assert.AreEqual(5ul, Cdts.Utility.Functions.FSumcount(1, 12));
            Assert.AreEqual(57ul, Cdts.Utility.Functions.FSumcount(1, 123));
            Assert.AreEqual(2ul, Cdts.Utility.Functions.FSumcount(5, 15));
            Assert.AreEqual(29ul, Cdts.Utility.Functions.FSumcount(5, 153));
            Assert.AreEqual(1ul, Cdts.Utility.Functions.FSumcount(0, 15));
            Assert.AreEqual(11ul, Cdts.Utility.Functions.FSumcount(0, 100));
            Assert.AreEqual(25ul, Cdts.Utility.Functions.FSumcount(0, 153));
            Assert.AreEqual(217ul, Cdts.Utility.Functions.FSumcount(0, 1015));
        }
        [TestMethod]
        public void FibonacciTest()
        {
            Assert.AreEqual(1, Cdts.Utility.Functions.Fibonacci(1));
            Assert.AreEqual(1, Cdts.Utility.Functions.Fibonacci(2));
            Assert.AreEqual(2, Cdts.Utility.Functions.Fibonacci(3));
            Assert.AreEqual(3, Cdts.Utility.Functions.Fibonacci(4));
            Assert.AreEqual(5, Cdts.Utility.Functions.Fibonacci(5));
            Assert.AreEqual(8, Cdts.Utility.Functions.Fibonacci(6));
        }
    }
}
