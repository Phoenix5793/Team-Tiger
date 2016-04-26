using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestUser
    {
        public User SetupTestUser()
        {
            return new User
            {
                FirstName = "Testy",
                Surname = "McTestFace",
                Password = "abc123",
                SSN = "8080980",
                PhoneNumber = "123321",
                UserLevel = UserLevel.Student
            };
        }

        [TestMethod]
        public void User_Has_Full_Name()
        {
            User testUser = SetupTestUser();

            string expected = "Testy McTestFace";
            string actual = testUser.FullName();

            Assert.AreEqual(expected, actual);
        }
    }
}
