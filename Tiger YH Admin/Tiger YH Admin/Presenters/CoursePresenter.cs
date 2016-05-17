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
            Console.WriteLine("12. Visa alla betyg för en kurs");

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
                case "12":
                    ShowGradesForCourse();
                    break;
            }
        }

        private static Course SearchForCourse()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Tryck enter utan att ange namn för att avbryta.");
                string input = UserInput.GetInput<string>("Sök kurs-id:");

                if (input == string.Empty)
                {
                    return null;
                }

                var courseStore = new CourseStore();
                Course course = courseStore.FindById(input);

                if (course == null)
                {
                    Console.WriteLine("Det finns ingen kurs med det id:t.");
                }
                else
                {
                    return course;
                }
            }
        }
        private static void ShowGradesForCourse()
        {
            Course course = SearchForCourse();
            if (course != null)
            {
                PrintCourseGrades(course);
            }
        }

        private static void PrintCourseGrades(Course course)
        {
            GradeStore gradeStore = new GradeStore();
            UserStore userStore = new UserStore();
            List<Grade> grades = gradeStore.FindByCourseId(course.CourseId).ToList();

            Console.Clear();
            Console.WriteLine(
                "Student-id".PadRight(12) +
                "Student".PadRight(40) +
                "Betyg");
            Console.WriteLine(new string('-', 60));

            foreach (Grade grade in grades)
            {
                User student = userStore.FindById(grade.StudentId);

                Console.WriteLine(
                    grade.StudentId.PadRight(12) +
                    student.FullName().PadRight(40) +
                    grade.Result
                    );
            }

            UserInput.WaitForContinue();
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
            UserStore userStore = new UserStore();

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
            PrintCourseInfo(courseToEdit, userStore);
            Console.WriteLine("Ändras till:");
            PrintCourseInfo(newCourse, userStore);

            Console.WriteLine();
            bool confirm = UserInput.AskConfirmation("Vill du spara ändringarna?");

            if (confirm)
            {
                courseStore.Remove(existingCourse.CourseId);
                courseStore.AddItem(newCourse);
                courseStore.Save();
                Console.WriteLine("Ändringar sparade.");
            }
        }

        private static void DeleteCourse()
        {
            CourseStore courseStore = new CourseStore();
            GradeStore gradeStore = new GradeStore();


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
            List<Grade> grades = gradeStore.FindByCourseId(courseToRemove.CourseId).ToList();

            bool confirm = UserInput.AskConfirmation($"Vill du radera {courseToRemove.CourseName}?");

            if (confirm && grades.Count == 0  )
            {
                courseList.Remove(courseToRemove);
                courseStore = new CourseStore(courseList);
                courseStore.Save();

                Console.WriteLine("Kursen raderad");
            }
            else
            {
                Console.WriteLine("Kursen har betygsatta studenter");
                UserInput.WaitForContinue();
            }
        }

        internal static void ChangeTeacherForCourses()
        {
            throw new NotImplementedException();
        }

        private static void PrintCourseInfo(Course course, UserStore userStore)
        {
            User teacher = userStore.FindById(course.CourseTeacher);

            Console.WriteLine(
                course.CourseId.PadRight(10) +
                course.CourseName.PadRight(40) +
                course.StartDate.ToShortDateString().PadRight(12) +
                course.EndDate.ToShortDateString().PadRight(12) +
                teacher.FullName()
                );
        }

        private static void PrintCourseList(List<string> courses)
        {
            CourseStore courseStore = new CourseStore();
            UserStore userStore = new UserStore();

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
                PrintCourseInfo(course, userStore);
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
                EducationClassStore classStore = new EducationClassStore();

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
                }
                else if (isTeacher && course.CourseTeacher != user.UserName)
                {
                    Console.WriteLine("Du är ej lärare för den kursen.");
                    Console.WriteLine();
                }
                else
                {
                    // TODO: skriv extension method
                    EducationClass klass = classStore.All().Single(c => c.HasCourse(course.CourseId));
                    List<string> studentNames = klass.GetStudentList();
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
