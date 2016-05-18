using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.DataStore
{
    public class GoalStore : DataStore<Goal>
    {
        public override Goal FindById(string id)
        {
            return All().SingleOrDefault(g => g.GoalId.ToLower() == id.ToLower());
        }
    }
}
