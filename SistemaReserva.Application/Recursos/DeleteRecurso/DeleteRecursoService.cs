using SistemReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Recursos.DeleteRecurso
{
    public class DeleteRecursoService : IDeleteRecursoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteRecursoAsync(int id)
        {
            var recurso = await _unitOfWork.RecursoRepository.GetByIdAsync(id);
            if (recurso == null)
            {
                throw new Exception("Recurso não encontrado");
            }
            await _unitOfWork.RecursoRepository.RemoveAsync(recurso);
            await _unitOfWork.CommitAync();
            return true;
        }
    }
}
