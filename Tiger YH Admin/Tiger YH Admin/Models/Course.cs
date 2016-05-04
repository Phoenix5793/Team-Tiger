using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    [IgnoreEmptyLines]
    public class Course : IHasStudentList
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseTeacher { get; set; }
        private string StudentString { get; set; } = string.Empty;

        public void SetStudentList(List<string> users)
        {
            StudentString = string.Join(",", users);
        }

        public List<string> GetStudentList()
        {
            return StudentString.Split(',').ToList();
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



    }



}
