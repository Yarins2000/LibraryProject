using System;
using System.Collections.Generic;

namespace Library.Model
{
    public class Journal : LibraryItem
    {
        /// <summary>
        /// Gets or sets the collection of journal's contributers.
        /// </summary>
        public List<string> Contributers { get; private set; }
        /// <summary>
        /// Gets or sets the collection of journal's editors.
        /// </summary>
        public List<string> Editors { get; private set; }
        /// <summary>
        /// Gets or sets the frequency of the journal by the enum <see cref="JournalFrequency"/>
        /// </summary>
        public JournalFrequency Frequency { get; set; }
        /// <summary>
        /// Gets or sets the collection of journal's genres.
        /// </summary>
        public List<string> Genres { get; private set; }

        public Journal(string title, DateTime publishDate, double price, JournalFrequency freq)
            : base(title, publishDate, price)
        {
            Contributers = new List<string>();
            Editors = new List<string>();
            Genres = new List<string>();
            Frequency = freq;
        }

        public override string ToString()
        {
            return $"{Title}, {string.Join(", ", Editors)}, {string.Join(", ", Contributers)}, {string.Join(", ", Genres)}, {Frequency}, {PublishDate:d}, ₪{Price}";
        }
    }

    public enum JournalFrequency
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        BiMonthly,
        Quarterly,
        Semiannual,
        Annually,
        BiAnnual,
        Other
    }
}
