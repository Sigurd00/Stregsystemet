using System;
using System.Collections.Generic;
using System.Linq;

namespace Stregsystemet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<User> users = new List<User>()
            {
                new User("Sigurd", "Skadborg", "sigurd", "j.skadborg00@gmail.com", 100),
                new User("Sigurd", "Skadborg", "sigurd", "j.skadborg00@gmail.com", 100),
                new User("Sigurd", "Skadborg", "sigurd", "j.skadborg00@gmail.com", 100),
                new User("Sigurd", "Skadborg", "sigurd", "j.skadborg00@gmail.com", 100),
            };
            foreach (User user  in users)
            {
                Console.WriteLine(user.ID);
            }
        }
    }
}