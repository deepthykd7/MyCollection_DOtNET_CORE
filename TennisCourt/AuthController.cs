using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BLL;
using DTO;
using DTO.Request;
using DTO.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly UserRegistrationBLL _userRegistrationBLL;
    BaseResponse<LoginResponse> response = new BaseResponse<LoginResponse>();

    public AuthController(IConfiguration configuration, UserRegistrationBLL userRegistrationBLL)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _userRegistrationBLL = userRegistrationBLL ?? throw new ArgumentNullException(nameof(userRegistrationBLL));
    }
    [HttpPost("GenerateToken")]
    public IActionResult GenerateToken([FromBody] Login_request loginRequest)
    {
                if (IsValidUser(loginRequest))
        {
            var token = GenerateJwtToken(loginRequest.username);
            return Ok(new { Token = token });
        }

        return Unauthorized();
    }

    private bool IsValidUser(Login_request loginRequest)
    {
        response = _userRegistrationBLL.LoginUser(loginRequest);
        if (response.status == ResponseTypeContants.SUCCESS)
        {
            return true;
        }
        else
            return false;
    }

    private string GenerateJwtToken(string username)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
                    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpirationInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
