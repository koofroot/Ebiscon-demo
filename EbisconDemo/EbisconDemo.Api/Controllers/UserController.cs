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
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(
            IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegistrationApiModel model)
        {
            var mapped = _mapper.Map<RegistrationDto>(model);
            await _userService.RegisterAsync(mapped);
            
            // todo: 204 ??
            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginApiModel model)
        {
            var mapped = _mapper.Map<LoginRequestDto>(model);
            var token = await _userService.LoginAsync(mapped);

            return Ok(token);
        }
    }
}
