using System;

namespace Stregsystemet
{
    public class User
    {
        public delegate string UserBalanceNotification(User user, decimal balance);
        public int ID { get; private set; }
        private static int s_ID = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public User(string firstName, string lastName, string username, string email, decimal initialBalance)
        {
            try
            {
                ValidateUsername(username);
                ValidateEmail(email);
                ID = ++s_ID;
                FirstName = firstName;
                LastName = lastName;
                Username = username;
                Email = email;
                Balance = initialBalance;
            }
            catch (InvalidCharacterException ex)
            {
                throw ex;
            }
        }

        private void ValidateUsername(string username)
        {
            foreach (char c in username)
            {
                if (!(char.IsLower(c) || char.IsDigit(c) || c == '_'))
                {
                    throw new InvalidCharacterException($"Found an invalid character: {c}");
                }
            }
        }
        private void ValidateEmail(string email)
        {
            string[] split = email.Split('@');
            if(split.Length == 0 || split.Length > 2)
            {
                throw new InvalidEmailException();
            }
            string local = split[0];
            string domain = split[1];
        }
    }
}