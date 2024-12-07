using System.Text;

namespace PasswordManager
{
    internal class Program
    {
        private static readonly Dictionary<string, string> passwords = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            startUp();

            while(true)
            {
                Console.WriteLine("\nPlease Select an Option.");
                Console.WriteLine("1. List all Passwords");
                Console.WriteLine("2. Add/Change a Password");
                Console.WriteLine("3. Get a Password");
                Console.WriteLine("4. Delete a Password");
                Console.WriteLine("5. Exit");
                Console.Write("Your Option: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                var selectedOption = Console.ReadLine();
                Console.ResetColor();

                if (selectedOption == "1")
                    listAllPasswords();
                else if (selectedOption == "2")
                    addChangePassword();
                else if (selectedOption == "3")
                    getPassword();
                else if (selectedOption == "4")
                    deletePassword();
                else if (selectedOption == "5")
                    break;
                else
                    Console.WriteLine("Invalid Option");
            }
        }

        private static void startUp()
        {
            readAllPasswords();

            if (!passwords.ContainsKey("masterKey"))
            {
                Console.WriteLine("Hi, Looks like you are running this application for the first time. Let's setup a Master Key.");
                Console.Write("Please Enter A Master Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string masterKey = Console.ReadLine() ?? string.Empty;

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Master Key is set successfully. Please remember this key. You will need this key to access your passwords.");
                Console.ResetColor();
                passwords.Add("masterKey", masterKey);

                saveAllPasswords();
            }
            else
            {
                Console.Write("Please Enter Master Key: ");
                Console.ForegroundColor = ConsoleColor.Blue;
                string masterKey = Console.ReadLine() ?? string.Empty;
                Console.ResetColor();

                if (passwords["masterKey"] != masterKey)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid Master Key");
                    Console.ResetColor();
                    Environment.Exit(0);
                }
            }
        }

        private static void listAllPasswords()
        {
            Console.WriteLine("\nList of all Passwords: ");
            foreach (var item in passwords)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{item.Key}={item.Value}");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private static void addChangePassword()
        {
            Console.Write("Please Enter A Website Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string websiteName = Console.ReadLine() ?? string.Empty;
            Console.ResetColor();

            if (passwords.ContainsKey(websiteName))
            {
                Console.Write("Password already exists. Do you want to change it? (Y/N): ");
                var changePassword = Console.ReadLine();

                if (changePassword == "Y" || changePassword == "y")
                {
                    Console.Write("Please Enter A Password: ");
                    Console.ForegroundColor = ConsoleColor.Red;
                    string password = Console.ReadLine() ?? string.Empty;
                    Console.ResetColor();
                    passwords[websiteName] = password;
                }
            }
            else
            {
                Console.Write("Please Enter A Password: ");
                Console.ForegroundColor = ConsoleColor.Red;
                string password = Console.ReadLine() ?? string.Empty;
                Console.ResetColor();
                passwords.Add(websiteName, password);
            }

            saveAllPasswords();
        }

        private static void getPassword()
        {
            Console.Write("Please Enter A Website Name: ");
            Console.ForegroundColor = ConsoleColor.Green;
            string websiteName = Console.ReadLine() ?? string.Empty;
            Console.ResetColor();

            if (passwords.ContainsKey(websiteName))
            {
                Console.Write("Password for ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{websiteName}");
                Console.ResetColor();
                Console.Write(" is ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{passwords[websiteName]}");
                Console.ResetColor();
            }
            else
                Console.WriteLine("Password not found");
        }

        private static void deletePassword()
        {
            Console.Write("Please Enter A Website Name: ");
            Console.ForegroundColor = ConsoleColor.Red;
            string websiteName = Console.ReadLine() ?? string.Empty;
            Console.ResetColor();

            if (passwords.ContainsKey(websiteName))
                passwords.Remove(websiteName);
            else
                Console.WriteLine("Password not found");

            saveAllPasswords();
        }

        private static void saveAllPasswords()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in passwords)
                sb.AppendLine($"{item.Key}={EncryptionUtility.Encrypt(item.Value)}");
            File.WriteAllText("passwords.txt", sb.ToString());
        }

        private static void readAllPasswords()
        {
            if(File.Exists("passwords.txt"))
            {
                var lines = File.ReadAllText("passwords.txt");
                foreach (var line in lines.Split(Environment.NewLine))
                {
                    if (!String.IsNullOrEmpty(line))
                    {
                        var equalIndex = line.IndexOf("=");
                        var name = line.Substring(0, equalIndex);
                        var password = line.Substring(equalIndex + 1);
                        passwords.Add(name, EncryptionUtility.Decrypt(password));
                    }
                }
            }
        }
    }
}
