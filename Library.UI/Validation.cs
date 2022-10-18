using System;

namespace Library.UI
{
    /// <summary>
    /// Static class for checking the validations of item's parameters.
    /// </summary>
    public static class Validation
    {
        public static bool IsNotEmpty(string text)
        {
            return text != "";
        }

        /// <summary>
        /// Check wether the price is valid(can be cast to double and greater than 0).
        /// </summary>
        /// <param name="price">The price that received as string.</param>
        /// <returns>true if the price is valid, otherwise false.</returns>
        public static bool IsPriceValid(string price)
        {
            if (double.TryParse(price, out double p) && p > 0)
                return true;
            return false;
        }

        /// <summary>
        /// Check wether the text is alphabetical and contains legal characters.
        /// </summary>
        /// <param name="text">The text that received as string.</param>
        /// <returns>true if the text is valid, otherwise false.</returns>
        public static bool IsLegalCharacters(string text)
        {
            foreach (var character in text)
                if (!(char.IsLetter(character) || character == '.' || character == '-' || character == ' ' || character == ',' || character == '\''))
                    return false;
            return true;
        }

        /// <summary>
        /// Check wether the date is maximum the current date.
        /// </summary>
        /// <param name="dt">The received date.</param>
        /// <returns>true if the date is valid, otherwise false.</returns>
        public static bool IsDateValid(DateTime dt)
        {
            return dt < DateTime.Now;
        }

        /// <summary>
        /// Check wether a text can be added to a string list.
        /// </summary>
        /// <param name="text">The text that received as string.</param>
        /// <returns>true if the string is valid, otherwise false.</returns>
        public static bool CanBeAddToList(string text)
        {
            foreach (var character in text)
                if (!(char.IsLetter(character) || character == '.' || character == ',' || character == ' '))
                    return false;
            return true;
        }

        /// <summary>
        /// Check wether a text can be cast to int.
        /// </summary>
        /// <param name="text">The text that received as string.</param>
        /// <returns>true if the text is valid, otherwise false.</returns>
        public static bool IsNumber(string text)
        {
            foreach (var character in text)
                if (!char.IsDigit(character))
                    return false;
            return true;
        }
    }
}
