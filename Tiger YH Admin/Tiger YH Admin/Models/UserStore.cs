using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    class UserStore : DataStore<User>
    {
	    public UserStore() : base()
	    {

	    }
        public User GetUserByName(string name)
        {
			//TODO: Skriv tester
	        List<User> userList = Load().ToList();

	        return userList.SingleOrDefault(u => u.UserName == name);
        }

	    public bool AddUser(User newUser)
	    {
			// TODO: Skriv tester
		    User existingUser = DataSet.SingleOrDefault(u => u.UserName == newUser.UserName);
		    List<User> userList = DataSet.ToList();

		    if (existingUser == null)
		    {
			    userList.Add(newUser);
			    DataSet = userList;
				Save();
			    return true;
		    }

		    return false;
	    }
    }
}
