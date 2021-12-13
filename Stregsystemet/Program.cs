using System;
using System.Collections.Generic;

namespace Stregsystemet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger logger = new StregsystemLogger();
            IStregsystem stregsystem = new Stregsystem(logger);
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);

            stregsystem.UserBalanceWarning += sc.OnUserBalanceWarning;
            ui.CommandEntered += sc.OnCommandEntered;
            ui.CommandEntered += logger.OnCommandEntered;
            ui.Start();
        }
    }
}