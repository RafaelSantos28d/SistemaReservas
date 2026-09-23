using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.GetReservaById
{
    public interface IGetReservasByIdService
    {
        Task<PagedList<GetReservasByIdResponse>> GetMinhasReservas(string userId,int pageNumber,int pageSize);
    }
}
