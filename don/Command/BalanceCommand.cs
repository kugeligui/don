using DON.Model;
using DON.Param;
using DON.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace DON.Command
{
    public class BalanceCommand : BaseCommand
    {
        public const string command = "balance";

        public const string desc = "查看账户信息";

        public static void Run(string command)
        {
            switch (command)
            {
                case "userBalance":
                    string[] tokens = DonConfig.InputTokens;
                    Console.WriteLine("代币\t余额");
                    foreach (string token in tokens)
                    {
                        BalanceModel model = GetTokenBalance(DonConfig.Account, token);
                        Console.WriteLine(token + ":\t" + model.balance.ToString("N"));
                    }
                    break;
            }
        }

        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="account">账号名</param>
        /// <param name="token">代币名字</param>
        /// <returns>账户余额信息</returns>
        public static BalanceModel GetTokenBalance(string account, string token)
        {
            List<KeyFieldParam> keyFields = new List<KeyFieldParam>();
            keyFields.Add(new KeyFieldParam("userbalance", account));
            BalanceModel model = IOSTApiUtils.GetTokenBalance(account, token);
            return model;
        }
    }
}
