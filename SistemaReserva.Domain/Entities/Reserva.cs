using SistemaReserva.Domain.Enums;
using SistemaReserva.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Domain.Entities
{
    public class Reserva
    {
        public Reserva(int recursoId, string? descricao, string userId, DateTimeOffset inicio, DateTimeOffset fim)
        {
            Validation(recursoId, descricao, userId, inicio, fim);
        }
        public Reserva(int reservaId,int recursoId, string? descricao, string userId, DateTimeOffset inicio, DateTimeOffset fim,StatusReserva status)
        {
            DomainValidationException.When(reservaId < 0, "O id do recurso é obrigatório");
            ReservaId = reservaId;
            Validation(recursoId, descricao, userId, inicio, fim);
            Status = status;
        }
        public Reserva()
        {
            
        }
        public int ReservaId { get; private set; }
        public int RecursoId { get; private set; }
        public Recurso? Recurso { get; private set; }
        public string? Descricao { get; private set; }
        public string UserId { get; private set; }
        public ApplicationUser? User { get; private set; }
        public DateTimeOffset Inicio { get; private set; }
        public DateTimeOffset Fim { get; private set; }
        public StatusReserva Status { get; private set; }

        public void Update(string descricao, DateTimeOffset inicio, DateTimeOffset fim, StatusReserva status)
        {
           
            Descricao = descricao;
            Inicio = inicio;
            Fim = fim;
            Status = status;
        }

        public void Validation( int recursoId, string descricao, string userId, DateTimeOffset inicio, DateTimeOffset fim)
        {
            DomainValidationException.When(string.IsNullOrEmpty(userId), "Id do usuário é obrigatório");
            DomainValidationException.When(inicio <= DateTimeOffset.UtcNow, "A data de início deve ser futura");
            DomainValidationException.When(inicio >= fim, "A data de início deve ser anterior à data de fim");
            DomainValidationException.When(recursoId <= 0, "O id do recurso é obrigatório");
            RecursoId= recursoId;
            Descricao= descricao;
            UserId = userId;
            Inicio = inicio;
            Fim = fim;
             
        }
        public void Cancelar()
        {
            Status = StatusReserva.Cancelada;
        }
    }
}
