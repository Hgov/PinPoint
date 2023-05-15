using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PinPoint.Data.Enums.Enums;

namespace PinPoint.Infrastructure.MapperService.Models
{
    public class PutContactDTO
    {
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? bio { get; set; }
        public DateTime? birth_date { get; set; }
        public Gender? gender { get; set; }
    }
}
