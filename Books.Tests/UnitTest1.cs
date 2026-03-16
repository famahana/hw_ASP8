using Calculator;

namespace Books.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CalculatorSum_FiveAndTwo_ReturnsSeven()
        {
            int a = 5;
            int b = 2;
            
            int result = 10;
            int answer = Calc.Mult(a, b);
            Assert.Equal(result, answer);

        }
    }
}