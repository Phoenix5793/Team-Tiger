using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Creators
{
    class UserCreator : ICreator<User>
    {
        private UserLevel _newUserMaxLevel;

        public User Create(IDataStore<User> userStore, UserLevel maxLevel)
        {
            _newUserMaxLevel = maxLevel;

            return Create(userStore);
        }

        public User Create(IDataStore<User> userStore, User existingUser = null)
        {
            string oldUserName = String.Empty;

            Console.Clear();
            string userName = string.Empty;

            if (existingUser == null)
            {
                Console.WriteLine("Skapa ny användare");
                existingUser = new User();
            }
            else
            {
                Console.WriteLine($"Redigerar {existingUser.FullName()} ({existingUser.UserName})");
                oldUserName = existingUser.UserName;
                userName = existingUser.UserName;
            }

            Console.WriteLine();

            
            bool loopName = false;
            if (existingUser.UserName == null)
            {
                do
                {

                    Console.WriteLine("Lämna namnet tomt för att avbryta");
                    userName = UserInput.GetEditableField("Användarnamn", existingUser.UserName);

                    User checkUser = userStore.FindById(userName);
                    
                    if (checkUser != null && existingUser.UserName != userName)
                    {
                        Console.WriteLine("Användarnamnet är upptaget");
                        UserInput.WaitForContinue();
                        loopName = true;
                    }
                    else if (userName == string.Empty)
                    {
                        return null;
                    }
                    else
                    {
                        break;
                    }
                } while (loopName);
            }

            Console.Write("Lösenord: ");
            string password = UserInput.GetInput<string>();

            foreach (UserLevel userLevel in Enum.GetValues(typeof(UserLevel)))
            {
                if (userLevel >= _newUserMaxLevel)
                {
                    Console.WriteLine((int)userLevel + " " + userLevel);
                }
            }

            int chosenLevel;
            bool loopLevel = true;
            do
            {
                Console.Write("Användarnivå:");
                chosenLevel = UserInput.GetInput<int>();

                if (chosenLevel < (int) _newUserMaxLevel)
                {
                    Console.WriteLine("Ogiltig användarnivå");
                }
                else
                {
                    loopLevel = false;
                }
            } while (loopLevel);

            bool isValid = false;
            string ssn = string.Empty;
            string firstName = UserInput.GetEditableField("Förnamn", existingUser.FirstName);
            string lastName = UserInput.GetEditableField("Efternamn", existingUser.LastName);

            do
            {
                Console.Clear();
                Console.WriteLine("Ange enligt följande: yymmddxxxx");
                string input = UserInput.GetEditableField("Personnummer", existingUser.SSN);
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
            string phoneNumber;
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

            Console.Clear();
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
                    userList.Add(existingUser);
                    userStore = new UserStore(userList);
                    Console.WriteLine($"Användare {existingUser.UserName} redigerad");
                }
                else
                {
                    userStore.AddItem(existingUser);
                    Console.WriteLine($"Ny användare {existingUser.UserName} skapad");
                }
                
                userStore.Save();
                
                UserInput.WaitForContinue();
            }

            return existingUser;
        }
    }
}