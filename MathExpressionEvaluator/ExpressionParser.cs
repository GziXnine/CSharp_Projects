namespace MathEvaluator
{
    internal class ExpressionParser
    {
        private const string _validOperations = "+*/%^";
        public MathExpression Parse(string expression)
        {
            var mathExpression = new MathExpression();
            expression = expression.Trim();
            string token = "";
            bool leftSideInitialized = false, operandInitialized = false;

            for (int i = 0; i < expression.Length; i++)
            {
                var currentChar = expression[i];
                if (char.IsDigit(currentChar))
                {
                    if (operandInitialized && mathExpression.Operation == MathOperation.None)
                    {
                        mathExpression.Operation = ParseOperation(token.ToString());
                        token = "";
                    }

                    token += currentChar;
                    if (i == expression.Length - 1 && leftSideInitialized)
                    {
                        mathExpression.RightSideOperand = double.Parse(token);
                        break;
                    }
                }
                else if (_validOperations.Contains(currentChar))
                {
                    if(!leftSideInitialized)
                    {
                        mathExpression.LeftSideOperand = double.Parse(token);
                        leftSideInitialized = true;
                    }
                     
                    mathExpression.Operation = ParseOperation(currentChar.ToString());
                    token = "";
                }
                else if (currentChar == '-' && i > 0)
                {
                    if (mathExpression.Operation == MathOperation.None)
                    {
                        mathExpression.Operation = MathOperation.Subtract;

                        if (!leftSideInitialized)
                        {
                            mathExpression.LeftSideOperand = double.Parse(token);
                            leftSideInitialized = true;
                            token = "";
                        }
                    }
                    else 
                        token += currentChar;
                }
                else if (char.IsLetter(currentChar))
                {
                    if(!leftSideInitialized)
                    {
                        if (!string.IsNullOrWhiteSpace(token))
                            mathExpression.LeftSideOperand = double.Parse(token);
                        else
                            mathExpression.LeftSideOperand = double.Parse("1");
                        token = "";
                    }

                    token += currentChar;
                    leftSideInitialized = true;
                    operandInitialized = true;
                }
                else if (currentChar == ' ')
                {
                    if(!leftSideInitialized)
                    {
                        mathExpression.LeftSideOperand = double.Parse(token);
                        leftSideInitialized = true;
                        token = "";
                    }
                    else if (mathExpression.Operation == MathOperation.None)
                    {
                        mathExpression.Operation = ParseOperation(token.ToString());
                        token = "";
                    }
                }
                else
                    token += currentChar;
            }

            return mathExpression;
        }

        private MathOperation ParseOperation(string operation)
        {
            switch (operation.ToLower())
            {
                case "+":
                    return MathOperation.Add;
                case "-":
                    return MathOperation.Subtract;
                case "*":
                    return MathOperation.Multiply;
                case "/":
                    return MathOperation.Divide;
                case "%":
                case "mod":
                    return MathOperation.Modulus;
                case "^":
                case "pow":
                    return MathOperation.Power;
                case "fac":
                    return MathOperation.Factorial;
                case "sin":
                    return MathOperation.Sin;
                case "cos":
                    return MathOperation.Cos;
                case "tan":
                    return MathOperation.Tan;
                default:
                    return MathOperation.None;
            }
        }
    }
}
