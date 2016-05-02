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

            testCourseStore = new CourseStore {DataSet = courseList};
        }

    }
}
