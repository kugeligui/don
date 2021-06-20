namespace DON.Model
{
    /// <summary>
    /// TokenInfo
    /// </summary>
    public class TokenBaseInfo
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 合约地址
        /// </summary>
        public string contractAddress { get; set; }

        public TokenBaseInfo(string _name, string _contractAddress)
        {
            name = _name;
            contractAddress = _contractAddress;
        }
    }
}
