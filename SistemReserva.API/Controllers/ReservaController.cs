using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReserva.Application.Reservas.CancelarReserva;
using SistemReserva.Application.Reservas.CreateReserva;
using SistemReserva.Application.Reservas.GetReservaByEmail;
using SistemReserva.Domain.Pagination;
using System.Security.Claims;

namespace SistemReserva.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly ICreateReservaService _service;
        private readonly IGetReservasByIdService _serviceById;
        private readonly ICancelarReservaService _cancelarReserva;
        public ReservaController(ICreateReservaService service, IGetReservasByIdService serviceByEmail, ICancelarReservaService cancelarReserva)
        {
            _service = service;
            _serviceById = serviceByEmail;
            _cancelarReserva = cancelarReserva;
        }

        [HttpPost]
        public async Task<ActionResult<CreateReservaResponse>> CreateReservaAsync(CreateReservaRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var create = await _service.CreateReservaAsync(request,userId);

            return Ok(create);
        }
        [HttpGet("minhas-reservas")]
        public async Task<ActionResult<PagedList<GetReservasByIdResponse>>> GetMinhasReservas(int pageNumber, int pagesize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var reservas = await _serviceById.GetMinhasReservas(userId, pageNumber, pagesize);
            return Ok(reservas);
        }
        [HttpPut("{id}/Cancelar")]
        public async Task<ActionResult>CancelarReserva(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole("Admin");

            await _cancelarReserva.CancelarReserva(id, userId, isAdmin);

            return NoContent();
        }
    }
}
