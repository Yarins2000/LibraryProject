using Library.Model;
using System.Collections.Generic;

namespace Library.Data
{
    /// <summary>
    /// Static class for sorting a list of <see cref="LibraryItem"/> by diferent conditions.
    /// </summary>
    public static class SortItems
    {
        //title A-Z
        public static void SortByTitleAZ(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item1.Title.CompareTo(item2.Title));
        }
        //title Z-A
        public static void SortByTitleZA(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item2.Title.CompareTo(item1.Title));
        }
        //price low-high
        public static void SortByPriceLTH(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item1.Price.CompareTo(item2.Price));
        }
        //price high-low
        public static void SortByPriceHTL(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item2.Price.CompareTo(item1.Price));
        }
        //date old to new
        public static void SortByDateOTN(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item1.PublishDate.CompareTo(item2.PublishDate));
        }
        //date new to old
        public static void SortByDateNTO(List<LibraryItem> list)
        {
            list.Sort((item1, item2) => item2.PublishDate.CompareTo(item1.PublishDate));
        }
    }
}
