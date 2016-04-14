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
			do
			{
				Console.Clear();
				Console.Write("Användarnamn: ");
				string userName = UserInput.GetInput<string>();

				existingUser = userStore.FindById(userName);
				if (existingUser == null)
				{
					Console.Write("Lösenord: ");
					string password = UserInput.GetInput<string>();

					foreach (UserLevel userLevel in Enum.GetValues(typeof(UserLevel)))
					{
						Console.WriteLine( (int) userLevel + " " + userLevel);
					}
					Console.Write("Användarnivå:");
					int chosenLevel = UserInput.GetInput<int>();
					Console.ReadKey();

					User newUser = new User
					{
						UserName = userName,
						Password = password,
						UserLevel = (UserLevel) chosenLevel
					};

					var userList = userStore.DataSet.ToList();
				}
				else
				{
					Console.WriteLine("Användarnamnet är upptaget");
				}

			} while (existingUser == null);

			return existingUser;
		}

	}
}
