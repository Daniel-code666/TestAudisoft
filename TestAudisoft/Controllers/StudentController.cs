using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Common.Dtos;
using TestAudisoft.Application.Student.Dtos;
using TestAudisoft.Application.Student.UseCases.StudentBusiness;
using TestAudisoft.Enums;

namespace TestAudisoft.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentBusiness _studentBusiness;

        public StudentController(IStudentBusiness studentBusiness)
        {
            _studentBusiness = studentBusiness;
        }

        /// <summary>
        /// Devuelve la lista paginada de estudiantes. Permite filtrar por nombre, apellido y correo electrónico.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PagedResult<StudentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<StudentDto>>> GetAll([FromQuery] StudentFilter filter)
        {
            PagedResult<StudentDto> result = await _studentBusiness.GetAll(filter);
            return Ok(result);
        }

        /// <summary>
        /// Devuelve la información de un estudiante específico consultando por su identificador. Si no existe, devuelve NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDto>> GetById(int id)
        {
            StudentDto? student = await _studentBusiness.GetById(id);

            if (student is null)
                return NotFound();

            return Ok(student);
        }

        /// <summary>
        /// Devuelve la información de un estudiante junto con las calificaciones asociadas y el detalle de los profesores relacionados. Si el estudiante no existe, devuelve NotFound.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}/grades")]
        [ProducesResponseType(typeof(StudentWithGradesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentWithGradesDto>> GetStudentWithGrades(int id)
        {
            StudentWithGradesDto? student = await _studentBusiness.GetStudentWithGrades(id);

            if (student is null)
                return NotFound();

            return Ok(student);
        }

        /// <summary>
        /// Crea un nuevo estudiante. Valida que el correo no exista previamente y que los datos requeridos sean válidos.
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status201Created)]
        public async Task<ActionResult<DbActions>> Create([FromBody] StudentCreateDto student)
        {
            DbActions result = await _studentBusiness.CreateStudent(student);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>
        /// Actualiza la información de un estudiante existente a partir de su identificador. Valida existencia y unicidad del correo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(DbActions), StatusCodes.Status200OK)]
        public async Task<ActionResult<DbActions>> Update(int id, [FromBody] StudentUpdateDto student)
        {
            student.Id = id;

            DbActions result = await _studentBusiness.UpdateStudent(student);
            return Ok(result);
        }
    }
}
