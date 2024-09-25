using FreshFarm.Domain.Service.Auth;
using FreshFarm.Domain.Entities;
using FreshFarm.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FreshFarm.Infrastructure.Persistence.Repositories.Auth;


public class AuthRepository : IAuthRepository
{
    private readonly FreshFarmDbContext _ctx;

    public AuthRepository(FreshFarmDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddAsync(UserEntity user)
    {
        await _ctx.AddAsync(user);
        await _ctx.SaveChangesAsync();
    }

    public async Task<UserEntity?> GetUserByEmailAsync(string email)
    {
        var result = await _ctx.Set<UserEntity>().FirstOrDefaultAsync(u => u.Email == email);
        return result;
    }
}