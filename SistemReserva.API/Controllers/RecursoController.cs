using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReserva.Application.Recursos.CreateRecurso;

namespace SistemReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {
        private readonly ICreateRecursoService _createRecursoService;

        public RecursoController(ICreateRecursoService createRecursoService)
        {
            _createRecursoService = createRecursoService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateRecursoResponse>>CreateRecursoAsync(CreateRecursoRequest request)
        {
            var recurso = await _createRecursoService.CreateRecursoAsync(request);
            return Ok(recurso);
        }
    }
}
