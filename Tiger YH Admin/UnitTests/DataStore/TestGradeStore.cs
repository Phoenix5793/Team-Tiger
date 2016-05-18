using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.DataStore;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestGradeStore
    {
        private GradeStore _testStore;
        private Grade _testGrade;
        private List<Grade> _testGradeList;

        [TestInitialize]
        public void Initialize()
        {
            _testGrade = new Grade
            {
                CourseId = "oop1",
                StudentId = "adad",
                Result = GradeLevel.VG
            };

            _testGradeList = new List<Grade> {_testGrade};
            _testStore = new GradeStore(_testGradeList);
        }

        [TestMethod]
        public void FindById__Finds_Grade()
        {
            string input = "oop1:adad";
            GradeLevel expectedGradeLevel = GradeLevel.VG;

            Grade actualGrade = _testStore.FindById(input);
            GradeLevel actualGradeLevel = actualGrade.Result;

            Assert.IsNotNull(actualGrade);
            Assert.AreEqual(expectedGradeLevel, actualGradeLevel);
        }

        [TestMethod]
        public void GradeStudent__Can_Grade_Student()
        {
            User inputStudent = new User
            {
                UserName = "adad"
            };
            string inputGradeId = "oop1:adad";
            Grade expectedGrade = new Grade
            {
                CourseId = "oop1",
                StudentId = "adad",
                Result = GradeLevel.VG
            };

            Grade actualGrade = _testStore.GradeStudent(inputStudent, _testGrade);
            Grade foundGrade = _testStore.FindById(inputGradeId);

            Assert.AreEqual(expectedGrade.CourseId, actualGrade.CourseId);
            Assert.AreEqual(expectedGrade.StudentId, actualGrade.StudentId);
            Assert.AreEqual(expectedGrade.Result, actualGrade.Result);
            Assert.IsNotNull(foundGrade);
        }
    }
}
