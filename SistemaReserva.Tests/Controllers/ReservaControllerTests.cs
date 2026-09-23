using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SistemaReserva.API.Controllers;
using SistemaReserva.Application.Reservas.CancelarReserva;
using SistemaReserva.Application.Reservas.CreateReserva;
using SistemaReserva.Application.Reservas.GetAllReservas;
using SistemaReserva.Application.Reservas.GetMinhasReservas;
using SistemaReserva.Domain.Pagination;
using System.Security.Claims;


namespace SistemaReserva.Tests.Controllers
{
    
    public class ReservaControllerTests
    {
        private readonly Mock<ICreateReservaService> _createReservaService;
        private readonly Mock<ICancelarReservaService> _cancelarReservaService;
        private readonly Mock<IGetAllReservasService> _getAllReservasService;
        private readonly Mock<IGetMinhasReservasService> _getByIdReservaService;
        private readonly ReservaController _reservaController;

        public ReservaControllerTests()
        {
            _createReservaService = new Mock<ICreateReservaService>();
            _cancelarReservaService = new Mock<ICancelarReservaService>();
            _getAllReservasService = new Mock<IGetAllReservasService>();
            _getByIdReservaService = new Mock<IGetMinhasReservasService>();
            _reservaController = new ReservaController(_createReservaService.Object,
                                                       _getByIdReservaService.Object,
                                                       _cancelarReservaService.Object,
                                                       _getAllReservasService.Object);
        }

        private static ClaimsPrincipal CriarUsuarioFake(string userId,bool isAdmin)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, userId) };
            if (isAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }
            var identity = new ClaimsIdentity(claims, "TestAuth");
            return new ClaimsPrincipal(identity);
        }
        

        [Fact]
        public async Task CreateReserva_ShouldReturnOK()
        {
            string userId = "1";
            var createReseva = new CreateReservaRequest()
            {
                
                RecursoId = 1,
                Descricao = "",
                Inicio = DateTimeOffset.UtcNow.AddDays(1),
                Fim = DateTimeOffset.UtcNow.AddDays(1).AddHours(1)
            };

            var expectedResult = new CreateReservaResponse()
            {
                ReservaId = 1,
                RecursoId = 1,
                RecursoName = "Sala 01",
                Descricao = "",
                UserEmail = "rafael@gmail.com",
                Inicio = DateTimeOffset.UtcNow.AddDays(1),
                Fim = DateTimeOffset.UtcNow.AddDays(1).AddHours(1),
                Status = Domain.Enums.StatusReserva.Confirmada
            };

            _reservaController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = CriarUsuarioFake(userId,isAdmin:true) }
            };
            _createReservaService.Setup(x => x.CreateReservaAsync(createReseva, userId)).ReturnsAsync(expectedResult);

            //Act
            var result = await _reservaController.CreateReservaAsync(createReseva);

            //Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);

            Assert.Equal(nameof(_reservaController.GetMinhasReservas),createdResult.ActionName);
            Assert.Equal(userId, createdResult.RouteValues["id"]);

            var actualValue = Assert.IsType<CreateReservaResponse>(createdResult.Value);
            Assert.Equal(expectedResult,actualValue);

            _createReservaService.Verify(s=> s.CreateReservaAsync(createReseva, userId), Times.Once());
        }

        [Fact]
        public async Task GetMinhasReservas_ShouldReturnOk()
        {
            //Arrange
            var userId = "1";
            var reserva1 = new GetMinhasReservasResponse()
            {
                ReservaId = 1,
                RecursoId = 1,
                Descricao = "Reserva teste",
                RecursoName = "Sala teste",
                Inicio = DateTimeOffset.Now.AddDays(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(1),
                Status = Domain.Enums.StatusReserva.Confirmada
            };
            var reserva2 = new GetMinhasReservasResponse()
            {
                ReservaId = 1,
                RecursoId = 1,
                Descricao = "Reserva teste",
                RecursoName = "Sala teste",
                Inicio = DateTimeOffset.Now.AddDays(1).AddHours(2),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(3),
                Status = Domain.Enums.StatusReserva.Confirmada
            };
            var expectedResult = new PagedList<GetMinhasReservasResponse>(new[] {reserva1, reserva2 },1,2,2);
            _reservaController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = CriarUsuarioFake(userId,isAdmin:true) }
            };

            _getByIdReservaService.Setup(s=>s.GetMinhasReservas(userId,1,2)).ReturnsAsync(expectedResult);

            //Act
            var result = await _reservaController.GetMinhasReservas(1, 2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<PagedList<GetMinhasReservasResponse>>(okResult.Value);

            Assert.Equal(expectedResult,actualValue);
            _getByIdReservaService.Verify(s=>s.GetMinhasReservas(userId,1,2), Times.Once());
        }
        [Fact]
        public async Task CancelarReserva_ShouldReturnNoContent()
        {
            //Arrange
            _cancelarReservaService.Setup(s => s.CancelarReserva(1, "1", true));
            _reservaController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = CriarUsuarioFake("1", isAdmin: true) }
            };
            //Act
            var result = await _reservaController.CancelarReserva(1);

            //Asser
            Assert.IsType<NoContentResult>(result);

            _cancelarReservaService.Verify(s => s.CancelarReserva(1, "1", true), Times.Once());
        }
        [Fact]
        public async Task GetAll_ShouldReturnOk()
        {
            var userId = "1";
            var reserva1 = new GetAllReservasResponse()
            {
                ReservaId = 1,
                RecursoId = 1,
                Descricao = "Reserva teste",
                RecursoName = "Sala teste",
                Inicio = DateTimeOffset.Now.AddDays(1),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(1),
                Status = Domain.Enums.StatusReserva.Confirmada
            };
            var reserva2 = new GetAllReservasResponse()
            {
                ReservaId = 1,
                RecursoId = 1,
                Descricao = "Reserva teste",
                RecursoName = "Sala teste",
                Inicio = DateTimeOffset.Now.AddDays(1).AddHours(2),
                Fim = DateTimeOffset.Now.AddDays(1).AddHours(3),
                Status = Domain.Enums.StatusReserva.Confirmada
            };
            var expectedResult = new PagedList<GetAllReservasResponse>(new[] { reserva1, reserva2 },1,2,2);
            _reservaController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = CriarUsuarioFake(userId, isAdmin: true) }
            };

            _getAllReservasService.Setup(s=>s.GetAllReservasAsync(1,userId,1,2)).ReturnsAsync(expectedResult);

            //Act
            var result = await _reservaController.GetAll(1,userId,1,2);

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualValue = Assert.IsType<PagedList<GetAllReservasResponse>>(okResult.Value);

            Assert.Equal(expectedResult, actualValue);
            _getAllReservasService.Verify(S=>S.GetAllReservasAsync(1, userId,1,2), Times.Once());

        }
    }
}
