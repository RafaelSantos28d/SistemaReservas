using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Interfaces
{
    public interface IRecursoRepository
    {
        Task<PagedList<Recurso>> GetAllRecursoAsync(int currentPage, int pageSize);
        Task<Recurso> GetByIdAsync(int id);
        Task<Recurso> CreateAsync(Recurso recurso);
        Task<bool> UpdateAsync(Recurso recurso);
        Task<bool> RemoveAsync(int id);
    }
}
