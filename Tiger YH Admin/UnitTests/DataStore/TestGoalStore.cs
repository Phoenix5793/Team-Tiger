using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.DataStore;

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
                CourseId = "kurs1",
                GoalId = "1",
                Description = "Den studerande ska kunna räkna till fem"
            };

            testGoalStore = new GoalStore();
            testGoalStore.AddItem(testGoal);
        }

        [TestMethod]
        public void FindById__Finds_Goal()
        {
            string input = "1";
            string expected = "kurs1";

            Goal actualGoal = testGoalStore.FindById(input);
            string actual = actualGoal.CourseId;

            Assert.AreEqual(expected, actual);
        }
    }
}
