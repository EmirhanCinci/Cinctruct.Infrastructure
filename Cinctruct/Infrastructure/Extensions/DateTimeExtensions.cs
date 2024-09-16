namespace Infrastructure.Extensions
{
	/// <summary>
	/// Provides extension methods for the <see cref="DateTime"/> struct.
	/// </summary>
	public static class DateTimeExtensions
	{
		/// <summary>
		/// Determines if the given <see cref="DateTime"/> instance represents a weekend day (Saturday or Sunday).
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> instance to check.</param>
		/// <returns><c>true</c> if the date is a weekend; otherwise, <c>false</c>.s</returns>
		public static bool IsWeekend(this DateTime dateTime)
		{
			return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
		}

		/// <summary>
		/// Determines if the given <see cref="DateTime"/> instance represents a weekday (Monday through Friday).
		/// </summary>
		/// <param name="dateTime">The <see cref="DateTime"/> instance to check.</param>
		/// <returns><c>true</c> if the date is a weekday; otherwise, <c>false</c>.</returns>
		public static bool IsWeekday(this DateTime dateTime)
		{
			return !dateTime.IsWeekend();
		}

		/// <summary>
		/// Calculates the age in years based on the given birthdate.
		/// </summary>
		/// <param name="birthdate">The birthdate to calculate the age from.</param>
		/// <returns>The calculated age in years.</returns>
		public static int CalculateAge(this DateTime birthdate)
		{
			var today = DateTime.Today;
			var age = today.Year - birthdate.Year;
			if (birthdate.Date > today.AddYears(-age)) age--;
			return age;
		}
	}
}
