using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tiger_YH_Admin
{
    static class UserInput
    {
        public static T GetInput<T>()
        {
            string input = Console.ReadLine();
            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static T GetInput<T>(string message)
        {
            Console.Write($"{message} ");
            return GetInput<T>();
        }

        public static T GetInput<T>(string message, int minLength)
        {
            string input;

            do
            {
                Console.Write($"{message} ");
                input = Console.ReadLine();

                if (input.Length < minLength)
                {
                    Console.WriteLine($"Input för kort, minst {minLength} tecken");
                }
            } while (input.Length < minLength);

            return GetInput<T>();
        }

        public static bool AskConfirmation(string message)
        {
            do
            {
                Console.WriteLine(message);
                Console.Write("(j/n): ");
                string input = Console.ReadLine().ToLower();

                switch (input)
                {
                    case "j":
                        return true;
                    case "n":
                        return false;
                }
            } while (true);
        }

        public static DateTime GetDate(string message)
        {
            bool keepGoing;
            DateTime date = default(DateTime);

            do
            {
                try
                {
                    keepGoing = false;
                    date = GetInput<DateTime>(message);
                }
                catch (FormatException)
                {
                    Console.WriteLine();
                    Console.WriteLine("Ogiltigt datumformat!");
                    keepGoing = true;
                }
            } while (keepGoing);

            return date;
        }

        public static void WaitForContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Tryck enter för att fortsätta");
            ConsoleKey key;
            do
            {
                key = Console.ReadKey().Key;
            } while (key != ConsoleKey.Enter);
        }
        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }

        public static string GetEditableField(string description, string fieldName)
        {
            string input = fieldName;
            Console.Write($"{description}: {fieldName}");

            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            while (keyInfo.Key != ConsoleKey.Enter)
            {
                if (keyInfo.Key != ConsoleKey.Backspace)
                {
                    input += keyInfo.KeyChar;
                    Console.Write(keyInfo.KeyChar);
                }
                else if (!string.IsNullOrEmpty(input))
                {
                    input = input.Substring(0, input.Length - 1);
                    int cursorPos = Console.CursorLeft;
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(cursorPos - 1, Console.CursorTop);
                }

                keyInfo = Console.ReadKey(true);
            }

            Console.WriteLine();
            return input;
        }
    }
}
