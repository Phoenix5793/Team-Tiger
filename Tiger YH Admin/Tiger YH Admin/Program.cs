using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;


namespace Tiger_YH_Admin
{
    class Program
    {
        static void Main(string[] args)
        {
            User user = new User()
            {
                UserName = "staffan",
                Password = "abc123"
            };

            DataStore<User> ds = new UserStore();

            List<User> userList = ds.DataSet.ToList();

            var userExists = userList.SingleOrDefault(u => u.UserName == user.UserName);
            if (userExists != null)
            {
                // Användaren fanns redan
                Console.WriteLine("Användaren fanns redan");
            }
            else
            {
                // Användaren finns inte
                Console.WriteLine("Användaren fanns inte");
                userList.Add(user);
            }

            ds.DataSet = userList;
            ds.Save();

            //Course newCourse = new Course
            //{
            //    CourseId = "oop1",
            //    CourseName = "Objektorienterad Programmering 1",
            //    CourseTeacher = "admin",
            //    StartDate = DateTime.Today,
            //    EndDate = DateTime.Today,
            //};

            //List<Course> courseList = new List<Course>();
            //courseList.Add(newCourse);

            //DataStore<Course> ds = new DataStore<Course>();
            //ds.DataSet = courseList;
            //ds.Save();
        }
    }
}
