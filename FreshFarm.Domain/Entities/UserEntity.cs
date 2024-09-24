namespace FreshFarm.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        #region Properties
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        public DateTime LastLoginDate { get; private set; }
        public bool IsEmailVerified { get; private set; }

        #endregion

        #region Constructor
        private UserEntity(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            LastLoginDate = DateTime.UtcNow;
            IsEmailVerified = false;
        }

        #endregion

        #region Factory Method
        public static UserEntity Create(string firstName, string lastName, string email, byte[] passwordHash, byte[] passwordSalt)
        {
            return new UserEntity(firstName, lastName, email, passwordHash, passwordSalt);
        }
        #endregion

        #region Methods
        public void UpdatePassword(byte[] newHash, byte[] newSalt)
        {
            PasswordHash = newHash;
            PasswordSalt = newSalt;
        }

        public void VerifyEmail()
        {
            IsEmailVerified = true;
        }

        public void UpdateLastLoginDate()
        {
            LastLoginDate = DateTime.UtcNow;
        }

        public void UpdateUserDetails(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }
        #endregion
    }
}


