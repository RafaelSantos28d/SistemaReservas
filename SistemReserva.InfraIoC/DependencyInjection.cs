
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SistemaReserva.Application.Common;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemReserva.Application.Auth.Login;
using SistemReserva.Application.Auth.Register;
using SistemReserva.Application.Recursos.DeleteRecurso;
using SistemReserva.Application.Recursos.UpdateRecurso;
using SistemReserva.Application.Reservas.CancelarReserva;
using SistemReserva.Application.Reservas.CreateReserva;
using SistemReserva.Application.Reservas.GetReservaByEmail;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Infrastructure.Context;
using SistemReserva.Infrastructure.Identity;
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
            services.AddIdentityCore<ApplicationUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<BancoContext>()
                    .AddDefaultTokenProviders();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = configuration["Jwt:Issuer"],
                   ValidAudience = configuration["Jwt:Audience"],
                   IssuerSigningKey = new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
               };
           });

            services.AddScoped<IRecursoRepository, RecursoRepository>();
            services.AddScoped<ICreateRecursoService, CreateRecursoService>();
            services.AddScoped<IGetRecursosService, GetRecursosServices>();
            services.AddScoped<IUpdateRecursoService, UpdateRecursoService>();
            services.AddScoped<IDeleteRecursoService, DeleteRecursoService>();

            //Reserva
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ICreateReservaService, CreateReservaService>();
            services.AddScoped<IGetReservasByIdService, GetReservasByIdService>();
            services.AddScoped<ICancelarReservaService, CancelarReservaService>();

            //Auth
            services.AddScoped<IRegisterService, RegisterService>();
            services.AddScoped<ILoginService,LoginService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddAutoMapper(
                cfg => { },
                typeof(DomainMappingProfile).Assembly

            );

            return services;
        }

    }
}
