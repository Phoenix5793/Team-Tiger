using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    public class EducationClass
    {
        public string ClassId { get; set; }
        public string Description { get; set; }
        public string EducationSupervisorId { get; set; }
        public string StudentString { get; set; }
        private string CourseString { get; set; } = string.Empty;

        public List<string> GetStudentList()
        {
            return StudentString.Split(',').ToList();
        }

        public void SetStudentList(List<string> users)
        {
            StudentString = string.Join(",", users);
        }

        public bool HasStudent(User student)
        {
            return HasStudent(student.UserName);
        }

        public bool HasStudent(string studentName)
        {
            List<string> studentList = GetStudentList();
            return studentList.Contains(studentName);
        }

        public bool AddCourse(string courseId, CourseStore courseStore)
        {
            Course newCourse = courseStore.FindById(courseId);
            bool result = AddCourse(newCourse, courseStore);
            return result;
        }

        public bool AddCourse(Course course, CourseStore courseStore)
        {
            bool courseExists = HasCourse(course);

            if (courseExists)
            {
                return false;
            }
            else
            {
                List<string> courseList = CourseString.Split(',').ToList();
                courseList.Add(course.CourseId);
                CourseString = string.Join(",", courseList);
                return true;
            }
        }

        public bool RemoveCourse(Course course)
        {
            return RemoveCourse(course.CourseId);
        }

        public bool RemoveCourse(string courseId)
        {
            List<string> courseList = CourseString.Split(',').ToList();
            bool result = courseList.Remove(courseId);
            CourseString = string.Join(",", courseList);

            return result;
        }

        public bool HasCourse(Course course)
        {
            return HasCourse(course.CourseId);
        }

        public bool HasCourse(string courseId)
        {
            List<string> courseList = CourseString.Split(',').ToList();

            return courseList.Contains(courseId);
        }

    }
}
