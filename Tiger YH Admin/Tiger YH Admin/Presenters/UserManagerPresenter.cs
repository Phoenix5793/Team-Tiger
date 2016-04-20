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
                MainMenu();

                int menuChoice = UserInput.GetInput<int>("Ditt val:");

                switch (menuChoice)
                {
                    case 0:
                        return;
                    case 1:
                        SearchForUser();
                        break;
                    case 2:
                        ListAllUsers();
                        Console.ReadKey();
                        break;
                }
            } while (loopMenu);
        }

        private static void MainMenu()
        {
            Console.WriteLine("0. Tillbaka till föregående meny");
            Console.WriteLine("1. Sök efter användare");
            Console.WriteLine("2. Lista alla användare");
            Console.WriteLine();
        }

        private static void SearchForUser()
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

        private static void ShowUserInfo(User user)
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

    }
}
