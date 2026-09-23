using SistemaReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursos
{
    public class GetRecursosResponse
    {
        public int RecursoId { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get;  set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}
