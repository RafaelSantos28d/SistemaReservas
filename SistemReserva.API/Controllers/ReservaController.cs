using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReserva.Application.Reservas.CreateReserva;
using SistemReserva.Application.Reservas.GetReservaByEmail;
using SistemReserva.Domain.Pagination;
using System.Security.Claims;

namespace SistemReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly ICreateReservaService _service;
        private readonly IGetReservasByIdService _serviceByEmail;

        public ReservaController(ICreateReservaService service, IGetReservasByIdService serviceByEmail)
        {
            _service = service;
            _serviceByEmail = serviceByEmail;
        }

        [HttpPost]
        public async Task<ActionResult<CreateReservaResponse>> CreateReservaAsync(CreateReservaRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var create = await _service.CreateReservaAsync(request,userId);

            return Ok(create);
        }
        [HttpGet("Email")]
        public async Task<ActionResult<PagedList<GetReservasByIdResponse>>> GetReservasByEmail(int pageNumber, int pagesize)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var reservas = await _serviceByEmail.GetReservasByEmail(userId, pageNumber, pagesize);
            return Ok(reservas);
        }
    }
}
