using System.ComponentModel.DataAnnotations;

namespace TestTask.Models
{
    public class InsurancePolicy
    {
        public uint Id { get; set; }

        [MaxLength(10)]
        public required string CustomerPassportSeries {get; set; }
        
        [MaxLength(50)]
        public required string CustomerPassportNumber { get; set; }

        public required uint WeeksPerPeriod { get; set; }

        public required uint PeriodicFee { get; set; }

        public uint WorkerId { get; set; }
        public uint PolicyTypeId { get; set; }
        public uint OfficeId { get; set; }
    }
}
