using EbisconDemo.Data.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IJwtTokenGenerator
    {
        Task<string> GenerateTokenAsync(User user, DateTime tokenLifetime);
    }
}
