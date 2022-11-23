using PinkPoint.Core.Repositories.Base;
using PinkPoint.Data.Domain;

namespace PinkPoint.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<bool> GetIdAnyAsync(Guid id);
        Task<bool> GetEmailAnyAsync(string email);
        Task<bool> GetPhoneAnyAsync(string phone);

    }
}
