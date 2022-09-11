using System.Diagnostics.CodeAnalysis;
using System.Text;
using Hw1;

namespace Hw1
{
    public class Program
    {
        public static double Result { get; private set; }
        public static StringBuilder Text { get; } = new StringBuilder();

        public static void Main(string[] args)
        {
            Text.Clear();
            for (int i = 0; i < args.Length; i++)
                Text.Append(args[i]);
            Console.WriteLine(Text.ToString());
            Parser.ParseCalcArguments(args, out double val1, out CalculatorOperation val2, out double val3);
            Result = Calculator.Calculate(val1, val2, val3);
        }
    }
} 