using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models.Creators;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Presenters
{
    static class EducationSupervisorPresenter
    {
        public static void ManageTeacherMenu()
        {
            Console.WriteLine("Utbildningsledare - Hantera lärare");
            Console.WriteLine();
            Console.WriteLine("1. Visa alla lärare");
            Console.WriteLine("2. Lärarbemanning");
            UserInput.WaitForContinue();
        }

        internal static void ManageClassMenu(User user)
        {
            Console.WriteLine("Hantera klasser");
            Console.WriteLine();
            Console.WriteLine("1. Skapa ny klass");
            Console.WriteLine("2. Visa klasser jag ansvarar för");
            Console.WriteLine("3. Visa kursplan för en klass");
            Console.WriteLine("4. Visa klasslista för en klass");
            Console.WriteLine("5. Byt lärare för en kurs");
            Console.WriteLine("6. Lägg till student i klass");
            Console.WriteLine("7. Ta bort student från klass");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            string menuChoice = UserInput.GetInput<string>();


            switch (menuChoice)
            {
                case "1":
                    var creator = new EducationClassCreator();
                    var classStore = new EducationClassStore();
                    creator.Create(classStore);
                    break;
                case "2":
                    ClassListPresenter.ListAllClasses(user);
                    break;
                case "3":
                    ClassListPresenter.ShowCoursesForClass();
                    break;
                case "4":
                    ClassListPresenter.ShowClass();
                    break;
                case "5":
                    CoursePresenter.ChangeTeacherForCourses();
                    break;
                case "6":
                    ClassListPresenter.AddStudentToClass();
                    break;
                case "7":
                    ClassListPresenter.RemoveStudentFromClass();
                    break;

                default:
                    return;
            }
        }
    }
}