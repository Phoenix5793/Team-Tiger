using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Models.Creators;
using Tiger_YH_Admin.Presenters;

namespace Tiger_YH_Admin
{
    static class Menu
    {
        public static void MainMenu(User user)
        {
            bool loopMenu = true;
            while (loopMenu)
            {
                Console.Clear();
                switch (user.UserLevel)
                {
                    case UserLevel.Admin:
                        AdminMainMenu();
                        break;
                    case UserLevel.EducationSupervisor:
                        EducationSupervisorMainMenu(user);
                        break;
                    case UserLevel.Teacher:
                        TeacherMainMenu(user);
                        break;
                    case UserLevel.Student:
                        StudentMainMenu(user);
                        break;
                }

                return;
            }
        }

        private static void StudentMainMenu(User user)
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Studentmeny");
            Console.WriteLine();
            Console.WriteLine("0. Logga ut");
            Console.WriteLine("1. Visa mina studieresultat");
            Console.WriteLine("2. Klasslista");
            Console.WriteLine("3. Kursplan");
            Console.WriteLine("4. Byt lösenord");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            // TODO: Implementera
            switch (menuChoice)
            {
                case "0":
                    return;
                case "1":
                    Console.WriteLine("Ej implementerad");
                    break;
                case "2":
                    Console.WriteLine("Ej implementerad");
                    break;
                case "3":
                    Console.WriteLine("Ej implementerad");
                    break;
                case "4":
                    Console.WriteLine("Ej implementerad");
                    break;
            }
        }

        private static void TeacherMainMenu(User user)
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Lärarmeny");
            Console.WriteLine();
            Console.WriteLine("0. Logga ut");
            Console.WriteLine("1. Visa mina kurser");
            Console.WriteLine("2. Visa klasslistor");
            Console.WriteLine("3. Betygsätt studenter");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            // TODO: Implementera
            switch (menuChoice)
            {
                case "0":
                    return;
                case "1":
                    Console.WriteLine("Ej implementerad");
                    break;
                case "2":
                    Console.WriteLine("Ej implementerad");
                    break;
                case "3":
                    Console.WriteLine("Ej implementerad");
                    break;
            }
        }

        public static void AdminMainMenu()
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Admin-meny");
            Console.WriteLine();
            Console.WriteLine("0. Logga ut");
            Console.WriteLine("1. Skapa användare");
            Console.WriteLine("2. Redigera användarinfo");
            Console.WriteLine("3. Byt lösenord för användare");
            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "0":
                    return;
                case "1":
                    UserStore userStore = new UserStore();
                    UserCreator creator = new UserCreator();
                    creator.Create(userStore);
                    break;
                case "2":
                    Console.WriteLine("Ej implementerad");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.WriteLine("Ej implementerad");
                    Console.ReadKey();
                    break;
                case "4":
                    var ds = new EducationClassStore();
                    EducationClassCreator edCreator = new EducationClassCreator();
                    edCreator.Create(ds);
                    break;
                case "5":
                    ClassListPresenter.MainMenu();
                    Console.ReadKey();
                    break;
            }
        }

        public static string[] LoginMenu()
        {
            Console.WriteLine("Inloggning");
            Console.WriteLine();
            Console.WriteLine("Lämna användarnamn tomt för att avsluta");
            string userName = UserInput.GetInput<string>("Användarnamn:");

            if (userName == string.Empty)
            {
                Environment.Exit(0);
            }

            //TODO: Göm lösenordet bättre
            Console.Write("Lösenord: ");
            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.Black;
            string password = UserInput.GetInput<string>();
            Console.ResetColor();

            return new string[] {userName, password};
        }

        public static void EducationSupervisorMainMenu(User educationSupervisor)
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Utbildningledare-meny");
            Console.WriteLine();
            Console.WriteLine("0. Logga ut");
            Console.WriteLine("1. Skapa konton");
            Console.WriteLine("2. Hantera lärare");
            Console.WriteLine("3. Hantara klasser");
            Console.WriteLine("4. Hantera studenter");
            Console.WriteLine("5. Hantera kurser");
            Console.WriteLine("6. Mitt konto");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "0":
                    return;
                case "1":
                    //TODO: Inte köra samma rutin som admin
                    UserStore userStore = new UserStore();
                    UserCreator creator = new UserCreator();
                    creator.Create(userStore);
                    break;
                case "2":
                    EducationSupervisorManageTeacherMenu();
                    Console.ReadKey();
                    break;
                case "3":
                    CoursePresenter.CourseManagementMenu();
                    break;
                case "4":
                    UserManagerPresenter.ManageStudents();
                    break;
                case "5":
                    Console.WriteLine("Ej implementerad");
                    Console.ReadKey();
                    break;
                case "6":
                    Console.WriteLine("Ej implementerad");
                    Console.ReadKey();
                    break;
            }
        }

        // TODO: stoppa metod på bättre ställe
        private static void EducationSupervisorManageTeacherMenu()
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Utbildningsledare - Hantera lärare");
            Console.WriteLine();
            Console.WriteLine("1. Visa alla lärare");
            Console.WriteLine("2. Visa alla studenter för en lärare");
        }
    }
}