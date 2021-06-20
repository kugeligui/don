using DON.Command;
using System;

namespace DON
{
    class Program
    {

        static void Main(string[] args)
        {
            DonConfig.SetConfig(args);
            DonConfig.Token1 = "don";
            DonConfig.Token2 = "husd";
            DonConfig.Amount = 1;
            TokenCommand.Run("swap");
            if (args.Length < 2)
            {
                PrintHelp();
                return;
            }
            DonConfig.SetConfig(args);
            string commond = args != null && args.Length > 0 ? args[0] : null;
            string subCommond = args != null && args.Length > 0 ? args[1] : null;
            switch (commond)
            {
                default:
                    PrintHelp();
                    return;
                case BalanceCommand.command:
                    BalanceCommand.Run(subCommond);
                    break;
            }
        }

        static void PrintHelp()
        {
            Console.WriteLine(CommandHelper.GetHelp());
        }
    }
}
