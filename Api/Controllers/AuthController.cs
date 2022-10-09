using Api.Base;
using Business.Abstract;
using Entities.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserForLogin userForLogin)
    {
        var userToLogin = _authService.Login(userForLogin);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var accessToken = _authService.CreateAccessToken(userToLogin.Data!);
        return FromResult(accessToken);
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] UserForRegister userForRegister)
    {
        var userExists = _authService.UserExists(userForRegister.Email);
        if (!userExists.Success)
        {
            return BadRequest(userExists.Message);
        }
        var registerResult = _authService.Register(userForRegister);
        var result = _authService.CreateAccessToken(registerResult.Data!);
        return FromResult(result);
    }
}