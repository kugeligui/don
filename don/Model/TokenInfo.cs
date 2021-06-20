namespace DON.Model
{
    public class TokenInfo
    {
        /// <summary>
        /// token 名字
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// token 全称
        /// </summary>
        public string full_name { get; set; }

        /// <summary>
        /// token 发行人
        /// </summary>
        public string issuer { get; set; }

        /// <summary>
        /// token 总发行量，其值等于 total_supply_float 乘以 decimal
        /// </summary>
        public string total_supply { get; set; }

        /// <summary>
        /// token 当前发行量，其值等于 current_supply_float 乘以 decimal
        /// </summary>
        public string current_supply { get; set; }

        /// <summary>
        /// token 小数位
        /// </summary>
        public int _decimal { get; set; }

        /// <summary>
        /// token 能否进行转账
        /// </summary>
        public bool can_transfer { get; set; }

        /// <summary>
        /// 是否只能由代币发行者转账
        /// </summary>
        public bool only_issuer_can_transfer { get; set; }

        /// <summary>
        /// token 总发行量
        /// </summary>
        public double total_supply_float { get; set; }

        /// <summary>
        /// token 当前发行量
        /// </summary>
        public double current_supply_float { get; set; }
    }
}
