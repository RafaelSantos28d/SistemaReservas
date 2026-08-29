using SistemReserva.Domain.Interfaces;
using SistemReserva.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.Repositories
{
    public class UnitOfWork :IUnitOfWork
    {
        private readonly BancoContext _context;
        private IRecursoRepository _recursoRepository;
        private IReservaRepository _reservaRepository;
        public UnitOfWork(BancoContext context)
        {
            _context = context;
        }
        public IRecursoRepository RecursoRepository
        {
            get
            {
                return _recursoRepository = _recursoRepository ?? new RecursoRepository(_context);
            }
        }
        public IReservaRepository ReservaRepository
        {
            get
            {
                return _reservaRepository = _reservaRepository ?? new ReservaRepository(_context);
            }
        }

        public async Task CommitAync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

