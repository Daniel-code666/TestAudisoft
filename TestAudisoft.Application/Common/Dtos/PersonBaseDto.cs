namespace TestAudisoft.Application.Common.Dtos
{
    public abstract class PersonBaseDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }

    public abstract class PersonReadDto : PersonBaseDto
    {
        public int Id { get; set; }
    }

    public abstract class PersonCreateDto : PersonBaseDto
    {
    }

    public abstract class PersonUpdateDto : PersonBaseDto
    {
        public int Id { get; set; }
    }
}
