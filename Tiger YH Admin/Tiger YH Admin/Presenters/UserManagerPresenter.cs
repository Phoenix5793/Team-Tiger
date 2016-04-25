using System;
using System.Linq;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class UserManagerPresenter
    {
        public static void Run()
        {
            bool loopMenu = true;
            do
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
                        SearchForUser();
                        break;
                    case "2":
                        ListAllUsers();
                        Console.ReadKey();
                        break;
                }
            } while (loopMenu);
        }

        public static void SearchForUser()
        {
            bool keepLooping = true;

            do
            {
                Console.WriteLine("Tryck enter utan att ange namn för att avbryta.");
                string input = UserInput.GetInput<string>("Sök användarnamn:");

                if (input == string.Empty)
                {
                    return;
                }

                var userStore = new UserStore();
                var user = userStore.FindById(input);

                if (user == null)
                {
                    Console.WriteLine("Det finns ingen användare med det användarnamnet.");
                }
                else
                {
                    ShowUserInfo(user);
                }
            } while (keepLooping);

        }

        public static void ShowUserInfo(User user)
        {
            Console.WriteLine("Visar användare");
            Console.WriteLine();
            Console.WriteLine($"Användarnamn: {user.UserName} ({user.UserLevel})");
            Console.WriteLine($"Namn: {user.FullName()}");
            Console.WriteLine($"Personnummer: {user.SSN}");
            Console.WriteLine($"Telefonnummer: {user.PhoneNumber}");
            Console.WriteLine();
        }

        private static void ListAllUsers()
        {
            var userList = new UserStore().DataSet.ToList();

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
        }

        public static void ManageStudents()
        {
            Console.Clear();

            Console.WriteLine("Hantera studenter");
            Console.WriteLine();
            Console.WriteLine("1. Visa student information");
            Console.WriteLine("2. Ändra student information");
            Console.WriteLine("3. Sätt betyg");
            Console.WriteLine("4. Lägg till student");
            Console.WriteLine("5. Ta bort student");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "1":
                    Console.WriteLine("Visar studeninformation"); //TODO : Fixa metod
                    break;
                case "2":
                    Console.WriteLine("Ändrar studentinformation"); //TODO: Fixa metod
                    break;
                case "3":
                    Console.WriteLine("Sätt betyg på student"); //TODO: Fixa metod
                    break;
                case "4":
                    ClassListPresenter.AddStudentToClass();
                    break;
                case "5":
                    ClassListPresenter.RemoveStudentFromClass();
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
                    Console.WriteLine("Fel användarnamn eller lösenord");
                    Console.ReadKey();
                }

            } while (loopMenu);

            return user;
        }


    }
}
