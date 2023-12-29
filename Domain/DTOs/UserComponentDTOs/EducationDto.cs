namespace DTOs
{
    public class EducationDto
    {
        public Guid Id { get; set; }
        public string Institution { get; set; }
        public string Degree { get; set; }
        public string GraduationYear { get; set; }
        public Guid UserId { get; set; }
    }
}