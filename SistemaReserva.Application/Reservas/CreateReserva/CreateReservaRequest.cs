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
        public DateTime Inicio { get;  set; }
        public DateTime Fim { get;  set; }
        public StatusReserva Status { get;  set; }
    }
}
