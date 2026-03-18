using Microsoft.Extensions.DependencyInjection;
using TestAudisoft.Application.Abstractions.Persistence;
using TestAudisoft.Infrastructure.Persistence.Repository;

namespace TestAudisoft.Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
                => services
                    .AddScoped<IStudentRepository, StudentRepository>()
                    .AddScoped<IProfessorRepository, ProfessorRepository>()
                    .AddScoped<IGradesRepository, GradesRepository>()
                    .AddScoped<ICommonRepository, CommonRepository>();
    }
}
