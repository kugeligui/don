using Newtonsoft.Json;

namespace DON.Model
{
    /// <summary>
    /// 流动性模型
    /// </summary>
    public class LiquidityModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 币种名
        /// </summary>
        public string token
        {
            get
            {
                if (name == null) return null;
                return name.Replace("_", "");
            }
        }

        /// <summary>
        /// 币种名
        /// </summary>
        public string swapToken
        {
            get
            {
                if (name == null) return null;
                return name.Replace("_lp", "");
            }
        }

        /// <summary>
        /// 合约地址
        /// </summary>
        public string contractAddress { get; set; }

        public TokenModel symbol1;

        public TokenModel symbol2;

        public double invariant;

        public LiquidityModel()
        {

        }

        public LiquidityModel(string _name, string _symbol1, string _symbol2, string _contractAddress)
        {
            name = _name;
            symbol1 = new TokenModel(_symbol1);
            symbol2 = new TokenModel(_symbol2);
            contractAddress = _contractAddress;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
