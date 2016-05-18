﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

        public void OpenCoursePlan()
        {
            string coursePlanFile = $@"Kursplan\{CourseId}.txt";

            if (!File.Exists(coursePlanFile))
            {
                File.Create(coursePlanFile);
            }

            Console.WriteLine($"Försöker öppna {coursePlanFile}");
            Console.WriteLine("Väntar på att Notepad ska avslutas...");
            Process.Start(coursePlanFile).WaitForExit();
        }
    }
}
