namespace Library.Helpers
{
    public static class TextSimilarityHelper
    {
        public static double CalculateJaccardSimilarity(string text1, string text2)
        {
            var words1 = new HashSet<string>(text1.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));
            var words2 = new HashSet<string>(text2.ToLower().Split(' ', StringSplitOptions.RemoveEmptyEntries));

            var intersection = words1.Intersect(words2).Count();
            var union = words1.Union(words2).Count();

            return union == 0 ? 0 : (double)intersection / union;
        }
    }
}
