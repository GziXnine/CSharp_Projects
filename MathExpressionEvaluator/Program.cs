namespace MathEvaluator
{
    internal enum MathOperation
    {
        None,
        Add,
        Subtract,
        Multiply,
        Divide,
        Modulus,
        Power,
        Factorial,
        Sin,
        Cos,
        Tan
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var parser = new ExpressionParser();

                Console.Write("Please enter a math expression: ");
                Console.ForegroundColor = ConsoleColor.Green;
                var expression = parser.Parse(Console.ReadLine() ?? string.Empty);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"{expression.LeftSideOperand} {expression.Operation} {expression.RightSideOperand} = {expression.Evaluate(expression)}");
                Console.ResetColor();
            }
        }
    }
}
