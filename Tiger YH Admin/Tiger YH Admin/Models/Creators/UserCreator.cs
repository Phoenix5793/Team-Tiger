using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models.Creators
{
    class UserCreator : ICreator<User>
    {
        public User Create(IDataStore<User> userStore)
        {
            Console.Clear();
            Console.WriteLine("Skapa ny användare");
            Console.WriteLine();

            User existingUser = null;
            bool keepLooping = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Lämna namnet tomt för att avbryta");
                string input = UserInput.GetInput<string>("Användarnamn:");

                if (input == string.Empty)
                {
                    break;
                }

                existingUser = userStore.FindById(input);

                if (existingUser == null && keepLooping)
                {
                    Console.Write("Lösenord: ");
                    string password = UserInput.GetInput<string>();

                    foreach (UserLevel userLevel in Enum.GetValues(typeof (UserLevel)))
                    {
                        Console.WriteLine((int) userLevel + " " + userLevel);
                    }
                    Console.Write("Användarnivå:");
                    int chosenLevel = UserInput.GetInput<int>();

                    string firstName = null;
                    string surname = null;
                    string ssn = null;
                    string phoneNumber = null;

                    if (existingUser?.UserLevel != UserLevel.Admin)
                    {
                        Console.Write("Förnamn: ");
                        firstName = UserInput.GetInput<string>();
                        Console.Write("Efternamn:");
                        surname = UserInput.GetInput<string>();
                        Console.Write("Personnummer: ");
                        ssn = UserInput.GetInput<string>();
                        Console.Write("Telefonnummer: ");
                        phoneNumber = UserInput.GetInput<string>();
                    }

                    User newUser = new User
                    {
                        UserName = input,
                        Password = password,
                        UserLevel = (UserLevel) chosenLevel,
                        FirstName = firstName,
                        Surname = surname,
                        SSN = ssn,
                        PhoneNumber = phoneNumber
                    };

                    //TODO: Fråga om korrekt input
                    Console.Write("VIll du spara? Ja/nej ");
                    string answer = UserInput.GetInput<string>();

                    if (answer.ToLower() == "ja" || answer.ToLower() == "j")
                    {
                        userStore.AddItem(newUser);

                        Console.WriteLine($"Ny användare {newUser.UserName} skapad");
                        Console.ReadKey();
                        keepLooping = false;
                    }
                }
                else
                {
                    Console.WriteLine("Användarnamnet är upptaget");
                }
            } while (existingUser == null && keepLooping);

            return existingUser;
        }
    }
}