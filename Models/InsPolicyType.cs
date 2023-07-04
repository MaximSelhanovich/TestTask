namespace TestTask.Models
{
    public class InsPolicyType
    {
        public uint Id { get; set; }
        public required string Name { get; set; }
        
        public required uint InsuredSum{ get; set; }
    }
}
