using AutoMapper;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.UpdateRecurso
{
    public class UpdateRecursoService : IUpdateRecursoService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UpdateRecursoService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> UpdateRecursoAsync(UpdateRecursoRequest request)
        {
            var recurso =  _mapper.Map<Recurso>(request);
            await _unitOfWork.RecursoRepository.UpdateAsync(recurso);
            await _unitOfWork.CommitAync();
            return true;
        }
    }
}
