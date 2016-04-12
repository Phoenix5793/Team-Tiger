using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
	        bool loopMenu = true;

	        User user;
	        do
	        {
				Console.Clear();
				UserStore userDataStore = new UserStore();
				user = Menu.LoginMenu(userDataStore);

				if (user != null)
				{
					Console.WriteLine("Inloggade");
					loopMenu = false;
				}
				else
				{
					Console.WriteLine("Fel användarnamn eller lösenord");
					Console.ReadKey();
				}

			} while (loopMenu);

	        loopMenu = true;
	        while (loopMenu)
	        {
		        if (user.UserLevel == UserLevel.Admin)
		        {
					Menu.MainAdminMenu();
				}
		        else
		        {
			        Console.WriteLine("Du har inte admin-rättigheter");
		        }
			}
		}
    }
}
