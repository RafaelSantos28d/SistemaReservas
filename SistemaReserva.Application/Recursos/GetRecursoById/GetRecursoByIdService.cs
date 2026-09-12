using AutoMapper;
using SistemaReserva.Application.Reservas.GetAllReservas;
using SistemaReserva.Domain.Exceptions;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursoById
{
    public class GetRecursoByIdService :IGetRecursoByIdService
    {
        private readonly IRecursoRepository _recursoRepository;
        private readonly IMapper _mapper;

        public GetRecursoByIdService(IRecursoRepository recursoRepository, IMapper mapper)
        {
            _recursoRepository = recursoRepository;
            _mapper = mapper;
        }

        public async Task<GetRecursoByIdResponse> GetRecursoById(int recursoId)
        {
            var recurso = await _recursoRepository.GetByIdAsync(recursoId);

            if (recurso == null)
            {
                throw new NotFoundException("Recurso não encontrado.");
            }

            return _mapper.Map<GetRecursoByIdResponse>(recurso);

        }
    }
}
