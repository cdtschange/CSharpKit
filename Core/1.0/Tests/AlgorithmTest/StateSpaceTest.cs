using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Algorithm.StateSpace;

namespace AlgorithmTest
{
    public class MSModel : IModel<MSModel>
    {
        public int[] Value { get; set; }

        public MSModel(int[] value)
        {
            this.Value = value;
        }

        public MSModel()
        {
        }

        public override bool Equals(object obj)
        {
            MSModel ms = obj as MSModel;
            if (ms == null)
            {
                return false;
            }
            bool result = true;
            for (int i = 0; i < this.Value.Length; i++)
            {
                result &= (this.Value[i] == ms.Value[i]);
            }
            return result;
        }

        public MSModel MoveTo(MSModel vector)
        {
            MSModel model = new MSModel();
            model.Value = new int[this.Value.Length];

            for (int i = 0; i < this.Value.Length; i++)
            {
                model.Value[i] = this.Value[i] + vector.Value[i];
            }

            return model;
        }

        public bool Validate(A<MSModel> action)
        {
            MSModel model = new MSModel();
            model.Value = new int[this.Value.Length];

            for (int i = 0; i < this.Value.Length; i++)
            {
                model.Value[i] = this.Value[i] + action.Vector.Value[i];
            }
            if (model.Value[2] == this.Value[2])
            {
                return false;
            }
            if (model.Value[0] == 0 || model.Value[0] == 3)
            {
                return true;
            }
            if (model.Value[0] == model.Value[1])
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            string str = "(";
            for (int i = 0; i < this.Value.Length; i++)
            {
                str += this.Value[i] + ",";
            }
            str = str.Substring(0, str.Length - 1);
            str += ")";
            return str;
        }
    }


    /// <summary>
    /// StateSpaceTest 的摘要说明
    /// </summary>
    [TestClass]
    public class StateSpaceTest
    {
        public StateSpaceTest()
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
        public void MissionaryAndSavageTest()
        {
            D<MSModel> d = new D<MSModel>();

            S<MSModel> status0 = new S<MSModel>(new MSModel(new int[] { 3, 3, 1 }));
            d.Status0 = status0;
            List<S<MSModel>> status = new List<S<MSModel>>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        status.Add(new S<MSModel>(new MSModel(new int[] { i, j, k })));
                    }
                }
            }
            d.Status = status;
            List<S<MSModel>> goals = new List<S<MSModel>> { new S<MSModel>(new MSModel(new int[] { 0, 0, 0 })) };
            d.Goals = goals;

            List<int[]> actionList = new List<int[]>()
            {
                new int[] { 0, 1, 1 },
                new int[] { 0, 2, 1 },
                new int[] { 1, 0, 1 },
                new int[] { 1, 1, 1 },
                new int[] { 2, 0, 1 },
                new int[] { 0, -1, -1 },
                new int[] { 0, -2, -1 },
                new int[] { -1, 0, -1 },
                new int[] { -1, -1, -1 },
                new int[] { -2, 0, -1 }
            };


            List<A<MSModel>> actions = new List<A<MSModel>>();
            actionList.ForEach(a =>
            {
                var ac = new A<MSModel>(new MSModel(a));
                ac.SArea = status;
                actions.Add(ac);
            });

            d.Actions = actions;

            var list = d.Do();
            StringBuilder sb = new StringBuilder();
            int m = 1;
            list.ForEach(l =>
            {
                sb.AppendLine(string.Format("Solution:", m++));

                l.ForEach(il =>
                {
                    if (il.ParentAction != null)
                    {
                        sb.AppendLine(string.Format("Action:{0}", il.ParentAction.Vector));
                    }
                    sb.AppendLine(string.Format("S:{0}", il.Model));
                });
                sb.AppendLine("Solution Finish!");

            });
            string result = sb.ToString();
        }
    }
}
