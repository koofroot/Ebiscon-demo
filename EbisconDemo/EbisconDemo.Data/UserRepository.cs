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

        public Task CreateUserAsync(User user)
        {
            return _dbSet.AddAsync(user).AsTask();
        }

        public Task<User>? GetByCredentialsAsync(string email, string password)
        {
            return _dbSet.SingleOrDefaultAsync(x => x.Email.ToLower() == email.ToLower() && x.Password == password)!;
        }

        public Task<bool> IsExistAsync(string email)
        {
            return _dbSet.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }
        public async Task SetRoleAsync(string userEmail, UserType parsedRole)
        {
            var user = await _dbSet.SingleOrDefaultAsync(x => x.Email.ToLower() == userEmail.ToLower());

            user.UserType = parsedRole;
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
