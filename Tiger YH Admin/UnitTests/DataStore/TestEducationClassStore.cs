using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestEducationClassStore
    {
        private EducationClassStore _classStore;
        private EducationClass _testClass;

        [TestInitialize]
        public void Initialize()
        {
            _testClass = new EducationClass
            {
                ClassId = "su15",
                Description = "Systemutvecklare, agil applikationsprogrammering",
                EducationSupervisorId = "tina"
            };

            List<EducationClass> classList = new List<EducationClass> {_testClass};

            _classStore = new EducationClassStore();
            _classStore.DataSet = classList;
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

    }
}
