using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Models.Creators;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
	        bool loopMenu = true;

	        User user;
			UserStore userDataStore = new UserStore();

			do
			{
				Console.Clear();
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
			        Console.Clear();
			        int menuChoice = Menu.MainAdminMenu();

			        switch (menuChoice)
			        {
						case 1:
					        UserCreator creator = new UserCreator();
					        creator.Create(userDataStore);
					        break;
						case 2:
					        Console.WriteLine("Ej implementerad");
					        break;
						case 3:
					        Console.WriteLine("Ej implementerad");
					        break;
						case 4:
							var ds = new EducationClassStore();
							EducationClassCreator edCreator = new EducationClassCreator();
					        edCreator.Create(ds);
					        break;
			        }

			        Console.ReadKey();
		        }
		        else
		        {
			        Console.WriteLine("Du har inte admin-rättigheter");
			        Console.ReadKey();
		        }
	        }
		}
    }
}
