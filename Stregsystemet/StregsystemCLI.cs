using System;
using System.Collections.Generic;

namespace Stregsystemet
{
    public class StregsystemCLI : IStregsystemUI
    {
        private bool _running;
        private IStregsystem _stregsystem;
        public event StregsystemEvent? CommandEntered;
        public StregsystemCLI(IStregsystem stregsystem)
        {
            _stregsystem = stregsystem;
            _running = false;
        }

        public void Close()
        {
            _running = false;
        }

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            Console.WriteLine($"Could not find the admin command:{adminCommand}");
        }

        public void DisplayGeneralError(string errorString)
        {
            Console.WriteLine($"The system encountered an error: {errorString}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            Console.WriteLine($"{user.Username} does not have enough fonds to buy {product.Name}, please make sure you have sufficient funds");
        }

        public void DisplayProductNotFound(string product)
        {
            Console.WriteLine($"Could not find product: {product}");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            Console.WriteLine($"Too many arguments: {command}");
        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            Console.WriteLine(transaction.ToString());
        }

        public void DisplayUserBuysProduct(int count, BuyTransaction transaction)
        {
            Console.WriteLine($"{transaction} x{count}");
        }

        public void DisplayUserInfo(User user, IEnumerable<Transaction> transactions)
        {
            Console.WriteLine($"{user}, {user.Balance}");
            foreach (Transaction transaction in transactions)
            {
                Console.WriteLine(transaction.ToString());
            }
            
        }

        public void DisplayUserNotFound(string username)
        {
            Console.WriteLine($"[{username}] not found");
        }

        public void DisplayActiveProducts()
        {
            Console.WriteLine("List of all active products:");
            foreach (Product product in _stregsystem.ActiveProducts)
            {
                Console.WriteLine(product);
            }
        }

        public void Start()
        {
            string? command;
            _running = true;
            DisplayActiveProducts();
            while (_running)
            {
                command = Console.ReadLine();
                if (command is not null)
                {
                    OnCommandEntered(command);
                }
            }
        }

        protected virtual void OnCommandEntered(string command)
        {
            if(CommandEntered != null)
            {
                CommandEntered(command);
            }
        }
    }
}