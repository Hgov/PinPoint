using Microsoft.EntityFrameworkCore;
using PinPoint.Core.Repositories;
using PinPoint.Data.Domain;
using PinPoint.DataAccess.Helpers;
using PinPoint.Infrastructure.Repositories.Base;
using System.Net.Http.Headers;

namespace PinPoint.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public DataContext dataContext { get { return _Context as DataContext; } }

        public async Task<bool> IsEmailExist(string email) => await _Context.Users.AnyAsync(x=>x.email==email);
        public async Task<bool> IsUserByIdExist(Guid id) => await _Context.Users.AnyAsync(x => x.user_id == id);
        //public async Task<IEnumerable<User>> GetAsync()
        //{
        //    return await _Context.Users
        //    //.Include(x => x.gender)
        //    //.Include(x => x.Role)
        //    ////.Include(x => x.UserFiles)
        //    ////.ThenInclude(x => x.File)
        //        .ToListAsync();
        //}
        //public async Task<User> GetByIdAsync(Guid id)
        //{
        //    return await _Context.Users
        //        //.Where(x => x.user_id == id)
        //        //.Include(x => x.gender)
        //        //.Include(x => x.Role)
        //        ////.Include(x => x.UserFiles)
        //        ////.ThenInclude(x => x.File)
        //        .FirstOrDefaultAsync();
        //}
        //public async Task<bool> GetIdAnyAsync(Guid id)
        //{
        //    return await _Context.Users.AnyAsync(x => x.user_id == id);
        //}
        //public async Task<bool> GetEmailAnyAsync(string email)
        //{
        //    return await _Context.Users.AnyAsync(x => x.email == email);
        //}
        //public async Task<bool> GetPhoneAnyAsync(string phone)
        //{
        //    return await _Context.Users.AnyAsync(x => x.phone == phone);
        //}
    }
}
