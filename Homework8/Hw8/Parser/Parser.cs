using System.Globalization;
using Hw8.Calculator;
namespace Hw8.Parser;

public class Parser
{
    public static Operation ParseOperation(string arg)
    {
        return arg.ToLower() switch
        {
            "plus" => Operation.Plus,
            "minus" => Operation.Minus,
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => Operation.Invalid
        };
    }

    public static bool IsArgsCountSupported(int argsCount) => argsCount == 3;

    public static (ParserResult, ParserArgs?) ParseCalcArguments(params string[] args)
    {
        Operation operation = ParseOperation(args[1]);
        if (!Double.TryParse(args[0], NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture,
                out double val1) || !Double.TryParse(args[2], NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out double val2))
            return (ParserResult.InvalidNumber, null);
        if (operation == Operation.Invalid)
            return (ParserResult.InvalidOperation, null);
        if (operation == Operation.Divide && val2 < Double.Epsilon)
            return (ParserResult.DivisionByZero, null);
        return (ParserResult.Success, new ParserArgs(val1, operation, val2));
    }
}