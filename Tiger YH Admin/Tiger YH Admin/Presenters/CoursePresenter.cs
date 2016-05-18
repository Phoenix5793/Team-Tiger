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
            Console.WriteLine("13. Öppna kursplan");
            Console.WriteLine("14. Visa mål för en kurs");
            Console.WriteLine("15. Lägg till mål för en kurs");
            Console.WriteLine("16. Ta bort mål för en kurs");

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
                case "13":
                    ShowCoursePlan();
                    break;
                case "14":
                    ShowCourseGoals();
                    break;
                case "15":
                    CreateNewCourseGoal();
                    break;
            }
        }

        private static void CreateNewCourseGoal()
        {
            var goalStore = new GoalStore();

            Console.Clear();
            Console.WriteLine("Skapa nytt mål för kurs");
            Console.WriteLine();

            Course course = GetCourseById();
            if (course == null) return;

            Console.Clear();
            Console.WriteLine($"Kurs: {course.CourseName} ({course.CourseId})");

            int goalCount = goalStore.FindByCourseId(course.CourseId).Count();

            Console.WriteLine("Tryck enter för att avbryta");
            Console.WriteLine();
            while (true)
            {
                string description = UserInput.GetInput<string>("Den studerande ska:");

                if (description == string.Empty)
                {
                    break;
                }

                var goal = new Goal
                {
                    CourseId = course.CourseId,
                    GoalId = (goalCount + 1).ToString(),
                    Description = description
                };

                goalStore.AddItem(goal);
                goalStore.Save();
            }
            ShowCourseGoals(course);
        }

        private static void ShowCourseGoals(Course course = null)
        {
            var goalStore = new GoalStore();

            if (course == null)
            {
                course = GetCourseById();
                if (course == null) return;
            }

            List<Goal> goals = goalStore.FindByCourseId(course.CourseId).ToList();

            Console.Clear();
            Console.WriteLine(course.CourseName);
            Console.WriteLine();
            Console.WriteLine("Den studerande ska");
            foreach (Goal goal in goals)
            {
                Console.WriteLine(" - " + goal.Description);
            }

            UserInput.WaitForContinue();

        }

        private static Course GetCourseById()
        {
            var courseStore = new CourseStore();
            Course course;

            bool loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Tryck enter för att avbryta");
                string input = UserInput.GetInput<string>("Ange kurs-id:");

                if (input == string.Empty)
                {
                    return null;
                }

                course = courseStore.FindById(input);
                if (course == null)
                {
                    Console.WriteLine("Kurs med det id:t existerar inte");
                    UserInput.WaitForContinue();
                }
                else
                {
                    loop = false;
                }

            } while (loop);

            return course;
        }

        private static void ShowCoursePlan()
        {
            Course course = GetCourseById();
            course?.EditCoursePlan();
        }

        private static void ShowGradesForCourse()
        {
            Course course = GetCourseById();
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
            Console.WriteLine(new string('-', 80));

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
            Console.WriteLine("Kurs-id".PadRight(10) +
                              "Kursnamn".PadRight(40) +
                              "YH-poäng".PadRight(9) +
                              "Startdatum".PadRight(12) +
                              "Slutdatum".PadRight(12) +
                              "Lärare"
                );
            Console.WriteLine(new string('-', 80));

            foreach (Course course in courseList)
            {
                Console.WriteLine(
                    course.CourseId.PadRight(10) +
                    course.CourseName.Truncate(39).PadRight(40) +
                    course.CoursePoints.ToString().PadRight(9) +
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

            Console.Clear();
            Console.WriteLine("Lista över alla kurser");
            Console.WriteLine();
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

        private static void ChangeTeacherForCourses()
        {
            UserStore userStore = new UserStore();
            CourseStore courseStore = new CourseStore();

            Console.Clear();
            Console.WriteLine("Tryck enter för att avbryta");
            string courseId = UserInput.GetInput<string>("Kurs-id:");

            if (courseId == string.Empty)
            {
                return;
            }

            Course course = courseStore.FindById(courseId);
            if (course == null)
            {
                Console.WriteLine("Kursen finns inte");
                UserInput.WaitForContinue();
                return;
            }

            User currentTeacher = userStore.FindById(course.CourseTeacher);

            if (currentTeacher == null)
            {
                Console.WriteLine("Kursen saknar lärare");
            }
            else
            {
                Console.WriteLine($"Kurslärare: {currentTeacher.FullName()} ({currentTeacher.UserName})");
            }

            string newTeacherId = UserInput.GetInput<string>("ID för ny lärare:");
            User newTeacher = userStore.FindById(newTeacherId);
            if (newTeacher == null)
            {
                Console.WriteLine("Användaren finns inte");
            }
            else if (newTeacher.UserLevel != UserLevel.Teacher)
            {
                Console.WriteLine("Användaren är inte en lärare");
            }
            else
            {
                course.CourseTeacher = newTeacher.UserName;
                courseStore.Save();
                Console.WriteLine("Kursens lärare uppdaterad");
            }

            UserInput.WaitForContinue();
        }

        private static void PrintCourseInfo(Course course, UserStore userStore)
        {
            User teacher = userStore.FindById(course.CourseTeacher);

            Console.WriteLine(
                course.CourseId.PadRight(10) +
                course.CourseName.Truncate(40).PadRight(40) +
                course.CoursePoints.ToString().PadRight(9) +
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

            Console.Clear();
            Console.WriteLine("Lista över framtida kurser");
            Console.WriteLine();

            ListCourses(futureCourses);
        }

        private static void TeacherStaffingCurrentCourses()
        {
            var courseStore = new CourseStore();
            List<Course> currentCourses = courseStore.GetCurrentCourses().ToList();

            Console.Clear();
            Console.WriteLine("Lista över pågående kurser");
            Console.WriteLine();

            ListCourses(currentCourses);
        }

        private static void TeacherStaffingFinishedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> finishedCourses = courseStore.GetFinishedCourses().ToList();

            Console.Clear();
            Console.WriteLine("Lista över avslutade kurser");
            Console.WriteLine();

            ListCourses(finishedCourses);
        }

        private static void TeacherStaffingUnmannedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> unmannedCourses = courseStore.GetUnmannedCourses().ToList();

            Console.Clear();
            Console.WriteLine("Lista över obemannade kurser");
            Console.WriteLine();

            ListCourses(unmannedCourses);
        }

        private static void TeacherStaffingArrangedCourses()
        {
            var courseStore = new CourseStore();
            List<Course> unmannedCourses = courseStore.GetAllAgreedCourses().ToList();

            Console.Clear();
            Console.WriteLine("Lista över avtalade kurser");
            Console.WriteLine();

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
                    EducationClass klass = classStore.FindByCourseId(course.CourseId);
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

        public static void ShowStudentCourseList(User student)
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

        public static void ShowClassCourseList(EducationClass klass)
        {
            List<string> courses = klass.GetCourseList();

            PrintCourseList(courses);
            UserInput.WaitForContinue();
        }
    }
}
