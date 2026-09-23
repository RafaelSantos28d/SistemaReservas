using AutoMapper;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Interfaces;
using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Application.Recursos.GetRecursos
{
    public class GetRecursosService : IGetRecursosService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetRecursosService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedList<GetRecursosResponse>> GetRecursosAsync(int currentPage, int pageSize)
        {
            var recursos = await _unitOfWork.RecursoRepository.GetAllRecursoAsync(currentPage, pageSize);

            var response = _mapper.Map<List<GetRecursosResponse>>(recursos.Items);
            return new PagedList<GetRecursosResponse>(response, currentPage ,pageSize,recursos.TotalCount);
        }
    }
}
