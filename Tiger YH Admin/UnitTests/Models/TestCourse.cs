using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestCourse
    {
        private Course _testCourse;
        private User _testUser;
        private User _testAddUser;
        private List<string> _studentList;

        [TestInitialize]
        public void Initialize()
        {

            _studentList = new List<string>
            {
                "adam", "bertil", "caesar", "david", "erik", "johndoe"
            };

            _testCourse = new Course
            {
                CourseId = "oop1",
                CourseName = "Objektorienterad Programmering 1",
                CourseTeacher = "pontus",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            _testCourse.SetStudentList(new List<string> {"bertil"});

            _testUser = new User
            {
                UserName = "bertil"
            };

            _testAddUser = new User
            {
                UserName = "addeduser"
            };
        }


        [TestMethod]
        public void GetStudentList__Returns_Student_List()
        {
            List<string> expected = new List<string> { "bertil" };
            List<string> actual = _testCourse.GetStudentList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStudentList__Returns_Correct_Number_Of_Students()
        {
            int expectedCount = 1;
            string expectedStudent = "bertil";

            var actualList = _testCourse.GetStudentList();
            int actualCount = actualList.Count;
            string actualStudent = actualList.First();

            Assert.AreEqual(expectedCount, actualCount);
            Assert.AreEqual(expectedStudent, actualStudent);
        }

        [TestMethod]
        public void SetStudentList__Can_Set_With_A_List()
        {
            List<string> input = _studentList;
            List<string> expected = _studentList;

            // Set and get new list
            _testCourse.SetStudentList(input);
            List<string> actual = _testCourse.GetStudentList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Student_Exists_In_String()
        {
            string input = "bertil";

            bool expected = true;
            bool actual = _testCourse.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Validates_Properly()
        {
            string input = "john";

            bool expected = false;
            bool actual = _testCourse.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Runs_With_User_Object__Student_In_Course()
        {
            User input = _testUser;

            bool expected = true;
            bool actual = _testCourse.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Runs_With_User_Object__Student_Not_In_Course()
        {
            User input = new User
            {
                UserName = "usernotincourse"
            };

            bool expected = false;
            bool actual = _testCourse.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddStudent__Adds_A_Student()
        {
            User input = _testAddUser;
            bool expected = true;

            _testCourse.AddStudent(input);
            bool actual = _testCourse.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveStudent__Removes_A_Student()
        {
            User input = _testAddUser;
            bool expectedResult = true;
            bool expectedFound = false;

            _testCourse.AddStudent(input);
            bool actualResult = _testCourse.RemoveStudent(input);
            bool actualFound = _testCourse.HasStudent(input);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(expectedFound, actualFound);
        }
    }
}
