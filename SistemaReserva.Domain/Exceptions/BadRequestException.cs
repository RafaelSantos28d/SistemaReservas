using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
