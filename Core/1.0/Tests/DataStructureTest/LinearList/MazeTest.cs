using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.DataStructure.LinearList;

namespace DataStructureTest.LinearList
{
    /// <summary>
    /// Summary description for MazeTest
    /// </summary>
    [TestClass]
    public class MazeTest
    {
        public MazeTest()
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
        public void FindWayTest()
        {
            Maze maze = new Maze(4, 4);
            maze.Ways = new Maze.Cell[] 
            {
                new Maze.Cell(0,0),
                new Maze.Cell(1,0),
                new Maze.Cell(2,0),
                new Maze.Cell(3,1),
                new Maze.Cell(0,2),
                new Maze.Cell(3,2),
                new Maze.Cell(3,3),

                new Maze.Cell(0,1),
                new Maze.Cell(1,1),
                new Maze.Cell(2,1),
                new Maze.Cell(2,2),
                new Maze.Cell(1,2),
                new Maze.Cell(1,3),
                new Maze.Cell(2,3),
            };
            Assert.AreEqual(true, maze.FindWay());
        }
    }
}
