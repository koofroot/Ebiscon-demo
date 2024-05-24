using AutoMapper;
using EbisconDemo.Data.Interfaces;
using EbisconDemo.Data.Models;
using EbisconDemo.Data.Models.Enums;
using EbisconDemo.Services.Constants;
using EbisconDemo.Services.Exceptions;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;

namespace EbisconDemo.Services.Services
{
    public class UserService : IUserService
    {
        private const int TokenExpirationInMinutes = 30;

        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto loginDto)
        {
            if(string.IsNullOrWhiteSpace(loginDto.Password) || string.IsNullOrWhiteSpace(loginDto.Email))
            {
                throw new UserNotFoundException();
            }

            var hash = HashPassword(loginDto.Password);

            var user = await _userRepository.GetByCredentialsAsync(loginDto.Email, hash);

            if (user == null) 
            {
                throw new UserNotFoundException();
            }

            var expiration = DateTime.UtcNow.AddMinutes(TokenExpirationInMinutes);
            var jwt = await _jwtTokenGenerator.GenerateTokenAsync(user, expiration);

            return new LoginResponseDto
            {
                Token = jwt,
                ExpirationTime = expiration
            };
        }

        public async Task SetUserRoleAsync(string userEmail, string role)
        {
            var isRoleExist = Enum.TryParse<UserType>(role, true, out var parsedRole);

            var isUserExist = await _userRepository.IsExistAsync(userEmail);

            if(!isRoleExist || !isUserExist)
            {
                throw new UserNotFoundException("User or role is not found.");
            }

            await _userRepository.SetRoleAsync(userEmail, parsedRole);

            await _userRepository.SaveAsync();
        }

        public async Task RegisterAsync(RegistrationDto registerDto)
        {
            // todo: validate model

            var isUserExist = await _userRepository.IsExistAsync(registerDto.Email);

            if (isUserExist)
            {
                throw new UserAlreadyExistException();
            }

            var user = _mapper.Map<User>(registerDto);

            user.Password = HashPassword(user.Password);

            await _userRepository.CreateUserAsync(user);

            await _userRepository.SaveAsync();
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, EbisconConstants.PasswordSalt);
        }
    }
}
