using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.CreateReserva
{
    public class CreateReservaRequest
    {
        public int RecursoId { get;  set; }
        public string? Descricao { get;  set; }
        public DateTimeOffset Inicio { get;  set; }
        public DateTimeOffset Fim { get;  set; }
        
    }
}
