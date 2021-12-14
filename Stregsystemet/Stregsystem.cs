using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Stregsystemet
{
    public class Stregsystem : IStregsystem
    {
        public IEnumerable<Product> ActiveProducts
        {
            get
            {
                return _products.Where(p => p.Active);
            }
        }
        private List<Transaction> _executedTransactions;
        private List<Product> _products;
        private List<User> _users;
        public event UserBalanceNotification? UserBalanceWarning;

        private ILogger _logger;
        public Stregsystem(ILogger logger)
        {
            _logger = logger;
            _products = new List<Product>();
            _users = new List<User>();
            PopulateProducts();
            PopulateUsers();
            _executedTransactions = new List<Transaction>();
        }

        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction transaction = new InsertCashTransaction(user, amount);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction transaction = new BuyTransaction(user, product);
            ExecuteTransaction(transaction);
            return transaction;
        }

        public Product GetProductById(int id)
        {
            Product? product = _products.Find(product => product.ID == id);
            if (product == null)
            {
                throw new ArgumentException($"Could not find product with id={id}");
            }
            return product;
        }

        public IEnumerable<Transaction> GetTransactions(User user, int count)
        {
            IEnumerable<Transaction> transactions = _executedTransactions
                .Where((transaction) => transaction.User == user)
                .OrderByDescending(transaction => transaction.ID)
                .Take(count);

            return transactions;
        }

        public User GetUserByUsername(string username)
        {
            return GetUser((user) => user.Username == username);
        }

        public User GetUser(Func<User, bool> predicate)
        {
            User? user = _users.Find(user => predicate(user));
            if (user == null)
            {
                throw new UserNotFoundException("Could not find user with given predicate");
            }
            return user;
        }
        private void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                transaction.Execute();
                _executedTransactions.Add(transaction);
                _logger.Info(transaction.ToString());
            }
            catch (InsufficientCreditsException ex)
            {
                _logger.Warn(ex.Message);
                throw ex;
            }
            catch (InactiveProductException ex)
            {
                _logger.Warn(ex.Message);
                throw ex;
            }
            catch (ArgumentException ex)
            {
                _logger.Error($"Transaction failed:{ex.Message}");
                throw ex;
            }
        }
        private void PopulateProducts()
        {
            using (var reader = new StreamReader(@"./products.csv"))
            {
                //Førset linje er beskrivelse af fields, så discard
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if (line == null)
                    {
                        throw new IOException("Something is burning");
                    }
                    string[] fields = line.Split(';');
                    int id = int.Parse(fields[0]);
                    string name = ParseProductName(fields[1]);
                    decimal price = decimal.Parse(fields[2]);
                    bool active = fields[3] == "0" ? false : true;
                    _products.Add(new Product(id, name, price, active));

                }
            }
        }
        
        private void PopulateUsers()
        {
            using (var reader = new StreamReader(@"./users.csv"))
            {
                //Førset linje er beskrivelse af fields, så discard
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    if(line == null)
                    {
                        throw new IOException("Something is burning");
                    }
                    string[] fields = line.Split(',');
                    int id = int.Parse(fields[0]);
                    string firstName = fields[1];
                    string lastName = fields[2];
                    string username = fields[3];
                    decimal balance = decimal.Parse(fields[4]);
                    string email = fields[5];
                    User user = new User(id, firstName, lastName, username, email, balance);
                    user.UserBalanceWarning += OnUserBalanceWarning;
                    _users.Add(user);
                }
            }
        }

        protected virtual void OnUserBalanceWarning(User user, decimal balance)
        {
            //If any subscribers
            if(UserBalanceWarning is not null)
            {
                UserBalanceWarning(user, balance);
            }
        }

        private string ParseProductName(string productName)
        {
            //if (productName.Contains(' '))
            //{
            //    productName = $"\{productName}\"";
            //}
            //Fjern html tags
            //Pattern er: find '<' og match 0 eller flere efterfølgende til vi finder '>'
            return Regex.Replace(productName, @"<[^>]*>", String.Empty);
        }
    }
}