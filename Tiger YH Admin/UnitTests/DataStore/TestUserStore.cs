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
        private User _testUser;
        private List<User> _userList;
        private UserStore _userStore;

        [TestInitialize]
        public void Initialize()
        {
            _testUser = new User
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
            _userList.Add(_testUser);
            _userStore = new UserStore {DataSet = _userList};
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

        [TestMethod]
        public void User_Login_User_Does_Not_Exist()
        {
            User expectedUser = _userStore.LoginUser("zvzxvxbz", "hjhljkhjkl");

            Assert.IsNull(expectedUser);
        }

        [TestMethod]
        public void UserExists__Existing_User()
        {
            string input = _testUser.UserName;
            bool expected = true;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserExists__Nonexisting_User()
        {
            string input = "hjkhkjhkl";
            bool expected = false;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void UserExists__Username_Is_Substring_Of_Existing_User()
        {
            string input = "test";
            bool expected = false;

            bool actual = _userStore.HasUser(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasLevel__User_Exists__Has_Correct_Level()
        {
            string inputName = _testUser.UserName;
            UserLevel inputLevel = UserLevel.Student;
            bool expected = true;

            bool actual = _userStore.HasLevel(inputName, inputLevel);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasLevel__User_Exists__Has_Incorrect_Level()
        {
            string inputName = _testUser.UserName;
            UserLevel inputLevel = UserLevel.Admin;
            bool expected = false;

            bool actual = _userStore.HasLevel(inputName, inputLevel);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasLevel__User_Does_Not_Exist()
        {
            string inputName = "test";
            UserLevel inputLevel = UserLevel.Admin;
            bool expected = false;

            bool actual = _userStore.HasLevel(inputName, inputLevel);

            Assert.AreEqual(expected, actual);
        }



    }
}
