using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.CancelarReserva
{
    public interface ICancelarReservaService
    {
        Task CancelarReserva(int reservaId, string userId, bool isAdmin);
    }
}
