using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    public class CourseStore : DataStore<Course>
    {
        public CourseStore() : base()
        {
        }

        public CourseStore(IEnumerable<Course> courses) : base(courses)
        {
        }

        public override Course FindById(string id)
        {
            return DataSet.SingleOrDefault(c => c.CourseId == id);
        }

        public IEnumerable<Course> GetFinishedCourses()
        {
            return DataSet.Where(c => c.EndDate < DateTime.Today);
        }

        public IEnumerable<Course> GetCurrentCourses()
        {
            return DataSet.Where(c =>
                c.StartDate < DateTime.Today &&
                c.EndDate > DateTime.Today
                );
        }

        public IEnumerable<Course> GetFutureCourses()
        {
            return DataSet.Where(c => c.StartDate > DateTime.Today);
        }

        public IEnumerable<Course> GetUnmannedCourses()
        {
            return DataSet.Where(c => c.CourseTeacher == string.Empty);
        }

        public IEnumerable<Course> GetMannedCourses()
        {
            return GetFutureCourses().Where(c => c.CourseTeacher != string.Empty);
        }
    }
}