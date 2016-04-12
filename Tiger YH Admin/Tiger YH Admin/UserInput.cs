using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin
{
	static class UserInput
	{
		public static T GetInput<T>()
		{
			string input = Console.ReadLine();
			return (T)Convert.ChangeType(input, typeof(T));
		}

		public static void MainMenuInput()
		{
		}

		public static string LoginGetUserName()
		{
			return Console.ReadLine();

		}

		public static string LoginGetPassword()
		{
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ResetColor();
			return Console.ReadLine();
		}
	}
}
