using System;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Creators
{
    class UserCreator : ICreator<User>
    {
        public User Create(IDataStore<User> userStore)
        {
            User existingUser = null;
            bool keepLooping = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Skapa ny användare");
                Console.WriteLine();
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

                    foreach (UserLevel userLevel in Enum.GetValues(typeof(UserLevel)))
                    {
                        Console.WriteLine((int)userLevel + " " + userLevel);
                    }
                    Console.Write("Användarnivå:");
                    int chosenLevel = UserInput.GetInput<int>();
                    bool isValid = false;
                    string ssn = string.Empty;
                    string firstName = UserInput.GetInput<string>("Förnamn:");
                    string surname = UserInput.GetInput<string>("Efternamn:");

                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Ange enligt följande: yymmddxxxx");
                        input = UserInput.GetInput<string>("Personnummer:");
                        if (input.Length != 10)
                        {
                            Console.WriteLine("Personnummret måste vara 10 tecken");
                        }

                        else if (!input.IsValidLuhn())
                        {
                            Console.WriteLine("Ogiltigt personnummer");
                        }
                        else
                        {
                            ssn = input;
                            isValid = true;
                        }
                    } while (!isValid);

                    isValid = false;
                    string phoneNumber = string.Empty;
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("Telefonnummret måste vara 10 siffror långt");
                        phoneNumber = UserInput.GetInput<string>("Telefonnummer:");

                        if (phoneNumber.Length != 10)
                        {
                            Console.WriteLine("Telefonnummret måste vara 10 siffror");
                        }
                        else if (!phoneNumber.IsAllDigits())
                        {
                            Console.WriteLine("Endast siffror är tillåtna");
                        }
                        else
                        {
                            isValid = true;
                        }

                    } while (!isValid);



                    User newUser = new User
                    {
                        UserName = input,
                        Password = password,
                        UserLevel = (UserLevel)chosenLevel,
                        FirstName = firstName,
                        Surname = surname,
                        SSN = ssn,
                        PhoneNumber = phoneNumber
                    };

                    //TODO: Fråga om korrekt input
                    Console.WriteLine($"Andvändarnamn: {newUser.UserName}");
                    Console.WriteLine($"Lösenord: {newUser.Password}");
                    Console.WriteLine($"Namn: {newUser.FullName()}");
                    Console.WriteLine($"Personnummer: {newUser.SSN}");
                    Console.WriteLine($"Telefonnummer: {newUser.PhoneNumber}");
                    Console.WriteLine($"Användarnivå: {newUser.UserLevel}");

                    bool confirm = UserInput.AskConfirmation("Vill du spara användaren?");

                    if (confirm)
                    {
                        userStore.AddItem(newUser);
                        userStore.Save();

                        Console.WriteLine($"Ny användare {newUser.UserName} skapad");
                        UserInput.WaitForContinue();
                        keepLooping = false;
                    }
                }
                else
                {
                    Console.WriteLine("Användarnamnet är upptaget");
                    UserInput.WaitForContinue();
                }
            } while (existingUser == null && keepLooping);

            return existingUser;
        }
    }
}
