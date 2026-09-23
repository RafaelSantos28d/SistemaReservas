using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaReserva.API.Controllers;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.DeleteRecurso;
using SistemaReserva.Application.Recursos.GetRecursoById;
using SistemaReserva.Application.Recursos.GetRecursos;
using SistemaReserva.Application.Recursos.UpdateRecurso;
using SistemaReserva.Domain.Pagination;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Tests.Controllers
{
    public class RecursoControllerTests
    {
        private readonly Mock<ICreateRecursoService> _createService;
        private readonly Mock<IGetRecursosService> _getRecursosService;
        private readonly Mock<IUpdateRecursoService> _updateRecursoService;
        private readonly Mock<IDeleteRecursoService> _deleteRecursoService;
        private readonly Mock<IGetRecursoByIdService> _getRecursoByIdService;

        private readonly RecursoController _recursoControllerMock;

        public RecursoControllerTests()
        {
            _createService = new Mock<ICreateRecursoService>();
            _getRecursosService = new Mock<IGetRecursosService>();
            _updateRecursoService = new Mock<IUpdateRecursoService>();
            _deleteRecursoService = new Mock<IDeleteRecursoService>();
            _getRecursoByIdService = new Mock<IGetRecursoByIdService>();
            _recursoControllerMock = new RecursoController(_createService.Object,
                                                           _getRecursosService.Object, 
                                                           _updateRecursoService.Object, 
                                                           _deleteRecursoService.Object,
                                                           _getRecursoByIdService.Object);
        }
        [Fact]
        public async Task CreateRecurso_ShouldReturn201Created()
        {
            //Arrange
            var createRecurso = new CreateRecursoRequest()
            {
                Nome = "Quadra 2",
                Descricao = "Quadra segundo andar"
            };
            var expectedResult = new CreateRecursoResponse()
            {
                RecursoId = 1,
                Nome = "Quadra 2",
                Descricao = "Quadra segundo andar",
                Ativo = true,
                Reservas = null
            };

            _createService.Setup(s => s.CreateRecursoAsync(createRecurso)).ReturnsAsync(expectedResult);

            //Act
            var result = await _recursoControllerMock.CreateRecursoAsync(createRecurso);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Equal(nameof(_recursoControllerMock.GetRecursoByIdAsync), createdResult.ActionName);
            Assert.Equal(expectedResult.RecursoId, createdResult.RouteValues["id"]);

            var actualValue = Assert.IsType<CreateRecursoResponse>(createdResult.Value);
            Assert.Equal(expectedResult, actualValue);

            _createService.Verify(s => s.CreateRecursoAsync(createRecurso),Times.Once());
        }

        [Fact]
        public async Task GetRecurso_ShouldReturnOk()
        {
            //Assert
            var recurso1 = new GetRecursosResponse()
            {
                RecursoId = 1,
                Nome = "Quadra 2",
                Descricao = "Quadra segundo andar",
                Ativo = true,
                Reservas = null
            };
            var recurso2 = new GetRecursosResponse()
            {
                RecursoId = 2,
                Nome = "Quadra 3",
                Descricao = "Quadra terceiro andar",
                Ativo = true,
                Reservas = null
            };
            var expectedResult = new PagedList<GetRecursosResponse>(new[] { recurso1, recurso2 }, 1, 1, 2);

            _getRecursosService.Setup(s=>s.GetRecursosAsync(1,2)).ReturnsAsync(expectedResult);

            //Act
            var result = await _recursoControllerMock.Recursos(1, 2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<PagedList<GetRecursosResponse>>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _getRecursosService.Verify(s => s.GetRecursosAsync(1, 2), Times.Once);

        }
        [Fact]
        public async Task GetRecursoById_ShouldReturnOK()
        {
            //Assert
            var expectedResult = new GetRecursoByIdResponse()
            {
                Nome = "Quadra 2",
                Descricao = "Quadra segundo andar",
                Ativo = true,
            };

            _getRecursoByIdService.Setup(s => s.GetRecursoByIdAsync(1)).ReturnsAsync(expectedResult);

            //Act
            var result = await _recursoControllerMock.GetRecursoByIdAsync(1);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<GetRecursoByIdResponse>(okResult.Value);

            Assert.Equal(actualValue, expectedResult);
            _getRecursoByIdService.Verify(s=>s.GetRecursoByIdAsync(1), Times.Once);

        }

        [Fact]
        public async Task DeleteRecurso_ShouldReturnNoContent()
        {
            //Arrange
            _deleteRecursoService.Setup(s => s.DeleteRecurso(1)).ReturnsAsync(true);

            //Act
            var result = await _recursoControllerMock.DeleteRecursoAsync(1);

            //Assert
            Assert.IsType<NoContentResult>(result);

            _deleteRecursoService.Verify(s => s.DeleteRecurso(1), Times.Once);

        }

        [Fact]
        public async Task UpdateRecurso_ShouldReturnOK()
        {
            //Arrange
            var updateRequest = new UpdateRecursoRequest()
            {
                Nome = "Quadra 2",
                Descricao = "Quadra segundo andar",
                Ativo = true
            };

            _updateRecursoService.Setup(s=>s.UpdateRecursoAsync(1, updateRequest)).ReturnsAsync(true);

            //Act
            var result = await _recursoControllerMock.UpdateRecursoAsync(1, updateRequest);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<bool>(okResult.Value);

            Assert.Equal(actualValue, true);
            _updateRecursoService.Verify(s => s.UpdateRecursoAsync(1,updateRequest), Times.Once);


        }
    }
}
