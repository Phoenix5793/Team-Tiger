using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestUserStore
    {
        [TestMethod]
        public void Admin_Account_Exists()
        {
            UserStore userStore = new UserStore();
            User expectedUser = userStore.FindById("admin");

            Assert.IsTrue(expectedUser.UserName == "admin");
        }
    }
}
