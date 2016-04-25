using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string SSN { get; set; } // social security number
        public string PhoneNumber { get; set; }
        public UserLevel UserLevel { get; set; } = UserLevel.Student;

        public override string ToString()
        {
            return UserName;
        }

        public string FullName()
        {
            return $"{FirstName} {Surname}";
        }
    }
}
