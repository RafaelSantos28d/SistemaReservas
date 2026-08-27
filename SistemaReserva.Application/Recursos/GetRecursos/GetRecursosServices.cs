using AutoMapper;
using SistemReserva.Domain.Entities;
using SistemReserva.Domain.Interfaces;
using SistemReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.ListRecursos
{
    public class GetRecursosServices : IGetRecursosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRecursosServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<GetRecursoResponse>> GetListRecursosAsync(int currentPage, int pageSize)
        {
            var recursos = await _unitOfWork.RecursoRepository.GetAllRecursoAsync(currentPage, pageSize);

            var response = _mapper.Map<List<GetRecursoResponse>>(recursos.Items);
            return new PagedList<GetRecursoResponse>(response, currentPage ,pageSize,recursos.TotalCount);
        }
    }
}
