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
        private UserStore _userStore;

        [TestInitialize]
        public void Initialize()
        {
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

            _userList = new List<User>();
            _userList.Add(user);
            _userStore = new UserStore {DataSet = _userList};
        }

        [TestMethod]
        public void Can_Find_User()
        {
            User user = _userStore.FindById("testuser");

            string expected = "testuser";
            string actual = user.UserName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Returns_Null_If_User_Not_Found()
        {
            User expectedUser = _userStore.FindById("qwerty");

            Assert.IsNull(expectedUser);
        }

        [TestMethod]
        public void User_Login_Correct_Credentials()
        {
            User expectedUser = _userStore.LoginUser("testuser", "abc123");

            string expected = "McTestFace";
            string actual = expectedUser.Surname;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void User_Login_Incorrect_Credentials()
        {
            User expectedUser = _userStore.LoginUser("testuser", "hjhljkhjkl");

            Assert.IsNull(expectedUser);
        }
    }
}
