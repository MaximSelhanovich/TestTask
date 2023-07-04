using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class Office
    {
        public uint Id { get; set; }
        [DataType(DataType.PhoneNumber)]
        public required string PhoneNumber { get; set; }

        public required string Address { get; set; }
    }
}
