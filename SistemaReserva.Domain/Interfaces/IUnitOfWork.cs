using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRecursoRepository RecursoRepository { get; }
        IReservaRepository ReservaRepository { get; }
        Task CommitAsync();
    }
}
