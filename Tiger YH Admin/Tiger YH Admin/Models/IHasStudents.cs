using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    interface IHasStudentList
    {
        List<string> GetStudentList();
        void SetStudentList(List<string> students);
        bool HasStudent(string userName);
        bool HasStudent(User student);
    }
}
