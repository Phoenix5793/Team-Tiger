using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Creators;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        loopMenu = AdminMainMenu();
                        break;
                    case UserLevel.EducationSupervisor:
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        loopMenu = EducationSupervisorMainMenu(user);
                        break;
                    case UserLevel.Teacher:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        loopMenu = TeacherMainMenu(user);
                        break;
                    case UserLevel.Student:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        loopMenu = StudentMainMenu(user);
                        break;
                }
            } while (loopMenu);
        }

        private static bool StudentMainMenu(User user)
        {
            bool logout = false;

            do
            {
                Console.Clear();
                Console.WriteLine("Tiger Board!");
                Console.WriteLine("Studentmeny");
                Console.WriteLine();
                Console.WriteLine("0. Logga ut");
                Console.WriteLine("1. Visa mina studieresultat");
                Console.WriteLine("2. Klasslista");
                Console.WriteLine("3. Kurslista");
                Console.WriteLine("4. Byt lösenord");
                Console.WriteLine("5. Visa kursplan");
                Console.WriteLine("6. Visa kursmål");

                Console.WriteLine();
                Console.Write("Ditt val: ");
                string menuChoice = UserInput.GetInput<string>();

                // TODO: Implementera
                switch (menuChoice)
                {
                    case "0":
                        logout = LogoutConfirmation();
                        break;
                    case "1":
                        GradePresenter.ShowStudentGrades(user);
                        break;
                    case "2":
                        ClassListPresenter.ShowClassForStudent(user);
                        break;
                    case "3":
                        CoursePresenter.ShowStudentCourseList(user);
                        break;
                    case "4":
                        AccountPresenter.ChangePassword(user);
                        break;
                    case "5":
                        CoursePresenter.ShowCoursePlan(user);
                        break;
                    case "6":
                        CoursePresenter.ShowStudentCourseGoals(user);
                        break;
                }
            } while (!logout);
            return false;
        }

        private static bool TeacherMainMenu(User user)
        {
            bool logout = false;
            do
            {
                Console.Clear();
                Console.WriteLine("Tiger Board!");
                Console.WriteLine("Lärarmeny");
                Console.WriteLine();
                Console.WriteLine("0. Logga ut");
                Console.WriteLine("1. Visa mina kurser");
                Console.WriteLine("2. Visa studentlista för en kurs");
                Console.WriteLine("3. Betygsätt student");
                Console.WriteLine("4. Betygsätt delmål");
                Console.WriteLine("5. Byt lösenord");
                Console.WriteLine("6. Visa kursplan");
                Console.WriteLine("7. Visa kursmål");

                Console.WriteLine();
                Console.Write("Ditt val: ");
                string menuChoice = UserInput.GetInput<string>();

                switch (menuChoice)
                {
                    case "0":
                        logout = LogoutConfirmation();
                        break;
                    case "1":
                        CoursePresenter.ShowTeacherCourses(user);
                        break;
                    case "2":
                        CoursePresenter.ShowStudentsForCourse(user);
                        break;
                    case "3":
                        GradePresenter.GradeStudentInCourse(user);
                        break;
                    case "4":
                        GradePresenter.GradeStudentGoal(user);
                        break;
                    case "5":
                        AccountPresenter.ChangePassword(user);
                        break;
                    case "6":
                        CoursePresenter.ShowCoursePlan(user);
                        break;
                    case "7":
                        CoursePresenter.ShowCourseGoals();
                        break;

                }
            } while (!logout);
            return false;
        }

        public static bool AdminMainMenu()
        {
            bool logout = false;
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
                        logout = LogoutConfirmation();
                        break;
                    case "1":
                        UserStore userStore = new UserStore();
                        UserCreator creator = new UserCreator();
                        creator.Create(userStore);
                        break;
                    case "2":
                        UserManagerPresenter.ChangeUser(UserLevel.Admin);
                        break;
                    case "3":
                        AdminPresenter.ChangeUserPassword();
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


            } while (!logout);
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

            Console.Write("Lösenord: ");

            string password = string.Empty;
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace)
                {
                    password += keyInfo.KeyChar;
                    Console.Write('*');
                }
                else if (!string.IsNullOrEmpty(password))
                {
                    password = password.Substring(0, password.Length - 1);
                    int cursorPos = Console.CursorLeft;
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                }

                keyInfo = Console.ReadKey(true);
            }


            return new string[] {userName, password};
        }

        public static bool EducationSupervisorMainMenu(User educationSupervisor)
        {
            bool logout = false;

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
                Console.WriteLine("6. Byt lösenord");
                Console.WriteLine();

                string menuChoice = UserInput.GetInput<string>("Ditt val: ");

                switch (menuChoice)
                {
                    case "0":
                        logout = LogoutConfirmation();
                        break;
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
                        UserManagerPresenter.ManageStudents(educationSupervisor);
                        break;
                    case "5":
                        CoursePresenter.CourseManagementMenu(educationSupervisor);
                        break;
                    case "6":
                        AccountPresenter.ChangePassword(educationSupervisor);
                        break;
                }
            } while (!logout);
            return false;
        }

        private static bool LogoutConfirmation()
        {
            Console.Clear();
            return UserInput.AskConfirmation("Vill du logga ut?");
        }
    }
}
