using DON.Model;
using DON.Param;
using DON.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DON.Command
{
    public class StakingCommand : BaseCommand
    {
        public const string command = "staking";

        public const string desc = "质押挖矿";

        public static void Run(string command)
        {
            string[] inputTokens = DonConfig.Staking;
            List<TokenBaseInfo> inputTokenInfo = DonConfig.Tokens.Where(m => inputTokens.Contains(m.name)).ToList();
            List<TokenBaseInfo> inputLPTOkenInfo = DonConfig.LPTokens.Where(m => inputTokens.Contains(m.name)).ToList();
            switch (command)
            {
                case "exit":
                    foreach (TokenBaseInfo item in inputTokenInfo)
                    {
                        Console.WriteLine("正在退出" + item.name + "(" + item.contractAddress + ")的抵押");
                        Console.WriteLine(ContractExit(item));
                    }
                    foreach (TokenBaseInfo item in inputLPTOkenInfo)
                    {
                        Console.WriteLine("正在退出" + item.name + "(" + item.contractAddress + ")的抵押");
                        Console.WriteLine(ContractExit(item));
                        Console.WriteLine("正在退出" + item.name + "(" + item.contractAddress + ")流动性");
                        Console.WriteLine(WithdrawLiquidityWithLp(item.name));
                    }
                    break;
                case "getReward":
                    foreach (TokenBaseInfo item in inputTokenInfo)
                    {
                        Console.WriteLine("正在获取" + item.name + "(" + item.contractAddress + ")的奖励");
                        Console.WriteLine(GetReward(item.contractAddress));
                    }
                    foreach (TokenBaseInfo item in inputLPTOkenInfo)
                    {
                        Console.WriteLine("正在获取" + item.name + "(" + item.contractAddress + ")的奖励");
                        Console.WriteLine(GetReward(item.contractAddress));
                    }
                    break;
                case "getBalance":
                    foreach (TokenBaseInfo item in inputTokenInfo)
                    {
                        double balance = GetStakingBalance(item.contractAddress);
                        Console.WriteLine(item.name + "抵押数量：" + balance);
                    }
                    foreach (TokenBaseInfo item in inputLPTOkenInfo)
                    {
                        double balance = GetStakingBalance(item.contractAddress);
                        Console.WriteLine(item.name + "抵押数量：" + balance);
                    }
                    break;
                case "bnbWithdraw":
                    foreach (TokenBaseInfo item in inputTokenInfo)
                    {
                        double balance = GetStakingBalance(item.contractAddress);
                        Console.WriteLine(item.name + "抵押数量：" + balance);
                    }
                    foreach (TokenBaseInfo item in inputLPTOkenInfo)
                    {
                        Console.WriteLine("正在获取" + item.name + "(" + item.contractAddress + ")的奖励");
                        Console.WriteLine(GetReward(item.contractAddress));
                    }
                    break;
            }
        }

        /// <summary>
        /// 提取流动性
        /// </summary>
        /// <param name="lp">流动性交易对</param>
        /// <returns></returns>
        public static string WithdrawLiquidityWithLp(string token)
        {
            string[] cols = token.Split("_");
            string token1 = cols[0], token2 = cols[1];
            string tokenName = token.Replace("_", "");
            BalanceModel balance = BalanceCommand.GetTokenBalance(DonConfig.Account, tokenName);
            if (balance == null)
            {
                return "balance null";
            }
            string[] data = new string[] { token1, token2, tokenName, balance.balance.ToString() };
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, DonConfig.SwapAddress, "withdrawLiquidityWithLp", data);
        }

        /// <summary>
        /// 获取账户余额
        /// </summary>
        /// <param name="contract">智能合约地址</param>
        public static double GetStakingBalance(string contractAddress)
        {
            List<KeyFieldParam> keyFields = new List<KeyFieldParam>();
            keyFields.Add(new KeyFieldParam("userbalance", DonConfig.Account));
            ContractStorageModel model = IOSTApiUtils.GetBatchContractStorage(contractAddress, keyFields.ToArray());
            string data = model.datas[0];
            return NumberUtils.Parse(data);
        }

        /// <summary>
        /// 退出抵押
        /// </summary>
        /// <param name="lp"></param>
        internal static string ContractExit(TokenBaseInfo tokenInfo)
        {
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, tokenInfo.contractAddress, "exit", null);
        }

        /// <summary>
        /// 将BNB提现到钱包
        /// </summary>
        /// <returns></returns>
        internal static string IWBNBSwapWithdraw()
        {
            TokenBaseInfo tokenInfo = DonConfig.Tokens.FirstOrDefault(m => m.name == "iwbnb");
            if (tokenInfo == null)
            {
                return "请配置bnb的合约地址";
            }
            double amount = GetStakingBalance(tokenInfo.contractAddress);
            if (amount < 0.001)
            {
                return "amount<0.001";
            }
            string[] data = new string[] { amount.ToString(), DonConfig.BscAddress };
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, "Contract6ZPp62E2J4bChdQkvCEZTozLHC2GcefdxpWnj647DjGJ", "swapWithdraw", data);
        }

        /// <summary>
        /// 获取奖励
        /// </summary>
        /// <param name="lp"></param>
        internal static string GetReward(string contractAddress)
        {
            double balance = StakingCommand.GetStakingBalance(contractAddress);
            if (balance < 0.001)
            {
                return "未抵押或抵押数量小于0.001";
            }
            return IWalletCommand.Call(DonConfig.IWalletServer, DonConfig.Account, contractAddress, "getReward", null);
        }
    }
}
