using Lab10_MaxiFranco.Application.DTOs.Roles;
using Lab10_MaxiFranco.Application.UseCases.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab10_MaxiFranco.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RolesController(RoleUseCases cases) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await cases.GetAllAsync());

    [HttpGet("{id:guid}")] 
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await cases.GetByIdAsync(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto dto)
    {
        try { return StatusCode(201, await cases.CreateAsync(dto)); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    [HttpPut("{id:guid}")] 
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto dto)
    {
        try { return await cases.UpdateAsync(id, dto) ? NoContent() : NotFound(); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }

    [HttpDelete("{id:guid}")] 
    public async Task<IActionResult> Delete(Guid id) => await cases.DeleteAsync(id) ? NoContent() : NotFound();
}
