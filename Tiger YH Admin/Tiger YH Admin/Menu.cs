using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin
{
	static class Menu
	{
		public static void MainAdminMenu()
		{
			Console.WriteLine("Tiger Board!");
			Console.WriteLine("Admin-meny");
		}

		public static User LoginMenu(UserStore userStore)
		{
			Console.WriteLine("Inloggning");

			Console.Write("Användarnamn: ");
			string userName = UserInput.LoginGetUserName();

			Console.Write("Lösenord: ");
			string password = UserInput.LoginGetPassword();

			return userStore.LoginUser(userName, password);

		}
	}
}
