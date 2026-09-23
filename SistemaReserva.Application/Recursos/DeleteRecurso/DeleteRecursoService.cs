using SistemaReserva.Domain.Exceptions;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.DeleteRecurso
{
    public class DeleteRecursoService : IDeleteRecursoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DeleteRecurso(int id)
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
            await _unitOfWork.RecursoRepository.Remove(recurso);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
