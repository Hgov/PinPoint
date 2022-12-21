using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Data.Domain
{
    [Table("NLogs")]
    public class NLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? LogId { get; set; }
        public string? MachineName { get; set; }
        public DateTime? Logged { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? Logger { get; set; }
        public string? Properties { get; set; }
        public string? Callsite { get; set; }
        public string? Exception { get; set; }
    }
}
