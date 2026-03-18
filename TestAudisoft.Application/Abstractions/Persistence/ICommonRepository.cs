namespace TestAudisoft.Application.Abstractions.Persistence
{
    public interface ICommonRepository
    {
        Task<bool> CheckIfEmailExists(string email);
        Task<bool> CheckIfEmailExists(string email, int exclude_id, bool is_student);
        Task<bool> CheckIfGradeNameExists(string name);
        Task<bool> CheckIfGradeNameExists(string name, int exclude_id);
    }
}
