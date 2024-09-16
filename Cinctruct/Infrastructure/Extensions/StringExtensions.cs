using System.Globalization;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Infrastructure.Extensions
{
	/// <summary>
	/// Provides extension methods for operations on <see cref="string"/> objects.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Checks if the given string is a valid URL.
		/// </summary>
		/// <param name="url">The string to check.</param>
		/// <returns><c>true</c> if the string is a valid URL; otherwise, <c>false</c>.</returns>
		public static bool IsValidUrl(this string? url)
		{
			return Uri.TryCreate(url, UriKind.Absolute, out _);
		}

		/// <summary>
		/// Checks if the given string is a valid email address.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string is a valid email address; otherwise, <c>false</c>.</returns>
		public static bool IsValidEmail(this string value)
		{
			string pattern = @"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+$";
			return Regex.IsMatch(value, pattern);
		}

		/// <summary>
		/// Checks if the string contains at least one uppercase letter.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string contains an uppercase letter; otherwise, <c>false</c>.</returns>
		public static bool ContainUpperCase(this string value)
		{
			return value.Any(char.IsUpper);
		}

		/// <summary>
		/// Checks if the string contains at least one lowercase letter.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string contains a lowercase letter; otherwise, <c>false</c>.</returns>
		public static bool ContainLowerCase(this string value)
		{
			return value.Any(char.IsLower);
		}

		/// <summary>
		/// Checks if the string contains at least one digit.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string contains a digit; otherwise, <c>false</c>.</returns>
		public static bool ContainDigit(this string value)
		{
			return value.Any(char.IsDigit);
		}

		/// <summary>
		/// Checks if the string contains at least one special character.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string contains a special character; otherwise, <c>false</c>.</returns>
		public static bool ContainSpecialCharacter(this string value)
		{
			return value.Any(x => !char.IsLetterOrDigit(x));
		}

		/// <summary>
		/// Trims the string and replaces multiple spaces with a single space.
		/// </summary>
		/// <param name="value">The string to trim and reduce.</param>
		/// <returns>A trimmed and reduced string.</returns>
		public static string TrimAndReduce(this string value)
		{
			return Regex.Replace(value.Trim(), @"\s+", " ");
		}

		/// <summary>
		/// Converts the string to title case (each word's first letter is capitalized).
		/// </summary>
		/// <param name="value">The string to convert.</param>
		/// <returns>The string in title case.</returns>
		public static string ToTitleCase(this string value)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
		}

		/// <summary>
		/// Truncates the string to the specified length and appends an ellipsis if needed.
		/// </summary>
		/// <param name="value">The string to truncate.</param>
		/// <param name="maxLength">The maximum length of the string.</param>
		/// <returns>The truncated string with an ellipsis if it was too long.</returns>
		public static string Truncate(this string value, int maxLength)
		{
			if (string.IsNullOrEmpty(value)) return value;
			return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "...";
		}

		/// <summary>
		/// Removes all special characters from the string.
		/// </summary>
		/// <param name="value">The string to process.</param>
		/// <returns>The string with special characters removed.</returns>
		public static string RemoveSpecialCharacters(this string value)
		{
			return Regex.Replace(value, "[^a-zA-Z0-9_.]+", "", RegexOptions.Compiled);
		}

		/// <summary>
		/// Checks if the string contains only numeric characters.
		/// </summary>
		/// <param name="value">The string to check.</param>
		/// <returns><c>true</c> if the string contains only numeric characters; otherwise, <c>false</c>.</returns>
		public static bool IsNumeric(this string value)
		{
			return value.All(char.IsDigit);
		}

		/// <summary>
		/// Encodes the string to HTML entities.
		/// </summary>
		/// <param name="value">The string to encode.</param>
		/// <returns>The HTML encoded string.</returns>
		public static string EncodeHtml(this string value)
		{
			return WebUtility.HtmlEncode(value);
		}

		/// <summary>
		/// Decodes HTML entities in the string.
		/// </summary>
		/// <param name="value">The string to decode.</param>
		/// <returns>The HTML decoded string.s</returns>
		public static string DecodeHtml(this string value)
		{
			return WebUtility.HtmlDecode(value);
		}

		/// <summary>
		/// Converts the string to a URL-friendly slug.
		/// </summary>
		/// <param name="value">The string to convert.</param>
		/// <returns>The URL-friendly slug.</returns>
		public static string ToSlug(this string value)
		{
			value = value.ToLowerInvariant();
			value = RemoveSpecialCharacters(value);
			value = value.TrimAndReduce();
			value = value.Replace(" ", "-");
			return value;
		}

		/// <summary>
		/// Encodes the string to Base64.
		/// </summary>
		/// <param name="value">The string to encode.</param>
		/// <returns>The Base64 encoded string.</returns>
		public static string Base64Encode(this string value)
		{
			var bytes = Encoding.UTF8.GetBytes(value);
			return Convert.ToBase64String(bytes);
		}

		/// <summary>
		/// Decodes the Base64 encoded string.
		/// </summary>
		/// <param name="value">The Base64 encoded string to decode.</param>
		/// <returns>The decoded string.</returns>
		public static string Base64Decode(this string value)
		{
			var bytes = Convert.FromBase64String(value);
			return Encoding.UTF8.GetString(bytes);
		}

		/// <summary>
		/// Ensures that the string starts with the specified prefix.
		/// </summary>
		/// <param name="value">The string to check and modify.</param>
		/// <param name="prefix">The prefix to ensure.</param>
		/// <returns>The string with the ensured prefix.</returns>
		public static string EnsureStartsWith(this string value, string prefix)
		{
			if (!value.StartsWith(prefix))
			{
				return prefix + value;
			}
			return value;
		}

		/// <summary>
		/// Ensures that the string ends with the specified suffix.
		/// </summary>
		/// <param name="value">The string to check and modify.</param>
		/// <param name="suffix">The suffix to ensure.</param>
		/// <returns>The string with the ensured suffix.</returns>
		public static string EnsureEndsWith(this string value, string suffix)
		{
			if (!value.EndsWith(suffix))
			{
				return value + suffix;
			}
			return value;
		}

		/// <summary>
		/// Masks part of the string with a specified character, leaving the start and end of the string visible.
		/// </summary>
		/// <param name="value">The string to mask.</param>
		/// <param name="unmaskedStartLength">The number of characters at the start that should not be masked.</param>
		/// <param name="unmaskedEndLength">The number of characters at the end that should not be masked.</param>
		/// <param name="maskChar">The character used to mask the string.</param>
		/// <returns>The masked string.</returns>
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
