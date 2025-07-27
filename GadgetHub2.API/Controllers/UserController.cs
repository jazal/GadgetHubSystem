using AutoMapper;
using GadgetHub.API.Data;
using GadgetHub.API.Models;
using GadgetHub.API.Repositories;
using GadgetHub.Dtos.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly GadgetHubContext _context;
    private readonly UserService _userRepo;
    private readonly IMapper _mapper;

    public UserController(
        GadgetHubContext context,
        UserService repository, IMapper mapper)
    {
        _context = context;
        _userRepo = repository;
        _mapper = mapper;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto input)
    {
        var user = await _userRepo.Login(input);
        if (user is null) return Unauthorized();

        return Ok(user);
    }

    // GET: api/User
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _context.Users.ToListAsync();
        return Ok(_mapper.Map<List<UserDto>>(users));
    }

    // GET: api/User/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return NotFound();
        return Ok(_mapper.Map<UserDto>(user));
    }

    // POST: api/User
    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto dto)
    {
        var user = _mapper.Map<User>(dto);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<UserDto>(user));
    }

    // PUT: api/User/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateUserDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return NotFound();

        _mapper.Map(dto, user);

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(_mapper.Map<UserDto>(user));
    }

    // DELETE: api/User/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user == null) return NotFound();

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

