using System.ComponentModel.DataAnnotations;
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.Data.Domain
{
    public class NLog
    {
        [Key]
        public int? ID { get; set; }
        public string? MachineName { get; set; }
        public DateTime? Logged { get; set; }
        public Level Level { get; set; }
        public string? Message { get; set; }
        public string? Logger { get; set; }
        public string? Properties { get; set; }
        public string? Callsite { get; set; }
        public string? Exception { get; set; }
    }
}
