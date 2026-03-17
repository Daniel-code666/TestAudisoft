namespace TestAudisoft.Application.Grades.Dtos
{
    public class GradeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double GradeValue { get; set; }
        public int StudentId { get; set; }
        public int ProfessorId { get; set; }
    }

    public class GradeCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public double GradeValue { get; set; }
        public int StudentId { get; set; }
        public int ProfessorId { get; set; }
    }

    public class GradeUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double GradeValue { get; set; }
        public int StudentId { get; set; }
        public int ProfessorId { get; set; }
    }
}
