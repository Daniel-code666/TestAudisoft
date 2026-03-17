using Microsoft.AspNetCore.Mvc;
using TestAudisoft.Application.Student.UseCases.StudentBusiness;

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
    }
}
