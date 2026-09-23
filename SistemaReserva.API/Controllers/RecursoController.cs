using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.DeleteRecurso;
using SistemaReserva.Application.Recursos.GetRecursoById;
using SistemaReserva.Application.Recursos.GetRecursos;
using SistemaReserva.Application.Recursos.UpdateRecurso;
using SistemaReserva.Domain.Constants;
using SistemaReserva.Domain.Pagination;

namespace SistemaReserva.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoController : ControllerBase
    {
        private readonly ICreateRecursoService _createRecursoService;
        private readonly IGetRecursosService _getRecursosService;
        private readonly IUpdateRecursoService _updateRecursoService;
        private readonly IDeleteRecursoService _deleteRecursoService;
        private readonly IGetRecursoByIdService _getRecursoByIdService;
        public RecursoController(ICreateRecursoService createRecursoService, IGetRecursosService getRecursosService, IUpdateRecursoService updateRecursoService, IDeleteRecursoService deleteRecursoService, IGetRecursoByIdService getRecursoByIdService)
        {
            _createRecursoService = createRecursoService;
            _getRecursosService = getRecursosService;
            _updateRecursoService = updateRecursoService;
            _deleteRecursoService = deleteRecursoService;
            _getRecursoByIdService = getRecursoByIdService;
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<ActionResult<CreateRecursoResponse>> CreateRecursoAsync(CreateRecursoRequest request)
        {
            var recurso = await _createRecursoService.CreateRecursoAsync(request);
            
            return CreatedAtRoute("GetRecursoByIdAsync", new { id = recurso.RecursoId }, recurso);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedList<GetRecursosResponse>>> Recursos(int page, int pageSize)
        {
            var recursos = await _getRecursosService.GetRecursosAsync(page, pageSize);
            return Ok(recursos);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRecursoAsync([FromRoute] int id, UpdateRecursoRequest request)
        {
            var result = await _updateRecursoService.UpdateRecursoAsync(id, request);
            return Ok(result);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecursoAsync([FromRoute] int id)
        {

            var result = await _deleteRecursoService.DeleteRecurso(id);
            return NoContent();
        }
        [Authorize]
        [HttpGet("{id}",Name ="GetRecursoByIdAsync")]
        public async Task<ActionResult<GetRecursoByIdResponse>> GetRecursoByIdAsync([FromRoute] int id)
        {
            var recurso = await _getRecursoByIdService.GetRecursoByIdAsync(id);
            return Ok(recurso);
        }

    }
}
