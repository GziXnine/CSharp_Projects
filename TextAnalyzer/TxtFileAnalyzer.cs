namespace TextAnalyzer
{
    internal class TxtFileAnalyzer : FileAnalyzer, IFileAnalysis
    {
        public void AnalyzeFile(FileInfo path)
        {
            var text = File.ReadAllText(path.FullName);
            var result = new AnalysisResult();

            result.CharCount = text.Length;
            result.WordCount = text.Split(' ').Length;
            result.LineCount = text.Split('\n').Length;

            SetResult(result);
        }
    }
}