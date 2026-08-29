using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Enums;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Domain.Pagination;
using SistemReserva.Infrastructure.Context;
using SistemReserva.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly BancoContext _context;

        public ReservaRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<bool> Conflita(int recursoId, DateTime inicio, DateTime fim)
        {
            return await _context.Reservas.Where( r=>r.RecursoId == recursoId && r.Status==StatusReserva.Confirmada && inicio < r.Fim && fim > r.Inicio).AnyAsync();
        }

        public async Task<Reserva> CreateReservaAsync(Reserva reserva)
        {
            await _context.Reservas.AddAsync( reserva );
            return reserva;
        }

        public async Task<bool> DeleteReservaAsync(Reserva reserva)
        {
            _context.Reservas.Remove( reserva );
            return true;
        }

        public async Task<PagedList<Reserva>> GetAllReservasAsync(int pageNumber,int pageSize)
        {
            var query = _context.Reservas.Include(x => x.Recurso).Include(x => x.User);
            return await PaginationHelper.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<Reserva> GetReservaByIdAsync(int id)
        {
            return await _context.Reservas.FirstOrDefaultAsync(x=>x.ReservaId == id);
        }

        public async Task<Reserva> UpdateReservaAsync(Reserva reserva)
        {
            var update = await GetReservaByIdAsync(reserva.ReservaId);
            update.Update(reserva.ReservaId, reserva.RecursoId,reserva.Descricao, reserva.UserId, reserva.Inicio, reserva.Fim, reserva.Status);

            return update;
        }
    }
}
