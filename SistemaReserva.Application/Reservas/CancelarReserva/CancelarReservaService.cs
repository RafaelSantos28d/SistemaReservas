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
            if (reserva.UserId != userId && !isAdmin)
                throw new UnauthorizedAccessException("Você não tem permissão para cancelar esta reserva.");

            reserva.Cancelar();
            await _unitOfWork.CommitAync();
        }
    }
}
