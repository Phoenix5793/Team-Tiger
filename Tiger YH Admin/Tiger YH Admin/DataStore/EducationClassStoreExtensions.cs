using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.DataStore
{
    static class EducationClassStoreExtensions
    {
        public static IEnumerable<EducationClass> ForSupervisor(this IEnumerable<EducationClass> classes, User supervisor)
        {
            return classes.Where(c => c.EducationSupervisorId == supervisor.UserName);
        }
    }
}
