using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SistemaReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Infrastructure.EntitiesConfiguration
{
    public class RecursoConfiguration : IEntityTypeConfiguration<Recurso>
    {
        public void Configure(EntityTypeBuilder<Recurso> builder)
        {
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(200);

        }
    }
}
