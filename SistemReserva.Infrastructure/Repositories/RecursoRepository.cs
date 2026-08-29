using Microsoft.EntityFrameworkCore;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Domain.Pagination;
using SistemReserva.Infrastructure.Context;
using SistemReserva.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.Repositories
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

        public async Task<bool> RemoveAsync(Recurso recurso)
        {
            _context.Recursos.Remove(recurso);
            return true;

        }

        
    }
}

