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
        private EducationClass _testClass;
        private Course _testCourse;
        private User _testUser;

        [TestInitialize]
        public void Initialize()
        {
            _testClass = new EducationClass()
            {
                ClassId = "testclass",
                Description = "The Joy of Painting with Bob Ross",
                EducationSupervisorId = "bobross"
            };

            var studentList = new List<string>
            {
                "adam", "bertil", "caesar", "david", "erik", "johndoe"
            };

            _testClass.SetStudentList(studentList);

            _testCourse = new Course
            {
                CourseId = "oop1",
                CourseName = "Objektorienterad Programmering 1",
                CourseTeacher = "pontus",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            _testUser = new User
            {
                UserName = "bertil"
            };

            var courseList = new List<Course>();
            courseList.Add(_testCourse);
        }


        [TestMethod]
        public void GetStudentList__Returns_Student_List()
        {
            List<string> expected = new List<string>() { "adam", "bertil", "caesar", "david", "erik", "johndoe" };
            List<string> actual = _testClass.GetStudentList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStudentList__Returns_Correct_Number_Of_Students()
        {
            int expected = 6;

            int actual = _testClass.GetStudentList().Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void SetStudentList__Can_Set_With_A_List()
        {
            List<string> input = new List<string>() { "dilbert", "dogbert", "catbert" };
            List<string> expected = input.ToList();

            // Set and get new list
            _testClass.SetStudentList(input);
            List<string> actual = _testClass.GetStudentList();

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Student_Exists_In_String()
        {
            string input = "david";

            bool expected = true;
            bool actual = _testClass.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Validates_Properly()
        {
            string input = "john";

            bool expected = false;
            bool actual = _testClass.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Runs_With_User_Object__Student_In_Class()
        {
            User input = _testUser;

            bool expected = true;
            bool actual = _testClass.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasStudent__Runs_With_User_Object__Student_Not_In_class()
        {
            _testUser.UserName = "john";
            User input = _testUser;

            bool expected = false;
            bool actual = _testClass.HasStudent(input);

            Assert.AreEqual(expected, actual);
        }


    }
}
