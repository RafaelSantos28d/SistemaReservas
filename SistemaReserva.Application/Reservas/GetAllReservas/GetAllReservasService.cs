using SistemReserva.Domain.Interfaces;
using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemReserva.Application.Reservas.GetAllReservas
{
    public class GetAllReservasService : IGetAllReservasService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllReservasService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedList<GetAllReservasResponse>> GetAllReservasAsync(int? recursoId, string? userId, int currentPage, int pageSize)
        {
            var reservas = await _unitOfWork.ReservaRepository
                .GetAllComFiltroAsync(recursoId, userId, currentPage, pageSize);
            var items = reservas.Items.Select(r => new GetAllReservasResponse
            {
                ReservaId = r.ReservaId,
                RecursoId = r.RecursoId,
                RecursoName = r.Recurso.Nome,
                Descricao = r.Descricao,
                UserId = r.UserId,
                UserEmail = r.User.Email,
                Inicio = r.Inicio,
                Fim = r.Fim,
                Status = r.Status
            }).ToList();

            return new PagedList<GetAllReservasResponse>(items, currentPage, pageSize, reservas.TotalCount);
        }
    }
}
