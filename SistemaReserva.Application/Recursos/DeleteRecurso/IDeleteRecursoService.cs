using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.DeleteRecurso
{
    public interface IDeleteRecursoService
    {
        Task<bool> DeleteRecursoAsync(int id);
    }
}
