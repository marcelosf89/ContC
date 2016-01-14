using System;

namespace ContC.crosscutting.Exceptions
{
    public class EntidadeNaoEncontradaException : Exception
    {
        public EntidadeNaoEncontradaException(string message) : base(message)
        {
        }
    }
}
