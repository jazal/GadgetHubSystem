using GadgetHub.API.Base;
using GadgetHub.API.Models;
using GadgetHub.Dtos.Users;
using Microsoft.EntityFrameworkCore;

namespace GadgetHub.API.Repositories;

public class UserService
{
    private readonly IBaseRepository<User> _repo;

    public UserService(IBaseRepository<User> repo)
    {
        _repo = repo;
    }

    public Task<List<User>> GetAll() => _repo.GetAllAsync();
    public Task<User?> GetById(int id) => _repo.GetByIdAsync(id);
    public Task Add(User user) => _repo.AddAsync(user);
    public Task Update(User user) => _repo.UpdateAsync(user);
    public Task Delete(int id) => _repo.DeleteAsync(id);

    public async Task<UserDto> Login(LoginDto input)
    {
        var user = await _repo.GetAll()
            .Where(x => x.Email.Equals(input.Email) && x.Password == input.Password)
            .FirstOrDefaultAsync();

        if (user is null) return null;

        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            UserType = user.UserType
        };
    }
}
