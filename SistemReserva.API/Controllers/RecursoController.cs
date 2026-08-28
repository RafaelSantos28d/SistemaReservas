using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemReserva.Application.Recursos.UpdateRecurso;
using SistemReserva.Domain.Pagination;

namespace SistemReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {
        private readonly ICreateRecursoService _createRecursoService;
        private readonly IGetRecursosService _getRecursosService;
        private readonly IUpdateRecursoService _updateRecursoService;
        public RecursoController(ICreateRecursoService createRecursoService, IGetRecursosService getRecursosService, IUpdateRecursoService updateRecursoService)
        {
            _createRecursoService = createRecursoService;
            _getRecursosService = getRecursosService;
            _updateRecursoService = updateRecursoService;
        }

        [HttpPost]
        public async Task<ActionResult<CreateRecursoResponse>>CreateRecursoAsync(CreateRecursoRequest request)
        {
            var recurso = await _createRecursoService.CreateRecursoAsync(request);
            return Ok(recurso);
        }
        [HttpGet]
        public async Task<ActionResult<PagedList<GetRecursoResponse>>> Recursos(int page, int pageSize)
        {
            var recursos = await _getRecursosService.GetListRecursosAsync(page, pageSize);
            return Ok(recursos);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRecursoAsync([FromRoute]int id,UpdateRecursoRequest request)
        {
            id = request.RecursoId;
            var result = await _updateRecursoService.UpdateRecursoAsync(request);
            return Ok(result);
        }

    }
}
