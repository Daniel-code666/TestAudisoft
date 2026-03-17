namespace TestAudisoft.Application.Professor.Dtos
{
    public class ProfessorWithGradesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<ProfessorGradeDetailDto> Grades { get; set; } = [];
    }

    public class ProfessorGradeDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double GradeValue { get; set; }

        public int StudentId { get; set; }
        public string StudentFirstName { get; set; } = string.Empty;
        public string StudentLastName { get; set; } = string.Empty;
        public string StudentEmail { get; set; } = string.Empty;
    }
}
