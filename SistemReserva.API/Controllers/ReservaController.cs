using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemReserva.Application.Reservas.CreateReserva;
using System.Security.Claims;

namespace SistemReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {

        private readonly ICreateReservaService _service;

        public ReservaController(ICreateReservaService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<CreateReservaResponse>> CreateReservaAsync(CreateReservaRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var create = await _service.CreateReservaAsync(request,userId);

            return Ok(create);
        }
    }
}
