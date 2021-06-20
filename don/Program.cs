using DON.Command;
using DON.Constent;
using DON.Model;
using DON.Param;
using DON.Utils;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

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

            //bool isTimingReward = args != null && args.Length > 0 && args[0] == "reward";
            //if (isTimingReward)
            //{
            //    Console.WriteLine("开始获取奖励");
            //    GetReward();
            //    Console.WriteLine("结束获取奖励");
            //    Console.WriteLine("开始兑换DON");
            //    Console.WriteLine(SwapAllDon2Husd());
            //    Console.WriteLine("结束兑换DON");
            //}
            //else
            //{
            //    Console.WriteLine("开始检查合约是否修改");
            //    while (true)
            //    {
            //        bool isUpdateExchangeContract = CheckExchangeContractUpdate();
            //        if (isUpdateExchangeContract)
            //        {
            //            ExitStake();
            //        }
            //        else
            //        {
            //            Console.WriteLine(DateTime.Now.ToString() + " no change.");
            //        }
            //        Thread.Sleep(5 * 1000);
            //    }
            //}
        }

        static void PrintHelp()
        {
            Console.WriteLine(CommandHelper.GetHelp());
        }

        ///// <summary>
        ///// 提现全部挖矿收益
        ///// </summary>
        //static void GetReward()
        //{
        //    Console.WriteLine(GetReward(LiquidityModel.IWBNB_HUSD));
        //    Console.WriteLine(GetReward(LiquidityModel.DON_HUSD));
        //    Console.WriteLine(GetReward(LiquidityModel.IWBNB));
        //}





        //private static ContractModel exchangeContract;



        ////设置滑点为0.5%
        //private const double SlippageTolerance = 0.005;

        ///// <summary>
        ///// 兑换所有don到husd
        ///// </summary>
        ///// <returns></returns>
        //static string SwapAllDon2Husd()
        //{
        //    //获取账户DON个数
        //    BalanceModel balanceModel = GetTokenBalance(Account, LPConstent.DON_NAME);
        //    if (balanceModel == null)
        //    {
        //        return "no balance";
        //    }
        //    if (balanceModel.balance < 1)
        //    {
        //        return "balance min 1!";
        //    }
        //    double balance = balanceModel.balance;
        //    //获取总流动性
        //    LiquidityModel liquidity = GetLiquidityAmount(LiquidityModel.DON_HUSD);
        //    //计算当前价格
        //    double price = liquidity.symbol2.value / liquidity.symbol1.value;
        //    double minimumReceived = balance * price * (1 - SlippageTolerance);
        //    return SwapTokens(LPConstent.DON_NAME, LPConstent.HUSD_NAME, balance, minimumReceived);
        //}
    }
}
