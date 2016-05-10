using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;

namespace UnitTests
{
    [TestClass]
    public class TestValidation
    {
        [TestMethod]
        public void IsValidEmail__Typical_Address()
        {
            string input = "foo@example.net";
            bool expected = true;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Period_Is_Comma()
        {
            string input = "foo@example,net";
            bool expected = false;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Subdomain()
        {
            string input = "foo@bar.example.net";
            bool expected = true;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Periods()
        {
            string input = "foo.bar@example.net";
            bool expected = true;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Plus()
        {
            string input = "foo+spamtrap@example.net";
            bool expected = true;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Periods_And_Plus()
        {
            string input = "foo.bar+spamtrap@example.net";
            bool expected = true;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Invalid_With_Space_In_Username()
        {
            string input = "foo bar@example.net";
            bool expected = false;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Invalid_With_Space_In_Domain()
        {
            string input = "foobar@exam ple.net";
            bool expected = false;

            bool actual = input.IsValidEmail();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidLuhn__Valid_Number()
        {
            string input = "4170663214699432";
            bool expected = true;

            bool actual = input.IsValidLuhn();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidLuhn__Invalid_Number()
        {
            string input = "4170663214699439";
            bool expected = false;

            bool actual = input.IsValidLuhn();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsAllDigits__Is_All_Digits()
        {
            string input = "123456789";
            bool expected = true;

            bool actual = input.IsAllDigits();

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsAllDigits__Has_Non_Digits()
        {
            string input = "1234q56789";
            bool expected = false;

            bool actual = input.IsAllDigits();

            Assert.AreEqual(expected, actual);
        }

    }
}
