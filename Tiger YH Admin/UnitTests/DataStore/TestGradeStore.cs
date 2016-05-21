using System;
using System.Collections.Generic;
using System.Linq;
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
        private Grade _testCourseGrade;
        private Grade _testGoalGrade;
        private List<Grade> _testGradeList;
        private User _testUser;

        [TestInitialize]
        public void Initialize()
        {
            _testUser = new User
            {
                UserName = "adad"
            };

            _testCourseGrade = new Grade
            {
                CourseId = "oop1",
                StudentId = _testUser.UserName,
                Result = GradeLevel.VG
            };

            _testGoalGrade = new Grade
            {
                CourseId = "oop1",
                StudentId = _testUser.UserName,
                CourseGoal = "1",
                Result = GradeLevel.G
            };

            var otherGrade = new Grade
            {
                CourseId = "shouldnotbefound",
                StudentId = "xyxy",
                CourseGoal = "5",
                Result = GradeLevel.IG
            };

            _testGradeList = new List<Grade> {_testCourseGrade, _testGoalGrade, otherGrade};
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
        public void FindByCourseId__Finds_All_Grades()
        {
            string courseId = "oop1";
            int expectedGrades = 2;

            var courses = _testStore.FindByCourseId(courseId);
            int actualGrades = courses.Count();

            Assert.AreEqual(expectedGrades, actualGrades);
        }

        [TestMethod]
        public void FindByCourseId__Finds_Course_Grade()
        {
            string courseId = "oop1";
            var expectedGradeLevel = GradeLevel.VG;

            var courses = _testStore.FindByCourseId(courseId, GradeType.Course);
            var foundCourse = courses.Single(c => c.CourseId == courseId);
            var actualGradeLevel = foundCourse.Result;

            Assert.AreEqual(expectedGradeLevel, actualGradeLevel);
        }

        [TestMethod]
        public void FindByCourseId__Finds_Goal_Grade()
        {
            string courseId = "oop1";
            var expectedGradeLevel = GradeLevel.G;

            var courses = _testStore.FindByCourseId(courseId, GradeType.Goal);
            var foundCourse = courses.Single(c => c.CourseId == courseId);
            var actualGradeLevel = foundCourse.Result;

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

            Grade actualGrade = _testStore.GradeStudent(inputStudent, _testCourseGrade);
            Grade foundGrade = _testStore.FindById(inputGradeId);

            Assert.AreEqual(expectedGrade.CourseId, actualGrade.CourseId);
            Assert.AreEqual(expectedGrade.StudentId, actualGrade.StudentId);
            Assert.AreEqual(expectedGrade.Result, actualGrade.Result);
            Assert.IsNotNull(foundGrade);
        }

        [TestMethod]
        public void FindGradesForStudent_Find_Grade()
        {
            User inputStudent = _testUser;
            List<Grade> ExpectedListOfGrades = _testGradeList.FindAll(g => g.StudentId == _testUser.UserName);
            List<Grade> actualList = new List<Grade>();

            var listOfGrades = _testStore.FindGradesForStudent(inputStudent);
            foreach (Grade g in listOfGrades)
            {
                actualList.Add(g);
            }

            CollectionAssert.AreEqual(ExpectedListOfGrades, actualList);
        }

        [TestMethod]
        public void FindGradesForStudent_Cant_Find_Grade()
        {
            User inputStudent = new User
            {
                UserName = "pelle"
            };
            List<Grade> ExpectedListOfGrades = _testGradeList.FindAll(g => g.StudentId == _testUser.UserName);
            List<Grade> actualList = new List<Grade>();

            var listOfGrades = _testStore.FindGradesForStudent(inputStudent);
            foreach (Grade g in listOfGrades)
            {
                actualList.Add(g);
            }

            CollectionAssert.AreNotEqual(ExpectedListOfGrades, actualList);
        }
    }
}
