using System.Diagnostics.CodeAnalysis;
using System.Text;
using Hw1;

namespace Hw1
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                Parser.ParseCalcArguments(args, out double val1, out CalculatorOperation val2, out double val3);
                var result = Calculator.Calculate(val1, val2, val3);
                Console.WriteLine(result);
                return 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Wrong data: {ex.Message}");
                return -1;
            }
        }
    }
} 