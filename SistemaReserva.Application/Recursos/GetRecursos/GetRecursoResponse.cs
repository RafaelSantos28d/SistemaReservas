using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.ListRecursos
{
    public class GetRecursoResponse
    {
        public int RecursoId { get; set; }
        public string Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Ativo { get; private set; }
        public ICollection<Reserva>? Reservas { get; set; }
    }
}
