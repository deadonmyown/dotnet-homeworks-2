using Hw2;
using Xunit;

namespace Hw2Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("+", CalculatorOperation.Plus)]
        [InlineData("-", CalculatorOperation.Minus)]
        [InlineData("*", CalculatorOperation.Multiply)]
        [InlineData("/", CalculatorOperation.Divide)]
        public void TestCorrectOperations(string operation, CalculatorOperation operationExpected)
        {
            string[] args = { "1", operation, "2" };

            Parser.ParseCalcArguments(args, out double val1, out CalculatorOperation actualValue, out double val2);
            
            Assert.Equal(1, val1);
            Assert.Equal(operationExpected, actualValue);
            Assert.Equal(2, val2);
        }
        
        [Theory]
        [InlineData("f", "+", "3")]
        [InlineData("3", "+", "f")]
        [InlineData("a", "+", "f")]
        public void TestParserWrongValues(string val1, string operation, string val2)
        {
            string[] args = { val1, operation, val2 };

            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }
        
        [Fact]
        public void TestParserWrongOperation()
        {
            string[] args = { "1", "b", "3" };

            Assert.Throws<InvalidOperationException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }

        [Fact]
        public void TestParserWrongLength()
        {
            string[] args = { "1", "+", "2", "4"};

            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }
    }
}