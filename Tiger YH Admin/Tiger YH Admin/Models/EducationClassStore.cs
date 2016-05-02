using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    public class EducationClassStore : DataStore<EducationClass>
    {
        public EducationClassStore() : base()
        {
        }

        public EducationClassStore(IEnumerable<EducationClass> classes) : base(classes)
        {
        }

        public override EducationClass FindById(string id)
        {
            return DataSet.ToList().SingleOrDefault(c => c.ClassId == id);
        }
    }
}
