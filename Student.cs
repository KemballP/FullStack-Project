using System.Text.Json.Serialization;

namespace RegistrationSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentNumber { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation Property
        [JsonIgnore]
        public ICollection<Registration>? Registrations { get; set; }
    }
}
