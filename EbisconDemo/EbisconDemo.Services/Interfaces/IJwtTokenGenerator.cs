using EbisconDemo.Data.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user, DateTime tokenLifetime);
    }
}
