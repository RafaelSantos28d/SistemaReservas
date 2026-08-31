using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReserva.Application.Reservas.CancelarReserva;
using SistemReserva.Application.Reservas.CreateReserva;
using SistemReserva.Application.Reservas.GetAllReservas;
using SistemReserva.Application.Reservas.GetReservaByEmail;
using SistemReserva.Domain.Constants;
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
        private readonly IGetAllReservasService _getAllReservasService;
        public ReservaController(ICreateReservaService service, IGetReservasByIdService serviceByEmail, ICancelarReservaService cancelarReserva, IGetAllReservasService getAllReservasService)
        {
            _service = service;
            _serviceById = serviceByEmail;
            _cancelarReserva = cancelarReserva;
            _getAllReservasService = getAllReservasService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateReservaResponse>> CreateReservaAsync(CreateReservaRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var create = await _service.CreateReservaAsync(request,userId);

            return Ok(create);
        }
        [HttpGet("reservas")]
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
        [HttpGet]
        [Authorize(Roles = Roles.Admin)]
        public async Task<ActionResult<PagedList<GetAllReservasResponse>>> GetAll([FromQuery] int? recursoId, [FromQuery] string? userId, int page = 1, int pageSize = 10)
        {
            var response = await _getAllReservasService.GetAllReservasAsync(recursoId, userId, page, pageSize);
            return Ok(response);
        }
    }
}
