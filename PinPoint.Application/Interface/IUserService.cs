using Microsoft.AspNetCore.Mvc;
using PinPoint.Infrastructure.MapperService.Models.User;

namespace PinPoint.Application.Interface
{
    public interface IUserService<User> where User : class
    {
        Task<IActionResult> GetUserListAsync();
        Task<IActionResult> GetByIdUserAsync(Guid id);
        Task<IActionResult> PostUserAsync(PostUserDTO postUserDTO);
        Task<IActionResult> PostBulkUserAsync(IEnumerable<PostUserDTO> postUserDTO);
        Task<IActionResult> PutUserAsync(Guid id, PutUserDTO putUserDTO);
    }
}
