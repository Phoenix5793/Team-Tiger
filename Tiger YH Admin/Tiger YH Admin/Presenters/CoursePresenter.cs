using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Creators;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class CoursePresenter
    {
        public static void CourseManagementMenu(User user)
        {
            Console.Clear();

            Console.WriteLine("Hantera kurser");
            Console.WriteLine();
            Console.WriteLine("1. Skapa kurs");
            Console.WriteLine("2. Ta bort kurs");
            Console.WriteLine("3. Ändra lärare för en kurs");
            Console.WriteLine("4. Ändra kurs");
            Console.WriteLine("5. Visa alla kurser");
            Console.WriteLine("6. Visa avslutade kurser");
            Console.WriteLine("7. Visa pågående kurser");
            Console.WriteLine("8. Visa kommande kurser");
            Console.WriteLine("9. Visa obemannade kurser");
            Console.WriteLine("10. Visa avtalade kurser");
            Console.WriteLine("11. Visa klasslista för en kurs");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "1":
                    var creator = new CourseCreator();
                    var courseStore = new CourseStore();
                    creator.Create(courseStore);
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
                case "9":
                    TeacherStaffingUnmannedCourses();
                    break;
                case "10":
                    TeacherStaffingArrangedCourses();
                    break;
                case "11":
                    ShowStudentsForCourse(user);
                    break;
            }
        }

        private static void ListCourses(IEnumerable<Course> courseList)
        {
            Console.Clear();
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

            UserInput.WaitForContinue();
        }

        private static void ListAllCourses()
        {
            CourseStore courseStore = new CourseStore();
            List<Course> courseList = courseStore.All().ToList();
            ListCourses(courseList);
        }

        private static void EditCourse()
        {
            CourseStore courseStore = new CourseStore();

            Console.Clear();
            Console.WriteLine("Redigera kurs");
            Console.WriteLine();
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

            Console.Clear();
            Console.WriteLine("Radera kurs");
            Console.WriteLine();

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

        private static void PrintCourseList(List<string> courses)
        {
            CourseStore courseStore = new CourseStore();

            Console.Clear();
            Console.WriteLine(
                "Kurs-id".PadRight(10) +
                "Kursnamn".PadRight(40) +
                "Startdatum".PadRight(12) +
                "Slutdatum".PadRight(12) +
                "Lärare"
                );
            Console.WriteLine(new string('-', 80));

            foreach (string c in courses)
            {
                Course course = courseStore.FindById(c);
                PrintCourseInfo(course);
            }
        }

        private static void TeacherStaffingFutureCourses()
        {
            var courseStore = new CourseStore();
            List<Course> futureCourses = courseStore.GetFutureCourses().ToList();

            ListCourses(futureCourses);
        }

        private static void TeacherStaffingCurrentCourses()
        {
            var courseStore = new CourseStore();
            List<Course> currentCourses = courseStore.GetCurrentCourses().ToList();

            ListCourses(currentCourses);
        }

        private static void TeacherStaffingFinishedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> finishedCourses = courseStore.GetFinishedCourses().ToList();

            ListCourses(finishedCourses);
        }

        private static void TeacherStaffingUnmannedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> unmannedCourses = courseStore.GetUnmannedCourses().ToList();

            ListCourses(unmannedCourses);
        }

        private static void TeacherStaffingArrangedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> unmannedCourses = courseStore.GetAllAgreedCourses().ToList();

            ListCourses(unmannedCourses);
        }

        public static void ShowStudentsForCourse(User user)
        {
            bool isTeacher = user.HasLevel(UserLevel.Teacher);

            do
            {
                CourseStore courseStore = new CourseStore();

                Console.Clear();
                Console.WriteLine("Visa klasslista för kurs");
                Console.WriteLine();

                Console.WriteLine("Tryck enter för att avbryta.");
                string courseName = UserInput.GetInput<string>("Ange kurs-id:");

                if (courseName == string.Empty)
                {
                    break;
                }

                Course course = courseStore.FindById(courseName);

                if (course == null)
                {
                    Console.WriteLine("Finns ingen kurs med det namnet");
                    Console.WriteLine();
                    continue;
                }

                if (isTeacher && course.CourseTeacher != user.UserName)
                {
                    Console.WriteLine("Du är ej lärare för den kursen.");
                    Console.WriteLine();
                }
                else
                {
                    List<string> studentNames = course.GetStudentList();
                    UserManagerPresenter.PrintStudentList(studentNames);
                }
            } while (true);
        }

        public static void ShowTeacherCourses(User teacher)
        {
            CourseStore courseStore = new CourseStore();
            List<Course> courses = courseStore.All().ForTeacher(teacher).ToList();

            ListCourses(courses);
        }

        public static void ShowStudentCoursePlan(User student)
        {
            EducationClassStore classStore = new EducationClassStore();

            foreach (EducationClass klass in classStore.All())
            {
                if (klass.HasStudent(student.UserName))
                {
                    EducationClass studentClass = klass;
                    List<string> courseList = studentClass.GetCourseList();

                    PrintCourseList(courseList);
                    UserInput.WaitForContinue();

                    break;
                }
            }
        }

        public static void ShowClassCoursePlan(EducationClass klass)
        {
            List<string> courses = klass.GetCourseList();

            PrintCourseList(courses);
            UserInput.WaitForContinue();
        }
    }
}
