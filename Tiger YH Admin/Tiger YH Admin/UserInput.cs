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

		public static T GetInput<T>(string message)
		{
			Console.Write($"{message} ");
			return GetInput<T>();
		}

		public static T GetInput<T>(string message, int minLength)
		{
			string input;

			do
			{
				Console.Write($"{message} ");
				input = Console.ReadLine();

				if (input.Length < minLength)
				{
					Console.WriteLine($"Input för kort, minst {minLength} tecken");
				}
			} while (input.Length < minLength);

			return GetInput<T>();
		}
	}
}
