using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaReserva.API.Controllers;
using SistemaReserva.Application.Reservas.CreateReserva;
using SistemaReserva.Domain.Entities;
using SistemaReserva.Domain.Enums;
using SistemaReserva.Domain.Exceptions;
using SistemaReserva.Domain.Interfaces;
using SistemaReserva.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SistemaReserva.Tests.Services.ReservaService
{
    public class CreateReservaServiceTests
    {

        private readonly CreateReservaService _createReservaService;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
      
        private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
        private readonly Mock<IMapper> _mapper;

        public CreateReservaServiceTests()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(Mock.Of<IUserStore<ApplicationUser>>(), null, null, null,  null, null, null, null, null);
            _mapper = new Mock<IMapper>();
            _createReservaService = new CreateReservaService(_mapper.Object, _unitOfWorkMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task CreateReserva_ShouldCreateReserva()
        {
            var userId = "1";
            var user = new ApplicationUser() { Id= userId,Email = "teste@teste.com" };
            var reservaRequest = new CreateReservaRequest()
            {
                RecursoId = 1,
                Descricao = "Reserva test",
                Inicio = DateTimeOffset.Now.AddDays(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(1)
            };
            var recurso = new Recurso(reservaRequest.RecursoId, "Sala 02", "Sala Teste", true);
            var response = new CreateReservaResponse()
            {
                RecursoId = 1,
                Descricao = reservaRequest.Descricao,
                RecursoName = recurso.Nome,
                ReservaId = 1,
                UserEmail = user.Email,
                Inicio = reservaRequest.Inicio,
                Fim = reservaRequest.Fim,
                Status = Domain.Enums.StatusReserva.Confirmada
            };
            var reserva = new Reserva(reservaRequest.RecursoId, reservaRequest.Descricao,userId,reservaRequest.Inicio, reservaRequest.Fim);
            _unitOfWorkMock
            .Setup(x => x.RecursoRepository.GetByIdAsync(reservaRequest.RecursoId))
            .ReturnsAsync(recurso);

            _unitOfWorkMock
                .Setup(x => x.ReservaRepository
                    .Conflita(reservaRequest.RecursoId, reservaRequest.Inicio, reservaRequest.Fim))
                .ReturnsAsync(false);

            _unitOfWorkMock
                .Setup(x => x.ReservaRepository
                    .CreateReservaAsync(It.IsAny<Reserva>()))
                .ReturnsAsync(reserva);

            _unitOfWorkMock
                .Setup(x => x.CommitAync())
                .Returns(Task.CompletedTask);

            _userManagerMock
                .Setup(x => x.FindByIdAsync(userId))
                .ReturnsAsync(user);

            _mapper
                .Setup(x => x.Map<CreateReservaResponse>(It.IsAny<Reserva>()))
                .Returns(response);

            // Act

            var result = await _createReservaService.CreateReservaAsync(reservaRequest, userId);

            // Assert

            Assert.NotNull(result);
            Assert.Equal(reservaRequest.RecursoId, result.RecursoId);
            Assert.Equal(reservaRequest.Descricao, result.Descricao);
            Assert.Equal(recurso.Nome, result.RecursoName);
            Assert.Equal(user.Email, result.UserEmail);
            Assert.Equal(StatusReserva.Confirmada, result.Status);


        }
        [Fact]
        public async Task CreateReserva_ShouldThrowBadRequestException_ComConflict()
        {
            // Arrange

            var userId = "1";

            var reservaRequest = new CreateReservaRequest
            {
                RecursoId = 1,
                Descricao = "Reserva test",
                Inicio = DateTimeOffset.Now.AddDays(1).AddHours(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(2)
            };

            var recurso = new Recurso(
                reservaRequest.RecursoId,
                "Sala 02",
                "Sala Teste",
                true
            );

            _unitOfWorkMock
                .Setup(x => x.RecursoRepository.GetByIdAsync(reservaRequest.RecursoId))
                .ReturnsAsync(recurso);

            _unitOfWorkMock
                .Setup(x => x.ReservaRepository
                    .Conflita(reservaRequest.RecursoId, reservaRequest.Inicio, reservaRequest.Fim))
                .ReturnsAsync(true);

            // Act

            var result = () => _createReservaService.CreateReservaAsync(reservaRequest, userId);

            // Assert

            await Assert.ThrowsAsync<BadRequestException>(result);
        }
        [Fact]
        public async Task CreateReserva_ShouldThrowBadRequestException_ComRecursoInativo()
        {
            var userId = "1";
            var request = new CreateReservaRequest
            {
                RecursoId = 1,
                Descricao = "Reserva test",
                Inicio = DateTimeOffset.Now.AddDays(1).AddHours(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(2)
            };

            var recurso = new Recurso(
                request.RecursoId,
                "Sala 02",
                "Sala Teste",
                false
            );
            _unitOfWorkMock
                .Setup(x => x.RecursoRepository.GetByIdAsync(request.RecursoId))
                .ReturnsAsync(recurso);
            _unitOfWorkMock
               .Setup(x => x.ReservaRepository
                   .Conflita(request.RecursoId, request.Inicio, request.Fim))
               .ReturnsAsync(true);

            //Act
            var exception = await Assert.ThrowsAsync<BadRequestException>(
               () => _createReservaService.CreateReservaAsync(request, userId));

            //Assert
            Assert.Equal(
              "Recurso inválido ou inativo.",
              exception.Message
          );
        }

        [Fact]
        public async Task CreateReservaAsync_ComRecursoInexistente_DeveLancarBadRequestException()
        {
            //Arrange

            var reservaRequest = new CreateReservaRequest
            {
                RecursoId = 1,
                Descricao = "Reserva test",
                Inicio = DateTimeOffset.Now.AddDays(1).AddHours(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(2)
            };
            _unitOfWorkMock.Setup(r => r.RecursoRepository.GetByIdAsync(reservaRequest.RecursoId)).ReturnsAsync((Recurso)null!);

            //Act
            var act = () => _createReservaService.CreateReservaAsync(reservaRequest, "user-1");

            //Assert
            await Assert.ThrowsAsync<BadRequestException>(act);
            _unitOfWorkMock.Verify(r => r.ReservaRepository.CreateReservaAsync(It.IsAny<Reserva>()), Times.Never);
        }


    }
}
