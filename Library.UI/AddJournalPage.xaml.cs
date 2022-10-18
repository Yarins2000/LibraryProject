using Library.Data;
using Library.Model;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    public sealed partial class AddJournalPage : Page
    {
        readonly LibraryRepository repository;
        Journal journalToEdit;
        public AddJournalPage()
        {
            this.InitializeComponent();
            cmbJournalFrequency.ItemsSource = Enum.GetValues(typeof(JournalFrequency));
            repository = new LibraryRepository();
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
            else if (dt == new DateTime(1601, 1, 1))
            {
                ShowMessage("You haven't chose a date");
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


        /// <summary>
        /// Gets the data from the form, stores them in variables and after validation inserts them into a new journal.
        /// </summary>
        /// <returns>the new journal</returns>
        private Journal AddDetailsToJournal()
        {
            string title = txbJournalTitle.Text;

            int year = dpJournalPublishDate.Date.Year;
            int month = dpJournalPublishDate.Date.Month;
            int day = dpJournalPublishDate.Date.Day;
            DateTime publishDate = new DateTime(year, month, day);

            string stringPrice = txbJournalPrice.Text;
            double price;

            string editor = txbJournalEditors.Text;
            string contributer = txbJournalContributers.Text;
            string genre = txbJournalGenre.Text;
            string count = txbJournalCount.Text;

            var freq = cmbJournalFrequency.SelectedItem != null ?
                (JournalFrequency)cmbJournalFrequency.SelectedItem : JournalFrequency.Other;

            Journal newJournal;

            if (TitleValidation(title) && PublishDateValidation(publishDate) && PriceValidation(stringPrice))
            {
                price = double.Parse(stringPrice);
                newJournal = new Journal(title, publishDate, price, freq);

                if (genre != null && Validation.CanBeAddToList(genre))
                    newJournal.Genres.AddRange(genre.Split(','));

                if (Validation.IsNotEmpty(editor) && Validation.CanBeAddToList(editor))
                    newJournal.Editors.AddRange(editor.Split(','));

                if (Validation.IsNotEmpty(contributer) && Validation.CanBeAddToList(contributer))
                    newJournal.Contributers.AddRange(contributer.Split(','));

                if (Validation.IsNotEmpty(count) && Validation.IsNumber(count))
                    newJournal.Count = int.Parse(count);

                return newJournal;
            }
            return null;
        }

        /// <summary>
        /// Gets the journal from the <see cref="AddDetailsToJournal"/> function and inserts it into the data-base.
        /// </summary>
        private void btnAddJournal_Click(object sender, RoutedEventArgs e)
        {
            Journal newJournal = AddDetailsToJournal();
            if (newJournal != null)
            {
                repository.Add(newJournal);
                if (repository.GetSpecificItem(newJournal.Id) != null)
                {
                    ShowMessage("The Journal has been added successfully");
                    ClearFields();
                }
                if (newJournal.Id == Guid.Empty)
                {
                    ShowMessage("The journal is already exist in the repository. It was added to its stock");
                    ClearFields();
                }
            }
        }

        private void ClearFields()
        {
            txbJournalContributers.Text = "";
            txbJournalEditors.Text = "";
            txbJournalGenre.Text = "";
            txbJournalPrice.Text = "";
            txbJournalTitle.Text = "";
            cmbJournalFrequency.SelectedItem = null;
            dpJournalPublishDate.SelectedDate = null;
        }
        private void btnClearValues_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }


        /// <summary>
        ///Gets the edited journal from the <see cref="AddDetailsToJournal"/> function and updates it to the data-base. 
        /// </summary>
        private void btnEditJournal_Click(object sender, RoutedEventArgs e)
        {
            Journal editedJournal = AddDetailsToJournal();
            if (editedJournal != null)
                repository.Update(journalToEdit, editedJournal);
            if (repository.GetSpecificItem(editedJournal.Id) != null)
            {
                ShowMessage("The journal has been edited successfully");
                this.Frame.Navigate(typeof(CustomerPage), true);
            }
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
        /// Function that executed when the user enter the current page from another page.
        /// If the user entered the page as an employee, it means that he edits a certain journal and he receives it from the parameter that was passed in the navigation.
        /// </summary>
        /// <param name="e">Stores the data from the navigation</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                btnAddJournal.Visibility = Visibility.Collapsed;
                btnEditJournal.Visibility = Visibility.Visible;

                journalToEdit = e.Parameter as Journal;
                txbJournalTitle.Text = journalToEdit.Title;
                txbJournalPrice.Text = journalToEdit.Price.ToString();
                dpJournalPublishDate.Date = journalToEdit.PublishDate;
                txbJournalCount.Text = journalToEdit.Count.ToString();
                cmbJournalFrequency.SelectedItem = journalToEdit.Frequency;
                if (journalToEdit.Contributers != null)
                    txbJournalContributers.Text = string.Join(",", journalToEdit.Contributers);
                if (journalToEdit.Genres != null)
                    txbJournalGenre.Text = string.Join(",", journalToEdit.Genres);
                if (journalToEdit.Editors != null)
                    txbJournalEditors.Text = string.Join(",", journalToEdit.Editors);
            }
            else
            {
                btnAddJournal.Visibility = Visibility.Visible;
                btnEditJournal.Visibility = Visibility.Collapsed;
            }
        }

    }
}
