﻿using System;
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

					foreach (UserLevel userLevel in Enum.GetValues(typeof(UserLevel)))
					{
						Console.WriteLine( (int) userLevel + " " + userLevel);
					}
					Console.Write("Användarnivå:");
					int chosenLevel = UserInput.GetInput<int>();

					User newUser = new User
					{
						UserName = input,
						Password = password,
						UserLevel = (UserLevel) chosenLevel
					};

					//TODO: Fråga om korrekt input
					userStore.AddItem(newUser);

					Console.WriteLine($"Ny användare {newUser.UserName} skapad");
					Console.ReadKey();
					keepLooping = false;
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
