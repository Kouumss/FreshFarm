using FreshFarm.Domain.Service.User;
using FreshFarm.Domain.Entities;
using FreshFarm.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FreshFarm.Infrastructure.Persistence.Repositories.User;

public class UserRepository : IUserRepository
{
    private readonly FreshFarmDbContext _ctx;

    public UserRepository(FreshFarmDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<UserEntity> AddAsync(UserEntity user)
    {
        await _ctx.Users.AddAsync(user);
        await _ctx.SaveChangesAsync();
        return user;
    }

    public async Task<UserEntity?> GetByIdAsync(Guid userId)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IReadOnlyList<UserEntity>> GetAllAsync()
    {
        return await _ctx.Users.ToListAsync();
    }

    public async Task UpdateAsync(UserEntity user)
    {
        _ctx.Users.Update(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task RemoveAsync(Guid userId)
    {
        var user = await GetByIdAsync(userId);
        if (user != null)
        {
            _ctx.Users.Remove(user);
            await _ctx.SaveChangesAsync();
        }
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}