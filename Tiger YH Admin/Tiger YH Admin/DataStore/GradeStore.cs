﻿using System;
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
            return All().SingleOrDefault(g => g.GradeId == id);
        }

        public IEnumerable<Grade> FindByCourseId(string id, GradeType type = GradeType.All)
        {
            IEnumerable<Grade> grades = All().Where(g => g.CourseId == id);

            switch (type)
            {
                default:
                case GradeType.All:
                    return grades;
                case GradeType.Course:
                    return grades.Where(c => c.CourseGoal == null);
                case GradeType.Goal:
                    return grades.Where(c => c.CourseGoal != null);
            }
        }

        public IEnumerable<Grade> FindGradesForStudent(User student)
        {
            return All().Where(g => g.StudentId == student.UserName);
        }

        public IEnumerable<Grade> FindGradesForStudent(User student, Course course)
        {
            return All().Where(g => g.StudentId == student.UserName).Where(c => c.CourseId == course.CourseId);
        }

        public Grade GradeStudent(User student, Grade grade)
        {
            Grade newGrade = new Grade
            {
                StudentId = student.UserName,
                CourseId = grade.CourseId,
                CourseGoal = grade.CourseGoal,
                Result = grade.Result
            };

            Grade existingGrade = FindById(newGrade.GradeId);

            if (existingGrade != null)
            {
                existingGrade.StudentId = student.UserName;
                existingGrade.CourseId = grade.CourseId;
                existingGrade.CourseGoal = grade.CourseGoal;
                existingGrade.Result = grade.Result;

                return existingGrade;
            }
            else
            {
                AddItem(newGrade);
                return newGrade;
            }
        }
    }
}
