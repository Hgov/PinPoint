using Microsoft.EntityFrameworkCore;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.Repositories.Base;
using System.Net.Http.Headers;

namespace PinPoint.Infrastructure.Repositories
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        public ContactRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public DataContext dataContext { get { return _Context as DataContext; } }

        public async Task<bool> IsEmailExist(string email) => await _Context.Contacts.AnyAsync(x=>x.email==email);
        public async Task<bool> IsContactByIdExist(Guid id) => await _Context.Contacts.AnyAsync(x => x.contact_id == id);
        //public async Task<IEnumerable<Contact>> GetAsync()
        //{
        //    return await _Context.Contacts
        //    //.Include(x => x.gender)
        //    //.Include(x => x.Role)
        //    ////.Include(x => x.ContactFiles)
        //    ////.ThenInclude(x => x.File)
        //        .ToListAsync();
        //}
        //public async Task<Contact> GetByIdAsync(Guid id)
        //{
        //    return await _Context.Contacts
        //        //.Where(x => x.contact_id == id)
        //        //.Include(x => x.gender)
        //        //.Include(x => x.Role)
        //        ////.Include(x => x.ContactFiles)
        //        ////.ThenInclude(x => x.File)
        //        .FirstOrDefaultAsync();
        //}
        //public async Task<bool> GetIdAnyAsync(Guid id)
        //{
        //    return await _Context.Contacts.AnyAsync(x => x.contact_id == id);
        //}
        //public async Task<bool> GetEmailAnyAsync(string email)
        //{
        //    return await _Context.Contacts.AnyAsync(x => x.email == email);
        //}
        //public async Task<bool> GetPhoneAnyAsync(string phone)
        //{
        //    return await _Context.Contacts.AnyAsync(x => x.phone == phone);
        //}
    }
}
