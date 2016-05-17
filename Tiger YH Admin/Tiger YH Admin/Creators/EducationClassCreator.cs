﻿using System;
using System.Collections.Generic;
using Tiger_YH_Admin.DataStore;
using Tiger_YH_Admin.Models;

namespace Tiger_YH_Admin.Creators
{
    class EducationClassCreator : ICreator<EducationClass>
    {
        public EducationClass Create(IDataStore<EducationClass> dataStore, EducationClass existingClass = null)
        {
            EducationClass newClass = new EducationClass();
            UserStore userStore = new UserStore();

            bool keepLooping = true;
            do
            {
                Console.Clear();
                Console.WriteLine("Skapa ny klass");
                Console.WriteLine();
                string classId = UserInput.GetInput<string>("Klass-id:");
                // TODO: implementera uppdatering
                existingClass = dataStore.FindById(classId);

                if (existingClass != null && keepLooping)
                {
                    Console.WriteLine("Klass-id redan använt");
                    UserInput.WaitForContinue();
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
                        Console.WriteLine("Lägg till student-id: ");
                        newStudent = UserInput.GetInput<string>();
                        User student = userStore.FindById(newStudent);
                        if (student != null && student.HasLevel(UserLevel.Student))
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

            EducationClass klass = dataStore.AddItem(newClass);
            dataStore.Save();
            return klass;
        }
    }
}
