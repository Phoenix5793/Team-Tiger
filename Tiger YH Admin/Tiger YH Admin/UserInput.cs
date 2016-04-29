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
            return (T) Convert.ChangeType(input, typeof (T));
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
    }
}
