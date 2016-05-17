using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.DataStore
{
    static class UserStoreExtensions
    {
        public static IEnumerable<User> IsTeacher(this IEnumerable<User> teachers)
        {
            return teachers.Where(u => u.UserLevel == UserLevel.Teacher);
        }

    }
}
