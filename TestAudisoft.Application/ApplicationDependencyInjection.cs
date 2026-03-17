using Microsoft.Extensions.DependencyInjection;
using TestAudisoft.Application.Common;
using TestAudisoft.Application.Grades.UseCases.GradesBusiness;
using TestAudisoft.Application.Professor.UseCases.ProfessorBusiness;
using TestAudisoft.Application.Student.UseCases.StudentBusiness;

namespace TestAudisoft.Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
            => services.AddAutoMapper(cfg => { }, typeof(ApplicationAppProfile).Assembly)
                .AddScoped<IGradesBusiness, GradesBusiness>()
                .AddScoped<IProfessorBusiness, ProfessorBusiness>()
                .AddScoped<IStudentBusiness, StudentBusiness>();
    }
}
