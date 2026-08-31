using AutoMapper;
using SistemReserva.Domain.Exceptions;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.GetReservaByEmail
{
    public class GetReservasByIdService : IGetReservasByIdService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetReservasByIdService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedList<GetReservasByIdResponse>> GetMinhasReservas(string userId,int pageNumber,int pageSize)
        {
            var reservas = await _unitOfWork.ReservaRepository.GetReservasById(userId, pageNumber,pageSize);
            if(reservas is null)
            {
                throw new NotFoundException("Nenhuma reserva encontrada");
            }
            var items = reservas.Items.Select(r => new GetReservasByIdResponse
            {
                ReservaId = r.ReservaId,
                RecursoId = r.RecursoId,
                RecursoName = r.Recurso.Nome,  
                Descricao = r.Descricao,
                Inicio = r.Inicio,
                Fim = r.Fim,
                Status = r.Status
            }).ToList();

            return new PagedList<GetReservasByIdResponse>(items, pageNumber,pageSize, reservas.TotalCount);
        }
    }
}
