using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestEducationClassStoreExtensions
    {
        private EducationClassStore _testEducationClassStore;
        private EducationClass _testEducationClass;
        private User _testSupervisor;

        [TestInitialize]
        public void Initialize()
        {
            _testSupervisor = new User
            {
                UserName = "supervisor"
            };

            _testEducationClass = new EducationClass
            {
                ClassId = "klass1",
                Description = "Testklass",
                EducationSupervisorId = "supervisor"
            };

            var otherClass = new EducationClass
            {
                ClassId = "klass2",
                Description = "Should not be found",
                EducationSupervisorId = "someone"
            };

            var classList = new List<EducationClass> {_testEducationClass, otherClass};

            _testEducationClassStore = new EducationClassStore(classList);
        }

        [TestMethod]
        public void ForSupervisor__Finds_Correct_Class()
        {
            User inputUser = _testSupervisor;
            string expected = "klass1";

            IEnumerable<EducationClass> classes = _testEducationClassStore.All().ForSupervisor(inputUser);
            string actual = classes.Single(c => c.ClassId == expected).ClassId;

            Assert.AreEqual(expected, actual);
        }
    }
}
