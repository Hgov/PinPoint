

namespace PinkPoint.Data.Domain.Base
{
    public class AdditionalDomain
    {
        public DateTime? creation_tsz { get; set; }
        public DateTime? last_updated_tsz { get; set; }
        public DateTime? delete_tsz { get; set; }
        public bool? status_active { get; set; }
        public bool? status_visibility { get; set; }
    }
}
