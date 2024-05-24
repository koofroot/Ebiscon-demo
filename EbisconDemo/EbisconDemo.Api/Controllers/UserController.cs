using AutoMapper;
using EbisconDemo.Api.Models;
using EbisconDemo.Services.Interfaces;
using EbisconDemo.Services.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace EbisconDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IValidator<RegistrationApiModel> _registrationModelValidator;
        private readonly IValidator<LoginApiModel> _loginModelValidator;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(
            IValidator<RegistrationApiModel> registrationModelValidator,
            IUserService userService,
            IMapper mapper,
            IValidator<LoginApiModel> loginModelValidator)
        {
            _registrationModelValidator = registrationModelValidator;
            _userService = userService;
            _mapper = mapper;
            _loginModelValidator = loginModelValidator;
        }

        [HttpPost("register")]
        public IActionResult Register(RegistrationApiModel model)
        {
            var mapped = _mapper.Map<RegistrationDto>(model);
            _userService.Register(mapped);
            
            // todo: 204 ??
            return Created();
        }

        [HttpPost("login")]
        public IActionResult Login(LoginApiModel model)
        {
            var mapped = _mapper.Map<LoginRequestDto>(model);
            var token = _userService.Login(mapped);

            return Ok(token);
        }
    }
}
