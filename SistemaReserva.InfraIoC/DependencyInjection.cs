
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SistemaReserva.Application.Common;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemaReserva.Application.Auth.Login;
using SistemaReserva.Application.Auth.Register;
using SistemaReserva.Application.Recursos.DeleteRecurso;
using SistemaReserva.Application.Recursos.GetRecursoById;
using SistemaReserva.Application.Recursos.UpdateRecurso;
using SistemaReserva.Application.Reservas.CancelarReserva;
using SistemaReserva.Application.Reservas.CreateReserva;
using SistemaReserva.Application.Reservas.GetAllReservas;
using SistemaReserva.Application.Reservas.GetReservaById;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Interfaces;
using SistemaReserva.Infrastructure.Context;
using SistemaReserva.Infrastructure.Identity;
using SistemaReserva.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;


namespace SistemaReserva.InfraIoC
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
            services.AddScoped<IGetRecursoByIdService, GetRecursoByIdService>();

            //Reserva
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<ICreateReservaService, CreateReservaService>();
            services.AddScoped<IGetReservasByIdService, GetReservasByIdService>();
            services.AddScoped<ICancelarReservaService, CancelarReservaService>();
            services.AddScoped<IGetAllReservasService, GetAllReservasService>();
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
