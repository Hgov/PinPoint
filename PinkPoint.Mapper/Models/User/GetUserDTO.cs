using PinkPoint.Mapper.Models.Base;
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.Mapper.Models.User
{
    public class GetUserDTO:AdditionalDomainDTO
    {
        public Guid user_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public Gender? gender { get; set; }
    }
}
