using System;

namespace QiPOS
{
    public sealed class UserAccount
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public int Priority { get; set; }

        public bool IsAdmin => Priority <= 1;
        public bool IsSuperUser => Priority == 0;

        public override string ToString()
        {
            return $"{Name} (Priority: {Priority})";
        }
    }
}
