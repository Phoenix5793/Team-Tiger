using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Presenters;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            UserStore userStore = new UserStore();
            User user = UserManagerPresenter.LoginUser(userStore);

            Menu.MainMenu(user);
        }
    }
}