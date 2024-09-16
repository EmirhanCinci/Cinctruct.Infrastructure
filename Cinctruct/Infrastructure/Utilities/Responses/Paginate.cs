namespace Infrastructure.Utilities.Responses
{
	/// <summary>
	/// Represents a paginated collection of items.
	/// </summary>
	/// <typeparam name="T">The type of items in the paginated collection.</typeparam>
	public class Paginate<T>
	{
		/// <summary>
		/// Gets or sets the number of items per page.
		/// </summary>
		public int Size { get; set; }

		/// <summary>
		/// Gets or sets the current page index (1-based).
		/// </summary>
		public int Index { get; set; } = 1;

		/// <summary>
		/// Gets or sets the total number of pages.
		/// </summary>
		public int Pages { get; set; }

		/// <summary>
		/// Gets or sets the total number of items.
		/// </summary>
		public long Count { get; set; }

		/// <summary>
		/// Gets or sets the list of items on the current page.
		/// </summary>
		public List<T>? Items { get; set; }

		/// <summary>
		/// Gets a value indicating whether there is a previous page.
		/// </summary>
		public bool HasPrevious => Index > 0;

		/// <summary>
		/// Gets a value indicating whether there is a next page.
		/// </summary>
		public bool HasNext => Index + 1 < Pages;
	}
}
