using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Interfaces
{
    public interface ITokenService
    {
        string GerarToken(ApplicationUser usuario, IList<string> roles);
        string GerarRefreshToken();
    }
}
