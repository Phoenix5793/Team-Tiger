using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using Tiger_YH_Admin.DataStore;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; } // social security number
        public string PhoneNumber { get; set; }
        public UserLevel UserLevel { get; set; } = UserLevel.Student;

        public string FullName()
        {
            return $"{FirstName} {LastName}";
        }

        public bool HasLevel(UserLevel level)
        {
            return UserLevel == level;
        }

        public EducationClass GetClass()
        {
            if (UserLevel != UserLevel.Student)
            {
                return null;
            }
            else
            {
                var classStore = new EducationClassStore();
                return classStore.FindByStudentId(UserName);
            }
        }
    }
}
