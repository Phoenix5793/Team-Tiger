using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tiger_YH_Admin.Models.Creators
{
    class EducationClassCreator : ICreator<EducationClass>
    {
        public EducationClass Create(IDataStore<EducationClass> dataStore)
        {
            Console.Clear();
            Console.WriteLine("Skapa ny klass");
            Console.WriteLine();

            EducationClass newClass = new EducationClass();
            UserStore userStore = new UserStore();

            bool keepLooping = true;
            do
            {
                Console.Clear();
                string classId = UserInput.GetInput<string>("Klass-id:");
                EducationClass existingClass = dataStore.FindById(classId);

                if (existingClass != null && keepLooping)
                {
                    Console.WriteLine("Klass-id redan använt");
                    Console.ReadKey();
                }
                else
                {
                    // Klassen finns inte
                    Console.Write("Klassbeskrivning: ");
                    string classDescription = UserInput.GetInput<string>();

                    string input;
                    User user;

                    bool isSupervisor = false;
                    do
                    {
                        Console.WriteLine("Utbildningsledare: ");
                        do
                        {
                            input = UserInput.GetInput<string>();
                            if (input == string.Empty)
                            {
                                Console.WriteLine("Kan inte lämna namnet tomt");
                            }
                        } while (input == string.Empty);

                        user = userStore.FindById(input);

                        if (user != null)
                        {
                            isSupervisor = user.HasLevel(UserLevel.EducationSupervisor);
                            if (!isSupervisor)
                            {
                                Console.WriteLine("Användaren är inte utbildningsledare");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Användaren finns inte");
                        }

                    } while (user == null || !isSupervisor);

                    string newStudent;
                    List<string> studentList = new List<string>();
                    do
                    {
                        //TODO: Bara kunna lägga till studenter
                        Console.WriteLine("Lägg till student-id: ");
                        newStudent = UserInput.GetInput<string>();
                        User student = userStore.FindById(newStudent);
                        if (student != null && student.UserLevel == UserLevel.Student)
                        {
                            studentList.Add(student.UserName);
                        }
                        else
                        {
                            Console.WriteLine("Användaren är inte student");
                        }
                    } while (newStudent.Length > 0);

                    newClass = new EducationClass
                    {
                        ClassId = classId,
                        Description = classDescription,
                        EducationSupervisorId = input,
                    };
                    newClass.SetStudentList(studentList);

                    keepLooping = false;
                }
            } while (keepLooping);

            return dataStore.AddItem(newClass);
        }
    }
}
