using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserLevel UserLevel { get; set; } = UserLevel.Student;
    }
}
