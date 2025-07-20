using AutoMapper;
using GadgetHub2.API.DTOs.Users;
using GadgetHub2.API.Models;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;

    public UserController(IUserRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var distributors = await _repository.GetAllAsync();
        return Ok(_mapper.Map<List<UserDto>>(distributors));
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        await _repository.AddAsync(user);
        await _repository.SaveAsync();
        return CreatedAtAction(nameof(Get), new { id = user.Id }, _mapper.Map<UserDto>(user));
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        _mapper.Map(dto, user);
        _repository.Update(user);
        await _repository.SaveAsync();
        return NoContent();
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return NotFound();

        _repository.Delete(user);
        await _repository.SaveAsync();
        return NoContent();
    }
}

