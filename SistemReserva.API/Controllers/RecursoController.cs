using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaReserva.Application.Recursos.CreateRecurso;
using SistemaReserva.Application.Recursos.ListRecursos;
using SistemReserva.Application.Recursos.DeleteRecurso;
using SistemReserva.Application.Recursos.GetRecursoById;
using SistemReserva.Application.Recursos.UpdateRecurso;
using SistemReserva.Domain.Constants;
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
        public async Task<ActionResult<CreateRecursoResponse>>CreateRecursoAsync(CreateRecursoRequest request)
        {
            var recurso = await _createRecursoService.CreateRecursoAsync(request);
            return CreatedAtAction(nameof(GetRecursoByIdAsync),new { id= recurso.RecursoId }, recurso);
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedList<GetRecursoResponse>>> Recursos(int page, int pageSize)
        {
            var recursos = await _getRecursosService.GetListRecursosAsync(page, pageSize);
            return Ok(recursos);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateRecursoAsync([FromRoute] int id,UpdateRecursoRequest request)
        {
            var result = await _updateRecursoService.UpdateRecursoAsync(id,request);
            return Ok(result);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRecursoAsync([FromRoute]int id)
        {

            var result = await _deleteRecursoService.DeleteRecursoAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetRecursoByIdResponse>> GetRecursoByIdAsync([FromRoute] int id)
        {
            var recurso = await _getRecursoByIdService.GetRecursoById(id);
            return Ok(recurso);
        }

    }
}
