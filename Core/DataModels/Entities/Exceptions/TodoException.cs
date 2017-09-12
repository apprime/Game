using System;

namespace Core.Entities.Exceptions
{
    class TodoException : Exception
    {
        public TodoException(string message) : base(message)
        {
        }
    }
}
