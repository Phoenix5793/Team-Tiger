﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Models
{
    public class UserStore : DataStore<User>
    {
        public override User FindById(string id)
        {
            return DataSet.ToList().SingleOrDefault(u => u.UserName == id);
        }

        public User LoginUser(string name, string password)
        {
            User user = FindById(name);

            if (user != null && password == user.Password)
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

        public bool HasLevel(string userName, UserLevel userLevel)
        {
            bool exists = HasUser(userName);

            if (!exists) return false;

            User user = FindById(userName);
            UserLevel level = user.UserLevel;
            bool correctLevel = userLevel == level;

            return correctLevel;
        }
    }
}
