﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Models.Creators;
using Tiger_YH_Admin.Presenters;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
	        bool loopMenu = true;

	        User user;
			UserStore userStore = new UserStore();

			do
			{
				Console.Clear();
				string[] credentials = Menu.LoginMenu();
				user = userStore.LoginUser(credentials[0], credentials[1]);

				if (user != null)
				{
					Console.WriteLine("Inloggad som " + user.UserLevel);
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
					        creator.Create(userStore);
					        break;
						case 2:
					        UserManagerPresenter.Run();
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
                if(user.UserLevel == UserLevel.EducationSupervisor)
                {
                    Console.Clear();

                    int menuChoice = Menu.EducationSupervisorMenu();

                    switch (menuChoice)
                    {
                        case 1:
                            UserCreator creator = new UserCreator();
                            creator.Create(userStore);
                            break;
                        case 2:
                            UserManagerPresenter.SearchForUser();
                            break;
                        case 3:
                            Menu.ManageCourses();
                            
                            break;
                        case 4:
                            Menu.ManageStudents();
                          
                            break;
                       case 5:
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
