namespace DON.Model
{
    public class NetworkInfo
    {
        /// <summary>
        /// 本节点的ID
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 邻居节点的数量
        /// </summary>
        public int peer_count { get; set; }
    }
}
