using System.Text.RegularExpressions;

namespace ValueObjects
{
    public interface IFormat
    {
        Regex Pattern { get; }

        string Replacement { get; }
    }
}