using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestDataStore
    {
        private User _testUser;
        private UserStore _testStore;

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

            List<User> userList = new List<User> {_testUser};

            _testStore = new UserStore(userList);
        }

        [TestMethod]
        public void FindById__Finds_Object()
        {
            string input = "testuser";

            User actual = _testStore.FindById(input);

            Assert.IsNotNull(actual);
        }

        [TestMethod]
        public void FindById__Returns_Null_If_Not_Found()
        {
            string input = "qwerty";

            User actual = _testStore.FindById(input);

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void Remove__Removes_Object()
        {
            string input = "testuser";

            _testStore.Remove(input);
            User actual = _testStore.FindById(input);

            Assert.IsNull(actual);
        }
    }
}
