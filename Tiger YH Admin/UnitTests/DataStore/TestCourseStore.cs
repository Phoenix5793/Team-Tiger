﻿using System;
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

            courseList.Add(new Course
            {
                CourseId = "oop4",
                CourseName = "Obemannad kurs",
                CourseTeacher = string.Empty,
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
            int expectedCourseCount = 2;
            string expectedCourseName1 = "Framtida kurs";
            string expectedCourseName2 = "Obemannad kurs";

            List<Course> actualCourse = testCourseStore.GetFutureCourses().ToList();
            int actualCourseCount = actualCourse.Count;
            string actualCourseName1 = actualCourse.First().CourseName;
            string actualCourseName2 = actualCourse.Last().CourseName;


            Assert.AreEqual(expectedCourseCount, actualCourseCount);
            Assert.AreEqual(expectedCourseName1, actualCourseName1);
            Assert.AreEqual(expectedCourseName2, actualCourseName2);
        }

        [TestMethod]
        public void GetUnmannedCourses__Finds_Unmanned_Course()
        {
            int expectedCourseCount = 1;
            string expectedCourseName = "Obemannad kurs";

            List<Course> actualCourse = testCourseStore.GetUnmannedCourses().ToList();
            int actualCourseCount = actualCourse.Count;
            string actualCourseName = actualCourse.First().CourseName;

            Assert.AreEqual(expectedCourseCount, actualCourseCount);
            Assert.AreEqual(expectedCourseName, actualCourseName);
        }
    }
}