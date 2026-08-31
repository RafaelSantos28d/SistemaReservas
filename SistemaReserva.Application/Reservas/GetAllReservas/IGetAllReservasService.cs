using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.GetAllReservas
{
    public interface IGetAllReservasService
    {
        Task<PagedList<GetAllReservasResponse>> GetAllReservasAsync(int? recursoId, string? userId, int currentPage, int pageSize);
    }
}
