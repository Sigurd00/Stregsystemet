using System;

namespace Stregsystemet
{
    public delegate void UserBalanceNotification(User user, decimal balance);
    public class User : IComparable<User>
    {
        public event UserBalanceNotification? UserBalanceWarning;
        public int ID { get; private set; }
        //private static int s_ID = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                ValidateUsername(value);
                _username = value;
            }
        }
        private string _email;

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                ValidateEmail(value);
                _email = value;
            }
        }
        private decimal _balance;
        public decimal Balance
        {
            get
            {
                return _balance;
            }
            set
            {
                _balance = value;
                if (value < 500)
                {
                    OnUserBalanceWarning();
                }
            }
        }
        public User(int id, string firstName, string lastName, string username, string email, decimal initialBalance)
        {
            try
            {
                Username = username;
                Email = email;
                _username = username;
                _email = email;
                //ID = ++s_ID;
                ID = id;
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
            if (split.Length <= 1 || split.Length > 2)
            {
                throw new InvalidEmailException($"Could not split the mail into local and domain parts, found {split.Length - 1} @'s in string");
            }
            string local = split[0];
            string domain = split[1];

            foreach (char c in local)
            {
                if (!(char.IsLetterOrDigit(c) || c == '.' || c == '_' || c == '-'))
                {
                    throw new InvalidCharacterException($"Found an invalid character: {c} in local");
                }
            }
            //Check first and last char in domain
            if (domain[0] == '.' || domain[0] == '-' || domain[domain.Length - 1] == '.' || domain[domain.Length - 1] == '-')
            {
                throw new InvalidCharacterException($"Found an invalid character: Domain must not have dot or dash in the start or end");
            }
            //Check characters between start + 1 and end - 1
            int dots = 0;
            for (int i = 1; i < domain.Length - 1; i++)
            {
                char c = domain[i];
                if (!(char.IsLetterOrDigit(c) || c == '.' || c == '-'))
                {
                    throw new InvalidCharacterException($"Found an invalid character: {c} in domain");
                }
                if (c == '.')
                {
                    dots++;
                }
            }
            if (dots == 0)
            {
                throw new InvalidEmailException("No dots were found in the domain");
            }
        }
        public override string ToString()
        {
            return $"{FirstName} {LastName} <{Email}>";
        }
        public override bool Equals(object? obj)
        {
            return Equals(obj as User);
        }
        public bool Equals(User other)
        {
            return other != null && 
                   ID == other.ID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ID);
        }

        public int CompareTo(User? other)
        {
            return ID.CompareTo(other.ID);
        }
        protected virtual void OnUserBalanceWarning()
        {
            //if any subscribers
            if(UserBalanceWarning is not null)
            {
                UserBalanceWarning(this, Balance);
            }
        }

    }
}