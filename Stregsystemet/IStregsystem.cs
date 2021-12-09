using System;
using System.Collections.Generic;

namespace Stregsystemet
{
    public interface IStregsystem
    {
        IEnumerable<Product> ActiveProducts { get; }
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        Product GetProductById(int id);
        IEnumerable<Transaction> GetTransactions(User user, int count);
        User GetUser(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        event UserBalanceNotification UserBalanceWarning;
    }
}