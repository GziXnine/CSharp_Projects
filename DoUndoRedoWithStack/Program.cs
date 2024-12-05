namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stack = new Stack<AppendTextCommand> ();
            var redoStack = new Stack<AppendTextCommand> ();
            string originalText = "";

            while (true)
            {
                Console.Write("Enter text to append, ('exit' to Exit, 'undo' to Undo, 'redo' to Redo): ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string input = Console.ReadLine() ?? string.Empty;
                input = input.Trim();
                Console.ResetColor();

                if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;
                else if (input.Equals("undo", StringComparison.OrdinalIgnoreCase))
                {
                    if (stack.Count > 0)
                    {
                        var command = stack.Pop();
                        redoStack.Push(command);
                        originalText = command.undo();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Nothing to undo.");
                        Console.ResetColor();
                    }
                }
                else if (input.Equals("redo", StringComparison.OrdinalIgnoreCase))
                {
                    if (redoStack.Count > 0)
                    {
                        var command = redoStack.Pop();
                        originalText = command.Execute();
                        stack.Push(command); // Move back to undo stack
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Nothing to redo.");
                        Console.ResetColor();
                    }
                }
                else
                {
                    var command = new AppendTextCommand(originalText, input);
                    originalText = command.Execute();
                    stack.Push(command);
                }
            }   
        }
    }

    class AppendTextCommand
    {
        private string _originalText;
        private readonly string _textToAppend;

        public AppendTextCommand(string originalText, string textToAppend)
        {
            _originalText = originalText;
            _textToAppend = textToAppend;
        }

        public string Execute()
        {
            _originalText += _textToAppend + " ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(_originalText);
            Console.ResetColor();
            return _originalText;
        }

        public string undo()
        {
            _originalText = _originalText.Substring(0, _originalText.Length - _textToAppend.Length - 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(_originalText);
            Console.ResetColor();
            return _originalText;
        }
    }
}