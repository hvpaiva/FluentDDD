namespace ValueObjects
{
    public interface IFormatter
    {
        IFormat FormattedFormat { get; }

        IFormat UnformattedFormat { get; }

        string Format(string value);

        string Unformat(string value);

        bool IsFormatted(string value);

        bool IsUnformatted(string value);

        bool IsFormattable(string value);

        void AssertFormattable(string value);
    }
}