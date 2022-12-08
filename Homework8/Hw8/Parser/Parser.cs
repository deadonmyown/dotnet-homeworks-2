using System.Globalization;
using Hw8.Calculator;
namespace Hw8.Parser;

public class Parser : IParser
{
    private Operation ParseOperation(string arg)
    {
        return arg.ToLower() switch
        {
            "plus" or "+" => Operation.Plus,
            "minus" or "-" => Operation.Minus,
            "multiply" or "*" => Operation.Multiply,
            "divide" or "/" => Operation.Divide,
            _ => Operation.Invalid
        };
    }

    public bool IsArgsCountSupported(int argsCount) => argsCount == 3;

    public void ParseCalcArguments(out double val1, out Operation operation, out double val2, params string[] args)
    {
        if (!Double.TryParse(args[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out val1) 
            || !Double.TryParse(args[2], NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out  val2))
            throw new ArgumentException(Messages.InvalidNumberMessage);
        
        operation = ParseOperation(args[1]);
        if (operation == Operation.Invalid)
            throw new InvalidOperationException(Messages.InvalidOperationMessage);

        if (operation == Operation.Divide && val2 < Double.Epsilon)
            throw new DivideByZeroException(Messages.DivisionByZeroMessage);
    }
}