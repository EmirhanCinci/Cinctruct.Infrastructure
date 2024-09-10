using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static bool IsValidUrl(this string? url)
        {
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }

        public static bool IsValidEmail(this string value)
        {
            string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
            return Regex.IsMatch(value, pattern);
        }

        public static bool ContainUpperCase(this string value)
        {
            return value.Any(char.IsUpper);
        }

        public static bool ContainLowerCase(this string value)
        {
            return value.Any(char.IsLower);
        }

        public static bool ContainDigit(this string value)
        {
            return value.Any(char.IsDigit);
        }

        public static bool ContainSpecialCharacter(this string value)
        {
            return value.Any(x => !char.IsLetterOrDigit(x));
        }

        public static string TrimAndReduce(this string value)
        {
            return Regex.Replace(value.Trim(), @"\s+", " ");
        }

        public static string ToTitleCase(this string value)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }

        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
        }

        public static string RemoveSpecialCharacters(this string value)
        {
            return Regex.Replace(value, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
        }

        public static bool IsNumeric(this string value)
        {
            return value.All(char.IsDigit);
        }

        public static string EncodeHtml(this string value)
        {
            return WebUtility.HtmlEncode(value);
        }

        public static string DecodeHtml(this string value)
        {
            return WebUtility.HtmlDecode(value);
        }

        public static string ToSlug(this string value)
        {
            value = value.ToLowerInvariant();
            value = RemoveSpecialCharacters(value);
            value = value.TrimAndReduce();
            value = value.Replace(" ", "-");
            return value;
        }

        public static string Base64Encode(this string value)
        {
            var bytes = Encoding.UTF8.GetBytes(value);
            return Convert.ToBase64String(bytes);
        }

        public static string Base64Decode(this string value)
        {
            var bytes = Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(bytes);
        }

        public static string EnsureStartsWith(this string value, string prefix)
        {
            if (!value.StartsWith(prefix))
            {
                return prefix + value;
            }
            return value;
        }

        public static string EnsureEndsWith(this string value, string suffix)
        {
            if (!value.EndsWith(suffix))
            {
                return value + suffix;
            }
            return value;
        }

        public static string Mask(this string value, int unmaskedStartLength = 1, int unmaskedEndLength = 1, char maskChar = '*')
        {
            if (value.Length <= unmaskedStartLength + unmaskedEndLength)
            {
                return value;
            }
            var maskedSectionLength = value.Length - unmaskedStartLength - unmaskedEndLength;
            var maskedSection = new string(maskChar, maskedSectionLength);
            return value.Substring(0, unmaskedStartLength) + maskedSection + value.Substring(value.Length - unmaskedEndLength);
        }
    }
}
