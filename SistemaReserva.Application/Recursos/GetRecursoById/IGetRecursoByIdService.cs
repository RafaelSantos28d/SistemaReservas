using SistemReserva.Application.Reservas.GetAllReservas;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.GetRecursoById
{
    public interface IGetRecursoByIdService
    {
        Task<GetRecursoByIdResponse> GetRecursoById(int recursoId);
    }
}
