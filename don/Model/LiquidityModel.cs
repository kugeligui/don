using DON.Constent;
using Newtonsoft.Json;

namespace DON.Model
{
    /// <summary>
    /// 流动性模型
    /// </summary>
    public class LiquidityModel
    {
        /// <summary>
        /// IWBNB_HUSD
        /// </summary>
        public readonly static LiquidityModel IWBNB_HUSD = new LiquidityModel(LPConstent.IWBNB_HUSD_LP_NAME, LPConstent.IWBNB_NAME, LPConstent.HUSD_NAME, DONContract.IWBNB_HUSD_LP);

        /// <summary>
        /// DON_HUSD
        /// </summary>
        public readonly static LiquidityModel DON_HUSD = new LiquidityModel(LPConstent.DON_HUSD_LP_NAME, LPConstent.DON_NAME, LPConstent.HUSD_NAME, DONContract.DON_HUSD_LP);

        /// <summary>
        /// DON_ISOT
        /// </summary>
        public readonly static LiquidityModel DON_ISOT = new LiquidityModel(LPConstent.DON_ISOT_LP_NAME, LPConstent.DON_NAME, LPConstent.IOST_NAME, DONContract.DON_IOST_LP);

        /// <summary>
        /// IOST_HUSD
        /// </summary>
        public readonly static LiquidityModel IOST_HUSD = new LiquidityModel(LPConstent.IOST_HUSD_LP_NAME, LPConstent.IOST_NAME, LPConstent.HUSD_NAME, DONContract.IOST_HUSD_LP);

        /// <summary>
        /// IOST_HUSD
        /// </summary>
        public readonly static LiquidityModel IWBNB = new LiquidityModel(LPConstent.IWBNB_NAME, LPConstent.IWBNB_NAME, null, DONContract.IWBNB);

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
