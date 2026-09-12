using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.CreateReserva
{
    public interface ICreateReservaService
    {
        Task<CreateReservaResponse> CreateReservaAsync(CreateReservaRequest request,string userId);
    }
}
