using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestUserStore
    {
        private User _testUser;
        private List<User> _userList;
        private UserStore _userStore;

        [TestInitialize]
        public void Initialize()
        {
            _testUser = new User
            {
                FirstName = "Testy",
                LastName = "McTestFace",
                Password = "abc123",
                PhoneNumber = "123321",
                SSN = "321123",
                UserName = "testuser",
                UserLevel = UserLevel.Student
            };

            _userList = new List<User>();
            _userList.Add(_testUser);
            _userStore = new UserStore(_userList);
        }

        [TestMethod]
        public void User_Login_Correct_Credentials()
        {
            User expectedUser = _userStore.LoginUser("testuser", "abc123");

            string expected = "McTestFace";
            string actual = expectedUser.LastName;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void User_Login_Incorrect_Credentials()
        {
            User expectedUser = _userStore.LoginUser("testuser", "hjhljkhjkl");

            Assert.IsNull(expectedUser);
        }

        [TestMethod]
        public void User_Login_User_Does_Not_Exist()
        {
            User expectedUser = _userStore.LoginUser("zvzxvxbz", "hjhljkhjkl");

            Assert.IsNull(expectedUser);
        }

        [TestMethod]
        public void HasUser__Existing_User()
        {
            string input = _testUser.UserName;
            bool expected = true;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasUser__Nonexisting_User()
        {
            string input = "hjkhkjhkl";
            bool expected = false;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasUser__Username_Is_Substring_Of_Existing_User()
        {
            string input = "test";
            bool expected = false;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
