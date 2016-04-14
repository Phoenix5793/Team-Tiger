using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
	class EducationClassStore : DataStore<EducationClass>
	{
		public override EducationClass FindById(string id)
		{
			return DataSet.ToList().SingleOrDefault(c => c.ClassId == id);
		}
	}
}
