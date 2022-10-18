using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Library.UI
{
    public sealed partial class EmployeePage : Page
    {
        public EmployeePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Navigate to <see cref="AddBookPage"/>.
        /// </summary>
        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddBookPage), null);
        }

        /// <summary>
        /// Navigate to <see cref="AddJournalPage"/>.
        /// </summary>
        private void btnAddJournal_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddJournalPage), null);
        }

        /// <summary>
        /// Navigate to <see cref="CustomerPage"/> and pass a parameter that determine whether the user is an employee.
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            bool continueAsEmployee = true;
            this.Frame.Navigate(typeof(CustomerPage), continueAsEmployee);
        }

        /// <summary>
        /// Navigate to <see cref="MainPage"/>.
        /// </summary>
        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        /// <summary>
        /// Navigate to <see cref="AddExistCountriesAndPublishers"/>.
        /// </summary>
        private void btnAddToISBN_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddExistCountriesAndPublishers), null);
        }
    }
}
