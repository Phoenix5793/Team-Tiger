using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestGrade
    {
        [TestMethod]
        public void Grade__Has_Default_Grade()
        {
            Grade input = new Grade();
            GradeLevel expected = GradeLevel.G;

            GradeLevel actual = input.Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
