
using System.ComponentModel.DataAnnotations;
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.Data.Domain
{
    public class Log
    {
        public Log()
        {

        }
        [Key]
        public Guid? LogId { get; set; }
        public string? ProjectName { get; set; }
        public string? MethodName { get; set; }
        public Level Level { get; set; }
        public object? Request { get; set; }
        public object? Response { get; set; }
        public Guid? RelatedLogId { get; set; }
        public DateTime? creation_tsz { get; set; }
    }
}
