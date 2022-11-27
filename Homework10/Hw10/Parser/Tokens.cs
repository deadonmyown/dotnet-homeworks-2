namespace Hw10.Parser;

public enum TokenType
{
    Constant,
    Add,
    Subtract,
    Multiply,
    Divide,
    OpenBrackets,
    CloseBrackets
}

public interface IToken
{
    public TokenType TokenType { get; }
}

public record OperatorToken (TokenType TokenType) : IToken;

public record OperandToken (TokenType TokenType, double Value) : IToken;