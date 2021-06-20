namespace DON.Model
{
    /// <summary>
    /// 合约
    /// </summary>
    public class ContractModel
    {
        public string id { get; set; }
        public string code { get; set; }
        public string language { get; set; }
        public string version { get; set; }
        public ABIModel[] abis { get; set; }
    }
}
