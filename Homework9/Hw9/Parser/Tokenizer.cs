using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;
using Hw9.ErrorMessages;

namespace Hw9.Parser;

public static class Tokenizer
{
    private static readonly StringBuilder _valueStringBuilder;
    private static readonly List<IToken> _tokens;

    static Tokenizer()
    {
        _valueStringBuilder = new StringBuilder();
        _tokens = new List<IToken>();
    }

    public static List<IToken> Parse(string? formula)
    {
        if (string.IsNullOrEmpty(formula)) throw new Exception(MathErrorMessager.EmptyString);
        
        _valueStringBuilder.Clear();
        _tokens.Clear();
        for (int i = 0; i < formula.Length; i++)
        {
            GenerateNewToken(formula, i);
        }
        
        GenerateOperandToken(_valueStringBuilder.ToString());
        CheckCorrectFormat(_tokens);
        return _tokens;
    }

    private static void GenerateNewToken(string text, int index)
    {
        if (CheckSpace(text[index]))
        {
            GenerateOperandToken(_valueStringBuilder.ToString());
        }
        else if (CheckOperator(text, index))
        {
            GenerateOperandToken(_valueStringBuilder.ToString());

            _tokens.Add(new OperatorToken(GetOperatorType(text[index])));
        }
        else
        {
            _valueStringBuilder.Append(text[index]);
        }
    }

    private static void GenerateOperandToken(string text)
    {
        if (text.Length > 0)
        {
            var token = TryCreateOperandToken(text);
            _valueStringBuilder.Clear();
            _tokens.Add(token);
        }
    }

    private static OperandToken TryCreateOperandToken(string text)
    {
        if (double.TryParse(
                text,
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out double result))
        {
            return new OperandToken(TokenType.Constant, result);
        }

        if (text.Length == 1)
            throw new Exception(MathErrorMessager.UnknownCharacterMessage(text[0]));
        throw new Exception(MathErrorMessager.NotNumberMessage(text));
    }

    private static bool CheckSpace(char character) => character switch
    {
        ' ' => true,
        _ => false
    };

    private static bool CheckOperator(string text, int index) => text[index] switch
    {
        '+' => true,
        '-' => CheckNotNegate(text, index), 
        '*' => true, 
        '/' => true, 
        '(' => true, 
        ')' => true,
        _ => false
    };

    private static bool CheckNotNegate(string text, int index) => text.Length <= index + 1
        ? throw new Exception(MathErrorMessager.EndingWithOperation)
        : CheckSpace(text[index + 1]); 

    [ExcludeFromCodeCoverage]
    private static TokenType GetOperatorType(char character) => character switch
    {
        '+' => TokenType.Add,
        '-' => TokenType.Subtract,
        '*' => TokenType.Multiply,
        '/' => TokenType.Divide,
        '(' => TokenType.OpenBrackets,
        ')' => TokenType.CloseBrackets,
        _ => throw new Exception(MathErrorMessager.UnknownCharacter)
    };
    
    [ExcludeFromCodeCoverage]
    private static string GetOperatorChar(TokenType type) => type switch
    {
       TokenType.Add => "+", 
       TokenType.Subtract => "-", 
       TokenType.Multiply => "*", 
       TokenType.Divide => "/", 
       TokenType.OpenBrackets => "(", 
       TokenType.CloseBrackets => ")",
       _ => throw new Exception(MathErrorMessager.UnknownCharacter)
    };

    private static bool CheckMathOperation(TokenType type) => type switch
    {
        TokenType.Add => true,
        TokenType.Subtract => true,
        TokenType.Multiply => true,
        TokenType.Divide => true,
        _ => false
    };

    [ExcludeFromCodeCoverage]
    private static void CheckCorrectFormat(List<IToken> tokens)
    {
        if (CheckMathOperation(tokens[0].TokenType))
            throw new Exception(MathErrorMessager.StartingWithOperation);
        else if (CheckMathOperation(tokens[tokens.Count - 1].TokenType))
            throw new Exception(MathErrorMessager.EndingWithOperation);
        for (int i = 1; i <= tokens.Count - 1; i++)
        {
            if (CheckMathOperation(tokens[i - 1].TokenType) && CheckMathOperation(tokens[i].TokenType))
                throw new Exception(MathErrorMessager.TwoOperationInRowMessage(GetOperatorChar(tokens[i - 1].TokenType),
                    GetOperatorChar(tokens[i].TokenType)));
            
            if (tokens[i - 1].TokenType == TokenType.OpenBrackets && CheckMathOperation(tokens[i].TokenType))
                throw new Exception(
                    MathErrorMessager.InvalidOperatorAfterParenthesisMessage(GetOperatorChar(tokens[i].TokenType)));
            
            if (CheckMathOperation(tokens[i - 1].TokenType) && tokens[i].TokenType == TokenType.CloseBrackets)
                throw new Exception(
                    MathErrorMessager.OperationBeforeParenthesisMessage(GetOperatorChar(tokens[i - 1].TokenType)));
        }
    }
}