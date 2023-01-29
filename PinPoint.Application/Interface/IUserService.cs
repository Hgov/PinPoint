using Microsoft.AspNetCore.Mvc;
using PinPoint.Infrastructure.MapperService.Models.User;
using PinPoint.Infrastructure.Response;

namespace PinPoint.Application.Interface
{
    public interface IUserService<User> where User : class
    {
        Task<IActionResult> GetUserListAsync();
        Task<ServiceResponse<GetUserDTO>> GetByIdUserAsync(Guid id);
        Task<ServiceResponse<GetUserDTO>> PostUserAsync(PostUserDTO postUserDTO);
        Task<ServiceResponse<GetUserDTO>> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTO);
    }
}
