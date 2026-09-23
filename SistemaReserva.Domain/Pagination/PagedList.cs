using SistemaReserva.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SistemaReserva.Domain.Pagination
{
    public class PagedList<T>
    {
        public PagedList(IEnumerable<T> items, int currentPage, int pageSize, int totalCount)
        {
            Validation(currentPage, pageSize);
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
        }
        
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; } = [];

        public void Validation(int currentPage, int pageSize)
        {
            DomainValidationException.When(currentPage < 1, "A página deve ser maior que 0");
            DomainValidationException.When( pageSize <1 || pageSize > 50, "O tamanho da página deve estar entre 1 e 50");
        }
    }
}
