using System;

namespace Library.Model
{
    /// <summary>
    /// Represents errors that occur due to wrong ISBN parameters.
    /// </summary>
    public class IsbnException : Exception
    {
        public IsbnException() { }
        public IsbnException(string message) : base(message) { }
        public IsbnException(string message, Exception inner) : base(message, inner) { }
    }
}
