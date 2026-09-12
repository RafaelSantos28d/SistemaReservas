using SistemaReserva.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Reservas.GetReservaByEmail
{
    public class GetReservasByIdResponse
    {
        public int ReservaId { get; set; }
        public int RecursoId { get; set; }
        public string RecursoName { get; set; }
        public string? Descricao { get; set; }
        public DateTimeOffset Inicio { get; set; }
        public DateTimeOffset Fim { get; set; }
        public StatusReserva Status { get; set; }
    }
}
