using Hw8.Calculator;
namespace Hw8.Parser;

public class ParserArgs
{
    public Operation Operation { get; }
    public double Value1 { get; }
    public double Value2 { get; }

    public ParserArgs(double value1, Operation operation, double value2)
    {
        Value1 = value1;
        Operation = operation;
        Value2 = value2;
    }
}