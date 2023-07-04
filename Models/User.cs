namespace TestTask.Models
{
    public class User
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public string? Password { get; set; }
        public required string Role { get; set; }
    }
}
