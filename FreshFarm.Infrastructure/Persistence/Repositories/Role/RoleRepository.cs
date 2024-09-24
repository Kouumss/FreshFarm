using FreshFarm.Contract.Service.Role;
using FreshFarm.Domain.Entities;
using FreshFarm.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FreshFarm.Infrastructure.Persistence.Repositories.Role;


 public class RoleRepository : IRoleRepository
    {
        private readonly FreshFarmDbContext _context;

        public RoleRepository(FreshFarmDbContext context)
        {
            _context = context;
        }

        public async Task<RoleEntity?> GetRoleByIdAsync(Guid id)
        {
            return await _context.Roles.FindAsync(id);
        }

        public async Task<IReadOnlyList<RoleEntity>> GetAllRolesAsync()
        {
            return await _context.Roles.ToListAsync();
        }

        public async Task AddRoleAsync(RoleEntity role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleAsync(Guid roleId)
        {
            var role = await GetRoleByIdAsync(roleId);
            if (role != null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _context.Roles.AnyAsync(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
        }
    }