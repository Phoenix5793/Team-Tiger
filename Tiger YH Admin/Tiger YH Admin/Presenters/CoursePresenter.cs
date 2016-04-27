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
            CourseStore courseStore = new CourseStore();

            string courseId = UserInput.GetInput<string>("Kurs-id:");
            Course existingCourse = courseStore.FindById(courseId);

            if (existingCourse == null)
            {
                Console.WriteLine("Kursen finns inte");
                return;
            }

            Course courseToEdit = courseStore.FindById(courseId);
            List<Course> courseList = courseStore.DataSet.ToList();

            Console.WriteLine("Tryck enter för att behålla gamla värdet");
            string newCourseId = UserInput.GetInput<string>("Nytt kurs-id:");
            string newCourseName = UserInput.GetInput<string>("Nytt kursnamn:");
            string newCourseTeacher = UserInput.GetInput<string>("Ny lärare:");

            Console.WriteLine("TODO: Nytt datum måste anges. YYYY-MM-DD");
            DateTime newCourseStartDate = UserInput.GetDate("Nytt startdatum:");
            DateTime newCourseEndDate = UserInput.GetDate("Nytt slutdatum:");

            Console.WriteLine("Ändringar:");
            Console.WriteLine(courseToEdit.CourseId + " => " + newCourseId);
            Console.WriteLine(courseToEdit.CourseName + " => " + newCourseName);
            Console.WriteLine(courseToEdit.CourseTeacher + " => " + newCourseTeacher);
            Console.WriteLine(courseToEdit.StartDate.ToString("yyyy-MM-dd") + " => " + newCourseStartDate.ToString("yyyy-MM-dd"));
            Console.WriteLine(courseToEdit.EndDate.ToString("yyyy-MM-dd") + " => " + newCourseEndDate.ToString("yyyy-MM-dd"));
            Console.WriteLine();

            bool confirm = UserInput.AskConfirmation("Vill du spara ändringarna?");

            if (confirm)
            {
                Course newCourse = new Course
                {
                    CourseId = newCourseId,
                    CourseName = newCourseName,
                    CourseTeacher = newCourseTeacher,
                    StartDate = newCourseStartDate,
                    EndDate = newCourseEndDate
                };

                courseStore.Remove(existingCourse.CourseId);
                courseStore.AddItem(newCourse);
            }
        }

        private static void DeleteCourse()
        {
            CourseStore courseStore = new CourseStore();

            string courseId = UserInput.GetInput<string>("Kurs-id:");
            bool courseExists = courseStore.FindById(courseId) != null;

            if (!courseExists)
            {
                Console.WriteLine("Kursen finns inte");
                return;
            }

            Course courseToRemove = courseStore.FindById(courseId);
            List<Course> courseList = courseStore.DataSet.ToList();

            bool confirm = UserInput.AskConfirmation($"Vill du radera {courseToRemove.CourseName}?");

            if (confirm)
            {
                courseList.Remove(courseToRemove);
                courseStore.DataSet = courseList;
                courseStore.Save();

                Console.WriteLine("Kursen raderad");
            }
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
                    //TODO: Validera att läraren finns
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
