using Library.Data;
using Library.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    public sealed partial class AddBookPage : Page
    {
        readonly LibraryRepository repository;
        Book bookToEdit;
        public AddBookPage()
        {
            this.InitializeComponent();
            repository = new LibraryRepository();
            cmbBookCountry.ItemsSource = ISBN.Countries.Values;
            cmbPublisher.ItemsSource = ISBN.Publishers.Values;
        }

        private async void ShowMessage(string s)
        {
            await new MessageDialog(s).ShowAsync();
        }

        /// <summary>
        /// Examine the title validation.
        /// </summary>
        /// <param name="title">the title that received as string</param>
        /// <returns>true if the title is valid, otherwise false</returns>
        private bool TitleValidation(string title)
        {
            if (!Validation.IsNotEmpty(title))
            {
                ShowMessage("You must write a title!");
                return false;
            }
            else if (!Validation.IsLegalCharacters(title))
            {
                ShowMessage("You must write only legal characters: letters, dots, spaces, commas and hyphens");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Examine the date validation.
        /// </summary>
        /// <param name="dt">the date that received as DateTime</param>
        /// <returns>true if the date is valid, otherwise false</returns>
        private bool PublishDateValidation(DateTime dt)
        {
            if (!Validation.IsDateValid(dt))
            {
                ShowMessage("The date is invalid. Cannot be after the current date.");
                return false;
            }
            //If the user doesn't choose a date, the default is 1/1/1601, so there is a necessity to check wether the user chose a date(perhaps he forgot)
            else if (dt == new DateTime(1601, 1, 1))
            {
                ShowMessage("You haven't chosen a date");
                return false;
            }
            return true;
        }
        /// <summary>
        /// Examine the price validation.
        /// </summary>
        /// <param name="stringPrice">The price that received as string</param>
        /// <returns>true if the price is valid, otherwise false</returns>
        private bool PriceValidation(string stringPrice)
        {
            if (!Validation.IsNotEmpty(stringPrice) || !Validation.IsPriceValid(stringPrice))
            {
                ShowMessage("The price is invalid. Only digits and dots");
                return false;
            }
            return true;
        }

        private int GenerateSerialNumber()
        {
            Random r = new Random();
            return r.Next(100, 1000);
        }

        /// <summary>
        /// Gets the data from the form, stores them in variables and after validation inserts them into a new book.
        /// </summary>
        /// <returns>the new book</returns>
        private Book AddDetailsToBook()
        {
            string title = txbBookTitle.Text;

            int year = dpBookPublishDate.Date.Year;
            int month = dpBookPublishDate.Date.Month;
            int day = dpBookPublishDate.Date.Day;
            DateTime publishDate = new DateTime(year, month, day);

            string stringPrice = txbBookPrice.Text;
            double price;

            string country = cmbBookCountry.SelectedItem?.ToString();
            string author = txbBookAuthor.Text;
            string publisher = cmbPublisher.SelectedItem?.ToString();
            string genre = txbBookGenre.Text;
            string synopsis = txbBookSynopsis.Text;
            string count = txbBookCount.Text;

            Book newBook;
            if (TitleValidation(title) && PublishDateValidation(publishDate) && PriceValidation(stringPrice))
            {
                price = double.Parse(stringPrice);

                newBook = new Book(title, publishDate, price, GenerateSerialNumber());

                if (country != null)
                    newBook.Isbn.SetCountry(country);

                if (Validation.IsNotEmpty(author) && Validation.CanBeAddToList(author))
                    newBook.Authors.AddRange(author.Split(','));

                if (publisher != null && Validation.IsLegalCharacters(publisher))
                    newBook.Publisher = publisher;

                if (Validation.IsNotEmpty(genre) && Validation.CanBeAddToList(genre))
                    newBook.Genres.AddRange(genre.Split(','));

                if (Validation.IsNotEmpty(synopsis))
                    newBook.Synopsis = synopsis;

                if (Validation.IsNotEmpty(count) && Validation.IsNumber(count))
                    newBook.Count = int.Parse(count);

                return newBook;
            }
            return null;
        }

        /// <summary>
        /// Gets the book from the <see cref="AddDetailsToBook"/> function and inserts it into the data-base.
        /// </summary>
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            Book newBook = AddDetailsToBook();
            if (newBook != null)
            {
                repository.Add(newBook);
                if (repository.GetSpecificItem(newBook.Id) != null)
                {
                    ShowMessage("The book has been added successfully");
                    ClearFields();
                }
                if (newBook.Id == Guid.Empty)
                {
                    ShowMessage("The book is already exist in the repository. It was added to its stock");
                    ClearFields();
                }
            }
        }

        private void ClearFields()
        {
            txbBookAuthor.Text = "";
            txbBookGenre.Text = "";
            txbBookPrice.Text = "";
            txbBookSynopsis.Text = "";
            txbBookTitle.Text = "";
            cmbBookCountry.SelectedItem = null;
            cmbPublisher.SelectedItem = null;
            dpBookPublishDate.SelectedDate = null;
        }

        private void btnClearValues_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Navigate to <see cref="MainPage"/>.
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        /// <summary>
        /// Navigate to <see cref="EmployeePage"/>.
        /// </summary>
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeePage), null);
        }

        /// <summary>
        ///Gets the edited book from the <see cref="AddDetailsToBook"/> function and updates it to the data-base. 
        /// </summary>
        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            Book editedBook = AddDetailsToBook();
            if (editedBook != null)
                repository.Update(bookToEdit, editedBook);
            if (repository.GetSpecificItem(editedBook.Id) != null)
            {
                ShowMessage("The book has been edited successfully");
                this.Frame.Navigate(typeof(CustomerPage), true);
            }
        }

        /// <summary>
        /// Function that executed when the user enter the current page from another page.
        /// If the user entered the page as an employee, it means that he edits a certain book and he receives it from the parameter that was passed in the navigation.
        /// </summary>
        /// <param name="e">Stores the data from the navigation</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                btnAddBook.Visibility = Visibility.Collapsed;
                btnEditBook.Visibility = Visibility.Visible;

                bookToEdit = e.Parameter as Book;
                txbBookTitle.Text = bookToEdit.Title;
                txbBookPrice.Text = bookToEdit.Price.ToString();
                dpBookPublishDate.Date = bookToEdit.PublishDate;
                txbBookCount.Text = bookToEdit.Count.ToString();
                if (bookToEdit.Authors != null)
                    txbBookAuthor.Text = string.Join(",", bookToEdit.Authors);
                if (bookToEdit.Genres != null)
                    txbBookGenre.Text = string.Join(",", bookToEdit.Genres);
                if (bookToEdit.Synopsis != null)
                    txbBookSynopsis.Text = bookToEdit.Synopsis;
                if (bookToEdit.Isbn.Country != 0)
                    cmbBookCountry.SelectedItem = ISBN.Countries[bookToEdit.Isbn.Country];
                if (bookToEdit.Isbn.Publisher != 0)
                    cmbPublisher.SelectedItem = ISBN.Publishers[bookToEdit.Isbn.Publisher];
            }
            else
            {
                btnAddBook.Visibility = Visibility.Visible;
                btnEditBook.Visibility = Visibility.Collapsed;
            }
        }

    }
}
