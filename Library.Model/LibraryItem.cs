using System;

namespace Library.Model
{
    /// <summary>
    /// Abstract class representing an item handled by the library.
    /// </summary>
    public abstract class LibraryItem
    {
        private const int DISCOUNT = 30; //discount in percents
        /// <summary>
        /// Unique identifier of this item.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The title item's name.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Print or publish date of item.
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// The Item's price.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// The quantity of an item in the repository.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Create an instance of library item.
        /// </summary>
        /// <param name="title">The name or title of the new item</param>
        /// <param name="publishDate">The print or publish date of the new item</param>
        protected LibraryItem(string title, DateTime publishDate, double price)
        {
            Title = title;
            PublishDate = publishDate;
            Price = DateTime.Now.Month == PublishDate.Month ? price - (price * DISCOUNT / 100) : price;
            Count = 1;
        }
    }
}
