using Hw8.Calculator;
namespace Hw8.Parser;

public interface IParser
{
    bool IsArgsCountSupported(int argsCount);
    
    void ParseCalcArguments(out double val1, out Operation operation, out double val2, params string[] args);
}