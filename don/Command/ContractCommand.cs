using DON.Model;
using DON.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DON.Command
{
    public class ContractCommand : BaseCommand
    {
        public const string command = "contract";

        public const string desc = "合约";

        private static Dictionary<string, ContractModel> storeContract = new Dictionary<string, ContractModel>();

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="command"></param>
        public static void Run(string command)
        {
            switch (command)
            {
                case "checkUpdate":
                default:
                    List<string> addresss = new List<string>();
                    addresss.Add(DonConfig.SwapAddress);
                    string[] inputTokens = DonConfig.InputTokens;
                    List<TokenBaseInfo> lptokens = DonConfig.LPTokens;
                    List<TokenBaseInfo> tokens = DonConfig.Tokens;
                    foreach (string token in inputTokens)
                    {
                        TokenBaseInfo lpTokenInfo = lptokens.FirstOrDefault(m => m.name == token);
                        if (lpTokenInfo != null)
                        {
                            addresss.Add(lpTokenInfo.contractAddress);
                        }
                        else
                        {
                            TokenBaseInfo tokenInfo = tokens.FirstOrDefault(m => m.name == token);
                            if (tokenInfo != null)
                            {
                                addresss.Add(tokenInfo.contractAddress);
                            }
                        }
                    }
                    while (true)
                    {
                        foreach (string address in addresss)
                        {
                            bool isUpdate = CheckExchangeContractUpdate(address);
                            if (isUpdate)
                            {
                                Console.WriteLine(DateTime.Now + " " + address + " 合约已更改，即将退出抵押");

                                return;
                            }
                            else
                            {
                                Console.WriteLine(DateTime.Now + " " + address + " 合约未更改");
                            }
                        }
                        Thread.Sleep(DonConfig.Interval * 1000);
                    }
            }
        }

        /// <summary>
        /// 检查交换合约是否更新
        /// </summary>
        /// <returns></returns>
        public static bool CheckExchangeContractUpdate(string contractAddress)
        {
            ContractModel model = IOSTApiUtils.GetContract(contractAddress);
            if (!storeContract.ContainsKey(contractAddress))
            {
                storeContract[contractAddress] = model;
            }
            ContractModel contract = storeContract[contractAddress];
            return contract.code != model.code || contract.version != model.version;
        }
    }
}
