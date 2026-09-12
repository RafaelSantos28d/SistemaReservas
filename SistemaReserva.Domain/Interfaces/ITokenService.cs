using SistemaReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(ApplicationUser usuario, IList<string> roles);
        string GerarRefreshToken();
    }
}
