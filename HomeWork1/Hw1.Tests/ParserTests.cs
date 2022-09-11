using System;
using Hw1;
using Xunit;

namespace Hw1Tests
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
            // arrange
            var args = new[] { "15", operation, "5" };
            
            //act
            Parser.ParseCalcArguments(args, out var val1, out var operationResult, out var val2);

            //assert
            Assert.Equal(15, val1);
            Assert.Equal(operationExpected, operationResult);
            Assert.Equal(5, val2);
        }
        
        [Theory]
        [InlineData("f", "+", "3")]
        [InlineData("3", "+", "f")]
        [InlineData("a", "+", "f")]
        public void TestParserWrongValues(string val1, string operation, string val2)
        {
            // arrange
            var args = new[] { val1, operation, val2 };
            
            //assert
            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }

        [Theory]
        [InlineData("a", "b", "q")]
        [InlineData("a", "+", "q")]
        [InlineData("a", "+", "3")]
        public void TestMainInvalidDataDouble(string value1, string value2, string value3)
        {
            Assert.Throws<ArgumentException>(() => Program.Main(new[] { value1, value1, value1 }));
        }

        [Theory]
        [InlineData("1", "+", "q")]
        [InlineData("1", ";", "3")]
        public void TestMainInvalidDataCalc(string value1, string value2, string value3)
        {
            Assert.Throws<InvalidOperationException>(() => Program.Main(new[] { value1, value1, value1 }));
        }

        [Fact]
        public void TestParserWrongOperation()
        {
            // arrange
            var args = new[] { "3", ".", "4" };
            
            //assert
            Assert.Throws<InvalidOperationException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }

        [Fact]
        public void TestParserWrongLength()
        {
            // arrange
            var args = new[] { "3", ".", "4", "5" };
            
            //assert
            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }
    }
}