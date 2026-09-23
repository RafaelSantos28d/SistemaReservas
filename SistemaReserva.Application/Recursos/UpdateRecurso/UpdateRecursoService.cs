using AutoMapper;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Exceptions;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.UpdateRecurso
{
    public class UpdateRecursoService : IUpdateRecursoService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpdateRecursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateRecursoAsync(int id,UpdateRecursoRequest request)
        {
            
            var recurso =  await _unitOfWork.RecursoRepository.GetByIdAsync(id);
            if(recurso == null)
            {
                throw new NotFoundException("Recurso não encontrado");
            }
            recurso.Update(request.Nome, request.Descricao,request.Ativo);
           
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
