using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.DeleteRecurso
{
    public interface IDeleteRecursoService
    {
        Task<bool> DeleteRecurso(int id);
    }
}
