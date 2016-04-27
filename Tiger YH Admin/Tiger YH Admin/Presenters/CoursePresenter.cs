using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin.Presenters
{
    static class CoursePresenter
    {
        public static void CourseManagementMenu()
        {
            Console.Clear();

            Console.WriteLine("Hantera kurser");
            Console.WriteLine();
            Console.WriteLine("1. Skapa kurs");
            Console.WriteLine("2. Ta bort kurs");
            Console.WriteLine("3. Lägg till lärare");
            Console.WriteLine("4. Ändra kurs");
            Console.WriteLine("5. Visa kurser");
        }

        internal static void ChangeTeacherForCourses()
        {
            
        }
    }
}
