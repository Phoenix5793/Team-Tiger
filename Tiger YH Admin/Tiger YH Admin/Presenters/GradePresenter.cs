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
            var userStore = new UserStore();
            var educationClassStore = new EducationClassStore();
            var courseStore = new CourseStore();
            var gradeStore = new GradeStore();

            List<string> courseList;
            Course course;
            User student;

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
                Console.WriteLine("Tryck enter för att avbryta");
                string studentName = UserInput.GetInput<string>("Ange student-id:");

                if (studentName == string.Empty)
                {
                    return;
                }

                student = userStore.FindById(studentName);

                if (student == null)
                {
                    Console.WriteLine("Finns ingen student med det id:t");
                }
                else
                {
                    // TODO: gör extension method
                    EducationClass studentClass =
                        educationClassStore.All().Single(e => e.HasStudent(student));
                    courseList = studentClass.GetCourseList();
                    break;
                }

            } while (true);

            do
            {
                string courseName = UserInput.GetInput<string>("Ange kurs-id:");
                if (courses.Exists(c => c.CourseId == courseName))
                {
                    // TODO: gör extension method
                    if (courseList.Contains(courseName))
                    {
                        course = courseStore.FindById(courseName);
                        break;
                    }

                    Console.WriteLine("Studentens klass läser inte kursen");
                    UserInput.WaitForContinue();
                }
                else
                {
                    Console.WriteLine("Kursen finns inte eller du är inte lärare för den");
                    UserInput.WaitForContinue();
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
                    case "g":
                    case "vg":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Ange ett giltigt betyg (IG, G, VG)");
                        break;
                }
            } while (loop);

            var gradeLevel = grade.ToEnum<GradeLevel>();

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

        public static void ShowStudentGrades(User student)
        {
            var gradeStore = new GradeStore();

            List<Grade> grades = gradeStore.FindGradesForStudent(student).ToList();
            PrintGrades(grades);
        }

        private static void PrintGrades(IEnumerable<Grade> grades)
        {
            var courseStore = new CourseStore();

            Console.Clear();
            Console.WriteLine(
                "Kurs-id".PadRight(10) +
                "Kursnamn".PadRight(40) +
                "Betyg".PadRight(10)
                );
            Console.WriteLine(new string('-', 60));

            foreach (Grade grade in grades)
            {
                Course course = courseStore.FindById(grade.CourseId);
                Console.WriteLine(
                    course.CourseId.PadRight(10) +
                    course.CourseName.PadRight(40) +
                    grade.Result);
            }

            UserInput.WaitForContinue();
        }
    }
}