using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Creators;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class UserManagerPresenter
    {
        private static User student;

        public static void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("0. Tillbaka till föregående meny");
                Console.WriteLine("1. Sök efter användare");
                Console.WriteLine("2. Lista alla användare");
                Console.WriteLine();

                string menuChoice = UserInput.GetInput<string>("Ditt val:");

                switch (menuChoice)
                {
                    case "0":
                        return;
                    case "1":
                        ShowUserAccount();
                        break;
                    case "2":
                        ListUsers();
                        break;
                }
            }
        }

        private static User SearchForUser()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tryck enter utan att ange namn för att avbryta.");
                string input = UserInput.GetInput<string>("Sök användarnamn:");

                if (input == string.Empty)
                {
                    return null;
                }

                var userStore = new UserStore();
                User user = userStore.FindById(input);

                if (user == null)
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet.");
                }
                else
                {
                    return user;
                }
            }
        }

        private static void ShowStudentInformation()
        {
            student = SearchForUser();
            if (student != null)
            {
                PrintStudentInfo(student);
            }
        }

        private static void ShowUserAccount()
        {
            User user = SearchForUser();
            if (user != null)
            {
                PrintUserInfo(user);
            }
        }

        public static void PrintUserInfo(User user)
        {
            Console.Clear();
            Console.WriteLine("Visar användare");
            Console.WriteLine();
            Console.WriteLine($"Användarnamn: {user.UserName} ({user.UserLevel})");
            Console.WriteLine($"Namn: {user.FullName()}");
            Console.WriteLine($"Personnummer: {user.SSN}");
            Console.WriteLine($"Telefonnummer: {user.PhoneNumber}");

            UserInput.WaitForContinue();
        }

        private static void PrintStudentInfo(User student)
        {
            GradePresenter.ShowStudentGrades(student);
        }

        public static void ListTeachers()
        {
            IEnumerable<User> teachers = new UserStore().All().IsTeacher();
            ListUsers(teachers);
        }

        private static void ListUsers(IEnumerable<User> userList = null)
        {
            if (userList == null)
            {
                userList = new UserStore().All().ToList();
            }

            Console.WriteLine(
                "Användarnamn".PadRight(14) +
                "Namn".PadRight(20) +
                "Typ".PadRight(10) +
                "Telefon".PadRight(10) +
                "Person-nr".PadRight(12)
                );
            Console.WriteLine(new string('-', 66));

            foreach (User user in userList)
            {
                Console.Write(user.UserName.PadRight(14));
                Console.Write(user.FullName().PadRight(20));
                Console.Write(user.UserLevel.ToString().PadRight(10));
                Console.Write(user.PhoneNumber.PadRight(10));
                Console.Write(user.SSN.PadRight(12));
                Console.WriteLine();
            }

            UserInput.WaitForContinue();
        }

        public static void ManageStudents(User user)
        {
            Console.Clear();

            Console.WriteLine("Hantera studenter");
            Console.WriteLine();
            Console.WriteLine("1. Visa student-information");
            Console.WriteLine("2. Ändra student-information");
            Console.WriteLine("3. Sätt betyg");
            Console.WriteLine("4. Lägg till student");
            Console.WriteLine("5. Ta bort student");
            Console.WriteLine("6. Visa studieresultat för en student");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "1":
                    ShowStudentInformation();
                    break;
                case "2":
                    UserManagerPresenter.ChangeUser(UserLevel.Student);
                    break;
                case "3":
                    GradePresenter.GradeStudent(user);
                    break;
                case "4":
                    ClassListPresenter.AddStudentToClass();
                    break;
                case "5":
                    ClassListPresenter.RemoveStudentFromClass();
                    break;
                case "6":
                    ShowStudentInformation();
                    break;
                default:
                    break;
            }
        }

        public static User LoginUser(UserStore userStore)
        {
            bool loopMenu = true;
            User user;

            do
            {
                Console.Clear();
                string[] credentials = Menu.LoginMenu();
                user = userStore.LoginUser(credentials[0], credentials[1]);

                if (user != null)
                {
                    Console.WriteLine("Inloggad som " + user.UserLevel);
                    loopMenu = false;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("Fel användarnamn eller lösenord");
                    UserInput.WaitForContinue();
                }
            } while (loopMenu);

            return user;
        }

        public static void PrintStudentList(List<string> studentNames)
        {
            Console.Clear();
            Console.WriteLine(
                "Anv.namn".PadRight(14) +
                "Namn".PadRight(30) +
                "Telefon".PadRight(15)
                );
            Console.WriteLine(new string('-', 60));

            UserStore userStore = new UserStore();

            foreach (string studentName in studentNames)
            {
                User student = userStore.FindById(studentName);
                Console.WriteLine(
                    student.UserName.PadRight(14) +
                    student.FullName().PadRight(30) +
                    student.PhoneNumber.PadRight(15)
                    );
            }

            UserInput.WaitForContinue();
        }

        public static void ChangeUser(UserLevel maxLevel)
        {
            UserStore userStore = new UserStore();
            UserCreator creator = new UserCreator();

            Console.Clear();
            Console.WriteLine("Tryck enter för att avbryta");
            string userName = UserInput.GetInput<string>("Ange användarnamn att redigera:");

            if (userName == string.Empty)
            {
                return;
            }

            User user = userStore.FindById(userName);

            if (user == null)
            {
                Console.WriteLine("Användaren finns inte");
                UserInput.WaitForContinue();
                return;
            }

            if (user.UserLevel < maxLevel)
            {
                Console.WriteLine($"Kan ej redigera användarnivån {user.UserLevel}");
                UserInput.WaitForContinue();
                return;
            }

            creator.Create(userStore, user);
        }
    }
}
