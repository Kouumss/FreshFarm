namespace FreshFarm.Application.Services.Role;

using FreshFarm.Contract.Service.Role;
using FreshFarm.Domain.Entities;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<RoleEntity?> GetRoleByIdAsync(Guid roleId)
    {
        return await _roleRepository.GetRoleByIdAsync(roleId);
    }

    public async Task<IReadOnlyList<RoleEntity>> GetAllRolesAsync()
    {
        return await _roleRepository.GetAllRolesAsync();
    }

    public async Task AddRoleAsync(string roleName, Guid adminId)
    {
        if (await _roleRepository.RoleExistsAsync(roleName))
        {
            throw new Exception("Role already exists.");
        }

        var newRole = RoleEntity.Create(roleName);
        await _roleRepository.AddRoleAsync(newRole);
    }

    public async Task RemoveRoleAsync(Guid roleId, Guid adminId)
    {
        // Assurez-vous que l'administrateur a le droit de supprimer des rôles
        await _roleRepository.RemoveRoleAsync(roleId);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleRepository.RoleExistsAsync(roleName);
    }
}