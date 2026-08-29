using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Enums;
using SistemReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.CreateReserva
{
    public class CreateReservaService : ICreateReservaService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        public CreateReservaService(IMapper mapper, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<CreateReservaResponse> CreateReservaAsync(CreateReservaRequest request,string userId)
        {
            var recurso = await _unitOfWork.RecursoRepository.GetByIdAsync(request.RecursoId);
            if (recurso is null || !recurso.Ativo)
                throw new InvalidOperationException("Recurso inválido ou inativo.");
            var conflita = await _unitOfWork.ReservaRepository
                .Conflita(request.RecursoId, request.Inicio, request.Fim);
            if (conflita)
                throw new InvalidOperationException("Recurso já reservado nesse horário.");

            var reserva = new Reserva(
                    request.RecursoId,
                    request.Descricao,
                    userId,
                    request.Inicio,
                    request.Fim
                    );


            var create = await _unitOfWork.ReservaRepository.CreateReservaAsync(reserva);
            await _unitOfWork.CommitAync();

            var usuario = await _userManager.FindByIdAsync(userId);

            var response = _mapper.Map<CreateReservaResponse>(create);
            response.RecursoName = recurso.Nome;
            response.UserEmail = usuario.Email;
            return response;
        }
    }
}
