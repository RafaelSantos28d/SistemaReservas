using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SistemReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Infrastructure.Context
{
    public class BancoContext:IdentityDbContext<ApplicationUser>
    {
        public BancoContext(DbContextOptions<BancoContext>options):base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BancoContext).Assembly);
        }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
    }
}
