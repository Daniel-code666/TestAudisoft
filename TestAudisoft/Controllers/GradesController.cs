using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Grades.UseCases.GradesBusiness;

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
    }
}
