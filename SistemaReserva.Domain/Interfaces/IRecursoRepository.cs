using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Interfaces
{
    public interface IRecursoRepository
    {
        Task<PagedList<Recurso>> GetAllRecursoAsync(int currentPage, int pageSize);
        Task<Recurso> GetByIdAsync(int id);
        Task<Recurso> CreateAsync(Recurso recurso);
        Task<bool> RemoveAsync(Recurso recurso);
        Task<bool> PossuiReservasAsync(int recursoId);
    }
}
