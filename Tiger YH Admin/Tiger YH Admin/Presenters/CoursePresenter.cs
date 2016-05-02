using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("6. Visa avslutade kurser");
            Console.WriteLine("7. Visa pågående kurser");
            Console.WriteLine("8. Visa kommande kurser");


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
                case "6":
                    TeacherStaffingFinishedCourses();
                    break;
                case "7":
                    TeacherStaffingCurrentCourses();
                    break;
                case "8":
                    TeacherStaffingFutureCourses();
                    break;

            }
            Console.ReadKey();
        }

        private static void ListAllCourses()
        {
            CourseStore courseStore = new CourseStore();
            List<Course> courseList = courseStore.All().ToList();


            Console.WriteLine("Kurs-id".PadRight(10) +
                              "Kursnamn".PadRight(40) +
                              "Startdatum".PadRight(12) +
                              "Slutdatum".PadRight(12) +
                              "Lärare"
                );
            Console.WriteLine(new string('-', 80));

            foreach (Course course in courseList)
            {
                Console.WriteLine(
                    course.CourseId.PadRight(10) +
                    course.CourseName.PadRight(40) +
                    course.StartDate.ToShortDateString().PadRight(12) +
                    course.EndDate.ToShortDateString().PadRight(12) +
                    course.CourseTeacher
                    );
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

            Console.WriteLine("Tryck enter för att behålla gamla värdet");
            string newCourseId = UserInput.GetInput<string>("Nytt kurs-id:");
            string newCourseName = UserInput.GetInput<string>("Nytt kursnamn:");
            string newCourseTeacher = UserInput.GetInput<string>("Ny lärare:");

            Console.WriteLine("TODO: Nytt datum måste anges. YYYY-MM-DD");
            DateTime newCourseStartDate = UserInput.GetDate("Nytt startdatum:");
            DateTime newCourseEndDate = UserInput.GetDate("Nytt slutdatum:");

            if (newCourseId == string.Empty)
            {
                newCourseId = courseToEdit.CourseId;
            }
            if (newCourseName == string.Empty)
            {
                newCourseName = courseToEdit.CourseName;
            }
            if (newCourseTeacher == string.Empty)
            {
                newCourseTeacher = courseToEdit.CourseTeacher;
            }
            //TODO: Hantera tom input på datum

            Course newCourse = new Course
            {
                CourseId = newCourseId,
                CourseName = newCourseName,
                CourseTeacher = newCourseTeacher,
                StartDate = newCourseStartDate,
                EndDate = newCourseEndDate
            };

            Console.WriteLine("Ändra från:");
            PrintCourseInfo(courseToEdit);
            Console.WriteLine("Ändras till:");
            PrintCourseInfo(newCourse);

            Console.WriteLine();
            bool confirm = UserInput.AskConfirmation("Vill du spara ändringarna?");

            if (confirm)
            {
                courseStore.Remove(existingCourse.CourseId);
                courseStore.AddItem(newCourse);
                Console.WriteLine("Ändringar sparade.");
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
            List<Course> courseList = courseStore.All().ToList();

            bool confirm = UserInput.AskConfirmation($"Vill du radera {courseToRemove.CourseName}?");

            if (confirm)
            {
                courseList.Remove(courseToRemove);
                courseStore = new CourseStore(courseList);
                courseStore.Save();

                Console.WriteLine("Kursen raderad");
            }
        }

        private static void CreateCourse()
        {
            CourseStore courseStore = new CourseStore();
            UserStore userStore = new UserStore();

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

                    string courseTeacher = string.Empty;
                    bool loop = true;
                    do
                    {
                        string teacherName = UserInput.GetInput<string>("Lärare:");
                        User teacher = userStore.FindById(teacherName);
                        if (teacher == null)
                        {
                            Console.WriteLine("Användaren existerar inte");
                        }
                        else if (!teacher.HasLevel(UserLevel.Teacher))
                        {
                            Console.WriteLine("Användaren är inte en lärare");
                        }
                        else
                        {
                            courseTeacher = teacher.UserName;
                            break;
                        }
                    } while (loop);

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

        private static void PrintCourseInfo(Course course)
        {
            Console.WriteLine(
                course.CourseId.PadRight(10) +
                course.CourseName.PadRight(40) +
                course.StartDate.ToShortDateString().PadRight(12) +
                course.EndDate.ToShortDateString().PadRight(12) +
                course.CourseTeacher
                );
        }

        private static void TeacherStaffingMenu()
        {
            Console.Clear();
            Console.WriteLine("Lärarbemanning");
            Console.WriteLine();
            Console.WriteLine("0. Föregående meny");

            string menu = UserInput.GetInput<string>("Val:");

            switch (menu)
            {
                case "0":
                    return;
            }
        }

        private static void TeacherStaffingFutureCourses()
        {
            throw new NotImplementedException();
        }

        private static void TeacherStaffingCurrentCourses()
        {
            throw new NotImplementedException();
        }

        private static void TeacherStaffingFinishedCourses()
        {
            throw new NotImplementedException();
        }
    }
}