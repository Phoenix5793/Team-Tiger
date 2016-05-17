﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace Tiger_YH_Admin.Models
{
    [DelimitedRecord("|")]
    [IgnoreEmptyLines]
    public class Course
    {
        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CourseTeacher { get; set; }
    }
}
