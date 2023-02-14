using PinPoint.Data.Domain.Base;

namespace PinPoint.Infrastructure.MapperService.Models.User
{
    public class DeleteUserDTO : AdditionalDomain
    {
        public Guid user_id { get; set; }
    }
}
