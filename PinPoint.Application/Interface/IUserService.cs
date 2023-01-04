using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.Response;

namespace PinPoint.Application.Interface
{
    public interface IUserService<User> where User : class
    {
        Task<ServiceResponse<GetUserDTO>> GetUserListAsync();
        Task<ServiceResponse<GetUserDTO>> GetByIdUserAsync(Guid id);
        Task<ServiceResponse<GetUserDTO>> PostUserAsync(PostUserDTO postUserDTO);
        Task<User> PutUserAsync(User user);
    }
}
