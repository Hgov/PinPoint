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
<<<<<<< HEAD
        Task<IActionResult> PutUserAsync(Guid id, PutUserDTO putUserDTO);
=======
<<<<<<< Updated upstream
=======
        Task<IActionResult> PutUserAsync(Guid id, PutUserDTO putUserDTO);
        Task<IActionResult> DeleteByIdUserAsync(Guid id);
>>>>>>> Stashed changes
>>>>>>> cec7eb19645063b06087ae3eac9d2594120c7975
    }
}
