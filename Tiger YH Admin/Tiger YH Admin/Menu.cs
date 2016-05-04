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
            do
            {
                Console.Clear();
                switch (user.UserLevel)
                {
                    case UserLevel.Admin:
                        loopMenu = AdminMainMenu();
                        break;
                    case UserLevel.EducationSupervisor:
                        loopMenu = EducationSupervisorMainMenu(user);
                        break;
                    case UserLevel.Teacher:
                        loopMenu = TeacherMainMenu(user);
                        break;
                    case UserLevel.Student:
                        loopMenu = StudentMainMenu(user);
                        break;
                }
            } while (loopMenu);
        }

        private static bool StudentMainMenu(User user)
        {
            bool loop = true;

            do
            {
                Console.Clear();
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
                        return false;
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
            } while (loop);
            return false;
        }

        private static bool TeacherMainMenu(User user)
        {
            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Tiger Board!");
                Console.WriteLine("Lärarmeny");
                Console.WriteLine();
                Console.WriteLine("0. Logga ut");
                Console.WriteLine("1. Visa mina kurser");
                Console.WriteLine("2. Visa studentlista för en kurs");
                Console.WriteLine("3. Visa klasslistor");
                Console.WriteLine("4. Betygsätt studenter");

                Console.WriteLine();
                Console.Write("Ditt val: ");
                string menuChoice = UserInput.GetInput<string>();

                // TODO: Implementera
                switch (menuChoice)
                {
                    case "0":
                        loop = false;
                        break;
                    case "1":
                        Console.WriteLine("Ej implementerad");
                        break;
                    case "2":
                        CoursePresenter.ShowStudentsForCourse(user);
                        break;
                    case "3":
                        Console.WriteLine("Ej implementerad");
                        break;
                    case "4":
                        Console.WriteLine("Ej implementerad");
                        break;
                }
            } while (loop);
            return false;
        }

        public static bool AdminMainMenu()
        {
            bool loop = true;
            do
            {
                Console.Clear();
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
                        return false;
                    case "1":
                        UserStore userStore = new UserStore();
                        UserCreator creator = new UserCreator();
                        creator.Create(userStore);
                        break;
                    case "2":
                        Console.WriteLine("Ej implementerad");
                        UserInput.WaitForContinue();
                        break;
                    case "3":
                        Console.WriteLine("Ej implementerad");
                        UserInput.WaitForContinue();
                        break;
                    case "4":
                        var ds = new EducationClassStore();
                        EducationClassCreator edCreator = new EducationClassCreator();
                        edCreator.Create(ds);
                        break;
                    case "5":
                        ClassListPresenter.MainMenu();
                        break;
                }


            } while (loop);
            return false;
        }

        public static string[] LoginMenu()
        {
            Console.Clear();
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

        public static bool EducationSupervisorMainMenu(User educationSupervisor)
        {
            bool loop = true;

            do
            {
                Console.Clear();
                Console.WriteLine("Tiger Board!");
                Console.WriteLine("Utbildningledare-meny");
                Console.WriteLine();
                Console.WriteLine("0. Logga ut");
                Console.WriteLine("1. Skapa konton");
                Console.WriteLine("2. Hantera lärare");
                Console.WriteLine("3. Hantera klasser");
                Console.WriteLine("4. Hantera studenter");
                Console.WriteLine("5. Hantera kurser");
                Console.WriteLine("6. Mitt konto");
                Console.WriteLine();

                string menuChoice = UserInput.GetInput<string>("Ditt val: ");

                switch (menuChoice)
                {
                    case "0":
                        return false;
                    case "1":
                        //TODO: Inte köra samma rutin som admin
                        UserStore userStore = new UserStore();
                        UserCreator creator = new UserCreator();
                        creator.Create(userStore);
                        break;
                    case "2":
                        EducationSupervisorPresenter.ManageTeacherMenu();
                        break;
                    case "3":
                        EducationSupervisorPresenter.ManageClassMenu(educationSupervisor);
                        break;
                    case "4":
                        UserManagerPresenter.ManageStudents();
                        break;
                    case "5":
                        CoursePresenter.CourseManagementMenu(educationSupervisor);
                        break;
                    case "6":
                        AccountPresenter.ManageAccountMenu(educationSupervisor);
                        break;
                }
            } while (loop);
            return false;
        }
    }
}
