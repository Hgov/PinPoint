using PinPoint.Infrastructure.MapperService.Models.Base;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Infrastructure.MapperService.Models
{
    public class GetContactDTO : AdditionalDomainDTO
    {
        public Guid contact_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public Gender? gender { get; set; }
    }
}
