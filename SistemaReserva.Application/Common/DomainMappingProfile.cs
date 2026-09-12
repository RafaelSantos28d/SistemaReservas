using AutoMapper;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemaReserva.Application.Recursos.GetRecursoById;
using SistemaReserva.Application.Recursos.UpdateRecurso;
using SistemaReserva.Application.Reservas.CreateReserva;
using SistemaReserva.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Common
{
    public class DomainMappingProfile :Profile
    {
        public DomainMappingProfile()
        {
            CreateMap<CreateRecursoRequest, Recurso>().ReverseMap();
            CreateMap<CreateRecursoResponse, Recurso>().ReverseMap();
            CreateMap<GetRecursoResponse, Recurso>().ReverseMap();
            CreateMap<UpdateRecursoRequest,Recurso>().ReverseMap();
            CreateMap<GetRecursoByIdResponse, Recurso>().ReverseMap();


            CreateMap<CreateReservaRequest, Reserva>().ReverseMap();
            CreateMap<CreateReservaResponse, Reserva>().ReverseMap();

            


        }
    }
}
