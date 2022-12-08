using Hw11.Dto;
using Hw11.Exceptions;
using Hw11.Parser;


namespace Hw11.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    private readonly IExceptionHandler _handler;
    
    public MathCalculatorService(IExceptionHandler handler)
    {
        _handler = handler;
    }
    
    public async Task<double> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            var list = await Task.Run(() => Tokenizer.Parse(expression));
            var postfixList = await Task.Run(() => PostfixNotation.Sort(list));
            var expressionTree = await Task.Run(() => ExpressionTree.ConvertTokensToExpression(postfixList));
            
            var listExpression = await Task.Run(() => new ListExpression(expressionTree));
            return await MathExpressionVisitor.VisitAsync(listExpression.Expressions);
        }
        catch(Exception ex)
        {
            _handler.HandleException(ex);
            throw;
        }
    }
}