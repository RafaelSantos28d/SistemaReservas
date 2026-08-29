using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.CreateReserva
{
    public interface ICreateReservaService
    {
        Task<CreateReservaResponse> CreateReservaAsync(CreateReservaRequest request,string userId);
    }
}
