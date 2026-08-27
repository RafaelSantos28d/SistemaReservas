using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Infrastructure.Context;
using SistemReserva.Infrastructure.Repositories;
using SistemaReserva.Application.Common;
using System;
using System.Collections.Generic;
using System.Text;
using SistemaReserva.Application.Recursos.ListRecursos;

namespace SistemReserva.InfraIoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BancoContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(
                    configuration.GetConnectionString("DefaultConnection")),

                b => b.MigrationsAssembly(typeof(BancoContext).Assembly.FullName)
                ));



            services.AddScoped<IRecursoRepository, RecursoRepository>();
            services.AddScoped<ICreateRecursoService, CreateRecursoService>();
            services.AddScoped<IGetRecursosService, GetRecursosServices>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(
                cfg => { },
                typeof(DomainMappingProfile).Assembly

            );

            return services;
        }

    }
}
