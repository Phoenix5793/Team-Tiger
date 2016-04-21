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
	}
}
