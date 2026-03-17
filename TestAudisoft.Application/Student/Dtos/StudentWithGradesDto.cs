namespace TestAudisoft.Application.Student.Dtos
{
    public class StudentWithGradesDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<StudentGradeDetailDto> Grades { get; set; } = [];
    }

    public class StudentGradeDetailDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double GradeValue { get; set; }

        public int ProfessorId { get; set; }
        public string ProfessorFirstName { get; set; } = string.Empty;
        public string ProfessorLastName { get; set; } = string.Empty;
        public string ProfessorEmail { get; set; } = string.Empty;
    }
}
