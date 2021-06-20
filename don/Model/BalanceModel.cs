namespace DON.Model
{
    /// <summary>
    /// 账号指定代币的余额
    /// </summary>
    public class BalanceModel
    {
        /// <summary>
        /// 余额
        /// </summary>
        public double balance { get; set; }

        /// <summary>
        /// 冻结信息
        /// </summary>
        public FrozenBalanceModel[] frozen_balances { get; set; }
    }
}
