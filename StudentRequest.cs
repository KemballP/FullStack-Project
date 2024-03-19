namespace RegistrationSystem.Models
{
    public class StudentRequest
    {
        public string? StudentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
