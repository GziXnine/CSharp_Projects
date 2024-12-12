namespace TextAnalyzer
{
    public class FileAnalyzer
    {
        private AnalysisResult _result;

        public AnalysisResult GetResult() => _result;

        public void SetResult(AnalysisResult result)
        {
            _result = result;
        }
    }
}
