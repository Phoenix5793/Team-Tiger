using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestUserStore
    {
        private List<User> _userList;

        [TestInitialize]
        public void Initialize()
        {
            _userList = new List<User>();

            User user = new User
            {
                FirstName = "Testy",
                Surname = "McTestFace",
                Password = "abc123",
                PhoneNumber = "123321",
                SSN = "321123",
                UserName = "testuser",
                UserLevel = UserLevel.Student
            };

            _userList.Add(user);
        }

        [TestMethod]
        public void Can_Find_User()
        {
            UserStore userStore = new UserStore {DataSet = _userList};
            User expectedUser = userStore.FindById("testuser");

            Assert.IsTrue(expectedUser.UserName == "testuser");
        }

        [TestMethod]
        public void Returns_Null_If_User_Not_Found()
        {
            UserStore userStore = new UserStore {DataSet = _userList};
            User expectedUser = userStore.FindById("qwerty");

            Assert.IsNull(expectedUser);
        }
    }
}
