using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Interfaces
{
    public interface IUserService
    {
        LoginResponseDto Login(LoginRequestDto mapped);
        void SetUserRole(string userEmail, string role);
        void Register(RegistrationDto mapped);
    }
}
