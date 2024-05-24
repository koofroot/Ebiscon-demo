using AutoMapper;
using BCrypt.Net;
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
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
        }

        public LoginResponseDto Login(LoginRequestDto loginDto)
        {
            if(string.IsNullOrWhiteSpace(loginDto.Password) || string.IsNullOrWhiteSpace(loginDto.Email))
            {
                throw new UserNotFoundException();
            }

            var hash = HashMeBitch(loginDto.Password);

            var user = _userRepository.GetByCredentials(loginDto.Email, hash);

            if (user == null) 
            {
                throw new UserNotFoundException();
            }

            var expiration = DateTime.UtcNow.AddMinutes(30);
            var jwt = _jwtTokenGenerator.GenerateToken(user, expiration);

            return new LoginResponseDto
            {
                Token = jwt,
                ExpirationTime = expiration
            };
        }

        public void SetUserRole(string userEmail, string role)
        {
            var isRoleExist = Enum.TryParse<UserType>(role, true, out var parsedRole);

            var isUserExist = _userRepository.IsExist(userEmail);

            if(!isRoleExist || !isUserExist)
            {
                throw new UserNotFoundException("User or role is not found.");
            }

            _userRepository.SetRole(userEmail, parsedRole);

            _userRepository.Save();
        }

        public void Register(RegistrationDto registerDto)
        {
            // todo: validate model

            var isExist = _userRepository.IsExist(registerDto.Email);

            if (isExist)
            {
                throw new UserAlreadyExistException();
            }

            var user = _mapper.Map<User>(registerDto);

            user.Password = HashMeBitch(user.Password);

            _userRepository.CreateUser(user);

            _userRepository.Save();
        }

        private string HashMeBitch(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, EbisconConstants.PasswordSalt);
        }
    }
}
