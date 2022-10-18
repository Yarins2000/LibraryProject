using Library.Model;
using Library.Data;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Library.UI
{
    public sealed partial class AddExistCountriesAndPublishers : Page
    {
        //A page to add a new country/publisher to their dictionaries.
        public AddExistCountriesAndPublishers()
        {
            this.InitializeComponent();
        }

        private async void ShowMessage(string msg)
        {
            await new MessageDialog(msg).ShowAsync();
        }

        /// <summary>
        /// Event that occurs when pressing the "Add" button. Attempting to add a country or/and add a publisher.
        /// Showing messages correspondingly.
        /// </summary>
        private void btnAddToDict_Click(object sender, RoutedEventArgs e)
        {
            if (IsCountryValid())
            {
                int countryNum = int.Parse(txbCountryNum.Text);
                if (!IsCountryAlreadyExist(txbCountry.Text, countryNum))
                {
                    ISBN.Countries.Add(countryNum, txbCountry.Text);
                    txbCountry.Text = string.Empty;
                    txbCountryNum.Text = string.Empty;
                    ShowMessage("The country was added");
                }
                else
                    ShowMessage("The country is already exist");
            }
            else
                ShowMessage("Invalid country input");

            if (IsPublisherValid())
            {
                int PublisherNum = int.Parse(txbPublisherNum.Text);
                if (!IsPublisherAlreadyExist(txbPublisher.Text, PublisherNum))
                {
                    ISBN.Publishers.Add(PublisherNum, txbPublisher.Text);
                    txbPublisher.Text = string.Empty;
                    txbPublisherNum.Text = string.Empty;
                    ShowMessage("The publisher was added");
                }
                else
                    ShowMessage("The publisher is already exist");
            }
            else
                ShowMessage("Invalid publisher input");
        }

        /// <summary>
        /// Checks if the country is valid.
        /// </summary>
        /// <returns>true if the country is valid, otherwise false.</returns>
        private bool IsCountryValid()
        {
            if (txbCountry == null || txbCountryNum == null)
                return false;
            if (!Validation.IsLegalCharacters(txbCountry.Text))
                return false;
            if (!int.TryParse(txbCountryNum.Text, out _))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the publisher is valid.
        /// </summary>
        /// <returns>true if the publisher is valid, otherwise false.</returns>
        private bool IsPublisherValid()
        {
            if (txbPublisher == null || txbPublisherNum == null)
                return false;
            if (!Validation.IsLegalCharacters(txbPublisher.Text))
                return false;
            if (!int.TryParse(txbPublisherNum.Text, out _))
                return false;

            return true;
        }

        /// <summary>
        /// Checks if the country is already exist in its dictionary.
        /// </summary>
        /// <param name="country">The country value.</param>
        /// <param name="num">The country number(key in dictionary).</param>
        /// <returns>true if at least one of the parameters is existing, otherwise false.</returns>
        private bool IsCountryAlreadyExist(string country, int num)
        {
            if (ISBN.Countries.ContainsKey(num) || ISBN.Countries.ContainsValue(country))
                return true;
            return false;
        }
        /// <summary>
        /// Checks if the publisher is already exist in its dictionary.
        /// </summary>
        /// <param name="publisher">The publisher value.</param>
        /// <param name="num">The publisher number(key in dictionary).</param>
        /// <returns>true if at least one of the parameters is existing, otherwise false.</returns>
        private bool IsPublisherAlreadyExist(string publisher, int num)
        {
            if (ISBN.Publishers.ContainsKey(num) || ISBN.Publishers.ContainsValue(publisher))
                return true;
            return false;
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
