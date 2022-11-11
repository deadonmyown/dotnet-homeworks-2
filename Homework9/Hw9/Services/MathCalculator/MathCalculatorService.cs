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
            var list = Tokenizer.Parse(expression);
            var postfixList = PostfixNotation.Sort(list);
            var expressionTree = ExpressionTree.ConvertTokensToExpression(postfixList);

            var taskExpression = await Task.Run(() => new OldExpressionVisitor().Visit(expressionTree));
            /*var visitor = new MathExpressionVisitor();
            var dictResult = visitor.DictionaryVisit(expressionTree);
            var result = await dictResult[expressionTree].Value;*/
            var listExpression = new ListExpression(expressionTree);
            var result = await MathExpressionVisitor.VisitAsync(listExpression.Expressions);
            return new CalculationMathExpressionResultDto(result);
        }
        catch(Exception ex)
        {
            return new CalculationMathExpressionResultDto(ex.Message);
        }
    }
}