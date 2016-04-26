﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin;
using Tiger_YH_Admin.Models;

namespace UnitTests.Models
{
    [TestClass]
    public class TestEducationClass
    {
        private EducationClass _testClass;

        [TestInitialize]
        public void Initialize()
        {
            _testClass = new EducationClass()
            {
                ClassId = "testclass",
                Description = "The Joy of Painting with Bob Ross",
                EducationSupervisorId = "bobross",
                StudentString = "adam,bertil,caesar,david,erik,johndoe"
            };
        }

        [TestMethod]
        public void Has_Class_Id()
        {
            string expected = "testclass";
            string actual = _testClass.ClassId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StudentString__Returns_Student_String()
        {
            string expected = "adam,bertil,caesar,david,erik,johndoe";
            string actual = _testClass.StudentString;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void StudentString__Setting_With_List_Returns_Student_List()
        {
            List<string> input = new List<string> {"dilbert", "dogbert", "catbert"};
            string expected = "dilbert,dogbert,catbert";

            _testClass.SetStudentList(input);
            string actual = _testClass.StudentString;

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
    }
}
