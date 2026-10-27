using Lab10_MaxiFranco.Application.DTOs.Auth;
using Lab10_MaxiFranco.Application.UseCases.Auth;
using Microsoft.AspNetCore.Mvc;
namespace Lab10_MaxiFranco.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthController(LoginUseCase login) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var result = await login.ExecuteAsync(dto);
        return result is null ? Unauthorized(new { message = "Credenciales inválidas." }) : Ok(result);
    }
}
