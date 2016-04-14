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
	}
}
