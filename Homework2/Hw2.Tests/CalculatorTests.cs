using Hw2;
using Xunit;

namespace Hw2Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(15, 5, CalculatorOperation.Plus, 20)]
        [InlineData(15, 5, CalculatorOperation.Minus, 10)]
        [InlineData(15, 5, CalculatorOperation.Multiply, 75)]
        [InlineData(15, 5, CalculatorOperation.Divide, 3)]
        public void TestAllOperations(int value1, int value2, CalculatorOperation operation, int expectedValue)
        {
            double actualValue = Calculator.Calculate(value1, operation, value2);
            
            Assert.Equal(expectedValue, actualValue);
        }
        
        [Fact]
        public void TestInvalidOperation()
        {
            Assert.Throws<ArgumentOutOfRangeException>(
                () => Calculator.Calculate(13, CalculatorOperation.Undefined, 37));
        }

        [Fact]
        public void TestDividingNonZeroByZero()
        {
            double actualValue = Calculator.Calculate(228, CalculatorOperation.Divide, 0);
            
            Assert.Equal(double.PositiveInfinity, actualValue);
        }

        [Fact]
        public void TestDividingZeroByNonZero()
        {
            double actualValue = Calculator.Calculate(0, CalculatorOperation.Divide, 322);
            
            Assert.Equal(0, actualValue);
        }
        
        [Fact]
        public void TestDividingZeroByZero()
        {
            double actualValue = Calculator.Calculate(0, CalculatorOperation.Divide, 0);
            
            Assert.Equal(double.NaN, actualValue);
        }
    }
}