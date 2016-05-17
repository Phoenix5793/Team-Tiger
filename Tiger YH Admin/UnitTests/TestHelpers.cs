using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;

namespace UnitTests
{
    [TestClass]
    public class TestHelpers
    {
        [TestMethod]
        public void Truncate__Does_Not_Truncate_Short_String()
        {
            string input = "Sju sjösjuka sjömän";
            string expected = "Sju sjösjuka sjömän";

            string actual = input.Truncate(25);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void Truncate__Shortens_String()
        {
            string input = "Sju sjösjuka sjömän";
            string expected = "Sju sjö...";

            string actual = input.Truncate(10);

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected.Length, actual.Length);
        }

        [TestMethod]
        public void Truncate__Shortens_String_With_Characters()
        {
            string input = "Sju sjösjuka sjömän";
            string expected = "Sju sjö!";

            string actual = input.Truncate(8, "!");

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected.Length, actual.Length);
        }
    }
}
