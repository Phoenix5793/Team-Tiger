using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestEducationClass
    {
        private EducationClass _testClass;
        private CourseStore _courseStore;
        private Course _testCourse;
        private User _testUser;
        private User _testAddUser;

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
                "adam",
                "bertil",
                "caesar",
                "david",
                "erik",
                "johndoe"
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

            _testAddUser = new User
            {
                UserName = "addeduser"
            };

            var courseList = new List<Course>();
            courseList.Add(_testCourse);
            _courseStore = new CourseStore(courseList);
        }

        [TestMethod]
        public void Has_Class_Id()
        {
            string expected = "testclass";
            string actual = _testClass.ClassId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetStudentList__Returns_Student_List()
        {
            List<string> expected = new List<string>() {"adam", "bertil", "caesar", "david", "erik", "johndoe"};
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
            List<string> input = new List<string>() {"dilbert", "dogbert", "catbert"};
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

        [TestMethod]
        public void AddCourse__Can_Add_A_Course_Object()
        {
            Course input = _testCourse;
            bool expected = true;

            bool actual = _testClass.AddCourse(input, _courseStore);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddCourse__Cannot_Add_Course_Twice()
        {
            Course input = _testCourse;
            bool expected = false;

            _testClass.AddCourse(input, _courseStore);
            bool actual = _testClass.AddCourse(input, _courseStore);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddCourse__Can_Add_A_Course_By_Id_String()
        {
            string input = _testCourse.CourseId;
            bool expected = true;

            bool actual = _testClass.AddCourse(input, _courseStore);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveCourse__Can_Remove_Course()
        {
            Course input = _testCourse;
            bool expected = true;
            _testClass.AddCourse(input, _courseStore);

            bool actual = _testClass.RemoveCourse(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void RemoveCourse__Can_Remove_Course_By_Id()
        {
            string input = _testCourse.CourseId;
            bool expected = true;
            _testClass.AddCourse(input, _courseStore);

            bool actual = _testClass.RemoveCourse(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasCourse__Finds_Existing_Course()
        {
            Course input = _testCourse;
            bool expected = true;
            _testClass.AddCourse(input, _courseStore);

            bool actual = _testClass.HasCourse(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasCourse__Finds_Existing_Course_From_String()
        {
            string input = _testCourse.CourseId;
            bool expected = true;
            _testClass.AddCourse(input, _courseStore);

            bool actual = _testClass.HasCourse(input);

            Assert.AreEqual(expected, actual);
        }


        [TestMethod]
        public void HasCourse__Course_Does_Not_Exist()
        {
            Course input = _testCourse;
            bool expected = false;

            bool actual = _testClass.HasCourse(input);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetCourseList__Returns_Course_List()
        {
            var expected = new List<string> {"oop1"};
            _testClass.AddCourse(_testCourse.CourseId, _courseStore);

            List<string> actual = _testClass.GetCourseList();

            Assert.AreEqual(expected[0], actual[0]);
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
