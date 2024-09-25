
using Microsoft.EntityFrameworkCore;
using FreshFarm.Domain.Entities;
using System.Security.Cryptography;
using System.Text;

namespace FreshFarm.Infrastructure.Persistence.DbContexts;

public class FreshFarmDbContext : DbContext
{
    public FreshFarmDbContext(DbContextOptions<FreshFarmDbContext> options) : base(options)
    {
    }

    // Constructeur sans paramètre pour les migrations
    public FreshFarmDbContext() : base(new DbContextOptions<FreshFarmDbContext>())
    {
    }
    public async Task InitializeDataAsync()
    {
        if (!Users.Any())
        {
            var usersToAdd = new List<(string FirstName, string LastName, string Email, string Password)>
        {
            ("John", "Doe", "john.doe@example.com", "password1"),
            ("Jane", "Smith", "jane.smith@example.com", "password2"),
            ("Mike", "Johnson", "mike.johnson@example.com", "password3"),
            ("Emily", "Davis", "emily.davis@example.com", "password4")
        };

            foreach (var userInfo in usersToAdd)
            {
                var existingUser = Users.FirstOrDefault(u => u.Email == userInfo.Email);

                if (existingUser is null)
                {
                    // Utilisation de la méthode GeneratePassword pour créer le hash et le salt
                    GeneratePassword(userInfo.Password, out byte[] passwordHash, out byte[] passwordSalt);

                    // Création de l'utilisateur
                    var newUser = UserEntity.Create(userInfo.FirstName, userInfo.LastName, userInfo.Email, passwordHash, passwordSalt);
                    Users.Add(newUser);
                }
            }

            await SaveChangesAsync();
        }
    }


    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    private void GeneratePassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        passwordSalt = new byte[16];
        RandomNumberGenerator.Fill(passwordSalt);

        using SHA512 sha512 = SHA512.Create();

        var saltedPassword = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(passwordSalt));
        passwordHash = sha512.ComputeHash(saltedPassword);
    }
}
