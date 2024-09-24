
using Microsoft.EntityFrameworkCore;
using FreshFarm.Domain.Entities;

namespace FreshFarm.Infrastructure.Persistence.DbContexts;

public class FreshFarmDbContext: DbContext
    {
        public FreshFarmDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
