using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.GetReservaByEmail
{
    public interface IGetReservasByIdService
    {
        Task<PagedList<GetReservasByIdResponse>> GetMinhasReservas(string userId,int pageNumber,int pageSize);
    }
}
