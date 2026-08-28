using AutoMapper;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemReserva.Application.Recursos.UpdateRecurso;
using SistemReserva.Domain.Entities;
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
        }
    }
}
