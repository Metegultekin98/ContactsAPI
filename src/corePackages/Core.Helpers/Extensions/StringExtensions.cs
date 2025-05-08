namespace Core.Helpers.Extensions;

public static class StringExtensions
{
    public static string ReplaceFirst(this string input, string search, string replacement)
    {
        int index = input.IndexOf(search);
        if (index < 0)
        {
            return input;
        }

        return input.Substring(0, index) + replacement + input.Substring(index + search.Length);
    }
}