using System;
using System.Collections.Generic;
using System.Linq;

namespace Stregsystemet
{
    public class StregsystemController
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
        private char _adminOperator = ':';
        private Dictionary<string, Action<string[]>> _adminCommands;
        public StregsystemController(IStregsystemUI ui, IStregsystem stregsystem)
        {
            _ui = ui;
            _stregsystem = stregsystem;
            _adminCommands = new Dictionary<string, Action<string[]>>
            {
                {":q",          QuitProgram},
                {":quit",       QuitProgram},
                {":activate",   ActivateProduct },
                {":deactivate", DeactivateProduct},
                {":crediton",   CreditOnProduct},
                {":creditoff",  CreditOffProduct},
                {":addcredits", AddCreditsUser},
            };

        }

        public void OnCommandEntered(string command)
        {
            ParseCommand(command);
        }

        public void OnUserBalanceWarning(User user, decimal balance)
        {
            _ui.DisplayCashWarning(user, balance);
        }

        private void ParseCommand(string command)
        {
            command = command.Trim();
            string[] commandArgs = command.Split(' ');
            try
            {
                ExecuteCommand(command, commandArgs);
            }
            catch (UserNotFoundException)
            {
                _ui.DisplayUserNotFound(commandArgs[0]);
            }
            catch (FormatException)
            {
                _ui.DisplayGeneralError("Id was not a number");
            }
            catch (ArgumentException)
            {
                _ui.DisplayProductNotFound($"Could not find product with ID: {commandArgs[1]}");
            }
            catch (InsufficientCreditsException ex)
            {
                _ui.DisplayInsufficientCash(ex.Message);
            }
            catch (InactiveProductException)
            {
                _ui.DisplayProductNotFound(commandArgs[1]);
            }
            catch(KeyNotFoundException ex)
            {
                _ui.DisplayAdminCommandNotFoundMessage(ex.Message);
            }
            catch(Exception ex)
            {
                _ui.DisplayGeneralError(ex.Message);
            }

        }

        private void ExecuteCommand(string command, string[] commandArgs)
        {
            if (command.StartsWith(_adminOperator))
            {
                ExecuteAdminCommand(command, commandArgs.Skip(1).ToArray());
            }
            else
            {
                switch (commandArgs.Length)
                {
                    case (int)CommandType.None:
                        _ui.DisplayGeneralError("Command cannot be empty BAKA");
                        break;
                    case (int)CommandType.UsernameLookup:
                        HandleUsernameLookup(commandArgs[0]);
                        break;
                    case (int)CommandType.BuyProduct:
                        HandleBuyProduct(commandArgs[0], commandArgs[1]);
                        break;
                    case (int)CommandType.MultiBuyProduct:
                        HandleMultiBuyProduct(commandArgs[0], commandArgs[1], commandArgs[2]);
                        break;
                    default:
                        _ui.DisplayTooManyArgumentsError(command);
                        break;
                }
            }

        }

        private void ExecuteAdminCommand(string command, string[] args)
        {
            string[] commandArgs = command.Split(' ');
            _adminCommands[commandArgs[0]](args);
        }

        private void HandleUsernameLookup(string username)
        {
            try
            {
                User user = _stregsystem.GetUserByUsername(username);
                IEnumerable<Transaction> transactions = _stregsystem.GetTransactions(user, 10);
                _ui.DisplayUserInfo(user, transactions);
            }
            catch (UserNotFoundException)
            {
                _ui.DisplayUserNotFound(username);
            }
        }

        private void HandleBuyProduct(string username, string productID)
        {
            int intID = int.Parse(productID);
            User user = _stregsystem.GetUserByUsername(username);
            Product product = _stregsystem.GetProductById(intID);
            BuyTransaction transaction = _stregsystem.BuyProduct(user, product);
            _ui.DisplayUserBuysProduct(transaction);

        }
        private void HandleMultiBuyProduct(string username, string count, string productID)
        {
            int intCount = int.Parse(count);
            int intID = int.Parse(productID);
            User user = _stregsystem.GetUserByUsername(username);
            for (int i = 0; i < intCount; i++)
            {
                Product product = _stregsystem.GetProductById(intID);
                BuyTransaction transaction = _stregsystem.BuyProduct(user, product);
                _ui.DisplayUserBuysProduct(transaction);
            }


        }

        private void QuitProgram(string[] args)
        {
            _ui.Close();
        }

        private void ActivateProduct(string[] args)
        {
            _stregsystem.GetProductById(int.Parse(args[0])).Active = true;
        }

        private void DeactivateProduct(string[] args)
        {
            _stregsystem.GetProductById(int.Parse(args[0])).Active = false;
        }

        private void CreditOnProduct(string[] args)
        {
            _stregsystem.GetProductById(int.Parse(args[0])).CanBeBoughtOnCredit = true;
        }

        private void CreditOffProduct(string[] args)
        {
            _stregsystem.GetProductById(int.Parse(args[0])).CanBeBoughtOnCredit = false;
        }

        private void AddCreditsUser(string[] args)
        {
            _stregsystem.AddCreditsToAccount(_stregsystem.GetUserByUsername(args[0]), decimal.Parse(args[1]));
        }
    }
}