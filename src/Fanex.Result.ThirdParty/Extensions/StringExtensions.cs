namespace Fanex.Result.ThirdParty.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string[] ToArray(this string delimitedString)
        {
            if (string.IsNullOrWhiteSpace(delimitedString))
            {
                return new string[0];
            }

            return delimitedString.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
