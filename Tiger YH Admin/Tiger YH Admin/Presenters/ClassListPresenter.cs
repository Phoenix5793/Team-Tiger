using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
	static class ClassListPresenter
	{
		public static void Run()
		{
			MainMenu();
			int menuChoice = UserInput.GetInput<int>();

			switch (menuChoice)
			{
				case 0:
				return;
				case 1:
					ListAllClasses();
					break;
				case 2:
					ShowClass();
					break;
			}
		}

		public static void MainMenu()
		{
			Console.Clear();
			Console.WriteLine("Visa klasslistor");
			Console.WriteLine();
			Console.WriteLine("0. Tillbaka till föregående meny");
			Console.WriteLine("1. Lista alla klasser");
			Console.WriteLine("2. Visa en klass");
		}

		public static void ListAllClasses()
		{
			List<EducationClass> classList = new EducationClassStore().DataSet.ToList();


			Console.WriteLine(
				"Klassnamn".PadRight(10) +
				"Utb.ledare".PadRight(11) +
				"Beskrivning".PadRight(40)
			);

			Console.WriteLine(new string('-', 66));

			foreach (EducationClass educationClass in classList)
			{
				Console.WriteLine(
					educationClass.ClassId.PadRight(10) +
					educationClass.EducationSupervisorId.PadRight(11) +
					educationClass.Description.PadRight(40)
				);
			}
		}

		public static void ShowClass()
		{
			EducationClassStore classStore = new EducationClassStore();
			UserStore studentStore = new UserStore();
			bool keepLooping = true;
			do
			{
				Console.WriteLine("Tryck enter för att avbryta");
				string classId = UserInput.GetInput<string>("Ange klass-id:");

				if (classId == string.Empty)
				{
					return;
				}

				EducationClass edClass = classStore.FindById(classId);

				if (edClass == null)
				{
					Console.WriteLine($"Finns ingen klass med id {classId}");
				}
				else
				{
					Console.Clear();
					Console.WriteLine(
						"Student-id".PadRight(12) +
						"Namn".PadRight(25) +
						"Telefon".PadRight(15)
						);
					Console.WriteLine(new string('-', 60));

					List<string> studentList = edClass.GetStudentList();
					foreach (string studentId in studentList)
					{
						User student = studentStore.FindById(studentId);

						Console.WriteLine(
							student.UserName.PadRight(12) +
							student.FullName().PadRight(25) +
							student.PhoneNumber.PadRight(15)
						);
					}
					Console.WriteLine();
				}
			} while (keepLooping);
		}
	}
}
