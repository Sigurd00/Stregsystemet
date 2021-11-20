using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace Stregsystemet.Tests
{
    public class TransactionTests : IDisposable
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(10, 10)]
        public void Transaction_ID_Increments_From_Static_Value(int numberOfTransactions, int expectedTransactionID)
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            List<Transaction> transactions = new List<Transaction>();
            for (var i = 0; i < numberOfTransactions; i++)
            {
                transactions.Add(new InsertCashTransaction(user, 123));
            }
            Assert.Equal(expectedTransactionID, transactions[transactions.Count - 1].ID);
        }
        [Fact]
        public void InsertCashTransaction_IsA_Transaction()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Transaction transaction = new InsertCashTransaction(user, 123);
            Assert.IsAssignableFrom<Transaction>(transaction);
        }

        [Fact]
        public void InsertCashTransaction_NegativeCash_throws()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            
            Assert.Throws<ArgumentException>(() =>new InsertCashTransaction(user, -123));
        }

        [Fact]
        public void InsertCashTransaction_Increases_users_balance_on_execute()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Transaction transaction = new InsertCashTransaction(user, 123);
            transaction.Execute();
            Assert.Equal(123, user.Balance);
        }
        [Fact]
        public void InsertCashTransaction_ToString_returns_correctly()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Transaction transaction = new InsertCashTransaction(user, 123);
            Assert.Equal($"INDBETALING: 123, Test Name <test.name@gmail.com>, {DateTime.Now} <1>", transaction.ToString());
        }
        [Fact]
        public void BuyTransaction_IsA_Transaction()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 0);
            Product product = new Product(1, "Diverse", 100, true);
            Transaction transaction = new BuyTransaction(user, product);
            Assert.IsAssignableFrom<Transaction>(transaction);
        }

        [Fact]
        public void BuyTransaction_Execute_Removes_Value_From_Users_balance()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 500);
            Product product = new Product(1, "Diverse", 100, true);
            Transaction transaction = new BuyTransaction(user, product);
            transaction.Execute();
            Assert.Equal(400, user.Balance);
        }
        [Fact]
        public void BuyTransaction_Execute_throws_Exception_on_InsufficientCredits()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 000);
            Product product = new Product(1, "Diverse", 100, true);
            Transaction transaction = new BuyTransaction(user, product);
            Assert.Throws<InsufficientCreditsException>(() => transaction.Execute());
        }
        [Fact]
        public void BuyTransaction_Execute_throws_Exception_on_Inactive_Product()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 500);
            Product product = new Product(1, "Diverse", 100, false);
            Transaction transaction = new BuyTransaction(user, product);
            Assert.Throws<InactiveProductException>(() => transaction.Execute());
        }

        [Fact]
        public void BuyTransaction_ToString_returns_correctly()
        {
            User user = new User("Test", "Name", "valid_username", "test.name@gmail.com", 500);
            Product product = new Product(1, "Diverse", 100, false);
            Transaction transaction = new BuyTransaction(user, product);
            Assert.Equal($"KØB: 100, Test Name <test.name@gmail.com>, 1 Diverse 100, {DateTime.Now} <1>", transaction.ToString());
        }

        public void Dispose()
        {
            FieldInfo? transactionIDField = typeof(Transaction).GetField("s_ID", BindingFlags.Static | BindingFlags.NonPublic);
            if (transactionIDField != null)
            {
                transactionIDField.SetValue(null, 0);
            }
        }
    }
}
