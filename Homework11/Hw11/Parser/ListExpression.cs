using System.Linq.Expressions;

namespace Hw11.Parser;

public class ListExpression
{
    public List<Expression> Expressions { get; }

    public ListExpression(Expression expression)
    {
        Expressions = new List<Expression>();
        
        Visit(expression);
    }

    private void Visit(Expression expression)
    {
        Visit((dynamic)expression);
    }

    private void Visit(BinaryExpression? expression)
    {
        Expressions.Add(expression);
        Visit(expression.Left);
        Visit(expression.Right);
    }

    private void Visit(ConstantExpression? expression)
    {
        Expressions.Add(expression);
    }
}