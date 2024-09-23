
namespace FreshFarm.Domain.Entities;

    public class UserEntity : BaseEntity
    {
        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        #endregion

        #region Constructor
        public UserEntity(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
        }
        #endregion

        #region Factory Method
        public static UserEntity Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            return new UserEntity(firstName, lastName, email, passwordHash, passwordSalt);
        }
        #endregion
    }


