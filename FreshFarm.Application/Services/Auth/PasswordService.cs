using System.Security.Cryptography;
using System.Text;
using FreshFarm.Domain.Service.Auth;

namespace FreshFarm.Application.Services.Auth;

public class PasswordService : IPasswordService
{
    public void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        passwordSalt = new byte[16];
        RandomNumberGenerator.Fill(passwordSalt);

        using SHA512 sha512 = SHA512.Create();

        var saltedPassword = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(passwordSalt));
        passwordHash = sha512.ComputeHash(saltedPassword);
    }

    public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
    {
        using SHA512 sha512 = SHA512.Create();
        var saltedPassword = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(storedSalt));
        var computedHash = sha512.ComputeHash(saltedPassword);

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != storedHash[i])
            {
                return false;
            }
        }

        return true;
    }
}