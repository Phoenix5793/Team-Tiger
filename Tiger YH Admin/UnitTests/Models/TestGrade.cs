using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestGrade
    {
        private Grade _testGrade;
        private Grade _testGradeGoal;

        [TestInitialize]
        public void Initialize()
        {
            _testGrade = new Grade
            {
                CourseId = "kurs",
                StudentId = "user",
                Result = GradeLevel.G
            };

            _testGradeGoal = new Grade
            {
                CourseId = "kurs",
                StudentId = "user",
                CourseGoal = "1",
                Result = GradeLevel.VG
            };
        }

        [TestMethod]
        public void Grade__Has_Default_Grade()
        {
            Grade input = new Grade();
            GradeLevel expected = GradeLevel.IG;

            GradeLevel actual = input.Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Grade__Course_Grade_Has_Valid_GradeId()
        {
            Grade input = _testGrade;
            string expected = "kurs:user";

            string actual = input.GradeId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Grade__Goal_Grade_Has_Valid_GradeId()
        {
            Grade input = _testGradeGoal;
            string expected = "kurs:user:1";

            string actual = input.GradeId;

            Assert.AreEqual(expected, actual);
        }
    }
}
