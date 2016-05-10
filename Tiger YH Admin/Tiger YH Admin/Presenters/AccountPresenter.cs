using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    class AccountPresenter
    {
        internal static void ManageAccountMenu(User user)
        {
        }

        public static void ChangePassword(User user)
        {
            UserStore userStore = new UserStore();

            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Ändra lösenord");
                Console.WriteLine();

                string oldPassword = UserInput.GetInput<string>("Ange nuvarande lösenord:");

                if (user.Password == oldPassword)
                {
                    string newPassword = UserInput.GetInput<string>("Ange nytt lösenord:");
                    user.Password = newPassword;

                    List<User> users = userStore.All().ToList();

                    foreach (User u in users)
                    {
                        if (u.UserName == user.UserName)
                        {
                            u.Password = newPassword;
                        }
                    }
                    userStore.Save();
                    Console.WriteLine("Lösenord ändrat");
                    UserInput.WaitForContinue();
                    loop = false;
                }
                else
                {
                    Console.WriteLine("Fel lösenord");
                }
            } while (loop);
        }
    }
}