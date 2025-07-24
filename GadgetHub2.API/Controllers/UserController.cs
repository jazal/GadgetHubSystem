using AutoMapper;
using GadgetHub.Dtos.Users;
using GadgetHub2.API.Models;
using GadgetHub2.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace GadgetHub2.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService _repository;
    private readonly IMapper _mapper;

    public UserController(UserService repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto input)
    {
        var user = await _repository.Login(input);
        if (user is null) return Unauthorized();

        return Ok(user);
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var distributors = await _repository.GetAll();
        return Ok(_mapper.Map<List<UserDto>>(distributors));
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        await _repository.Add(user);
        return Ok(_mapper.Map<UserDto>(user));
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var user = await _repository.GetById(id);
        if (user == null) return NotFound();

        _mapper.Map(dto, user);
        await _repository.Update(user);
        return Ok(_mapper.Map<UserDto>(user));
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _repository.GetById(id);
        if (user == null) return NotFound();

        await _repository.Delete(id);
        return NoContent();
    }
}

