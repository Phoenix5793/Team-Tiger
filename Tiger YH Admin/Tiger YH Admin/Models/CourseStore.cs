using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
	public class CourseStore : DataStore<Course>
	{
		public override Course FindById(string id)
		{
			return DataSet.ToList().SingleOrDefault(c => c.CourseId == id);
		}
	}
}
