using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.EntitiesConfiguration
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.Property(x => x.Fim).IsRequired();
            builder.Property(x => x.Inicio).IsRequired();
            builder.Property(x => x.UserId).IsRequired();
            builder.HasOne(x => x.User).WithMany(x => x.Reservas).HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Recurso).WithMany(x => x.Reservas).HasForeignKey(x => x.RecursoId).OnDelete(DeleteBehavior.Restrict);
        }
    }

}
