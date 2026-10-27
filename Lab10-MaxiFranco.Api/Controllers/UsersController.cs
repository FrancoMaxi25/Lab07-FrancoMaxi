using Lab10_MaxiFranco.Application.DTOs.Users;
using Lab10_MaxiFranco.Application.UseCases.Users;
using Microsoft.AspNetCore.Mvc;
namespace Lab10_MaxiFranco.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController(CreateUserUseCase create, GetUsersUseCase getAll,
    GetUserByIdUseCase getById, UpdateUserUseCase update, DeleteUserUseCase delete) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await getAll.ExecuteAsync());
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await getById.ExecuteAsync(id);
        return user is null ? NotFound() : Ok(user);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        try
        {
            var user = await create.ExecuteAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
        }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateUserDto dto)
    {
        try { return await update.ExecuteAsync(id, dto) ? NoContent() : NotFound(); }
        catch (ArgumentException ex) { return BadRequest(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return Conflict(new { message = ex.Message }); }
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id) => await delete.ExecuteAsync(id) ? NoContent() : NotFound();
}
