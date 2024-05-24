using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;

namespace EbisconDemo.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByCredentialsAsync(string email, string password);

        Task<bool> IsExistAsync(string email);

        Task CreateUserAsync(User user);

        Task SaveAsync();

        Task SetRoleAsync(string userEmail, UserType parsedRole);
    }
}
