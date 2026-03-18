using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Professor.Dtos;
using TestAudisoft.Application.Professor.UseCases.ProfessorBusiness;
using TestAudisoft.Enums;

namespace TestAudisoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController : ControllerBase
    {
        private readonly IProfessorBusiness _professorBusiness;

        public ProfessorController(IProfessorBusiness professorBusiness)
        {
            _professorBusiness = professorBusiness;
        }

        /// <summary>
        /// Devuelve la lista paginada de profesores. Permite filtrar por nombre, apellido y correo electrónico.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(PagedResult<ProfessorDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<ProfessorDto>>> GetAll([FromBody] StudentFilter filter)
        {
            PagedResult<ProfessorDto> result = await _professorBusiness.GetAll(filter);
            return Ok(result);
        }

        /// <summary>
        /// Devuelve la información de un profesor específico consultando por su identificador. Si no existe, devuelve NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(ProfessorDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfessorDto>> GetById(int id)
        {
            ProfessorDto? professor = await _professorBusiness.GetById(id);

            if (professor is null)
                return NotFound();

            return Ok(professor);
        }

        /// <summary>
        /// Devuelve la información de un profesor junto con las calificaciones asociadas y el detalle de los estudiantes relacionados. Si el profesor no existe, devuelve NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/grades")]
        [ProducesResponseType(typeof(ProfessorWithGradesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProfessorWithGradesDto>> GetProfessorWithGrades(int id)
        {
            ProfessorWithGradesDto? professor = await _professorBusiness.GetProfessorWithGrades(id);

            if (professor is null)
                return NotFound();

            return Ok(professor);
        }

        /// <summary>
        /// Crea un nuevo profesor. Valida que el correo no exista previamente y que los datos requeridos sean válidos.
        /// </summary>
        /// <param name="professor"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status201Created)]
        public async Task<ActionResult<DbActions>> Create([FromBody] ProfessorCreateDto professor)
        {
            DbActions result = await _professorBusiness.CreateProfessor(professor);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Actualiza la información de un profesor existente a partir de su identificador. Valida existencia y unicidad del correo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="professor"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Update(int id, [FromBody] ProfessorUpdateDto professor)
        {
            professor.Id = id;

            DbActions result = await _professorBusiness.UpdateProfessor(professor);
            return Ok(result);
        }

        /// <summary>
        /// Elimina un profesor existente a partir de su identificador. Si el profesor no existe, se devuelve una excepción controlada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Delete(int id)
        {
            DbActions result = await _professorBusiness.DeleteProfessor(id);
            return Ok(result);
        }
    }
}
