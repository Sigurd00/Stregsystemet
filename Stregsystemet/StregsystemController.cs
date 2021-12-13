using System;
using System.Collections.Generic;

namespace Stregsystemet
{
    public partial class StregsystemController
    {
        IStregsystemUI _ui;
        IStregsystem _stregsystem;
        public enum CommandType
        {
            None = 0,
            UsernameLookup = 1,
            BuyProduct = 2,
            MultiBuyProduct = 3,
        }
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
        }

        public void OnCommandEntered(string command)
        {
            ParseCommand(command);
        }

        public void OnUserBalanceWarning(User user, decimal threshold)
        {

        }

        private void ParseCommand(string command)
        {
            string[] commandArgs = command.Split(' ');

            switch (commandArgs.Length)
            {
                case (int) CommandType.None:
                    _ui.DisplayGeneralError("Did you just press space? or enter?");
                    break;
                case (int) CommandType.UsernameLookup:
                    HandleUsernameLookup(commandArgs[0]);
                    break;
                case (int) CommandType.BuyProduct:
                    HandleBuyProduct(commandArgs[0], commandArgs[1]);
                    break;
                case (int) CommandType.MultiBuyProduct:
                    break;
                default:
                    _ui.DisplayTooManyArgumentsError(command);
                    break;
            }
        }

        private void HandleUsernameLookup(string username)
        {
            try
            {
                User user = _stregsystem.GetUserByUsername(username);
                IEnumerable<Transaction> transactions = _stregsystem.GetTransactions(user, 10);
                _ui.DisplayUserInfo(user, transactions);
            }
            catch (Exception)
            {
                _ui.DisplayUserNotFound(username);
            }
        }

        private void HandleBuyProduct(string username, string productID)
        {

        }
    }
}