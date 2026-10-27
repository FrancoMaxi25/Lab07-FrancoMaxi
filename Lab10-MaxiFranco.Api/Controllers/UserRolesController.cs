using Lab10_MaxiFranco.Application.DTOs.UserRoles;
using Lab10_MaxiFranco.Application.UseCases.UserRoles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Lab10_MaxiFranco.Api.Controllers;
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UserRolesController(UserRoleUseCases cases) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await cases.GetAllAsync());
    [HttpGet("user/{userId:guid}")]
    public async Task<IActionResult> GetByUser(Guid userId) => Ok(await cases.GetByUserAsync(userId));
    [HttpPost]
    public async Task<IActionResult> Assign(AssignRoleDto dto)
    {
        try { return StatusCode(201, await cases.AssignAsync(dto)); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }
    [HttpDelete("{userId:guid}/{roleId:guid}")]
    public async Task<IActionResult> Remove(Guid userId, Guid roleId) =>
        await cases.RemoveAsync(userId, roleId) ? NoContent() : NotFound();
}
