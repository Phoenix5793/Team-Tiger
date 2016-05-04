using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Tiger_YH_Admin.Models.Creators
{
    class CourseCreator : ICreator<Course>
    {
        public  Course Create(IDataStore<Course> dataStore)
        {

            CourseStore courseStore = new CourseStore();
            UserStore userStore = new UserStore();
            Course newCourse = new Course();

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
                        Console.WriteLine("Lämna fältet tomt för att skapa kurs utan lärare");
                        string teacherName = UserInput.GetInput<string>("Lärare:");
                        User teacher = userStore.FindById(teacherName);

                        if(teacherName == string.Empty)
                        {

                            courseTeacher = string.Empty;
                            break;
                        }

                        else if (teacher == null)
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

                    newCourse = new Course
                    {
                        CourseId = courseId,
                        CourseName = courseName,
                        StartDate = courseStartDate,
                        EndDate = courseEndDate,
                        CourseTeacher = courseTeacher
                    };


                    string newStudent;
                    List<string> studentList = new List<string>();
                    do
                    {
                        newStudent = UserInput.GetInput<string>("Ange student-id för att lägga till student:");
                        User student = userStore.FindById(newStudent);
                        bool checkList = studentList.Contains(newStudent);

                        if (student != null && student.UserLevel == UserLevel.Student && checkList == false)
                        {
                            studentList.Add(student.UserName);
                            Console.WriteLine(student.UserName + " är nu tillagd i kursen");
                        }
                        else if(student == null && newStudent.Length > 0)
                        {
                            Console.WriteLine("Användaren är inte student");
                        }
                    } while (newStudent.Length > 0);
                    newCourse.SetStudentList(studentList);
                    Console.WriteLine("Kursen skapad");
                }
                
                return courseStore.AddItem(newCourse);

            } while (courseExists);










        }
    }



}
