using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace EbisconDemo.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly Context _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(Context context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public void CreateUser(User user)
        {
            _dbSet.Add(user);
        }

        public User GetByCredentials(string email, string password)
        {
            return _dbSet.SingleOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
        }

        public bool IsExist(string email)
        {
            return _dbSet.Any(x => x.Email.ToLower() == email.ToLower());
        }
        public void SetRole(string userEmail, UserType parsedRole)
        {
            var user = _dbSet.SingleOrDefault(x => x.Email.ToLower() == userEmail.ToLower());

            user.UserType = parsedRole;
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
