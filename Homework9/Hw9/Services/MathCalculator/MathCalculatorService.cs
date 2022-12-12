using System.Linq.Expressions;
using Hw9.Dto;
using Hw9.ErrorMessages;
using Hw9.Parser;

namespace Hw9.Services.MathCalculator;

public class MathCalculatorService : IMathCalculatorService
{
    public async Task<CalculationMathExpressionResultDto> CalculateMathExpressionAsync(string? expression)
    {
        try
        {
            var list = await Task.Run(() => Tokenizer.Parse(expression));
            var postfixList = await Task.Run(() => PostfixNotation.Sort(list));
            var expressionTree = await Task.Run(() => ExpressionTree.ConvertTokensToExpression(postfixList));
            
            var listExpression = await Task.Run(() => new ListExpression(expressionTree));
            var result = await MathExpressionVisitor.VisitAsync(listExpression.Expressions);
            return new CalculationMathExpressionResultDto(result);
        }
        catch(Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}