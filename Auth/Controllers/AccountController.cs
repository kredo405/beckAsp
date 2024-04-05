using Auth.Contracts;
using Auth.JwtAuthManager;
using Auth.JwtAuthManager.Models;
using Auth.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers;
[ApiController]
[Route("api/auth")]
public class AccountController: ControllerBase
{
    private readonly JwtTokenHandler _jwtTokenHandler;
    private readonly IUserManager _userManager;

    public AccountController(JwtTokenHandler jwtTokenHandler,UserManager userManager)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _userManager = userManager;
    }

    [HttpPost]
    public ActionResult<AuthenticationResponse?> Authenticate([FromBody] AuthenticationRequest authenticationRequest)
    {
        var authenticationResponse = _jwtTokenHandler.GenerateJwtToken(authenticationRequest);
        if (authenticationResponse == null) return Unauthorized();
        return authenticationResponse;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthenticationRequest request)
    {
        await _userManager.RegisterAsync(request.UserName, request.Password);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest request)
    {
        var token = await _userManager.LoginAsync(request.UserName, request.Password);
        return Ok(new { Token = token });
    }
}