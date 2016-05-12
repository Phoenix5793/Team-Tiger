using System;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Presenters;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.ResetColor();
                UserStore userStore = new UserStore();
                ProgramSetup(userStore);
                User user = UserManagerPresenter.LoginUser(userStore);
                Menu.MainMenu(user);
            }
        }

        static void ProgramSetup(UserStore userStore)
        {
            User adminUser = userStore.FindById("admin");

            if (adminUser == null)
            {
                Console.WriteLine("Admin-konto existerar ej! Skapar...");
                CreateAdmin(userStore);
                Console.WriteLine("Admin-konto skapat.");
                Console.WriteLine("Användarnamn: admin");
                Console.WriteLine("Lösenord: admin");
                Console.WriteLine("Byt genast lösenord på kontot!");
                UserInput.WaitForContinue();
            }
        }

        static void CreateAdmin(UserStore userStore)
        {
            User admin = new User
            {
                UserName = "admin",
                FirstName = "Admin",
                Password = "admin",
                UserLevel = UserLevel.Admin
            };

            userStore.AddItem(admin);
            userStore.Save();
        }
    }
}
