using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.CreateReserva
{
    public class CreateReservaResponse
    {
        public int ReservaId { get;  set; }
        public int RecursoId { get;  set; }
        public string RecursoName { get; set; }
        public string? Descricao { get;  set; }
        public string UserEmail { get;  set; }
        public DateTime Inicio { get;  set; }
        public DateTime Fim { get;  set; }
        public StatusReserva Status { get;  set; }
    }
}
