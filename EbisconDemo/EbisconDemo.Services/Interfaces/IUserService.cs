using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto mapped);
        Task SetUserRoleAsync(string userEmail, string role);
        Task RegisterAsync(RegistrationDto mapped);
    }
}
