using System.Collections.Generic;

namespace Stregsystemet
{
    public delegate void StregsystemEvent(string command);
    public interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayUserInfo(User user, IEnumerable<Transaction> transactions);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayUserBuysProduct(int count, BuyTransaction transaction);
        void Close();
        void DisplayInsufficientCash(string message);
        void DisplayGeneralError(string errorString);
        void DisplayActiveProducts();
        void DisplayCashWarning(User user, decimal balance);
        void Start();
        event StregsystemEvent CommandEntered;
    }
}