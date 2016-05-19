using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    public static class GradePresenter
    {
        public static void GradeStudentInCourse(User grader)
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

            List<Course> courses = GetCourses(grader, courseStore).ToList();

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

            GradeLevel gradeLevel = GetGrade();

            Console.WriteLine($"Student: {student.FullName()} ({student.UserName})");
            Console.WriteLine($"Kurs: {course.CourseName} ({course.CourseId})");
            Console.WriteLine($"Betyg: {gradeLevel}");
            bool confirm = UserInput.AskConfirmation("Betygsätt student?");

            if (confirm)
            {
                var grade = new Grade
                {
                    CourseId = course.CourseId,
                    StudentId = student.UserName,
                    Result = gradeLevel
                };

                gradeStore.GradeStudent(student, grade);
                gradeStore.Save();
            }
        }

        private static GradeLevel GetGrade()
        {
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
            return gradeLevel;
        }

        public static void GradeStudentGoal(User grader)
        {
            var courseStore = new CourseStore();
            var gradeStore = new GradeStore();
            var goalStore = new GoalStore();

            List<Course> courses = GetCourses(grader, courseStore).ToList();


            Course course = CoursePresenter.GetCourseById();
            if (course == null) return;
            if (grader.HasLevel(UserLevel.Teacher) && course.CourseTeacher != grader.UserName)
            {
                Console.WriteLine("Du är ej lärare för den kursen");
                UserInput.WaitForContinue();
                return;
            }

            User student = UserManagerPresenter.SearchForUser(UserLevel.Student);
            EducationClass klass = student.GetClass();

            if (klass == null)
            {
                Console.WriteLine("Användaren är inte en student");
                UserInput.WaitForContinue();
                return;
            }
            if (!klass.HasCourse(course.CourseId))
            {
                Console.WriteLine("Klassen läser ej den kursen");
                UserInput.WaitForContinue();
                return;
            }

            List<Goal> goals = goalStore.FindByCourseId(course.CourseId).ToList();

            foreach (Goal g in goals)
            {
                Console.WriteLine($"  {g.GoalId}: {g.Description.Truncate(95)}");
            }

            Console.WriteLine();
            string goalToGrade = UserInput.GetInput<string>("Välj mål att betygsätta:");

            Goal goal = goals.SingleOrDefault(g => g.GoalId == goalToGrade);

            if (goal == null)
            {
                Console.WriteLine($"Finns inget mål med id {goalToGrade}");
                UserInput.WaitForContinue();
                return;
            }

            GradeLevel gradeLevel = GetGrade();

            var grade = new Grade
            {
                StudentId = student.UserName,
                CourseId = course.CourseId,
                CourseGoal = goal.GoalId,
                Result = gradeLevel
            };

            Console.WriteLine();
            Console.WriteLine($"Student: {student.FullName()}");
            Console.WriteLine($"Kursmål: {goal.Description.Truncate(95)}");
            Console.WriteLine($"Betyg:   {grade.Result}");

            Console.WriteLine(grade.GradeId);
            bool confirm = UserInput.AskConfirmation("Spara betyg?");

            if (confirm)
            {
                Grade existingGrade = gradeStore.FindById(grade.GradeId);
                if (existingGrade == null)
                {
                    gradeStore.AddItem(grade);
                    gradeStore.Save();
                }
                else
                {
                    gradeStore.GradeStudent(student, grade);
                    gradeStore.Save();
                }
            }

            Console.WriteLine();
            UserInput.WaitForContinue();
        }

        private static IEnumerable<Course> GetCourses(User user, CourseStore courseStore)
        {
            var courses = new List<Course>();

            if (user.HasLevel(UserLevel.Teacher))
            {
                courses = courseStore.FindByTeacherId(user.UserName).ToList();
            }
            else if (user.HasLevel(UserLevel.EducationSupervisor))
            {
                courses = courseStore.All().ToList();
            }

            return courses;
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
            Console.WriteLine(new string('-', Console.WindowWidth));

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