using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Interfaces
{
    public interface IReservaRepository
    {
        Task<PagedList<Reserva>> GetAllReservasAsync(int pageNumber, int pageSize);
        Task<Reserva> GetReservaByIdAsync(int id);
        Task<Reserva> CreateReservaAsync(Reserva reserva);
        Task<bool> Conflita(int recursoId, DateTime inicio, DateTime fim);
        void Update(Reserva reserva);
        Task<bool> DeleteReservaAsync(Reserva reserva);
        Task<PagedList<Reserva>> GetReservasById(string userId,int pageNumber,int pageSize);
    }
}
