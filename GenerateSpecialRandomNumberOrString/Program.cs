using System.Text;

namespace MyApp
{
    internal class RandomGenerator
    {
        private static readonly Random random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            if (min >= max)
            {
                throw new ArgumentException("Minimum value must be less than the maximum value.");
            }
            return random.Next(min, max);
        }

        public static string GenerateRandomString(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Length must be a positive integer.");
            }

            const string smallBuffer = "abcdefghijklmnopqrstuvmxyz";
            const string capitalBuffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numberBuffer = "0123456789";
            const string specialBuffer = "!@#$%^&*()_+";

            Console.WriteLine("\n[1] Small Letters\n[2] Capital Letters\n[3] Numbers\n[4] Special Characters\n[5] All\n[6] Custom Combination");
            Console.Write("Please Choose Character Types:");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 6)
            {
                throw new ArgumentException("Invalid input. Please choose a valid option.");
            }

            StringBuilder characterPool = new StringBuilder();
            switch(choice) 
            {
                case 1:
                    characterPool.Append(smallBuffer);
                    break;              
                case 2:
                    characterPool.Append(capitalBuffer);
                    break;              
                case 3:
                    characterPool.Append(numberBuffer);
                    break;              
                case 4:
                    characterPool.Append(specialBuffer);
                    break;                               
                case 5: 
                    characterPool.Append(smallBuffer)
                                .Append(capitalBuffer)
                                .Append(numberBuffer)
                                .Append(specialBuffer);
                    break;
                case 6:
                    Console.Write("Enter custom characters: ");
                    string customInput = Console.ReadLine() ?? string.Empty;
                    if (string.IsNullOrWhiteSpace(customInput))
                    {
                        throw new ArgumentException("Custom input cannot be empty.");
                    }
                    characterPool.Append(customInput);
                    break;
            }

            string pool = characterPool.ToString();
            StringBuilder result = new StringBuilder();

            while (result.Length < length)
            {
                result.Append(pool[random.Next(pool.Length)]);
            }

            return result.ToString();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Main Menu:");
                Console.WriteLine("[1] Generate Random Number\n[2] Generate Random String\n[3]Exit\n");
                Console.Write("Please Choose an Option: ");

                if (!int.TryParse(Console.ReadLine(), out int choiceOption) || choiceOption < 1 || choiceOption > 3)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                if (choiceOption == 3)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Goodbye!");
                    Console.ResetColor();

                    break;
                }

                try
                {
                    switch (choiceOption)
                    {
                        case 1:
                            Console.Write("Please Choose a Minimum Value: ");
                            if (!int.TryParse(Console.ReadLine(), out int min)) throw new ArgumentException("Invalid minimum value.");

                            Console.Write("Please Choose a Maximum Value: ");
                            if (!int.TryParse(Console.ReadLine(), out int max)) throw new ArgumentException("Invalid maximum value.");

                            Console.Write("\nRandom Number: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(RandomGenerator.GenerateRandomNumber(min, max));
                            Console.ResetColor();
                            break;
                        case 2:
                            Console.Write("Please Choose a Length: ");
                            if (!int.TryParse(Console.ReadLine(), out int length)) throw new ArgumentException("Invalid length value.");

                            String Password = RandomGenerator.GenerateRandomString(length);
                            Console.Write("\nRandom String: ");
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine(Password);
                            Console.ResetColor();
                            break;
                        default:
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Error: {ex.Message}");
                    Console.ResetColor();
                }

                Console.WriteLine("___________________________");
            }
        }
    }
}
