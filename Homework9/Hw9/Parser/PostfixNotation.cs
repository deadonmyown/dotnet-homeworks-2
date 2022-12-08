using System.Diagnostics.CodeAnalysis;
using Hw9.ErrorMessages;
namespace Hw9.Parser;

public static class PostfixNotation
{
    private static readonly Stack<OperatorToken> _operatorsStack;
    private static readonly List<IToken> _postfixTokens;

    static PostfixNotation()
    {
        _operatorsStack = new Stack<OperatorToken>();
        _postfixTokens = new List<IToken>();
    }

    public static List<IToken> Sort(List<IToken> infixTokens)
    {
        _operatorsStack.Clear();
        _postfixTokens.Clear();
        for (int i = 0; i < infixTokens.Count; i++)
        {
            TokensHandle(infixTokens[i]);
        }

        while (_operatorsStack.Count > 0)
        {
            var stackToken = _operatorsStack.Pop();
            if (stackToken.TokenType == TokenType.OpenBrackets)
            {
                throw new Exception(MathErrorMessager.IncorrectBracketsNumber);
            }
            
            _postfixTokens.Add(stackToken);
        }

        return _postfixTokens;
    }

    [ExcludeFromCodeCoverage]
    private static void TokensHandle(IToken token)
    {
        switch(token)
        {
            case OperandToken operandToken:
                _postfixTokens.Add(operandToken);
                break;
            case OperatorToken operatorToken:
                OperatorTokenHandle(operatorToken);
                break;
            default:
                throw new Exception(MathErrorMessager.UnknownCharacter);
        }
    }

    private static void OperatorTokenHandle(OperatorToken token)
    {
        switch (token.TokenType)
        {
            case TokenType.OpenBrackets:
                _operatorsStack.Push(token);
                break;
            case TokenType.CloseBrackets:
                PushCloseBrackets(token);
                break;
            default:
                PushOperator(token);
                break;
        }
    }

    private static void PushCloseBrackets(OperatorToken token)
    {
        bool checkOpenBrackets = false;

        while (_operatorsStack.Count > 0)
        {
            var stackToken = _operatorsStack.Pop();
            if (stackToken.TokenType == TokenType.OpenBrackets)
            {
                checkOpenBrackets = true;
                break;
            }
            
            _postfixTokens.Add(stackToken);
        }

        if (!checkOpenBrackets)
        {
            throw new Exception(MathErrorMessager.IncorrectBracketsNumber);
        }
    }

    private static void PushOperator(OperatorToken token)
    {
        var tokenPriority = GetOperatorPriority(token);

        while (_operatorsStack.Count > 0)
        {
            var stackPeekToken = _operatorsStack.Peek();
            var stackPeekPriority = GetOperatorPriority(stackPeekToken);
            if (stackPeekPriority < tokenPriority)
            {
                break;
            }
            
            _postfixTokens.Add(_operatorsStack.Pop());
        }
        
        _operatorsStack.Push(token);
    }

    [ExcludeFromCodeCoverage]
    private static int GetOperatorPriority(OperatorToken token) =>
        token.TokenType switch
        {
            TokenType.OpenBrackets => 0,
            TokenType.Add or TokenType.Subtract => 1,
            TokenType.Multiply or TokenType.Divide => 2,
            _ => throw new Exception(MathErrorMessager.UnknownCharacter)
        };
}