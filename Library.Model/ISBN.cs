using System.Collections.Generic;
using System.Linq;

namespace Library.Model
{
    public class ISBN
    {
        private const int _prefix = 979;

        /// <summary>
        /// Gets or sets the collection of countries.
        /// </summary>
        public static Dictionary<int, string> Countries { get; set; }

        /// <summary>
        /// Gets or sets the collection of publisher.
        /// </summary>
        public static Dictionary<int, string> Publishers { get; set; }
        public int Country { get; set; }
        public int Publisher { get; private set; }

        /// <summary>
        ///The book's serial number for the <see cref="ISBN"/>. 
        ///Every title from the same publisher should have a unique serial number.
        /// </summary>
        public int SerialNumber { get; set; }

        /// <summary>
        /// The last number in the <see cref="ISBN"/>.It's calculated by unique method.
        /// </summary>
        public int Control { get { return (Country + Publisher + SerialNumber) % 10; } }

        /// <summary>
        /// Set the country by checking if the key exist in the dictionary.
        /// </summary>
        /// <param name="value">The country that the user insert</param>
        public void SetCountry(string value)
        {
            if (Countries.ContainsValue(value))
                this.Country = Countries.Keys.First(key => Countries[key] == value);
            else throw new IsbnException($"Unknown State '{value}' ");
        }

        /// <summary>
        /// Set the publisher by checking if the key exist in the dictionary.
        /// </summary>
        /// <param name="value">The publisher that the user insert</param>
        public void SetPublisher(string value)
        {
            if (Publishers.ContainsValue(value))
                this.Publisher = Publishers.Keys.First(key => Publishers[key] == value);
            else throw new IsbnException($"Unknown Publisher '{value}' ");
        }

        public override string ToString()
        {
            return $"{_prefix}-{Country:D3}-{Publisher:D3}-{SerialNumber:D3}-{Control}";
        }
    }
}
