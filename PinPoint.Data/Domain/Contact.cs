
using PinPoint.Data.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Data.Domain
{
    [Table("Contacts")]
    public class Contact : AdditionalDomain
    {
        [Key]
        public Guid? contact_id { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public Gender? gender { get; set; }


    }
}
