using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    public class UserStore : DataStore<User>
    {
        public UserStore() : base()
        {
        }

        public UserStore(IEnumerable<User> users) : base(users)
        {            
        }

        public override User FindById(string id)
        {
            return DataSet.ToList().SingleOrDefault(u => u.UserName == id);
        }

        public User LoginUser(string name, string password)
        {
            User user = FindById(name);

            if (user?.Password == password)
            {
                return user;
            }
            return null;
        }

        public bool HasUser(string userId)
        {
            User user = FindById(userId);
            return user != null;
        }
    }
}
