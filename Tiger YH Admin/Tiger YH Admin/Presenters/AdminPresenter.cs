using System;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class AdminPresenter
    {
        public static void ChangeUserPassword()
        {
            string userName = UserInput.GetInput<string>("Användarnamn:");
            User user = new UserStore().FindById(userName);

            if (user == null)
            {
                Console.WriteLine("Användaren finns inte");
                UserInput.WaitForContinue();
            }
            else
            {
                AccountPresenter.ChangePassword(user, false);
            }
        }
    }
}
