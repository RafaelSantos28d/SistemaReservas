using AutoMapper;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.CreateRecurso
{
    public class CreateRecursoService : ICreateRecursoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecursoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateRecursoResponse> CreateRecursoAsync(CreateRecursoRequest request)
        {
            var create = new Recurso(request.Nome, request.Descricao, ativo: true);
            var created = await _unitOfWork.RecursoRepository.CreateAsync(create);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CreateRecursoResponse>(created);
        }
    }
}
