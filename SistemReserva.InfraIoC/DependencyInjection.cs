using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SistemaReserva.Application.Common;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemReserva.Application.Recursos.DeleteRecurso;
using SistemReserva.Application.Recursos.UpdateRecurso;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Infrastructure.Context;
using SistemReserva.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

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
            services.AddScoped<IUpdateRecursoService, UpdateRecursoService>();
            services.AddScoped<IDeleteRecursoService, DeleteRecursoService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(
                cfg => { },
                typeof(DomainMappingProfile).Assembly

            );

            return services;
        }

    }
}
