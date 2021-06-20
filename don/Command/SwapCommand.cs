using DON.Model;
using DON.Param;
using DON.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DON.Command
{
    public class SwapCommand : BaseCommand
    {
        public const string command = "swap";

        public const string desc = "代币交换";

        public static void Run(string command)
        {
            string[] inputTokens = DonConfig.InputTokens;
            List<TokenBaseInfo> inputTokenInfo = DonConfig.Tokens.Where(m => inputTokens.Contains(m.name)).ToList();
            List<TokenBaseInfo> inputLPTOkenInfo = DonConfig.LPTokens.Where(m => inputTokens.Contains(m.name)).ToList();
            switch (command)
            {
                case "getLPAmount":
                    foreach (TokenBaseInfo item in inputLPTOkenInfo)
                    {
                        Console.WriteLine("正在获取" + item.name + "流动性总量");
                        LiquidityModel model = GetLiquidityAmount(item);
                        if (model == null)
                        {
                            Console.WriteLine("获取信息失败");
                        }
                        else
                        {
                            Console.WriteLine(model.symbol1.token + ":" + model.symbol1.value + "\t" + model.symbol2.token + ":" + model.symbol2.value);
                        }
                    }
                    break;
                case "swap":
                    Console.WriteLine("正在将" + DonConfig.Token1 + "兑换为" + DonConfig.Token2, DonConfig.Amount > 0 ? "，兑换数量：" + DonConfig.Amount : "，账户余额全部兑换");
                    Console.WriteLine(SwapToken(DonConfig.Token1, DonConfig.Token2, DonConfig.Amount));
                    break;
            }
        }

        /// <summary>
        /// 获取流动性总量
        /// </summary>
        /// <param name="token">流动性交易对</param>
        /// <param name="contractAddress">合约地址</param>
        /// <returns></returns>
        public static LiquidityModel GetLiquidityAmount(TokenBaseInfo tokenInfo)
        {
            string[] cols = tokenInfo.name.Split("_");
            string symbol1 = cols[0];
            string symbol2 = cols[1];
            List<KeyFieldParam> keyFields = new List<KeyFieldParam>();
            keyFields.Add(new KeyFieldParam(tokenInfo.name.Replace("_lp", ""), "amountData"));
            ContractStorageModel model = IOSTApiUtils.GetBatchContractStorage(DonConfig.SwapAddress, keyFields.ToArray());
            string data = model.datas[0];
            if (data == null || data == "null")
            {
                return null;
            }
            JObject pairs = JObject.Parse(data);
            LiquidityModel lp = new LiquidityModel();
            lp.name = tokenInfo.name;
            lp.contractAddress = tokenInfo.contractAddress;
            lp.symbol1 = new TokenModel(symbol1, NumberUtils.Parse(pairs.GetValue(symbol1).Value<string>()));
            lp.symbol2 = new TokenModel(symbol2, NumberUtils.Parse(pairs.GetValue(symbol2).Value<string>()));
            lp.invariant = double.Parse(pairs.GetValue(nameof(lp.invariant)).Value<string>());
            return lp;
        }

        /// <summary>
        /// SwapTokens
        /// </summary>
        /// <param name="symbol1">待兑换币</param>
        /// <param name="symbol2">目标币种</param>
        /// <param name="amount">兑换数量</param>
        /// <param name="minimumReceived">最小接收币数，低于此数量将兑换失败</param>
        /// <returns></returns>
        private static string SwapToken(string symbol1, string symbol2, double amount)
        {
            //获取账户余额
            BalanceModel balanceModel = BalanceCommand.GetTokenBalance(DonConfig.Account, symbol1);
            if (balanceModel == null)
            {
                return "no balance";
            }
            if (balanceModel.balance < 0.000001)
            {
                return "balance min 0.000001!";
            }
            double balance = balanceModel.balance;
            if (amount == 0)
            {
                amount = balance;
            }
            else if (amount > balance)
            {
                return "当前账户余额（" + balance + "）不足";
            }
            string lptoken1 = symbol1 + "_" + symbol2 + "_lp";
            string lptoken2 = symbol2 + "_" + symbol1 + "_lp";
            TokenBaseInfo lpTokenInfo = DonConfig.LPTokens.FirstOrDefault(m => m.name == lptoken1);
            if (lptoken1 == null)
            {
                lpTokenInfo = DonConfig.LPTokens.FirstOrDefault(m => m.name == lptoken2);
            }
            if (lpTokenInfo == null)
            {
                return "LP not exist!";
            }
            //获取总流动性
            LiquidityModel liquidity = GetLiquidityAmount(lpTokenInfo);
            //计算当前价格
            double price = liquidity.symbol2.value / liquidity.symbol1.value;
            double minimumReceived = amount * price * (1 - DonConfig.SlippageTolerance);
            string[] data = new string[] { symbol1, symbol2, amount.ToString(), minimumReceived.ToString() };
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, DonConfig.SwapAddress, "swapTokens", data);
        }

        /// <summary>
        /// SwapTokens
        /// </summary>
        /// <param name="symbol1">待兑换币</param>
        /// <param name="symbol2">目标币种</param>
        /// <param name="amount">兑换数量</param>
        /// <param name="minimumReceived">最小接收币数，低于此数量将兑换失败</param>
        /// <returns></returns>
        private static string SwapToken(string symbol1, string symbol2, double amount, double minimumReceived)
        {
            string[] data = new string[] { symbol1, symbol2, amount.ToString(), minimumReceived.ToString() };
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, DonConfig.SwapAddress, "swapTokens", data);
        }
    }
}
