using System;

namespace Data.Models.Exceptions
{
    public class TodoException : Exception
    {
        public TodoException(string message) : base(message)
        {
        }
    }
}
