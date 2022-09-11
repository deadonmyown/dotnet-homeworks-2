using System.Diagnostics.CodeAnalysis;
using Hw1;

namespace Hw1
{
    public class Program
    {
        [ExcludeFromCodeCoverage]
        public static void Main(string[] args)
        {
            var arg1 = args[0];
            var operation = args[1];
            var arg2 = args[2];
            
            string[] arguments = new string[] { arg1, operation, arg2 };
            Parse(arguments, out double val1, out double val3, out CalculatorOperation val2);
            Calculate(val1, val3, val2);
        }

        public static double Calculate(double val1, double val3, CalculatorOperation val2) => 
            Calculator.Calculate(val1, val2, val3);
        
        public static void Parse(string[] args, out double val1, out double val3, out CalculatorOperation val2) => 
            Parser.ParseCalcArguments(args, out val1,
            out val2, out val3);
    }
} 