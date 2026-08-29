using AutoMapper;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.CreateRecurso
{
    public class CreateRecursoService : ICreateRecursoService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unityOfWork;

        public CreateRecursoService(IUnitOfWork unityOfWork, IMapper mapper)
        {
            _unityOfWork = unityOfWork;
            _mapper = mapper;
        }

        public async Task<CreateRecursoResponse> CreateRecursoAsync(CreateRecursoRequest request)
        {
            var create = new Recurso(request.Nome, request.Descricao, ativo: true);
            var created = await _unityOfWork.RecursoRepository.CreateAsync(create);
            await _unityOfWork.CommitAync();
            return _mapper.Map<CreateRecursoResponse>(created);
        }
    }
}
