using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursoById
{
    public class GetRecursoByIdResponse
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get;  set; }
    }
}
