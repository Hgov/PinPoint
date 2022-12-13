using Microsoft.EntityFrameworkCore;
using PinkPoint.Core.Repositories;
using PinkPoint.Data.Domain;
using PinkPoint.DataAccess.Helpers;
using PinkPoint.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinkPoint.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DataContext dataContext) : base(dataContext)
        {

        }
        public DataContext dataContext { get { return _Context as DataContext; } }

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
