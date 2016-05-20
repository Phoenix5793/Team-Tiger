using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin;
using System.Linq;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestEducationClassStore
    {
        private EducationClassStore _classStore;
        private EducationClass _testClass;
        private User _testSuperVisor;
        private UserStore _testUserStore;

        [TestInitialize]
        public void Initialize()
        {
            _testClass = new EducationClass
            {
                ClassId = "su15",
                Description = "Systemutvecklare, agil applikationsprogrammering",
                StudentString = "sven",
                CourseString = "Mattematik"

            };

            _testSuperVisor = new User
            {
                UserLevel = UserLevel.EducationSupervisor,
                UserName = "tina"
            };
            _testClass.EducationSupervisorId = _testSuperVisor.UserName;

            List<EducationClass> classList = new List<EducationClass> {_testClass};
            List<User> userList = new List<User> { _testSuperVisor };

            _testUserStore = new UserStore(userList);
            _classStore = new EducationClassStore(classList);
        }

        [TestMethod]
        public void FindById__Can_Find_Class()
        {
            string input = "su15";
            string expected = "su15";

            EducationClass foundClass = _classStore.FindById(input);
            string actual = foundClass.ClassId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindById__Returns_Null_If_Class_Not_Found()
        {
            EducationClass foundClass = _classStore.FindById("qwerty");

            Assert.IsNull(foundClass);
        }

        [TestMethod]
        public void FindByStudentId__Can_Find_Student()
        {
            string input = "sven";
            string expected = "su15";

            EducationClass foundClass = _classStore.FindByStudentId(input);
            string actual = foundClass.ClassId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindByStudentId_Return_Null_If_Student_Not_Found()
        {
            string input = "ola";

            EducationClass foundClass = _classStore.FindByStudentId(input);

            Assert.IsNull(foundClass);
        }

        [TestMethod]
        public void FindByCourseId_Can_Find_Class()
        {
            string input = "Mattematik";
            string expected = "su15";

            EducationClass foundClass = _classStore.FindByCourseId(input);

            Assert.AreEqual(expected, foundClass.ClassId);
        }

        [TestMethod]
        public void FindByCourseId_Return_Null_If_Class_Not_Found()
        {
            string input = "Svenska";

            EducationClass foundClass = _classStore.FindByCourseId(input);

            Assert.IsNull(foundClass);
        }

        [TestMethod]
        public void GetClassesForSuperVisor_Can_Find_Class()
        {
            var input = _testSuperVisor;
            var expected = "su15";

            var foundClassList = _classStore.GetClassesForSupervisor(input);
            EducationClass foundClass = foundClassList.SingleOrDefault(c => c.EducationSupervisorId == input.UserName);

            Assert.AreEqual(expected, foundClass.ClassId);
        }
    }
}
