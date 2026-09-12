using SistemaReserva.Application.Reservas.GetAllReservas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursoById
{
    public interface IGetRecursoByIdService
    {
        Task<GetRecursoByIdResponse> GetRecursoById(int recursoId);
    }
}
