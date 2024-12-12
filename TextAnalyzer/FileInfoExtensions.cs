namespace TextAnalyzer
{
    public static class FileInfoExtensions
    {
        public static bool IsTextFile(this FileInfo fileInfo)
        {
            if (fileInfo.Extension == ".txt") return true;
            else return false;
        }        
        
        public static bool IsCSVFile(this FileInfo fileInfo)
        {
            if (fileInfo.Extension == ".csv") return true;
            else return false;
        }
    }
}
