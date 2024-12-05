namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write(">> ");
                string input = Console.ReadLine() ?? string.Empty;
                int whiteSpaceIndex = input.IndexOf(' ');
                string command = whiteSpaceIndex == -1 ? input : input.Substring(0, whiteSpaceIndex).ToLower();
                string path = whiteSpaceIndex == -1 ? string.Empty : input.Substring(whiteSpaceIndex + 1).Trim();

                switch (command)
                {
                    case "list":
                        foreach (var entry in Directory.GetDirectories(path))
                            Console.WriteLine($"\t[Dir] {entry}");           
                        
                        foreach (var entry in Directory.GetFiles(path))
                            Console.WriteLine($"\t[File] {entry}");
                        break;
                    case "info":
                        if (Directory.Exists(path))
                        { 
                            var dirInfo = new DirectoryInfo(path);
                            Console.WriteLine("\nDirectory Info: ");
                            Console.WriteLine($"Creation time: {dirInfo.CreationTime}");
                            Console.WriteLine($"Last Update: {dirInfo.LastWriteTime}");
                        }
                        else if (File.Exists(path))
                        {
                            var fileInfo = new FileInfo(path);
                            Console.WriteLine("\nFile Info: ");
                            Console.WriteLine($"Creation time: {fileInfo.CreationTime}");
                            Console.WriteLine($"Last Update: {fileInfo.LastWriteTime}");
                            Console.WriteLine($"File Size In Bytes: {fileInfo.Length}");
                        }
                        else
                            Console.WriteLine("Not found");
                        break;                    
                    case "mkdir":
                        Directory.CreateDirectory(path);
                        break;                    
                    case "remove":
                        if (Directory.Exists(path))
                            Directory.Delete(path);
                        else if (File.Exists(path))
                            File.Delete(path);
                        else
                            Console.WriteLine("Not found");
                        break;                    
                    case "read":
                        if(File.Exists(path))
                        {
                            string content = File.ReadAllText(path);
                            Console.WriteLine(content);
                        }
                        else
                            Console.WriteLine("Not found");
                        break;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
