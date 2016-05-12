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

            _testUser = new User
            {
                UserName = "bertil"
            };

            _testAddUser = new User
            {
                UserName = "addeduser"
            };
        }

    }
}
