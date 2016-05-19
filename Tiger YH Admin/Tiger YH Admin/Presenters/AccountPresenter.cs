using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class AccountPresenter
    {
        public static void ChangePassword(User user, bool requireOldPassword = true)
        {
            UserStore userStore = new UserStore();

            Console.Clear();
            Console.WriteLine("Ändra lösenord");
            Console.WriteLine();

            string oldPassword = string.Empty;
            if (requireOldPassword)
            {
                oldPassword = UserInput.GetInput<string>("Ange nuvarande lösenord:");
                if (oldPassword == string.Empty)
                {
                    return;
                }
            }

            if (user.Password == oldPassword || ! requireOldPassword)
            {
                string newPassword;
                bool isValid;
                do
                {
                    newPassword = UserInput.GetInput<string>("Ange nytt lösenord:");
                    isValid = newPassword.Length >= 3;
                    if (!isValid)
                    {
                        Console.WriteLine("Du måste ange ett lösenord som är minst 3 tecken");
                    }
                } while (!isValid);

                List<User> users = userStore.All().ToList();

                foreach (User u in users)
                {
                    if (u.UserName == user.UserName)
                    {
                        u.Password = newPassword;
                    }
                }
                userStore.Save();
                Console.WriteLine("Lösenord ändrat, det nya lösenordet börjar gälla vid nästa inloggning");
                UserInput.WaitForContinue();
            }
            else if (requireOldPassword)
            {
                Console.WriteLine("Fel lösenord");
                UserInput.WaitForContinue();
            }
        }
    }
}