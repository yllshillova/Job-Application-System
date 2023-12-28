namespace Domain.DTOs
{
    public class ResumeStorageDto
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        // You may or may not include FileData in the DTO based on your needs
    }
}