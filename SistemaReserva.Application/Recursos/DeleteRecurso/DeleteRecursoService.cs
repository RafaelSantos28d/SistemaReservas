using SistemReserva.Domain.Exceptions;
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
                throw new NotFoundException("Recurso não encontrado");
            }
            var possuiReservas = await _unitOfWork.RecursoRepository.PossuiReservasAsync(id);
            if(possuiReservas)
            {
                throw new BadRequestException("Não é possível excluir um recurso com reservas vinculadas.");
            }
            await _unitOfWork.RecursoRepository.RemoveAsync(recurso);
            await _unitOfWork.CommitAync();
            return true;
        }
    }
}
