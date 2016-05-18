using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    public class Goal
    {
        public string GoalId { get; set; }
        public string CourseId { get; set; }
        public string Description { get; set; }
    }
}
