using System;

namespace FreshFarm.Domain.Entities;

public class RoleEntity : BaseEntity
{
    public string Name { get; private set; }

    #region Constructor
    private RoleEntity(string name)
    {
        Name = name;
    }
    #endregion
    #region Factory Method
    public static RoleEntity Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Role name cannot be empty or null.", nameof(name));

        return new RoleEntity(name);
    }
    #endregion
}
