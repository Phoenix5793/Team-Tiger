using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestCourseStore
    {
        private Course testCourse;
        private CourseStore testCourseStore;

        [TestInitialize]
        public void Initialize()
        {
            testCourse = new Course
            {
                CourseId = "oop1",
                CourseName = "Objektorienterad Programmering 1",
                CourseTeacher = "pontus",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today
            };

            List<Course> courseList = new List<Course>();
            courseList.Add(testCourse);

            testCourseStore = new CourseStore();
            testCourseStore.DataSet = courseList;
        }

        [TestMethod]
        public void FindById__Can_Find_Course()
        {
            string input = "oop1";
            string expected = "oop1";

            Course foundCourse = testCourseStore.FindById(input);
            string actual = foundCourse.CourseId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindById__Returns_Null_If_Course_Not_Found()
        {
            Course foundCourse = testCourseStore.FindById("qwerty");

            Assert.IsNull(foundCourse);
        }

    }
}
