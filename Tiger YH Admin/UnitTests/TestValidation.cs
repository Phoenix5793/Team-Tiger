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

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Period_Is_Comma()
        {
            string input = "foo@example,net";
            bool expected = false;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        public void IsValidEmail__Has_Subdomain()
        {
            string input = "foo@bar.example.net";
            bool expected = true;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Periods()
        {
            string input = "foo.bar@example.net";
            bool expected = true;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Plus()
        {
            string input = "foo+spamtrap@example.net";
            bool expected = true;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Has_Periods_And_Plus()
        {
            string input = "foo.bar+spamtrap@example.net";
            bool expected = true;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Invalid_With_Space_In_Username()
        {
            string input = "foo bar@example.net";
            bool expected = false;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void IsValidEmail__Invalid_With_Space_In_Domain()
        {
            string input = "foobar@exam ple.net";
            bool expected = false;

            bool actual = Validation.IsValidEmail(input);

            Assert.AreEqual(expected, actual);
        }
    }
}
