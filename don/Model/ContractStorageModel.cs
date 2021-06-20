namespace DON.Model
{

    /// <summary>
    /// 
    /// 
    /// </summary>
    public class ContractStorageModel
    {
        /// <summary>
        /// 存储的数据，返回顺序与传入顺序一致
        /// </summary>
        public string[] datas { get; set; }

        /// <summary>
        /// 这个数据来自的区块的 hash
        /// </summary>
        public string block_hash { get; set; }

        /// <summary>
        /// 这个数据来自的区块的编号
        /// </summary>
        public string block_number { get; set; }
    }
}
