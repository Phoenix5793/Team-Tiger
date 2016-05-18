using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestUser
    {
        private User _testUser;

        [TestInitialize]
        public void Initialize()
        {
            _testUser = new User
            {
                FirstName = "Testy",
                LastName = "McTestFace",
                Password = "abc123",
                SSN = "8080980",
                PhoneNumber = "123321",
                UserLevel = UserLevel.Student
            };
        }

        [TestMethod]
        public void User_Has_Full_Name()
        {
            string expected = "Testy McTestFace";
            string actual = _testUser.FullName();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasLevel__Has_Correct_Level()
        {
            UserLevel input = UserLevel.Student;
            bool expected = true;

            bool actual = _testUser.HasLevel(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasLevel__Has_Wrong_Level()
        {
            UserLevel input = UserLevel.Admin;
            bool expected = false;

            bool actual = _testUser.HasLevel(input);

            Assert.AreEqual(expected, actual);
        }

    }
}
