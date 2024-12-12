namespace TextAnalyzer
{
    internal class CSVFileAnalyzer : FileAnalyzer, IFileAnalysis
    {
        public void AnalyzeFile(FileInfo path)
        {
            string[] text = File.ReadAllLines(path.FullName);
            var result = new AnalysisResult();

            result.CharCount = text.Length;
            result.FieldCount = text[0].Split(',').Length;

            SetResult(result);
        }
    }
}