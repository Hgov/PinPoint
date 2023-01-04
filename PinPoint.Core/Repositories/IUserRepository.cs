using PinPoint.Core.Repositories.Base;
using PinPoint.Data.Domain;

namespace PinPoint.Core.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> IsUserByIdExist(Guid id);
        //Task<IEnumerable<User>> GetAsync();
        //Task<User> GetByIdAsync(Guid id);
        //Task<bool> GetIdAnyAsync(Guid id);
        //Task<bool> GetEmailAnyAsync(string email);
        //Task<bool> GetPhoneAnyAsync(string phone);

    }
}
