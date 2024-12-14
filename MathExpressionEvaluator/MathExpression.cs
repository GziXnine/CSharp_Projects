namespace MathEvaluator
{
    internal class MathExpression
    {
        public double LeftSideOperand { get; set; }
        public double RightSideOperand { get; set; }
        public MathOperation Operation { get; set; }

        internal object Evaluate(MathExpression expression)
        {
            switch(expression.Operation)
            {
                case MathOperation.Add:
                    return expression.LeftSideOperand + expression.RightSideOperand;
                case MathOperation.Subtract:
                    return expression.LeftSideOperand - expression.RightSideOperand;
                case MathOperation.Multiply:
                    return expression.LeftSideOperand * expression.RightSideOperand;
                case MathOperation.Divide:
                    return expression.LeftSideOperand / expression.RightSideOperand;
                case MathOperation.Modulus:
                    return expression.LeftSideOperand % expression.RightSideOperand;
                case MathOperation.Power:
                    return Math.Pow(expression.LeftSideOperand, expression.RightSideOperand);
                case MathOperation.Sin:
                    return expression.LeftSideOperand * Math.Sin(ConvertToRadians(expression.RightSideOperand));                
                case MathOperation.Cos:
                    return expression.LeftSideOperand * Math.Cos(ConvertToRadians(expression.RightSideOperand));                
                case MathOperation.Tan:
                    return expression.LeftSideOperand * Math.Tan(ConvertToRadians(expression.RightSideOperand));
                case MathOperation.Factorial:
                    return expression.LeftSideOperand * Factorial(expression.RightSideOperand);
                default:
                    return "Invalid Expression <3";
            }
        }

        private double ConvertToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }

        private double Factorial(double rightSideOperand)
        {
            return rightSideOperand == 0 ? 1 : rightSideOperand * Factorial(rightSideOperand - 1);
        }
    }
}
