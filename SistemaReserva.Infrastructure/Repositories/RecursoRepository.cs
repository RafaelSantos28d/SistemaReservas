using Microsoft.EntityFrameworkCore;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Interfaces;
using SistemaReserva.Domain.Pagination;
using SistemaReserva.Infrastructure.Context;
using SistemaReserva.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Infrastructure.Repositories
{
    public class RecursoRepository :IRecursoRepository
    {
        private readonly BancoContext _context;

        public RecursoRepository(BancoContext context)
        {
            _context = context;
        }

        public async Task<Recurso> CreateAsync(Recurso recurso)
        {
            await _context.Recursos.AddAsync(recurso);
            return recurso;
        }

        public async Task<PagedList<Recurso>> GetAllRecursoAsync(int currentPage,int pageSize)
        {
            var query = _context.Recursos.AsNoTracking();
            return await PaginationHelper.CreateAsync(query, currentPage, pageSize);
        }

        public async Task<Recurso> GetByIdAsync(int id)
        {
            var recurso = await _context.Recursos.FirstOrDefaultAsync(x => x.RecursoId == id);
            return recurso;
        }

        public async Task<bool> PossuiReservasAsync(int recursoId)
        {
            
            return await _context.Reservas.AnyAsync(x => x.RecursoId == recursoId);
        }

        public async Task<bool> Remove(Recurso recurso)
        {
            _context.Recursos.Remove(recurso);
            return true;

        }

        
    }
}

