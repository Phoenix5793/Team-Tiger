using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.DataStore
{
    public class GradeStore : DataStore<Grade>
    {
        public GradeStore() : base()
        {
        }

        public GradeStore(IEnumerable<Grade> grades) : base(grades)
        {
        }

        public override Grade FindById(string id)
        {
            return DataSet.SingleOrDefault(g => g.GradeId == id);
        }

        public IEnumerable<Grade> FindGradesForStudent(User student)
        {
            return All().Where(g => g.StudentId == student.UserName);
        }

        public Grade GradeStudent(User inputStudent, Course inputCourse, GradeLevel inputGradeLevel)
        {
            Grade newGrade = new Grade
            {
                StudentId = inputStudent.UserName,
                CourseId = inputCourse.CourseId,
                Result = inputGradeLevel
            };

            Grade existingGrade = FindById(newGrade.GradeId);
            List<Grade> allGrades;

            if (existingGrade != null)
            {
                allGrades = All().Where(g => g.GradeId != existingGrade.GradeId).ToList();
            }
            else
            {
                allGrades = All().ToList();
            }

            allGrades.Add(newGrade);
            DataSet = allGrades;

            return newGrade;
        }
    }
}
