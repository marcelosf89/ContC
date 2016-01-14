using System;

namespace ContC.crosscutting.Exceptions
{
    public class ContCNegocioException : Exception
    {
        public ContCNegocioException()
        {
        }

        public ContCNegocioException(string message) : base(message)
        {
        }
    }
}
