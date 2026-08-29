using SistemReserva.Domain.Enums;
using SistemReserva.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Entities
{
    public class Reserva
    {
        public Reserva(int recursoId, string? descricao, string userId, DateTime inicio, DateTime fim)
        {
            Validation(recursoId, descricao, userId, inicio, fim);
        }
        public Reserva(int reservaId,int recursoId, string? descricao, string userId, DateTime inicio, DateTime fim,StatusReserva status)
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
        public DateTime Inicio { get; private set; }
        public DateTime Fim { get; private set; }
        public StatusReserva Status { get; private set; }

        public void Update(int reservaId,int recursoId,string descricao,string userId, DateTime inicio, DateTime fim, StatusReserva status)
        {
            ReservaId = reservaId; 
            RecursoId = recursoId;
            Descricao = descricao;
            UserId = userId;
            Inicio = inicio;
            Fim = fim;
            Status = status;
        }

        public void Validation( int recursoId, string descricao, string userId, DateTime inicio, DateTime fim)
        {
            DomainValidationException.When(string.IsNullOrEmpty(userId), "Id do usuário é obrigatório");
            DomainValidationException.When(inicio < DateTime.Now, "Data inválida");
            DomainValidationException.When(inicio > fim, "Data inválida");
            DomainValidationException.When(recursoId < 0, "O id do recurso é obrigatório");
            RecursoId= recursoId;
            Descricao= descricao;
            UserId = userId;
            Inicio = inicio;
            Fim = fim;
             
        }
    }
}
