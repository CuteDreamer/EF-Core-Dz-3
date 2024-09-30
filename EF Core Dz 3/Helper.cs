using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core_Dz_3
{
    public class Helper
    {

        public static bool Check<T>(T obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            if (!Validator.TryValidateObject(obj, context, results, true))
            {
                foreach (var error in results)
                {
                    Helper.WhriteErrorMessage(error.ErrorMessage);
                }
                return false;
            }
            return true;
        }

        public static void WhriteSuccessfulMessage(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = color;
        }
        public static void WhriteErrorMessage(string message)
        {
            var color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error! {message}");
            Console.ForegroundColor = color;
        }


        private const string _subMessage = "Enter";

        public static int GetInt(string value)
        {
            int result = 0;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (!int.TryParse(Console.ReadLine(), out result))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }

        public static string GetString(string value)
        {
            string result = String.Empty;
            Console.Write("{0} {1}: ", _subMessage, value);
            while (String.IsNullOrWhiteSpace(result = Console.ReadLine()))
            {
                Console.Write("{0} {1}: ", _subMessage, value);
            }
            return result;
        }
    }
}
