using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.ListRecursos
{
    public interface IGetRecursosService
    {
        Task<PagedList<GetRecursoResponse>> GetListRecursosAsync(int currentPage, int pageSize);
    }
}
