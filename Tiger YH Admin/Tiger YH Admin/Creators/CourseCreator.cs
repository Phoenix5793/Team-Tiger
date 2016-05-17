using System;
using System.Collections.Generic;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Creators
{
    class CourseCreator : ICreator<Course>
    {
        public  Course Create(IDataStore<Course> dataStore, Course existingCourse = null)
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

                }

                Course course = courseStore.AddItem(newCourse);
                courseStore.Save();
                Console.WriteLine("Kursen skapad");
                UserInput.WaitForContinue();
                return course;

            } while (courseExists);
        }
    }

}
