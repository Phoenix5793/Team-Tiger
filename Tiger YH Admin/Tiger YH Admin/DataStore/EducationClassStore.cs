﻿using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.DataStore
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
            return All().SingleOrDefault(c => c.ClassId.ToLower() == id.ToLower());
        }

        public EducationClass FindByStudentId(string id)
        {   
            return All().Single(e => e.HasStudent(id));
        }
           
        public IEnumerable<EducationClass> GetClassesForSupervisor(User supervisor)
        {
            return All().ForSupervisor(supervisor);
        }
    }
}
