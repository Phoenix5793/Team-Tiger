using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    static class CourseStoreExtensions
    {
        public static IEnumerable<Course> Unmanned(this IEnumerable<Course> courses)
        {
            return courses.Where(c => c.CourseTeacher == string.Empty);
        }

        public static IEnumerable<Course> Manned(this IEnumerable<Course> courses)
        {
            return courses.Where(c => c.CourseTeacher != string.Empty);
        }

        public static IEnumerable<Course> Finished(this IEnumerable<Course> courses)
        {
            return courses.Where(c => c.EndDate < DateTime.Today);
        }

        public static IEnumerable<Course> Current(this IEnumerable<Course> courses)
        {
            return courses.Where(c => c.StartDate < DateTime.Today && c.EndDate > DateTime.Today);
        }

        public static IEnumerable<Course> Future(this IEnumerable<Course> courses)
        {
            return courses.Where(c => c.StartDate > DateTime.Today);
        }

        public static IEnumerable<Course> ForTeacher(this IEnumerable<Course> courses, User teacher)
        {
            return courses.Where(c => c.CourseTeacher == teacher.UserName);
        }
    }
}
