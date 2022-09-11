using System.Diagnostics.CodeAnalysis;
using System.Text;
using Hw1;

namespace Hw1
{
    public class Program
    {
        public static bool IsWrong { get; private set; }

        public static void Main(string[] args)
        {
            IsWrong = false;
            try
            {
                Parser.ParseCalcArguments(args, out double val1, out CalculatorOperation val2, out double val3);
                var result = Calculator.Calculate(val1, val2, val3);
                Console.WriteLine(result);
                IsWrong = false;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Wrong data: {ex.Message}");
                IsWrong = true;
            }
        }
    }
} 