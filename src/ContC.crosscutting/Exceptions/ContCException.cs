using System;

namespace ContC.crosscutting.Exceptions
{
    public class ContCException : Exception
    {
        public ContCException()
        {
        }

        public ContCException(string message) : base(message)
        {
        }
    }
}
