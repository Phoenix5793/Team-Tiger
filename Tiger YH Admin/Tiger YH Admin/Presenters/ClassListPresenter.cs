using System;
using System.Collections.Generic;
using System.Linq;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Models.Creators;

namespace Tiger_YH_Admin.Presenters
{
    static class ClassListPresenter
    {
        public static void MainMenu()
        {
            Console.Clear();
            Console.WriteLine("Visa klasslistor");
            Console.WriteLine();
            Console.WriteLine("0. Tillbaka till föregående meny");
            Console.WriteLine("1. Lista alla klasser");
            Console.WriteLine("2. Visa en klass");

            string menuChoice = UserInput.GetInput<string>();

            switch (menuChoice)
            {
                case "0":
                    return;
                case "1":
                    ListAllClasses();
                    break;
                case "2":
                    ShowClass();
                    break;
            }
        }

        public static void ListAllClasses(User supervisor = null)
        {
            EducationClassStore classStore = new EducationClassStore();
            List<EducationClass> classList = classStore.DataSet.ToList();

            if (supervisor != null)
            {
                classList = classList.Where(c => c.EducationSupervisorId == supervisor.UserName).ToList();
            }

            Console.WriteLine(
                "Klassnamn".PadRight(10) +
                "Utb.ledare".PadRight(11) +
                "Beskrivning".PadRight(40)
                );

            Console.WriteLine(new string('-', 66));

            foreach (EducationClass educationClass in classList)
            {
                Console.WriteLine(
                    educationClass.ClassId.PadRight(10) +
                    educationClass.EducationSupervisorId.PadRight(11) +
                    educationClass.Description.PadRight(40)
                    );
            }

            Console.ReadKey();
        }

        public static void ShowClass()
        {
            EducationClassStore classStore = new EducationClassStore();
            UserStore studentStore = new UserStore();
            bool keepLooping = true;
            do
            {
                Console.WriteLine("Tryck enter för att avbryta");
                string classId = UserInput.GetInput<string>("Ange klass-id:");

                if (classId == string.Empty)
                {
                    return;
                }

                EducationClass edClass = classStore.FindById(classId);

                if (edClass == null)
                {
                    Console.WriteLine($"Finns ingen klass med id {classId}");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine(
                        "Student-id".PadRight(12) +
                        "Namn".PadRight(25) +
                        "Telefon".PadRight(15)
                        );
                    Console.WriteLine(new string('-', 60));

                    List<string> studentList = edClass.GetStudentList();
                    foreach (string studentId in studentList)
                    {
                        User student = studentStore.FindById(studentId);

                        Console.WriteLine(
                            student.UserName.PadRight(12) +
                            student.FullName().PadRight(25) +
                            student.PhoneNumber.PadRight(15)
                            );
                    }
                    Console.WriteLine();
                }
            } while (keepLooping);
        }

        public static void AddStudentToClass()
        {
            var classStore = new EducationClassStore();
            var studentStore = new UserStore();

            Console.WriteLine("Ange klass id: ");
            string classID = UserInput.GetInput<string>();
            var educationClass = classStore.FindById(classID);

            if (educationClass == null)
            {
                Console.WriteLine("Klassen kunde inte hittas");
                return;
            }

            Console.WriteLine("Ange student id: ");
            string studentID = UserInput.GetInput<string>();

            var studentUser = studentStore.FindById(studentID);

            if (studentUser == null)
            {
                Console.WriteLine("Finns ingen student med det namnet");
            }
            else if (studentUser.UserLevel != UserLevel.Student)
            {
                Console.WriteLine("Användaren är inte en student");
            }
            else if (educationClass.HasStudent(studentUser.UserName))
            {
                Console.WriteLine("Studenten finns redan i klassen");
            }
            else if (studentUser.UserLevel == UserLevel.Student)
            {
                Console.Clear();
                UserManagerPresenter.ShowUserInfo(studentUser);

                Console.WriteLine("Vill du lägga till student i klass? Ja/nej");
                string answer = UserInput.GetInput<string>().ToLower();

                if (answer == "ja" || answer == "j")
                {
                    List<string> studentList = educationClass.GetStudentList();

                    Console.WriteLine($"Före add: studentList har {studentList.Count} studenter");
                    studentList.Add(studentUser.UserName);
                    Console.WriteLine($"Efter add: studentList har {studentList.Count} studenter");
                    Console.ReadKey();

                    educationClass.SetStudentList(studentList);

                    var educationList = classStore.DataSet.ToList();

                    foreach (var item in educationList)
                    {
                        if (item.ClassId == educationClass.ClassId)
                        {
                            item.SetStudentList(educationClass.GetStudentList());
                            classStore.Save();
                        }
                    }
                }
            }
            Console.ReadKey();
        }

        public static void RemoveStudentFromClass()
        {
            var classStore = new EducationClassStore();
            var studentStore = new UserStore();

            do
            {
                Console.WriteLine("Tryck enter för att avbryta");
                string input = UserInput.GetInput<string>("Ange klass-id:");

                if (input == string.Empty)
                {
                    return;
                }

                EducationClass edClass = classStore.FindById(input);

                if (edClass == null)
                {
                    Console.WriteLine("Finns ingen klass med det id:t");
                }
                else
                {
                    input = UserInput.GetInput<string>("Ange student-id:");
                    User student = studentStore.FindById(input);

                    if (student == null)
                    {
                        Console.WriteLine("Studenten finns inte");
                    }
                    else
                    {
                        if (edClass.HasStudent(student.UserName))
                        {
                            bool confirmation =
                                UserInput.AskConfirmation(
                                    $"Vill du ta bort {student.FullName()} från klassen {edClass.ClassId}?");
                            if (confirmation)
                            {
                                List<string> studentList = edClass.GetStudentList();
                                studentList.Remove(student.UserName);
                                Console.WriteLine($"Plockade bort {student.UserName} från klassen");

                                edClass.SetStudentList(studentList);
                                classStore.Save();
                            }
                        }
                    }
                }
            } while (true);
        }

       

        public static void ShowCoursesForClass()
        {
            
        }
    }
}
