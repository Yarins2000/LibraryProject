using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Library.UI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("he-IL");
        }

        /// <summary>
        /// Navigate to <see cref="CustomerPage"/>.
        /// </summary>
        private void btnNavToCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomerPage), null);
        }

        /// <summary>
        /// Navigate to <see cref="EmployeePage"/>.
        /// </summary>
        private void btnNavToEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeePage), null);
        }

    }
}
