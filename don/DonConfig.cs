using DON.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DON
{
    public class DonConfig
    {
        private static IConfiguration Configuration { get; set; }

        public static string Account = null;

        public static string IWalletServer = null;

        public static string IWalletPath = null;

        public static string BscAddress = null;

        private static List<TokenBaseInfo> _lpTokens = new List<TokenBaseInfo>();

        public static List<TokenBaseInfo> LPTokens
        {
            get
            {
                return _lpTokens;
            }
        }

        private static List<TokenBaseInfo> _tokens = new List<TokenBaseInfo>();

        public static List<TokenBaseInfo> Tokens
        {
            get
            {
                return _tokens;
            }
        }

        public static string[] InputTokens = null;

        public static string[] Staking = null;

        public static string SwapAddress = null;

        public static int Interval = 30;

        public static string Token1 = null;

        public static string Token2 = null;

        public static double Amount = 0;

        /// <summary>
        /// 默认滑点5%
        /// </summary>
        public static double SlippageTolerance = 0.05;

        /// <summary>
        /// 设置配置文件
        /// </summary>
        public static void SetConfig(string[] args)
        {
            //构建Configuration
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("config.json");

            Configuration = builder.Build();
            BscAddress = GetConfigValue(Configuration, "bscAddress");
            Account = GetConfigValue(Configuration, "account");
            IWalletServer = Configuration.GetSection("iwallet").GetSection("server").Value;
            IWalletPath = Configuration.GetSection("iwallet").GetSection("path").Value;
            SwapAddress = Configuration.GetSection("swap").GetSection("contractAddress").Value;
            var staking = Configuration.GetSection("staking").GetChildren();
            List<string> stak = new List<string>();
            foreach (var item in staking)
            {
                stak.Add(item.Value);
            }
            Staking = stak.ToArray();
            int? interval = GetConfigIntValue(Configuration, "interval");
            if (interval.HasValue)
            {
                Interval = interval.Value;
            }
            double? slippageTolerance = GetConfigIntValue(Configuration, "slippageTolerance");
            if (slippageTolerance.HasValue)
            {
                SlippageTolerance = slippageTolerance.Value;
            }
            var tokens = Configuration.GetSection("tokens").GetChildren();
            _tokens.Clear();
            foreach (var item in tokens)
            {
                _tokens.Add(new TokenBaseInfo(item.Key, item.Value));
            }
            var lptokens = Configuration.GetSection("lptokens").GetChildren();
            _lpTokens.Clear();
            foreach (var item in lptokens)
            {
                _lpTokens.Add(new TokenBaseInfo(item.Key, item.Value));
            }
            List<string> inputTokens = new List<string>();
            inputTokens.AddRange(Tokens.Select(m => m.name));
            inputTokens.AddRange(LPTokens.Select(m => m.name));
            InputTokens = inputTokens.ToArray();

            //读取命令行参数
            string key = null;
            foreach (string item in args)
            {
                if (key == null && key.StartsWith("-"))
                {
                    key = item;
                }
                else
                {
                    switch (key)
                    {
                        case "-server":
                            IWalletServer = item;
                            break;
                        case "-account":
                            Account = item;
                            break;
                        case "-token1":
                            Token1 = item;
                            break;
                        case "-token2":
                            Token2 = item;
                            break;
                        case "-amount":
                            Amount = double.Parse(item);
                            break;
                        case "-interval":
                            Interval = int.Parse(item);
                            break;
                        case "-SlippageTolerance":
                            SlippageTolerance = double.Parse(item);
                            break;
                        case "-tokens":
                            try
                            {
                                InputTokens = JsonConvert.DeserializeObject<string[]>(item);
                                foreach (string token in InputTokens)
                                {

                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "-staking":
                            try
                            {
                                Staking = JsonConvert.DeserializeObject<string[]>(item);
                                foreach (string token in InputTokens)
                                {

                                }
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                            break;
                        case "-bscAddress":
                            BscAddress = item;
                            break;
                    }
                    key = null;
                }
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetConfigValue(IConfiguration config, string key)
        {
            if (config == null)
            {
                return null;
            }
            if (config.GetSection(key) != null)
            {
                return config.GetSection(key).Value;
            }
            return null;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static int? GetConfigIntValue(IConfiguration config, string key)
        {
            string value = GetConfigValue(config, key);
            if (value == null)
            {
                return null;
            }
            int v;
            if (int.TryParse(value, out v))
            {
                return v;
            }
            return null;
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <param name="config"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private static double? GetConfigDoubleValue(IConfiguration config, string key)
        {
            string value = GetConfigValue(config, key);
            if (value == null)
            {
                return null;
            }
            double v;
            if (double.TryParse(value, out v))
            {
                return v;
            }
            return null;
        }
    }
}
