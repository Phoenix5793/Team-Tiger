using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin
{
	static class Menu
	{
		public static int MainAdminMenu()
		{
			Console.WriteLine("Tiger Board!");
			Console.WriteLine("Admin-meny");
			Console.WriteLine();
			Console.WriteLine("1. Skapa användare");
			Console.WriteLine("2. Sök efter användare");
			Console.WriteLine("3. Skapa kurs");
			Console.WriteLine("4. Skapa klass");
			Console.WriteLine("5. Visa klasslista");
			Console.WriteLine();
			Console.Write("Ditt val: ");
			return UserInput.GetInput<int>();
		}

		public static string[] LoginMenu()
		{
			Console.WriteLine("Inloggning");

			string userName = UserInput.GetInput<string>("Användarnamn:");

			//TODO: Göm lösenordet bättre
			Console.Write("Lösenord: ");
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Black;
			string password = UserInput.GetInput<string>();
			Console.ResetColor();

			return new string[] {userName, password};

		}
	}
}
