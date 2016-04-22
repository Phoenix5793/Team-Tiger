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
    class EducationClass
    {
        public string ClassId { get; set; }
        public string Description { get; set; }
        public string EducationSupervisorId { get; set; }
        public string StudentString { get; set; }

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
            //TODO: Bättre validering
            return HasStudent(student.UserName);
        }

        public bool HasStudent(string studentName)
        {
            return StudentString.Contains(studentName);
        }
    }
}
