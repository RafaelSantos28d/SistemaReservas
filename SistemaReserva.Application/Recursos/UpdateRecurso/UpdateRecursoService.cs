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
        public UpdateRecursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateRecursoAsync(int id,UpdateRecursoRequest request)
        {
            
            var recurso =  await _unitOfWork.RecursoRepository.GetByIdAsync(id);
            recurso.Update(request.Nome, request.Descricao,request.Ativo);
           
            await _unitOfWork.CommitAync();
            return true;
        }
    }
}
