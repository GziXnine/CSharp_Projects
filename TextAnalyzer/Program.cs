namespace TextAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome To File Data Analyzer <3 !\n");
            Console.ResetColor();

            Console.Write("Please enter the file path: ");
            Console.ForegroundColor = ConsoleColor.Red;
            var path = Console.ReadLine() ?? string.Empty;
            Console.ResetColor();

            Console.WriteLine();

            DirectoryInfo directory = new DirectoryInfo(path);

            if (!directory.Exists)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("The directory does not exist.");
                Console.ResetColor();
                return;
            }

            var fileNames = directory.GetFiles();

            foreach (var file in fileNames)
            {
                if (file.IsTextFile())
                {
                    var txtFileAnalyzer = new TxtFileAnalyzer();
                    txtFileAnalyzer.AnalyzeFile(file);
                    var results = ((FileAnalyzer)txtFileAnalyzer).GetResult();

                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine($"File Name: {file.Name}");
                    Console.WriteLine($"File Size: {file.Length} bytes");
                    Console.WriteLine($"File Char Count: {results.CharCount}");
                    Console.WriteLine($"File Word Count: {results.WordCount}");
                    Console.WriteLine($"File Line Count: {results.LineCount}");
                    Console.WriteLine($"File Creation Time: {file.CreationTime}");
                    Console.WriteLine($"File Last Access Time: {file.LastAccessTime}");
                    Console.WriteLine($"File Last Write Time: {file.LastWriteTime}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
                else if (file.IsCSVFile())
                {
                    var txtFileAnalyzer = new CSVFileAnalyzer();
                    txtFileAnalyzer.AnalyzeFile(file);
                    var results = ((FileAnalyzer)txtFileAnalyzer).GetResult();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"File Name: {file.Name}");
                    Console.WriteLine($"File Size: {file.Length} bytes");
                    Console.WriteLine($"File Field Count: {results.FieldCount}");
                    Console.WriteLine($"File Creation Time: {file.CreationTime}");
                    Console.WriteLine($"File Last Access Time: {file.LastAccessTime}");
                    Console.WriteLine($"File Last Write Time: {file.LastWriteTime}");
                    Console.WriteLine();
                    Console.ResetColor();
                }
            }
        }
    }
}
