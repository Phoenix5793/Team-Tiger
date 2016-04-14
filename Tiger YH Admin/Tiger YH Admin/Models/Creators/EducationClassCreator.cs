using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models.Creators
{
	class EducationClassCreator : ICreator<EducationClass>
	{
		public EducationClass Create(IDataStore<EducationClass> dataStore)
		{
			EducationClass newClass = new EducationClass
			{
				ClassId = "su15",
				Description = "Systemutvecklare Agil Applikationsprogrammering",
				EducationSupervisorId = "admin",
				StudentString = "bob,bengt"
			};

			return dataStore.AddItem(newClass);
		}
	}
}
