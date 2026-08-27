using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IRecursoRepository RecursoRepository { get; }

        Task CommitAync();
    }
}
