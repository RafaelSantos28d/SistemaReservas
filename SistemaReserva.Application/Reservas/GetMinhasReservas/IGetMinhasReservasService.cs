using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.GetMinhasReservas
{
    public interface IGetMinhasReservasService
    {
        Task<PagedList<GetMinhasReservasResponse>> GetMinhasReservas(string userId,int pageNumber,int pageSize);
    }
}
