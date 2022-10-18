using System;
using System.Collections.Generic;

namespace Library.Model
{
    /// <summary>
    /// Class representing a book, handled by the library.
    /// </summary>
    public class Book : LibraryItem
    {
        private const string _defaultCountry = "Israel";

        /// <summary>
        ///  Get international standard book number <see cref="Library.Model.ISBN"/>
        /// </summary>
        public ISBN Isbn { get; set; }

        /// <summary>
        /// Get collection of book's authors.
        /// </summary>
        public List<string> Authors { get; set; }

        /// <summary>
        /// Get or set the book's publisher. Must be a known publisher by the <see cref="Library.Model.ISBN"/>
        /// </summary>
        /// <exception cref="IsbnException">Thrown when attempting to set publisher value that is not recognized by ISBN</exception>
        public string Publisher
        {
            get
            {
                if (ISBN.Publishers.ContainsKey(this.Isbn.Publisher))
                    return ISBN.Publishers[this.Isbn.Publisher];
                return string.Empty;
            }
            set
            {
                this.Isbn.SetPublisher(value);
            }
        }

        /// <summary>
        /// Get collection of book's genres.
        /// </summary>
        public List<string> Genres { get; set; }

        /// <summary>
        /// Get or set book's synopsis (short summary of the book)
        /// </summary>
        public string Synopsis { get; set; }

        /// <summary>
        /// create an instance of a book.
        /// </summary>
        /// <param name="title">The title of the new book</param>
        /// <param name="publishDate">The date the new book was published</param>
        /// <param name="price">The book's price</param>
        /// <param name="serialNumber">Optional parameter, the book's serial number for the <see cref="Model.ISBN"/>. 
        /// Every title from the same publisher should have a unique serial number</param>
        /// <param name="country">Optional parameter, the state form the <see cref="Model.ISBN"/>.
        /// Default value is "Israel"</param>

        public Book(string title, DateTime publishDate, double price, int serialNumber = 0, string country = _defaultCountry)
            : base(title, publishDate, price)
        {
            this.Isbn = new ISBN() { SerialNumber = serialNumber };
            this.Isbn.SetCountry(country);
            Authors = new List<string>();
            Genres = new List<string>();
        }

        public override string ToString()
        {
            return $"{Title}, {string.Join("&", Authors)}, {string.Join("&", Genres)}, {Publisher}, {PublishDate:d}, ₪{Price}";
        }
    }
}
