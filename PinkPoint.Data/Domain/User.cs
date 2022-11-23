
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PinkPoint.Data.Domain.Base;
using static PinkPoint.Data.Enums.Enums;

namespace PinkPoint.Data.Domain
{
    [Table("Users")]
    public class User : AdditionalDomain
    {
        public User()
        {

        }
        [Key]   
        public Guid user_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public virtual Gender? gender { get; set; }


    }
}
