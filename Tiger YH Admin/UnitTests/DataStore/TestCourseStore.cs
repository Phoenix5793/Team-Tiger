using System;
using System.Collections.Generic;
using System.Linq;
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

            List<Course> courseList = new List<Course>();

            courseList.Add(new Course
            {
                CourseId = "oop1",
                CourseName = "Pågående kurs",
                CourseTeacher = "pontus",
                StartDate = DateTime.Today.Subtract(new TimeSpan(7)),
                EndDate = DateTime.Today.AddDays(7)
            });

            courseList.Add(new Course
            {
                CourseId = "oop2",
                CourseName = "Avslutad kurs",
                CourseTeacher = "pontus",
                StartDate = DateTime.Parse("2016-01-01"),
                EndDate = DateTime.Parse("2016-01-30")
            });

            courseList.Add(new Course
            {
                CourseId = "oop3",
                CourseName = "Framtida kurs",
                CourseTeacher = "pontus",
                StartDate = DateTime.Today.AddDays(7),
                EndDate = DateTime.Today.AddDays(30)
            });

            testCourseStore = new CourseStore(courseList);
        }

        [TestMethod]
        public void GetFinishedCourses__Finds_Finished_Course()
        {
            int expectedCourseCount = 1;
            string expectedCourseName = "Avslutad kurs";

            List<Course> actualCourse = testCourseStore.GetFinishedCourses().ToList();
            int actualCourseCount = actualCourse.Count;
            string actualCourseName = actualCourse.First().CourseName;

            Assert.AreEqual(expectedCourseCount, actualCourseCount);
            Assert.AreEqual(expectedCourseName, actualCourseName);
        }

        [TestMethod]
        public void GetCurrentCourses__Finds_Current_Course()
        {
            int expectedCourseCount = 1;
            string expectedCourseName = "Pågående kurs";

            List<Course> actualCourse = testCourseStore.GetCurrentCourses().ToList();
            int actualCourseCount = actualCourse.Count;
            string actualCourseName = actualCourse.First().CourseName;

            Assert.AreEqual(expectedCourseCount, actualCourseCount);
            Assert.AreEqual(expectedCourseName, actualCourseName);
        }

        [TestMethod]
        public void GetFutureCourses__Finds_Future_Course()
        {
            int expectedCourseCount = 1;
            string expectedCourseName = "Framtida kurs";

            List<Course> actualCourse = testCourseStore.GetFutureCourses().ToList();
            int actualCourseCount = actualCourse.Count;
            string actualCourseName = actualCourse.First().CourseName;

            Assert.AreEqual(expectedCourseCount, actualCourseCount);
            Assert.AreEqual(expectedCourseName, actualCourseName);
        }

    }
}
