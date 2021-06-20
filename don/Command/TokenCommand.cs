using DON.Model;
using DON.Utils;
using Newtonsoft.Json;
using System;

namespace DON.Command
{
    public class TokenCommand : BaseCommand
    {
        public const string command = "token";

        public const string desc = "代币";

        public static void Run(string command)
        {
            switch (command)
            {
                default:
                case "getTokenInfo":
                    string[] tokens = DonConfig.InputTokens;
                    Console.WriteLine("token\ttoken 全称\ttoken 总发行量\ttoken 当前发行量");
                    foreach (string token in tokens)
                    {
                        TokenInfo model = GetTokenBalance(token.Replace("_", "").Replace("iwbnbhusdlp", "bnbhusdlp"));
                        if (model != null)
                        {
                            Console.WriteLine($"{model.symbol}\t{model.full_name}\t{model.total_supply_float}\t{model.current_supply_float}");
                        }
                        else
                        {
                            Console.WriteLine(token + "不存在");
                        }
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
        public static TokenInfo GetTokenBalance(string token)
        {
            TokenInfo model = IOSTApiUtils.getTokenInfo(token);
            return model;
        }
    }
}
