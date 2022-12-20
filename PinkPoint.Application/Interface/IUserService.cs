namespace PinPoint.Application.Interface
{
    public interface IUserService<User> where User : class
    {
        Task<IEnumerable<User>> GetUserListAsync();
        Task<User> GetByIdUserAsync(Guid guid);
        Task<User> PostUserAsync(User user);
    }
}
