
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.Infrastructure.MapperService.Models.User
{
    public class PostUserDTO
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public virtual Gender? gender { get; set; }
    }
}
