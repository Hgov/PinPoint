
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace PinkPoint.Application.Interface
{
    public interface IUserService<User> where User : class
    {
        Task<IEnumerable<User>> GetUserListAsync();
        Task<User> GetByIdUserAsync(Guid guid);
        Task<User> PostUserAsync(User user);
        Task<User> PutUserAsync(User user);
    }
}
