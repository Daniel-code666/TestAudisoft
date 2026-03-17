using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Professor.UseCases.ProfessorBusiness;

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
    }
}
