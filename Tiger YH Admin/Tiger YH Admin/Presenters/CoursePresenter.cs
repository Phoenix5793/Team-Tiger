using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class CoursePresenter
    {
        public static void CourseManagementMenu()
        {
            Console.Clear();

            Console.WriteLine("Hantera kurser");
            Console.WriteLine();
            Console.WriteLine("1. Skapa kurs");
            Console.WriteLine("2. Ta bort kurs");
            Console.WriteLine("3. Ändra lärare för en kurs");
            Console.WriteLine("4. Ändra kurs");
            Console.WriteLine("5. Visa kurser");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "1":
                    CoursePresenter.CreateCourse();
                    break;
                case "2":
                    CoursePresenter.DeleteCourse();
                    break;
                case "3":
                    ChangeTeacherForCourses();
                    break;
                case "4":
                    CoursePresenter.EditCourse();
                    break;
                case "5":
                    CoursePresenter.ListAllCourses();
                    break;
            }
            Console.ReadKey();
        }

        private static void ListAllCourses()
        {
            CourseStore courseStore = new CourseStore();
            List<Course> courseList = courseStore.DataSet.ToList();

            foreach (Course course in courseList)
            {
                Console.WriteLine(course.CourseId);
            }

        }

        private static void EditCourse()
        {
            throw new NotImplementedException();
        }

        private static void DeleteCourse()
        {
            throw new NotImplementedException();
        }

        private static void CreateCourse()
        {
            CourseStore courseStore = new CourseStore();
            string courseId;
            bool courseExists;
            do
            {
                courseId = UserInput.GetInput<string>("Kurs-id:");
                courseExists = courseStore.FindById(courseId) != null;
                if (courseExists)
                {
                    Console.WriteLine("Kurs-id redan använt");
                }
                else
                {
                    string courseName = UserInput.GetInput<string>("Kursbeskrivning:");
                    Console.WriteLine("Skriv datum enligt YYYY-MM-DD");
                    DateTime courseStartDate = UserInput.GetDate("Startdatum:");
                    DateTime courseEndDate = UserInput.GetDate("Slutdatum:");
                    string courseTeacher = UserInput.GetInput<string>("Lärare:");

                    Course newCourse = new Course
                    {
                        CourseId = courseId,
                        CourseName = courseName,
                        StartDate = courseStartDate,
                        EndDate = courseEndDate,
                        CourseTeacher = courseTeacher
                    };

                    courseStore.AddItem(newCourse);
                    Console.WriteLine("Kursen skapad");
                }
            } while (courseExists);
        }

        internal static void ChangeTeacherForCourses()
        {
            throw new NotImplementedException();
        }
    }
}
