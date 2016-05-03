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
            return All().Finished();
        }

        public IEnumerable<Course> GetCurrentCourses()
        {
            return All().Current();
        }

        public IEnumerable<Course> GetFutureCourses()
        {
            return All().Future();
        }

        public IEnumerable<Course> GetUnmannedCourses()
        {
            return All().Unmanned();
        }

        public IEnumerable<Course> GetMannedCourses()
        {
            return All().Manned();
        }

        public IEnumerable<Course> GetAllAgreedCourses()
        {
            IEnumerable<Course> currentManned = All().Current().Manned();
            IEnumerable<Course> futureManned = All().Future().Manned();

            return currentManned.Concat(futureManned);
        }
    }
}