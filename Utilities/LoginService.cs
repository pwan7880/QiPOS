using System;

namespace QiPOS
{
    public sealed class LoginService
    {
        private readonly UserRepository userRepository;

        public LoginService()
        {
            userRepository = new UserRepository();
        }

        public UserAccount Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;

            UserAccount user = userRepository.GetUserByName(username);
            if (user == null)
                return null;

            return PasswordHasher.VerifyPassword(password, user.PasswordHash) ? user : null;
        }
    }
}
