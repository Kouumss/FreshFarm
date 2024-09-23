namespace FreshFarm.Contract.Service.Auth;
public interface IPasswordService
{
    void CreateHashPassword(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt);
}