using System.Linq.Expressions;

namespace Hw9.Parser;

public static class ExpressionTree
{
    public static Expression ConvertTokensToExpression(List<IToken> tokens)
    {
        var expressionStack = new Stack<Expression>();
        for (int i = 0; i < tokens.Count; i++)
        {
            if (tokens[i].TokenType == TokenType.Constant)
            {
                expressionStack.Push(Expression.Constant(((OperandToken)tokens[i]).Value));
            }
            else
            {
                var rightToken = expressionStack.Pop();
                var leftToken = expressionStack.Pop();
                var newExpression = GetExpressionFromToken(tokens[i], leftToken, rightToken);
                expressionStack.Push(newExpression);
            }
        }

        return expressionStack.Peek();
    }
    
    private static Expression GetExpressionFromToken(IToken token, Expression left, Expression right) => token.TokenType switch
    {
        TokenType.Add => Expression.Add(left, right),
        TokenType.Subtract => Expression.Subtract(left, right),
        TokenType.Multiply => Expression.Multiply(left, right),
        _ => Expression.Divide(left, right)
    };
}