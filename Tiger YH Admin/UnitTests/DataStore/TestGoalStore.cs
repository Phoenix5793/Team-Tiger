using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.DataStore;
using System.Linq;

namespace UnitTests.DataStore
{
    [TestClass]
    public class TestGoalStore
    {
        private Goal testGoal;
        private GoalStore testGoalStore;

        [TestInitialize]
        public void Initialize()
        {
            testGoal = new Goal
            {
                CourseId = "kurs50",
                GoalId = "50",
                Description = "Den studerande ska kunna räkna till fem"
            };

            testGoalStore = new GoalStore();
            testGoalStore.AddItem(testGoal);
        }

        [TestMethod]
        public void FindById__Finds_Goal()
        {
            string input = "50";
            string expected = "kurs50";

            Goal actualGoal = testGoalStore.FindById(input);
            string actual = actualGoal.CourseId;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void FindByCourseId_Find_Goal()
        {
            string input = "kurs50";
            Goal expected = testGoal;

            var findGoalList = testGoalStore.FindByCourseId(input);
            Goal actualGoal = findGoalList.SingleOrDefault(g => g.CourseId == input);

            Assert.AreEqual(expected, actualGoal);
        }

        [TestMethod]
        public void FindByCourseId_Cant_Find_Goal_Return_Null()
        {
            string input = "kurserna";
            Goal expected = testGoal;

            var findGoalList = testGoalStore.FindByCourseId(input);
            Goal actualGoal = findGoalList.SingleOrDefault(g => g.CourseId == input);

            Assert.IsNull(actualGoal);
        }
    }
}
