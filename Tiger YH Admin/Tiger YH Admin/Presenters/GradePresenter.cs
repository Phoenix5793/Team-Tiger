using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    public static class GradePresenter
    {
        public static void GradeStudent(User user)
        {
            UserStore userStore = new UserStore();
            CourseStore courseStore = new CourseStore();
            GradeStore gradeStore = new GradeStore();
            Course course;
            User student;
            GradeLevel gradeLevel;


            Console.Clear();
            Console.WriteLine("Betygsätt student");
            Console.WriteLine();

            List<Course> courses;
            if (user.HasLevel(UserLevel.Teacher))
            {
                courses = courseStore.FindByTeacherId(user.UserName).ToList();
            }
            else
            {
                courses = courseStore.All().ToList();
            }

            do
            {
                string courseName = UserInput.GetInput<string>("Ange kurs-id:");
                if (courses.Exists(c => c.CourseId == courseName))
                {
                    Console.WriteLine("kursen finns");
                    course = courseStore.FindById(courseName);
                    break;
                }
                else
                {
                    Console.WriteLine("Kursen finns inte eller du är inte lärare för den");
                }

            } while (true);


            do
            {
                string studentName = UserInput.GetInput<string>("Ange student-id:");
                student = userStore.FindById(studentName);

                if (student == null)
                {
                    Console.WriteLine("Finns ingen student med det id:t");
                }
                else
                {
                    break;
                }

            } while (true);

            string grade;
            bool loop = true;
            do
            {
                grade = UserInput.GetInput<string>("Ange betyg:");
                switch (grade.ToLower())
                {
                    case "ig":
                        loop = false;
                        break;
                    case "g":
                        loop = false;
                        break;
                    case "vg":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Ange ett giltigt betyg (IG, G, VG)");
                        break;
                }
            } while (loop);

            gradeLevel = grade.ToEnum<GradeLevel>();

            Console.WriteLine($"Student: {student.FullName()} ({student.UserName})");
            Console.WriteLine($"Kurs: {course.CourseName} ({course.CourseId})");
            Console.WriteLine($"Betyg: {gradeLevel}");
            bool confirm = UserInput.AskConfirmation("Betygsätt student?");

            if (confirm)
            {
                gradeStore.GradeStudent(student, course, gradeLevel);
                gradeStore.Save();
            }
        }
    }
}