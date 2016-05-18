using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Creators
{
    class UserCreator : ICreator<User>
    {
        public User Create(IDataStore<User> userStore, User existingUser = null)
        {
            string oldUserName = String.Empty;

            Console.Clear();

            if (existingUser == null)
            {
                Console.WriteLine("Skapa ny användare");
                existingUser = new User();
            }
            else
            {
                Console.WriteLine($"Redigerar {existingUser.FullName()} ({existingUser.UserName})");
                oldUserName = existingUser.UserName;
            }

            Console.WriteLine();

            string userName = string.Empty;
            bool loopName = false;
            if (existingUser.UserName == string.Empty)
            {
                do
                {
                    Console.WriteLine("Lämna namnet tomt för att avbryta");
                    userName = UserInput.GetEditableField("Användarnamn", existingUser.UserName);

                    User checkUser = userStore.FindById(userName);
                    if (existingUser.UserName == userName)
                    {
                    }
                    if (checkUser != null && existingUser.UserName != userName)
                    {
                        Console.WriteLine("Användarnamnet är upptaget");
                        UserInput.WaitForContinue();
                        loopName = true;
                    }
                    else if (userName == string.Empty)
                    {
                        break;
                    }
                } while (loopName);
            }

            Console.Write("Lösenord: ");
            string password = UserInput.GetInput<string>();

            foreach (UserLevel userLevel in Enum.GetValues(typeof(UserLevel)))
            {
                Console.WriteLine((int) userLevel + " " + userLevel);
            }
            Console.Write("Användarnivå:");
            int chosenLevel = UserInput.GetInput<int>();
            bool isValid = false;
            string ssn = string.Empty;
            string firstName = UserInput.GetEditableField("Förnamn", existingUser.FirstName);
            string lastName = UserInput.GetEditableField("Efternamn", existingUser.LastName);

            string input;
            do
            {
                Console.Clear();
                Console.WriteLine("Ange enligt följande: yymmddxxxx");
                input = UserInput.GetEditableField("Personnummer", existingUser.SSN);
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
                phoneNumber = UserInput.GetEditableField("Telefonnummer", existingUser.PhoneNumber);

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


            existingUser = new User
            {
                UserName = userName,
                Password = password,
                UserLevel = (UserLevel) chosenLevel,
                FirstName = firstName,
                LastName = lastName,
                SSN = ssn,
                PhoneNumber = phoneNumber
            };

            Console.WriteLine($"Användarnamn: {existingUser.UserName}");
            Console.WriteLine($"Lösenord: {existingUser.Password}");
            Console.WriteLine($"Namn: {existingUser.FullName()}");
            Console.WriteLine($"Personnummer: {existingUser.SSN}");
            Console.WriteLine($"Telefonnummer: {existingUser.PhoneNumber}");
            Console.WriteLine($"Användarnivå: {existingUser.UserLevel}");

            bool confirm = UserInput.AskConfirmation("Vill du spara användaren?");

            if (confirm)
            {
                if (oldUserName != string.Empty)
                {
                    List<User> userList = userStore.All().Where(u => u.UserName != oldUserName).ToList();
                    userStore = new UserStore(userList);
                }

                userStore.AddItem(existingUser);
                userStore.Save();

                Console.WriteLine($"Ny användare {existingUser.UserName} skapad");
                UserInput.WaitForContinue();
            }

            return existingUser;
        }
    }
}