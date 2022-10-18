using Library.Model;
using System;
using System.Collections.Generic;

namespace Library.Data
{
    public class ItemsDB
    {
        //single tone
        private static ItemsDB _instance;
        public static ItemsDB Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ItemsDB();
                return _instance;
            }
        }
        public List<LibraryItem> LibraryItems { get; private set; }
        private ItemsDB()
        {
            LibraryItems = new List<LibraryItem>();
            Init();
        }

        /// <summary>
        /// Initialize the mocked data.
        /// </summary>
        private void Init()
        {
            #region countries
            ISBN.Countries = new Dictionary<int, string>();
            ISBN.Countries.Add(965, "Israel");
            ISBN.Countries.Add(123, "United Kingdom");
            ISBN.Countries.Add(605, "Turkey");
            ISBN.Countries.Add(606, "Romania");
            ISBN.Countries.Add(607, "Mexico");
            ISBN.Countries.Add(88, "Italy");
            ISBN.Countries.Add(968, "Mexico");
            ISBN.Countries.Add(987, "Argentina");
            ISBN.Countries.Add(84, "Spain");
            ISBN.Countries.Add(972, "Portugal");
            ISBN.Countries.Add(618, "Greece");
            #endregion

            #region publishers
            ISBN.Publishers = new Dictionary<int, string>();
            ISBN.Publishers.Add(22, "Penguin english library");
            ISBN.Publishers.Add(36, "Bloomsbury");
            ISBN.Publishers.Add(25, "Ofakim");
            ISBN.Publishers.Add(115, "Dvir");
            ISBN.Publishers.Add(55, "Zmora Bitan");
            ISBN.Publishers.Add(87, "Yavne");
            ISBN.Publishers.Add(93, "Carmel");
            ISBN.Publishers.Add(67, "Jerusalem Inst");
            ISBN.Publishers.Add(100, "Evrit");
            #endregion

            #region adding books
            var book1 = new Book("Pride and Prejudice", new DateTime(2012, 6, 12), 89, 123);
            book1.Authors.Add("Jane Austen");
            book1.Publisher = "Penguin english library";
            book1.Synopsis = "A lawyer's advice to his children as he defends the real mockingbird of Harper Lee's classic novel - a black man falsely charged with the rape of a white girl. Through the young eyes of Scout and Jem Finch, Harper Lee explores with exuberant humour the irrationality of adult attitudes to race and class in the Deep South of the 1930s. The conscience of a town steeped in prejudice, violence and hypocrisy is pricked by the stamina of one man's struggle for justice. But the weight of history will only tolerate so much.";
            book1.Genres.Add("Action and adventure");
            book1.Genres.Add("Classic");
            book1.Genres.Add("Drama");
            book1.Genres.Add("Historical fiction");
            book1.Id = Guid.NewGuid();

            var book2 = new Book("To Kill A Mockingbird", new DateTime(2015, 4, 6), 75, 145);
            book2.Authors.Add("Harper Lee");
            book2.Publisher = "Penguin english library";
            book2.Synopsis = "When Elizabeth Bennet first meets eligible bachelor Fitzwilliam Darcy, she thinks him arrogant and conceited; he is indifferent to her good looks and lively mind. When she later discovers that Darcy has involved himself in the troubled relationship between his friend Bingley and her beloved sister Jane, she is determined to dislike him more than ever. In the sparkling comedy of manners that follows, Jane Austen shows the folly of judging by first impressions and superbly evokes the friendships, gossip and snobberies of provincial middle-class life.";
            book2.Genres.Add("Young adult");
            book2.Genres.Add("Classic");
            book2.Genres.Add("Drama");
            book2.Genres.Add("Historical fiction");
            book2.Id = Guid.NewGuid();

            var book3 = new Book("The Great Gatsby", new DateTime(2000, 2, 24), 60, 98);
            book3.Authors.Add("F. Scott Fitzgerald");
            book3.Publisher = "Penguin english library";
            book3.Synopsis = "Young, handsome and fabulously rich, Jay Gatsby is the bright star of the Jazz Age, but as writer Nick Carraway is drawn into the decadent orbit of his Long Island mansion, where the party never seems to end, he finds himself faced by the mystery of Gatsby's origins and desires. Beneath the shimmering surface of his life, Gatsby is hiding a secret: a silent longing that can never be fulfilled. And soon, this destructive obsession will force his world to unravel.";
            book3.Genres.Add("Classic");
            book3.Genres.Add("Drama");
            book3.Genres.Add("Historical fiction");
            book3.Id = Guid.NewGuid();

            var book4 = new Book("Harry Potter and the Philosopher's Stone", new DateTime(1997, 6, 26), 78, 100, "United Kingdom");
            book4.Authors.Add("J.K.Rowling");
            book4.Publisher = "Bloomsbury";
            book4.Genres.Add("Fanatasy");
            book4.Id = Guid.NewGuid();
            #endregion

            #region adding journals
            var journal1 = new Journal("Saving Forests", new DateTime(2022, 5, 1), 100, JournalFrequency.Annually);
            journal1.Editors.Add("David Brindley");
            journal1.Contributers.Add("National Geographic");
            journal1.Genres.Add("nature");
            journal1.Id = Guid.NewGuid();

            var journal2 = new Journal("Pulse of the heartland", new DateTime(2021, 10, 15), 90, JournalFrequency.BiMonthly);
            journal2.Editors.Add("National Wild Lifes");
            journal2.Contributers.Add("National Wild Lifes");
            journal2.Genres.Add("nature");
            journal2.Id = Guid.NewGuid();

            var journal3 = new Journal("History-General Grant", new DateTime(2018, 7, 10), 75, JournalFrequency.Monthly);
            journal3.Editors.Add("National Geographic");
            journal3.Contributers.Add("National Geographic");
            journal3.Genres.Add("history");
            journal3.Id = Guid.NewGuid();
            #endregion

            LibraryItems.AddRange(new LibraryItem[] { book1, book2, book3, book4, journal1, journal2, journal3 });

        }
    }
}
