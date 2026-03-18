using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Grades.Dtos;
using TestAudisoft.Application.Grades.UseCases.GradesBusiness;
using TestAudisoft.Enums;

namespace TestAudisoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly IGradesBusiness _gradesBusiness;

        public GradesController(IGradesBusiness gradesBusiness)
        {
            _gradesBusiness = gradesBusiness;
        }

        /// <summary>
        /// Devuelve la lista de calificaciones con paginación y filtros opcionales.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost("GetAll")]
        [ProducesResponseType(typeof(PagedResult<GradeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<GradeDto>>> GetAll([FromBody] GradesFilter filter)
        {
            PagedResult<GradeDto> result = await _gradesBusiness.GetAll(filter);
            return Ok(result);
        }

        /// <summary>
        /// Devuelve una calificación por su ID. Si no se encuentra, devuelve un 404 Not Found.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GradeDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GradeDto>> GetById(int id)
        {
            GradeDto? grade = await _gradesBusiness.GetById(id);

            if (grade is null)
                return NotFound();

            return Ok(grade);
        }

        /// <summary>
        /// Devuelve la lista de calificaciones asociadas a un estudiante específico. Si no se encuentran calificaciones para el estudiante, devuelve una lista vacía.
        /// </summary>
        /// <param name="student_id"></param>
        /// <returns></returns>
        [HttpGet("student/{student_id:int}")]
        [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetByStudentId(int student_id)
        {
            IEnumerable<GradeDto> grades = await _gradesBusiness.GetByStudentId(student_id);
            return Ok(grades);
        }

        /// <summary>
        /// Devuelve la lista de calificaciones asociadas a un profesor específico. Si no se encuentran calificaciones para el profesor, devuelve una lista vacía.
        /// </summary>
        /// <param name="professor_id"></param>
        /// <returns></returns>
        [HttpGet("professor/{professor_id:int}")]
        [ProducesResponseType(typeof(IEnumerable<GradeDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GradeDto>>> GetByProfessorId(int professor_id)
        {
            IEnumerable<GradeDto> grades = await _gradesBusiness.GetByProfessorId(professor_id);
            return Ok(grades);
        }

        /// <summary>
        /// Crea una calificación
        /// </summary>
        /// <param name="grades"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Create([FromBody] GradeCreateDto grades)
        {
            DbActions result = await _gradesBusiness.CreateGrades(grades);
            return Ok(result);
        }

        /// <summary>
        /// Actualiza una calificación
        /// </summary>
        /// <param name="grades"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Update([FromBody] GradeUpdateDto grades)
        {
            DbActions result = await _gradesBusiness.UpdateGrades(grades);
            return Ok(result);
        }

        /// <summary>
        /// Elimina una calificación existente a partir de su identificador. Si la calificación no existe, se devuelve una excepción controlada.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Delete(int id)
        {
            DbActions result = await _gradesBusiness.DeleteGrades(id);
            return Ok(result);
        }
    }
}
