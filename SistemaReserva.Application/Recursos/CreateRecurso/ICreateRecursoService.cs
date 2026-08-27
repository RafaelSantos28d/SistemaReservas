using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.CreateRecurso
{
    public interface ICreateRecursoService
    {
        Task<CreateRecursoResponse> CreateRecursoAsync(CreateRecursoRequest request);
    }
}
