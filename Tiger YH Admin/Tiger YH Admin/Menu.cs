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
        
        
       
        public static int EducationSupervisorMenu()
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Utbildningledare-meny");
            Console.WriteLine();
            Console.WriteLine("1. Skapa användare");
            Console.WriteLine("2. Sök efter användare"); //TODO: implementera
            Console.WriteLine("3. Hantera kurser"); 
            Console.WriteLine("4. Hantera studenter"); 
            
            
            Console.WriteLine();
            Console.Write("Ditt val: ");
            return UserInput.GetInput<int>();






        }
        public static void ManageCourses()
        {

            Console.Clear();
         
            Console.WriteLine("Hantera kurser");
            Console.WriteLine();
            Console.WriteLine("1. Skapa kurs");
            Console.WriteLine("2. Ta bort kurs");
            Console.WriteLine("3. Lägg till lärare");
            Console.WriteLine("4. Ändra kurs");
            Console.WriteLine("5. Visa kurser");




        }
        public static void ManageStudents()
        {
            Console.Clear();

            Console.WriteLine("Hantera studenter");
            Console.WriteLine();
            Console.WriteLine("1. Visa student information");
            Console.WriteLine("2. Ändra student information");
            Console.WriteLine("3. Sätt betyg");
            Console.WriteLine("3. Ta bort student");
           




        }

    }
}
