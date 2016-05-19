using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestUserStoreExtensions
    {
        private User _testUserTeacher;
        private User _testUserStudent;
        private UserStore _testUserStore;

        [TestInitialize]
        public void Initialize()
        {
            _testUserTeacher = new User
            {
                UserName = "teacher",
                UserLevel = UserLevel.Teacher
            };
            _testUserStudent = new User
            {
                UserName = "student",
                UserLevel = UserLevel.Student
            };

            var users = new List<User> {_testUserStudent, _testUserTeacher};

            _testUserStore = new UserStore(users);
        }

        [TestMethod]
        public void IsTeacher__Finds_Teacher()
        {
            var expectedLevel = UserLevel.Teacher;
            string expected = "teacher";

            IEnumerable<User> teachers = _testUserStore.All().IsTeacher();
            User actualUser = teachers.Single(u => u.UserLevel == expectedLevel);
            string actual = actualUser.UserName;

            Assert.AreEqual(expected, actual);
        }
    }
}
