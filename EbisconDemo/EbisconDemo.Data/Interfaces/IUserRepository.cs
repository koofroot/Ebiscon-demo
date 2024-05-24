using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;

namespace EbisconDemo.Data.Interfaces
{
    public interface IUserRepository
    {
        User GetByCredentials(string email, string password);

        bool IsExist(string email);

        void CreateUser(User user);

        void Save();

        void SetRole(string userEmail, UserType parsedRole);
    }
}
