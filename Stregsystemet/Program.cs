using System;
using System.Collections.Generic;

namespace Stregsystemet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ILogger logger = new StregsystemLogger();
            Stregsystem stregsystem = new Stregsystem(logger);
        }
    }
}