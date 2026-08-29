using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.UpdateRecurso
{
    public class UpdateRecursoRequest
    {
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
