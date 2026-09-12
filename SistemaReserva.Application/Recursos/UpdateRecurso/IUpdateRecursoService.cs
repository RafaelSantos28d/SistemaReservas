using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.UpdateRecurso
{
    public interface IUpdateRecursoService
    {
        Task<bool> UpdateRecursoAsync(int id,UpdateRecursoRequest request);
    }
}
