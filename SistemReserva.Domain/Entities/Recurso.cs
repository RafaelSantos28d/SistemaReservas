using SistemReserva.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Domain.Entities
{
    public class Recurso
    {
        public int RecursoId { get; private set; }
        public string Nome { get; private set; }
        public string? Descricao { get; private set; }
        public bool Ativo { get; private set; }
        public ICollection<Reserva> Reservas { get; private set; }
        public Recurso(int recursoId, string nome, string descricao, bool ativo)
        {
            DomainValidationException.When(recursoId < 0, "O Id deve ser positivo");
            RecursoId = recursoId;
            Validation( nome, descricao, ativo);
        }
        public Recurso( string nome, string descricao, bool ativo)
        {
            Validation(nome, descricao, ativo);
        }
        public Recurso()
        {
        }
        public void Update(int id,string nome, string descricao, bool ativo)
        {
            RecursoId = id;
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }
        public void Validation( string nome, string descricao, bool ativo)
        {
            
            DomainValidationException.When(nome.Length > 250, "Nome digitado inválido");
            DomainValidationException.When(string.IsNullOrEmpty(nome), "O nome é obrigatório");

            
            Nome = nome;
            Descricao = descricao;
            Ativo = ativo;
        }
    }
}
