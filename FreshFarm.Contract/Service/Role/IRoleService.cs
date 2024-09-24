using FreshFarm.Domain.Entities;

namespace FreshFarm.Contract.Service.Role;

public interface IRoleService
    {
        Task<RoleEntity?> GetRoleByIdAsync(Guid roleId);
        Task<IReadOnlyList<RoleEntity>> GetAllRolesAsync();
        Task AddRoleAsync(string roleName, Guid adminId);
        Task RemoveRoleAsync(Guid roleId, Guid adminId);
        Task<bool> RoleExistsAsync(string roleName);
    }