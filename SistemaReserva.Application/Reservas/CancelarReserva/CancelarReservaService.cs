using SistemReserva.Domain.Enums;
using SistemReserva.Domain.Exceptions;
using SistemReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.CancelarReserva
{
    public class CancelarReservaService : ICancelarReservaService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CancelarReservaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task CancelarReserva(int reservaId, string userId, bool isAdmin)
        {
            var reserva = await _unitOfWork.ReservaRepository.GetReservaByIdAsync(reservaId);
            if(reserva is null)
            {
                throw new NotFoundException("Reserva não encontrada");
            }
            if (reserva.UserId != userId && !isAdmin)
            {
                throw new UnauthorizedAccessException("Você não tem permissão para cancelar esta reserva.");
            }
                
            if (reserva.Status == StatusReserva.Cancelada)
            {
                throw new BadRequestException("Esta reserva já está cancelada.");
            }
            if (reserva.Fim < DateTime.Now)
            {
                throw new BadRequestException("Não é possível cancelar uma reserva que já ocorreu.");
            }
            reserva.Cancelar();
            await _unitOfWork.CommitAync();
        }
    }
}
