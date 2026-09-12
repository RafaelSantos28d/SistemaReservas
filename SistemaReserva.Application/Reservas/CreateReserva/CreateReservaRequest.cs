using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.CreateReserva
{
    public class CreateReservaRequest
    {
        public int RecursoId { get;  set; }
        public string? Descricao { get;  set; }
        public DateTimeOffset Inicio { get;  set; }
        public DateTimeOffset Fim { get;  set; }
        
    }
}
