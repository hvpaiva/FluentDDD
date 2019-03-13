using System.Text.RegularExpressions;

namespace ValueObjects
{
    public class Format : IFormat
    {
        public Format(string pattern, string replacement, RegexOptions options = RegexOptions.Compiled)
        {
            Pattern = new Regex(pattern, options);
            Replacement = replacement;
        }

        public Regex Pattern { get; }

        public string Replacement { get; }
    }
}