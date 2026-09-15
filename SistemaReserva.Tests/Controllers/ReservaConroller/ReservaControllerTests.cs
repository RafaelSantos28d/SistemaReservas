using Moq;
using SistemaReserva.API.Controllers;
using SistemaReserva.Application.Reservas.CreateReserva;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaReserva.Tests.Controllers.ReservaConroller
{
    
    public class ReservaControllerTests
    {
        private readonly Mock<ICreateReservaService> _createReservaService;
        private readonly Mock<ReservaController> _reservaController;

        public ReservaControllerTests(Mock<ICreateReservaService> createReservaService, Mock<ReservaController> reservaController)
        {
            _createReservaService = createReservaService;
            _reservaController = reservaController;
        }

       
    }
}
