using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.UpdateRecurso
{
    public interface IUpdateRecursoService
    {
        Task<bool> UpdateRecursoAsync(UpdateRecursoRequest request);
    }
}
