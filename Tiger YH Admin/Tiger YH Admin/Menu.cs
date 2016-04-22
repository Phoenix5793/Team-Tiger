using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger_YH_Admin.Models;
using Tiger_YH_Admin.Presenters;

namespace Tiger_YH_Admin
{
	static class Menu
    {
        public static int MainAdminMenu()
		{
			Console.WriteLine("Tiger Board!");
			Console.WriteLine("Admin-meny");
			Console.WriteLine();
			Console.WriteLine("1. Skapa användare");
			Console.WriteLine("2. Sök efter användare");
			Console.WriteLine("3. Skapa kurs");
			Console.WriteLine("4. Skapa klass");
			Console.WriteLine();
			Console.Write("Ditt val: ");
			return UserInput.GetInput<int>();
		}

		public static string[] LoginMenu()
		{
			Console.WriteLine("Inloggning");

			string userName = UserInput.GetInput<string>("Användarnamn:");

			//TODO: Göm lösenordet bättre
			Console.Write("Lösenord: ");
			Console.ForegroundColor = ConsoleColor.Black;
			Console.BackgroundColor = ConsoleColor.Black;
			string password = UserInput.GetInput<string>();
			Console.ResetColor();

			return new string[] {userName, password};

		}

        public static int EducationSupervisorMenu()
        {
            Console.WriteLine("Tiger Board!");
            Console.WriteLine("Utbildningledare-meny");
            Console.WriteLine();
            Console.WriteLine("1. Skapa användare");
            Console.WriteLine("2. Sök efter användare");
            Console.WriteLine("3. Hantera kurser");
            Console.WriteLine("4. Hantera studenter");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            return UserInput.GetInput<int>();
        }
        public static void ManageCourses()
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
        public static void ManageStudents()
        {
            Console.Clear();

            Console.WriteLine("Hantera studenter");
            Console.WriteLine();
            Console.WriteLine("1. Visa student information");
            Console.WriteLine("2. Ändra student information");
            Console.WriteLine("3. Sätt betyg");
            Console.WriteLine("4. Lägg till student");
            Console.WriteLine("5. Ta bort student");

            Console.WriteLine();
            Console.Write("Ditt val: ");
            int menuChoice = UserInput.GetInput<int>();

            switch (menuChoice)
            {
                case 1:
					Console.WriteLine("Visar studeninformation"); //TODO : Fixa metod
                    break;
                case 2:
					Console.WriteLine("Ändrar studentinformation"); //TODO: Fixa metod
                    break;
                case 3:
					Console.WriteLine("Sätt betyg på student"); //TODO: Fixa metod
                    break;
                case 4:
					AddStudentToClass();
                    break;
                case 5:
		            RemoveStudentFromClass();
					break;
                default:
                    break;
            }
        }

        public static void AddStudentToClass()
        {
            var classStore = new EducationClassStore();
            var studentStore = new UserStore();

            Console.WriteLine("Ange klass id: ");
            string classID = UserInput.GetInput<string>();
            var checkClassID = classStore.FindById(classID);

            if(checkClassID == null)
            {
                Console.WriteLine("Klassen kunde inte hittas");
	            return;
            }

            Console.WriteLine("Ange student id: ");
            string studentID = UserInput.GetInput<string>();

            var studentUser = studentStore.FindById(studentID);

            if(studentUser != null && studentUser.UserLevel == UserLevel.Student)
            {
                Console.Clear();
                UserManagerPresenter.ShowUserInfo(studentUser);

                Console.WriteLine("Vill du lägga till student i klass? Ja/nej");
                string answer = UserInput.GetInput<string>().ToLower();

                if (answer == "ja" || answer == "j")
                {

                    List<string> studentList = checkClassID.GetStudentList();
                    studentList.Add(studentUser.UserName);
                    checkClassID.SetStudentList(studentList);

                    var educationList =  classStore.DataSet.ToList();

                    foreach (var item in educationList)
                    {
                        if(item.ClassId == checkClassID.ClassId)
                        {
                            item.StudentString = checkClassID.StudentString;
                            classStore.Save();
                        }
                    }
                }

            }
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
							bool confirmation = UserInput.AskConfirmation($"Vill du ta bort {student.FullName()} från klassen {edClass.ClassId}?");
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

	}
}
