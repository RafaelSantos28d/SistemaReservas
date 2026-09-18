using AutoMapper;
using Moq;
using Pomelo.EntityFrameworkCore.MySql.Query.ExpressionVisitors.Internal;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Enums;
using SistemaReserva.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Tests.Services.RecursoServices
{
    public class CreateRecursoServiceTests
    {
        private readonly CreateRecursoService _service;
        private readonly Mock<IMapper> _mapper;
        private readonly Mock<IUnitOfWork> _unitOfWork;

        public CreateRecursoServiceTests()
        {
            _mapper = new Mock<IMapper>();
            _unitOfWork = new Mock<IUnitOfWork>();
            _service = new CreateRecursoService(_unitOfWork.Object, _mapper.Object);
        }

        [Fact]
        public async Task CreateRecurso_ShouldCreateRecurso()
        {
            //Arrange
            var recursoRequest = new CreateRecursoRequest()
            {
                Nome = "Sala 02",
                Descricao = "Sala Teste"
            };
            var recursoResponse = new CreateRecursoResponse()
            {
                Nome = recursoRequest.Nome,
                Descricao = recursoRequest.Descricao,
                Ativo = true,
                RecursoId = 1,
                Reservas = null
            };

            var recurso = new Recurso(recursoRequest.Nome, recursoRequest.Descricao, true);
            _unitOfWork.Setup(rep=>rep.RecursoRepository.CreateAsync(It.IsAny<Recurso>())).ReturnsAsync(recurso);
            _mapper.Setup(m=>m.Map<CreateRecursoResponse>(It.IsAny<Recurso>())).Returns(recursoResponse);
            //Act
            var result = await _service.CreateRecursoAsync(recursoRequest);

            //Assert
            Assert.Equal(recursoResponse, result);
            Assert.Equal(recursoRequest.Nome, result.Nome);
            Assert.Equal(recursoRequest.Descricao, result.Descricao);
        }
    }
}
