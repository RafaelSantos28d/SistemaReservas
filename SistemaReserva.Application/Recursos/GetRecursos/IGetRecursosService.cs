using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursos
{
    public interface IGetRecursosService
    {
        Task<PagedList<GetRecursosResponse>> GetRecursosAsync(int currentPage, int pageSize);
    }
}
