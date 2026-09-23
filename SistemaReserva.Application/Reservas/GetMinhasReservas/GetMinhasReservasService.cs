using AutoMapper;
using SistemaReserva.Domain.Exceptions;
using SistemaReserva.Domain.Interfaces;
using SistemaReserva.Domain.Pagination;


namespace SistemaReserva.Application.Reservas.GetMinhasReservas
{
    public class GetMinhasReservasService : IGetMinhasReservasService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetMinhasReservasService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        
        public async Task<PagedList<GetMinhasReservasResponse>> GetMinhasReservas(string userId,int pageNumber,int pageSize)
        {
            var reservas = await _unitOfWork.ReservaRepository.GetReservasByUserIdAsync(userId, pageNumber,pageSize);
            if(reservas is null)
            {
                throw new NotFoundException("Nenhuma reserva encontrada");
            }
            var items = reservas.Items.Select(r => new GetMinhasReservasResponse
            {
                ReservaId = r.ReservaId,
                RecursoId = r.RecursoId,
                RecursoName = r.Recurso.Nome,  
                Descricao = r.Descricao,
                Inicio = r.Inicio,
                Fim = r.Fim,
                Status = r.Status
            }).ToList();

            return new PagedList<GetMinhasReservasResponse>(items, pageNumber,pageSize, reservas.TotalCount);
        }
    }
}
