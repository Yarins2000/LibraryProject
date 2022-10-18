using Library.Data;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Library.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomerPage : Page
    {
        readonly ItemsDB items;
        readonly IRepository<LibraryItem> repository;
        List<LibraryItem> temp;
        bool asEmployee = false; //Flag that represents wether the user has entered as an employee or as a customer

        public CustomerPage()
        {
            this.InitializeComponent();
            items = ItemsDB.Instance;
            listItems.ItemsSource = items.LibraryItems;
            temp = new List<LibraryItem>();
            repository = new LibraryRepository();
            rbAll.IsChecked = true;
        }

        /// <summary>
        /// Function that executed when the user enter the current page from another page.
        /// If the user entered the page as an employee, he can edit or delete an existing item.
        /// If he is a customer, he can see the item's details or buy it.
        /// </summary>
        /// <param name="e">Stores the data from the navigation</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
                asEmployee = (bool)e.Parameter;

            if (asEmployee)
            {
                btnBuy.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Visible;
                btnDelete.Visibility = Visibility.Visible;
                btnShowDetails.Visibility = Visibility.Collapsed;
                btnBack.Visibility = Visibility.Visible;
            }
            else
            {
                btnBuy.Visibility = Visibility.Visible;
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                btnShowDetails.Visibility = Visibility.Visible;
                btnBack.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Sort the items by a specific order after selecting a sort method.
        /// </summary>
        private void cmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            temp = listItems.ItemsSource as List<LibraryItem>;
            switch (cmbSort.SelectedIndex)
            {
                case 0:
                    SortItems.SortByTitleAZ(temp);
                    break;
                case 1:
                    SortItems.SortByTitleZA(temp);
                    break;
                case 2:
                    SortItems.SortByPriceLTH(temp);
                    break;
                case 3:
                    SortItems.SortByPriceHTL(temp);
                    break;
                case 4:
                    SortItems.SortByDateOTN(temp);
                    break;
                case 5:
                    SortItems.SortByDateNTO(temp);
                    break;
            }

            listItems.ItemsSource = null;
            listItems.ItemsSource = temp;
        }

        /// <summary>
        /// Event that occurs after pressing the "buy" button. Attempting to buy the chosen item, if that worked out,
        /// the item get deleted from the repository, otherwise nothing. Showing a message correspondingly.
        /// </summary>
        private async void btnBuy_Click(object sender, RoutedEventArgs e)
        {
            if (!(listItems.SelectedItem is LibraryItem chosenItem))
            {
                var notSelected = new ContentDialog
                {
                    Content = "Please choose an item",
                    PrimaryButtonText = "Ok"
                };
                await notSelected.ShowAsync();
            }
            else
            {
                ContentDialog buyItem = new ContentDialog
                {
                    Title = "Buy this item?",
                    Content = "Are you sure you want to buy this item?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };

                ContentDialogResult result = await buyItem.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    repository.Delete(chosenItem.Id);
                    listItems.ItemsSource = null;
                    listItems.ItemsSource = items.LibraryItems;
                    rbAll.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// Sort the items according to the checked button.
        /// </summary>
        /// <param name="sender">The checked radio button</param>
        private void RbFilter_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton chosenRb)
            {
                GetItemsBySelectedRadioButton(chosenRb);
            }
        }
        /// <summary>
        /// Sort the items by their definition - <see cref="LibraryItem"/>/<see cref="Book"/>/<see cref="Journal"/>.
        /// </summary>
        /// <param name="rb">The checked radio button.</param>
        private void GetItemsBySelectedRadioButton(RadioButton rb)
        {
            temp = listItems.ItemsSource as List<LibraryItem>;
            listItems.ItemsSource = null;
            switch (rb.Name)
            {
                case "rbAll":
                    temp = items.LibraryItems;
                    break;
                case "rbBooks":
                    temp = items.LibraryItems.Where(item => item is Book).ToList();
                    break;
                case "rbJournals":
                    temp = items.LibraryItems.Where(item => item is Journal).ToList();
                    break;
            }
            listItems.ItemsSource = temp;
        }

        /// <summary>
        /// Navigate to <see cref="AddBookPage"/> or <see cref="AddJournalPage"/> according to the item's type.
        /// Passes the item as a parameter to edit.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is LibraryItem chosenItem)
            {
                if (chosenItem is Book)
                    this.Frame.Navigate(typeof(AddBookPage), chosenItem);
                else
                    this.Frame.Navigate(typeof(AddJournalPage), chosenItem);
            }
        }

        /// <summary>
        /// Event that occurs after pressing the "delete" button. Attempting to delete an item.
        /// </summary>
        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (listItems.SelectedItem is LibraryItem chosenItem)
            {
                ContentDialog deleteItem = new ContentDialog
                {
                    Title = "Delete this item?",
                    Content = "Are you sure you want to delete this item?",
                    PrimaryButtonText = "Yes",
                    CloseButtonText = "No"
                };

                ContentDialogResult result = await deleteItem.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    repository.Delete(chosenItem.Id);
                    listItems.ItemsSource = null;
                    listItems.ItemsSource = items.LibraryItems;
                    rbAll.IsChecked = true;
                }
            }
        }

        /// <summary>
        /// Event that occurs after pressing the "ShowDetails" button. Showing the item's full details.
        /// </summary>
        private async void btnShowDetails_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog cd = new ContentDialog()
            {
                PrimaryButtonText = "OK",
            };
            LibraryItem chosenItem = listItems.SelectedItem as LibraryItem;
            string title = chosenItem.Title;
            string publishDate = chosenItem.PublishDate.ToShortDateString();
            string price = chosenItem.Price.ToString();
            cd.Content = $"Title: {title}\nPublish Date: {publishDate}\nPrice: ₪{price}\n";

            if (chosenItem is Book chosenBook)
            {
                string author = string.Join(", ", chosenBook.Authors);
                string genre = string.Join(", ", chosenBook.Genres);
                string publisher = chosenBook.Publisher;
                string isbn = chosenBook.Isbn.ToString();
                string synopsis = chosenBook.Synopsis;
                cd.Content += $"Author/s: {author}\nGenre/s: {genre}\nPublisher: {publisher}\nISBN: {isbn}\nSynposis: {synopsis}\n";
            }
            if (chosenItem is Journal chosenJournal)
            {
                string freq = chosenJournal.Frequency.ToString();
                string contributer = string.Join(", ", chosenJournal.Contributers);
                string editor = string.Join(", ", chosenJournal.Editors);
                string genre = string.Join(", ", chosenJournal.Genres);
                cd.Content += $"Frequency: {freq}\nGenre/s: {genre}\nContributer/s: {contributer}\nEditors: {editor}\n";
            }
            cd.Content += $"Quantity in stock: {chosenItem.Count}";
            await cd.ShowAsync();
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
    }
}
