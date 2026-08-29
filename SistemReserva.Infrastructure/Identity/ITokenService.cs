using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.Identity
{
    public interface ITokenService
    {
        string GerarToken(ApplicationUser usuario, IList<string> roles);
        string GerarRefreshToken();
    }
}
