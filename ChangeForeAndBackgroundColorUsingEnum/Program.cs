namespace MyApp
{
    public class Program
    {
        public enum Colors
        {
            Black,
            DarkBlue,
            DarkGreen,
            DarkCyan,
            DarkRed,
            DarkMagenta,
            DarkYellow,
            Gray,
            DarkGray,
            Blue,
            Green,
            Cyan,
            Red,
            Magenta,
            Yellow,
            White
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Please Choose a Number..");
                Console.WriteLine("[1] Change A Background Color\t\t[2] Change A Foreground Color\t\t[3] Exit");

                if (!int.TryParse(Console.ReadLine(), out int choiceOption))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                // Exit option
                if (choiceOption == 3) break;

                Console.WriteLine("\nPlease Choose a Color..");
                foreach (var color in Enum.GetNames(typeof(Colors)))
                    Console.WriteLine($"- {color}");

                Console.Write("Which Color Do You Want to Choose?");
                string choiceColor = Console.ReadLine() ?? string.Empty;

                try
                {
                    // Parse and validate the color
                    Colors selectedColor = (Colors)Enum.Parse(typeof(Colors), choiceColor, true);

                    switch (choiceOption)
                    {
                        case 1:
                            Console.BackgroundColor = (ConsoleColor)selectedColor;
                            break;
                        case 2:
                            Console.ForegroundColor = (ConsoleColor)selectedColor;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Invalid color. Please choose from the list.");
                }
            }

            // Reset to default colors on exit
            Console.ResetColor();
        }
    }
}
