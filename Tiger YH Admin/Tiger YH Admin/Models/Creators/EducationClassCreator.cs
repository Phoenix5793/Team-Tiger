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
			Console.Clear();
			Console.WriteLine("Skapa ny klass");
			Console.WriteLine();

			EducationClass newClass = new EducationClass();
			UserStore userStore = new UserStore();

			bool keepLooping = true;
			do
			{
				Console.Clear();
				string classId = UserInput.GetInput<string>("Klass-id:");
				EducationClass existingClass = dataStore.FindById(classId);

				if (existingClass != null && keepLooping)
				{
					Console.WriteLine("Klass-id redan använt");
				}
				else
				{
					// Klassen finns inte
					Console.Write("Klassbeskrivning: ");
					string classDescription = UserInput.GetInput<string>();

					string input;
					User supervisor;
					do
					{
						Console.WriteLine("Utbildningsledare: ");
						input = UserInput.GetInput<string>();
						supervisor = userStore.FindById(input);
						if (supervisor == null)
						{
							Console.WriteLine("Användaren finns inte");
						}
						else if (supervisor.UserLevel != UserLevel.EducationSupervisor)
						{
							Console.WriteLine("Användaren är inte utbildningsledare");
						}
					} while (supervisor == null || supervisor.UserLevel != UserLevel.EducationSupervisor);

					string newStudent;
					List<string> studentList = new List<string>();
					do
					{
						//TODO: Bara kunna lägga till studenter
						Console.WriteLine("Lägg till student-id: ");
						newStudent = UserInput.GetInput<string>();
						User student = userStore.FindById(newStudent);
						if (student != null && student.UserLevel == UserLevel.Student)
						{
							studentList.Add(student.UserName);
						}
						else
						{
							Console.WriteLine("Användaren är inte student");
						}
					} while (newStudent.Length > 0);

					string classList = string.Join(",", studentList);

					newClass = new EducationClass
					{
						ClassId = classId,
						Description = classDescription,
						EducationSupervisorId = input,
						StudentString = classList
					};

					keepLooping = false;
				}
			} while (keepLooping);

			return dataStore.AddItem(newClass);
		}
	}
}
