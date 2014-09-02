using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cdts.Utility.Mail;

namespace UtilityTest.Mail.MailSets
{
    /// <summary>
    /// Mail_YeahnetTest 的摘要说明
    /// </summary>
    [TestClass]
    public class Mail_YeahnetTest
    {
        public Mail_YeahnetTest()
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

        private string account = "cdts_test1@yeah.net";
        private string password = "test123";
        private string newPassword = "test124";
        private string forwardAccount = "cdts_test1@163.com";
        private string forwardPassword = "test123";

        [TestMethod]
        public void RegisterTest()
        {
            MailBase mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            bool isValid = mail.CheckRegistAccount();
            if (isValid)
            {
                bool result = mail.RegistAccount();
                Assert.AreEqual(true, result);
                Assert.AreEqual("", mail.ErrorMessage);
            }
            else
            {
                Assert.AreNotEqual("", mail.ErrorMessage);
            }
        }

        [TestMethod]
        public void LoginTest()
        {
            MailBase mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            bool result = mail.Login();
            Assert.AreEqual(true, result);
            Assert.AreEqual("", mail.ErrorMessage);
        }

        [TestMethod]
        public void ChangePasswordTest()
        {
            MailBase mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            bool result = mail.ChangePassword(newPassword);
            Assert.AreEqual(true, result);
            mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, newPassword));
            result = mail.Login();
            Assert.AreEqual(true, result);
            result = mail.ChangePassword(password);
            Assert.AreEqual(true, result);
            mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            result = mail.Login();
            Assert.AreEqual(true, result);
            Assert.AreEqual("", mail.ErrorMessage);
        }

        [TestMethod]
        public void OpenPop3Test()
        {
            MailBase mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            bool result = mail.OpenPOP3();
            Assert.AreEqual(true, result);
            Assert.AreEqual("", mail.ErrorMessage);
        }

        [TestMethod]
        public void AutoForwardTest()
        {
            MailBase mail = MailFactory.CreateMailBase(MailFactory.CreateMailModel(account, password));
            bool result = mail.SetAutoForward(forwardAccount);
            Assert.AreEqual(true, result);
            result = mail.ActiveCertification(MailFactory.CreateMailModel(forwardAccount, forwardPassword));
            Assert.AreEqual(true, result);
            result = mail.CancelAutoForward();
            Assert.AreEqual(true, result);
            Assert.AreEqual("", mail.ErrorMessage);
        }
    }
}
