namespace Domain
{
    public class JobSeeker : User
    {
        public List<ApplicationEntity> Applications { get; set; }
    }
}