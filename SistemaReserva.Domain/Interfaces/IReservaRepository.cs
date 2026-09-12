using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<PagedList<Reserva>> GetAllReservasAsync(int pageNumber, int pageSize);
        Task<Reserva> GetReservaByIdAsync(int id);
        Task<Reserva> CreateReservaAsync(Reserva reserva);
        Task<bool> Conflita(int recursoId, DateTimeOffset inicio, DateTimeOffset fim);
        void Update(Reserva reserva);
        Task<bool> DeleteReservaAsync(Reserva reserva);
        Task<PagedList<Reserva>> GetReservasById(string userId,int pageNumber,int pageSize);
        Task<PagedList<Reserva>> GetAllComFiltroAsync(int? recursoId, string? userId, int currentPage, int pageSize);
    }
}
