using System;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.Role;

public interface IRoleRepository
{
    Task<RoleEntity?> GetRoleByIdAsync(Guid id);
    Task<IReadOnlyList<RoleEntity>> GetAllRolesAsync();
    Task AddRoleAsync(RoleEntity role);
    Task RemoveRoleAsync(Guid roleId);
    Task<bool> RoleExistsAsync(string roleName);
}