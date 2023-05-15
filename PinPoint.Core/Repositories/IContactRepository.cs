using PinPoint.Core.Repositories.Base;
using PinPoint.Data.Domain;

namespace PinPoint.Core.Repositories
{
    public interface IContactRepository : IRepository<Contact>
    {
        Task<bool> IsEmailExist(string email);
        Task<bool> IsContactByIdExist(Guid id);
        //Task<IEnumerable<Contact>> GetAsync();
        //Task<Contact> GetByIdAsync(Guid id);
        //Task<bool> GetIdAnyAsync(Guid id);
        //Task<bool> GetEmailAnyAsync(string email);
        //Task<bool> GetPhoneAnyAsync(string phone);

    }
}
